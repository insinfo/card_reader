using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;
using System.Windows;

using AForge;
using AForge.Imaging;
using AForge.Math;
using AForge.Video;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;


using Emgu;
using Emgu.Util;
using Emgu.CV;
using Emgu.CV.Structure;

namespace CardProcessing
{
    class MyPoint
    {
        public int x { get; set; }
        public int Y { get; set; }
        public string name { get; set; }

        public MyPoint(int x =0 ,int Y=0 ,string name="")
        {
            this.x = x; this.Y = Y; this.name = name;
        }
    }

    class ImagePreprocessing
    {       
        private SimpleShapeChecker shapeChecker = new SimpleShapeChecker();
        private List<IntPoint> Quadrilateral;
        private Pen mypen = new Pen(Color.Red, 3);
        private UnmanagedImage glyphImage;
        private float minConfidenceLevel = 0.80f;
        private float confidence;
        private const int stepSize = 3;
        private int glyphSize = 5;
        private int resMatching = -1;
        private byte[,] GlyphLeftTop;
        private byte[,] GlyphRightTop;
        private byte[,] GlyphRightBottom;
        private byte[,] GlyphLeftBottom;

                
        private List<MyPoint> pageOriginGlyphCoordinates = null;

        public ImagePreprocessing()
        {

            pageOriginGlyphCoordinates = new List<MyPoint>();
            // init data representing Glyphs defined in the PDF file.
            GlyphLeftTop = new byte[5, 5] {{0,0,0,0,0},
                                       {0,1,1,0,0},
                                       {0,1,0,1,0},
                                       {0,0,1,0,0},
                                       {0,0,0,0,0}};
            GlyphRightTop = new byte[5, 5] {{0,0,0,0,0},
                                       {0,1,1,0,0},
                                       {0,0,1,1,0},
                                       {0,1,1,0,0},
                                       {0,0,0,0,0}};
            GlyphRightBottom = new byte[5, 5] {{0,0,0,0,0},
                                       {0,0,1,0,0},
                                       {0,1,0,1,0},
                                       {0,0,1,0,0},
                                       {0,0,0,0,0}};
            GlyphLeftBottom = new byte[5, 5] {{0,0,0,0,0},
                                       {0,1,1,0,0},
                                       {0,0,1,0,0},
                                       {0,0,1,1,0},
                                       {0,0,0,0,0}};

        }

        // This function is used to calculate average brightness difference between pixels 
        // outside and inside of the object bounded by specified left and right edge
        private float CalculateAverageEdgesBrightnessDifference(
            List<IntPoint> leftEdgePoints,
            List<IntPoint> rightEdgePoints,
            UnmanagedImage image)
        {
            List<IntPoint> leftEdgePoints1 = new List<IntPoint>();
            List<IntPoint> leftEdgePoints2 = new List<IntPoint>();
            List<IntPoint> rightEdgePoints1 = new List<IntPoint>();
            List<IntPoint> rightEdgePoints2 = new List<IntPoint>();
            int tx1, tx2, ty;
            int widthM1 = image.Width - 1;
            for (int k = 0; k < leftEdgePoints.Count; k++)
            {
                tx1 = leftEdgePoints[k].X - stepSize;
                tx2 = leftEdgePoints[k].X + stepSize;
                ty = leftEdgePoints[k].Y;
                leftEdgePoints1.Add(new IntPoint((tx1 < 0) ? 0 : tx1, ty));
                leftEdgePoints2.Add(new IntPoint((tx2 > widthM1) ? widthM1 : tx2, ty));
                tx1 = rightEdgePoints[k].X - stepSize;
                tx2 = rightEdgePoints[k].X + stepSize;
                ty = rightEdgePoints[k].Y;
                rightEdgePoints1.Add(new IntPoint((tx1 < 0) ? 0 : tx1, ty));
                rightEdgePoints2.Add(new IntPoint((tx2 > widthM1) ? widthM1 : tx2, ty));
            }
            byte[] leftValues1 = image.Collect8bppPixelValues(leftEdgePoints1);
            byte[] leftValues2 = image.Collect8bppPixelValues(leftEdgePoints2);
            byte[] rightValues1 = image.Collect8bppPixelValues(rightEdgePoints1);
            byte[] rightValues2 = image.Collect8bppPixelValues(rightEdgePoints2);
            float diff = 0;
            int pixelCount = 0;
            for (int k = 0; k < leftEdgePoints.Count; k++)
            {
                if (rightEdgePoints[k].X - leftEdgePoints[k].X > stepSize * 2)
                {
                    diff += (leftValues1[k] - leftValues2[k]);
                    diff += (rightValues2[k] - rightValues1[k]);
                    pixelCount += 2;
                }
            }
            return diff / pixelCount;
        }

