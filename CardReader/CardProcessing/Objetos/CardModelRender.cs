using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CardProcessing
{
    class CardModelRender
    {
        private Canvas canvas;
        private CardModel cardModel;
        private RichTextBox tbShowProperties = null;
        public const int FILE_TYPE_PNG = 0;
        public const int FILE_TYPE_JPEG = 1;
        private MainWindow mainWindow;

        public CardModelRender(Canvas canvas, MainWindow mainWindow)
        {
            this.canvas = canvas;
            this.mainWindow = mainWindow;
        }

        public void setModel(CardModel cardModel)
        {
            this.cardModel = cardModel;
            draw();
        }

        public void draw()
        {
            canvas.Children.Clear();
            #region render cmLabel       
            if (cardModel.labelList != null)
            {
                foreach (CMLabel cmLabel in cardModel.labelList)
                {
                    canvas.Children.Add(cmLabel);
                    Canvas.SetLeft(cmLabel, cmLabel.X);
                    Canvas.SetTop(cmLabel, cmLabel.Y);
                    canvas.UpdateLayout();
                }
            }
            #endregion

            #region render cmImage      
            if (cardModel.imageList != null)
            {
                foreach (CMImage cmImage in cardModel.imageList)
                {
                    canvas.Children.Add(cmImage);
                    Canvas.SetLeft(cmImage, cmImage.X);
                    Canvas.SetTop(cmImage, cmImage.Y);
                    canvas.UpdateLayout();
                }
            }
            #endregion

            #region render cmRegistrationBlock           
            if (cardModel.registrationBlockList != null)
            {
                foreach (CMRegistrationBlock cmRegistrationBlock in cardModel.registrationBlockList)
                {
                    canvas.Children.Add(cmRegistrationBlock);
                    Canvas.SetLeft(cmRegistrationBlock, cmRegistrationBlock.X);
                    Canvas.SetTop(cmRegistrationBlock, cmRegistrationBlock.Y);
                    canvas.UpdateLayout();
                }
            }
            #endregion

            #region render cmTestBlock
            if (cardModel.testBlockList != null)
            {
                foreach (CMTestBlock cmTestBlock in cardModel.testBlockList)
                {
                    canvas.Children.Add(cmTestBlock);
                    Canvas.SetLeft(cmTestBlock, cmTestBlock.X);
                    Canvas.SetTop(cmTestBlock, cmTestBlock.Y);
                    canvas.UpdateLayout();
                }
            }
            #endregion

            #region render cmQuestionBlock     
            if (cardModel.questionBlockList != null)
            {
                foreach (CMQuestionBlock cmQuestionBlock in cardModel.questionBlockList)
                {
                    canvas.Children.Add(cmQuestionBlock);
                    Canvas.SetLeft(cmQuestionBlock, cmQuestionBlock.X);
                    Canvas.SetTop(cmQuestionBlock, cmQuestionBlock.Y);
                    canvas.UpdateLayout();
                }
            }
            #endregion

            #region render cmDayBlock     
            if (cardModel.dayBlockList != null)
            {
                foreach (CMDayBlock cmDayBlock in cardModel.dayBlockList)
                {
                    canvas.Children.Add(cmDayBlock);
                    Canvas.SetLeft(cmDayBlock, cmDayBlock.X);
                    Canvas.SetTop(cmDayBlock, cmDayBlock.Y);
                    canvas.UpdateLayout();
                }
            }
            #endregion

            #region render cmLangBlock     
            if (cardModel.langBlockList != null)
            {
                foreach (CMLangBlock cmLangBlock in cardModel.langBlockList)
                {
                    canvas.Children.Add(cmLangBlock);
                    Canvas.SetLeft(cmLangBlock, cmLangBlock.X);
                    Canvas.SetTop(cmLangBlock, cmLangBlock.Y);
                    canvas.UpdateLayout();
                }
            }
            #endregion

        }

        public void genQuestionCheckBox()
        {

            int leterCount = 5;
            int idCount = 70;
            int positionY = 1195;
            int positionX = 500;
            for (int line = 1; line < 31; line++)
            {
                for (int col = 0; col < leterCount; col++)
                {
                    string letra = "";
                    if (col == 0) { letra = "A"; }
                    if (col == 1) { letra = "B"; }
                    if (col == 2) { letra = "C"; }
                    if (col == 3) { letra = "D"; }
                    if (col == 4) { letra = "E"; }
                    tbShowProperties.AppendText("<CheckBox id=\"" + idCount + "\" col=\"" + letra + "\" line=\"" + line + "\" x=\"" + positionX + "\" y=\"" + positionY + "\" width=\"74\" height=\"49\" />" + " \r");
                    idCount++;
                    positionX += 124;
                }
                positionX = 500;
                positionY += 70;
            }
        }

        public void genQuestionLineLabel()
        {           
            int idCount = 370;
            int positionY = 1198;
            int positionX = 430;
            int fontSize = 48;
            for (int line = 1; line < 31; line++)
            {
                tbShowProperties.AppendText("<label id=\"" + idCount + "\" x=\"" + positionX + "\" y=\"" + positionY + "\"  fontSize=\""+ fontSize+"\" text=\"" + line+"\"/>" + " \r");
                idCount++;               
                positionY += 70;
            }
        }

        public void RenderToBitmap(string fileName, int fileType = FILE_TYPE_PNG)
        {
            
            const double SCREEN_DPI = 96.0; // Screen DPI
            const double TARGET_DPI = 300.0; // Print Quality DPI
            int Width = 2550;
            int Height = 3510;

            // Determine the constraining scale to maintain the aspect ratio and the bounds of the image size
            double scale = Math.Min(Width * SCREEN_DPI / canvas.Width, Height * SCREEN_DPI / canvas.Height);

            // Setup the bounds of the image
            Rect bounds = new Rect(0, 0, Width, Height);
            canvas.Measure(new Size(Width, Height));
            canvas.Arrange(bounds);

            RenderTargetBitmap bmp = new RenderTargetBitmap(Width, Height, SCREEN_DPI, SCREEN_DPI, PixelFormats.Pbgra32);
            bmp.Render(canvas);

            BitmapEncoder png = null;
            string name = "";
            if (fileType == FILE_TYPE_PNG)
            {
                png = new PngBitmapEncoder();
                png.Frames.Add(BitmapFrame.Create(bmp));
                name = fileName + ".png";
            }

            if (fileType == FILE_TYPE_JPEG)
            {
                png = new JpegBitmapEncoder();
                png.Frames.Add(BitmapFrame.Create(bmp));
                name = fileName + ".jpeg";
            }

            Stream stm = File.Create(name);
            png.Save(stm);

            stm.Close();
            stm = null;
            png = null;
            bmp.Clear();
            bmp = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

    }
}