        // Recognize a potential glyph
        private byte[,] Recognize(UnmanagedImage image, Rectangle rect, out float confidence)
        {
            int glyphStartX = rect.Left;
            int glyphStartY = rect.Top;
            int glyphWidth = rect.Width;
            int glyphHeight = rect.Height;
            int cellWidth = glyphWidth / glyphSize;
            int cellHeight = glyphHeight / glyphSize;
            int cellOffsetX = (int)(cellWidth * 0.2);
            int cellOffsetY = (int)(cellHeight * 0.2);
            int cellScanX = (int)(cellWidth * 0.6);
            int cellScanY = (int)(cellHeight * 0.6);
            int cellScanArea = cellScanX * cellScanY;
            int[,] cellIntensity = new int[glyphSize, glyphSize];
            unsafe
            {
                int stride = image.Stride;
                byte* srcBase = (byte*)image.ImageData.ToPointer() +
                    (glyphStartY + cellOffsetY) * stride +
                    glyphStartX + cellOffsetX;
                byte* srcLine;
                byte* src;
                for (int gi = 0; gi < glyphSize; gi++)
                {
                    srcLine = srcBase + cellHeight * gi * stride;
                    for (int y = 0; y < cellScanY; y++)
                    {
                        for (int gj = 0; gj < glyphSize; gj++)
                        {
                            src = srcLine + cellWidth * gj;
                            for (int x = 0; x < cellScanX; x++, src++)
                            {
                                cellIntensity[gi, gj] += *src;
                            }
                        }
                        srcLine += stride;
                    }
                }
            }

            // calculate value of each glyph's cell and set
            // glyphs' confidence to minim value of cell's confidence
            byte[,] glyphValues = new byte[glyphSize, glyphSize];
            confidence = 1f;
            for (int gi = 0; gi < glyphSize; gi++)
            {
                for (int gj = 0; gj < glyphSize; gj++)
                {
                    float fullness = (float)
                        (cellIntensity[gi, gj] / 255) / cellScanArea;
                    float conf = (float)System.Math.Abs(fullness - 0.5) + 0.5f;
                    glyphValues[gi, gj] = (byte)((fullness > 0.5f) ? 1 : 0);
                    if (conf < confidence)
                        confidence = conf;
                }
            }
            return glyphValues;
        }

        private int CheckForMatching(byte[,] ModelData, byte[,] rawGlyphData)
        {
            int size = rawGlyphData.GetLength(0);
            int sizeM1 = size - 1;
            bool match1 = true;
            bool match2 = true;
            bool match3 = true;
            bool match4 = true;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    byte value = rawGlyphData[i, j];
                    match1 &= (value == ModelData[i, j]);
                    match2 &= (value == ModelData[sizeM1 - i, sizeM1 - j]);
                    match3 &= (value == ModelData[sizeM1 - j, i]);
                    match4 &= (value == ModelData[j, sizeM1 - i]);
                }
            }
            if (match1)
                return 0;
            else if (match2)
                return 180;
            else if (match3)
                return 90;
            else if (match4)
                return 270;
            return -1;
        }
                              
        public bool Start(string filePath, out Bitmap resultBitmap)
        {
            bool state = true;
            Bitmap outputBitmap = null;
            try
            {               
                // Load the image and determine new dimensions
                System.Drawing.Image img = System.Drawing.Image.FromFile(filePath);                
               
                Bitmap resizedImg = new Bitmap(2550, 3510);
                Graphics gfx = Graphics.FromImage(resizedImg);

                // Paste source image on blank canvas,           
                gfx.DrawImage(img, 0, 0, img.Width, img.Height);

                UnmanagedImage inpuImage = UnmanagedImage.FromManagedImage(resizedImg);             

                UnmanagedImage grayImage = UnmanagedImage.Create(inpuImage.Width, inpuImage.Height, PixelFormat.Format8bppIndexed);
                Grayscale.CommonAlgorithms.BT709.Apply(inpuImage, grayImage);

                // Detecção de Borda
                DifferenceEdgeDetector edgeDetector = new DifferenceEdgeDetector();
                UnmanagedImage edgesImage = edgeDetector.Apply(grayImage);
                // Threshold edges
                Threshold thresholdFilter = new Threshold(40);
                thresholdFilter.ApplyInPlace(edgesImage);
                // **********
                // Detecção 
                // **********
                // Cria e configura um contador de blob 
                BlobCounter blobCounter = new BlobCounter();
                blobCounter.MinHeight = 40;
                blobCounter.MinWidth = 40;
                blobCounter.FilterBlobs = true;
                blobCounter.ObjectsOrder = ObjectsOrder.Size;
                // procura todos os blobs sozinhos
                blobCounter.ProcessImage(edgesImage);
                Blob[] blobs = blobCounter.GetObjectsInformation();
                // check cada blob
                for (int i = 0, n = blobs.Length; i < n; i++)
                {
                    List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blobs[i]);
                    List<IntPoint> corners = null;
                    // isto é um quadrilatero ?
                    if (shapeChecker.IsQuadrilateral(edgePoints, out corners))
                    {
                        List<IntPoint> leftEdgePoints, rightEdgePoints;
                        blobCounter.GetBlobsLeftAndRightEdges(blobs[i], out leftEdgePoints, out rightEdgePoints);
                        // calculate average difference between pixel values from outside of the
                        // shape and from inside
                        float diff = CalculateAverageEdgesBrightnessDifference(leftEdgePoints, rightEdgePoints, grayImage);
                        if (diff > 40)
                        {
                            Quadrilateral = corners;
                            QuadrilateralTransformation quadrilateralTransformation = new QuadrilateralTransformation(Quadrilateral, 100, 100);
                            glyphImage = quadrilateralTransformation.Apply(grayImage);
                            // Filter to pure Black & White
                            OtsuThreshold otsuThresholdFilter = new OtsuThreshold();
                            otsuThresholdFilter.ApplyInPlace(glyphImage);
                            // Try to recognize de Glyph
                            byte[,] glyphValues = Recognize(glyphImage, new Rectangle(0, 0, glyphImage.Width, glyphImage.Height), out confidence);
                            // If Glyph is recognized with minimum confidence...
                            if (confidence >= minConfidenceLevel)
                            {
                                // Search to match a glyph according to our predefined models.
                                resMatching = CheckForMatching(GlyphLeftTop, glyphValues);
                                if (resMatching != -1)
                                {
                                    //Debug.Print("GlyphLeftTop");                                
                                    pageOriginGlyphCoordinates.Add(new MyPoint(GetCenterPoint(Quadrilateral).X, GetCenterPoint(Quadrilateral).Y, "GlyphLeftTop"));
                                }
                                resMatching = CheckForMatching(GlyphRightTop, glyphValues);
                                if (resMatching != -1)
                                {
                                    //Debug.Print("GlyphRightTop");                                
                                    pageOriginGlyphCoordinates.Add(new MyPoint(GetCenterPoint(Quadrilateral).X, GetCenterPoint(Quadrilateral).Y, "GlyphRightTop"));
                                }
                                resMatching = CheckForMatching(GlyphRightBottom, glyphValues);
                                if (resMatching != -1)
                                {
                                    //Debug.Print("GlyphRightBottom");
                                    pageOriginGlyphCoordinates.Add(new MyPoint(GetCenterPoint(Quadrilateral).X, GetCenterPoint(Quadrilateral).Y, "GlyphRightBottom"));
                                }
                                resMatching = CheckForMatching(GlyphLeftBottom, glyphValues);
                                if (resMatching != -1)
                                {
                                    //Debug.Print("GlyphLeftBottom");
                                    pageOriginGlyphCoordinates.Add(new MyPoint(GetCenterPoint(Quadrilateral).X, GetCenterPoint(Quadrilateral).Y, "GlyphLeftBottom"));
                                }
                            }
                        }
                    }
                }
                if (pageOriginGlyphCoordinates.Count < 4)
                {
                    state = false;
                }
                else
                {
                    UnmanagedImage grayImage2 = UnmanagedImage.Create(inpuImage.Width, inpuImage.Height, PixelFormat.Format8bppIndexed);
                    Grayscale.CommonAlgorithms.BT709.Apply(inpuImage, grayImage2);
                   
                    OtsuThreshold otsuThresholdFilter = new OtsuThreshold();
                    otsuThresholdFilter.ApplyInPlace(grayImage2);

                    state = NormalizeImage(pageOriginGlyphCoordinates, grayImage2.ToManagedImage(),out outputBitmap);
                }               
            }
            catch(Exception ex)
            {
                state = false;
            }            
            resultBitmap = outputBitmap;
            return state;
        }

        private IntPoint GetCenterPoint(List<IntPoint> points)
        {
            if (points.Count < 4)
            {
                return new IntPoint();
            }
            double x0 = points[0].X;
            double x1 = points[1].X;
            double x2 = points[2].X;
            double x3 = points[3].X;

            double y0 = points[0].Y;
            double y1 = points[1].Y;
            double y2 = points[2].Y;
            double y3 = points[3].Y;

            return new IntPoint(Convert.ToInt32(Math.Round((x0 + x1 + x2 + x3) / 4, 0)), Convert.ToInt32(Math.Round((y0 + y1 + y2 + y3) / 4, 0)));            
        }

        private bool NormalizeImage(List<MyPoint> originGlyphCoordinates, Bitmap inputBitmap, out Bitmap resultBitmap)
        {
            bool state = true;
            Bitmap outputBitmap = null;
            try
            { 
                PointF glyphLeftTop = new PointF();
                PointF glyphRightTop = new PointF();
                PointF glyphRightBottom = new PointF();
                PointF glyphLeftBottom = new PointF();

                foreach (MyPoint point in originGlyphCoordinates)
                {
                    if (point.name == "GlyphLeftTop")
                    {
                        glyphLeftTop = new PointF { X = point.x, Y = point.Y };
                    }
                    if (point.name == "GlyphRightTop")
                    {
                        glyphRightTop = new PointF { X = point.x, Y = point.Y };
                    }
                    if (point.name == "GlyphRightBottom")
                    {
                        glyphRightBottom = new PointF { X = point.x, Y = point.Y };
                    }
                    if (point.name == "GlyphLeftBottom")
                    {
                        glyphLeftBottom = new PointF { X = point.x, Y = point.Y };
                    }
                }
            
                Image<Rgb, Byte> img = new Image<Rgb, Byte>(inputBitmap);
                int rows = img.Rows;
                int cols = img.Cols;
                int ch = img.NumberOfChannels;

                System.Drawing.Size imgSize = img.Size;

                PointF[] src  = new PointF[] { glyphLeftTop, glyphRightTop, glyphLeftBottom, glyphRightBottom };

                PointF[] dest = new PointF[] { new PointF { X = 92, Y = 84 }, new PointF { X = 2360, Y = 84 }, new PointF { X = 92, Y = 3396 }, new PointF { X = 2360, Y = 3396 } };

                Matrix<double> warp_dst = new Matrix<double>(rows, cols);
                warp_dst.SetZero();
                     
                var warp_mat = CvInvoke.GetPerspectiveTransform(src, dest);
                CvInvoke.WarpPerspective(img, warp_dst, warp_mat, warp_dst.Size);

                CvInvoke.Imwrite("result.jpg", warp_dst);
                outputBitmap = new Bitmap("result.jpg");
            }
            catch(Exception ex)
            {
                state = false;
            }
            resultBitmap = outputBitmap;
            return state;
        }

        
       
    }
}
