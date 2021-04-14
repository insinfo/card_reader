using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CardProcessing
{
    public class CMCheckBox : Shape
    {
        /** PROPRIEDADES PERSONALIZADAS ***/               
        public bool IsChecked { set; get; }
        public int Line { get; set; }
        public string Col { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        /*****/

        public static readonly DependencyProperty RadiusXProperty;
        public static readonly DependencyProperty RadiusYProperty;
        public static readonly DependencyProperty RoundTopLeftProperty;
        public static readonly DependencyProperty RoundTopRightProperty;
        public static readonly DependencyProperty RoundBottomLeftProperty;
        public static readonly DependencyProperty RoundBottomRightProperty;

        public int RadiusX
        {
            get { return (int)GetValue(RadiusXProperty); }
            set { SetValue(RadiusXProperty, value); }
        }

        public int RadiusY
        {
            get { return (int)GetValue(RadiusYProperty); }
            set { SetValue(RadiusYProperty, value); }
        }

        public bool RoundTopLeft
        {
            get { return (bool)GetValue(RoundTopLeftProperty); }
            set { SetValue(RoundTopLeftProperty, value); }
        }

        public bool RoundTopRight
        {
            get { return (bool)GetValue(RoundTopRightProperty); }
            set { SetValue(RoundTopRightProperty, value); }
        }

        public bool RoundBottomLeft
        {
            get { return (bool)GetValue(RoundBottomLeftProperty); }
            set { SetValue(RoundBottomLeftProperty, value); }
        }

        public bool RoundBottomRight
        {
            get { return (bool)GetValue(RoundBottomRightProperty); }
            set { SetValue(RoundBottomRightProperty, value); }
        }

        static CMCheckBox()
        {
            RadiusXProperty = DependencyProperty.Register("RadiusX", typeof(int), typeof(CMCheckBox));
            RadiusYProperty = DependencyProperty.Register("RadiusY", typeof(int), typeof(CMCheckBox));
            RoundTopLeftProperty = DependencyProperty.Register("RoundTopLeft", typeof(bool), typeof(CMCheckBox));
            RoundTopRightProperty = DependencyProperty.Register("RoundTopRight", typeof(bool), typeof(CMCheckBox));
            RoundBottomLeftProperty = DependencyProperty.Register("RoundBottomLeft", typeof(bool), typeof(CMCheckBox));
            RoundBottomRightProperty = DependencyProperty.Register("RoundBottomRight", typeof(bool), typeof(CMCheckBox));
        }

        public CMCheckBox()
        {
            this.IsChecked = false;
            this.Line = 1;
            this.Col = "A";           
            this.X = 50;
            this.Y = 50;
           
            base.Height = 46;
            base.Width = 46;
            base.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            base.Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            base.StrokeThickness = 2;
            base.Arrange(new Rect(0, 0, 74, 49));
           // base.IsHitTestVisible = false;
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                Geometry result = new RectangleGeometry(new Rect(0, 0, base.Width, base.Height), RadiusX, RadiusY);
                double halfWidth = base.Width / 2;
                double halfHeight = base.Height / 2;

                if (!RoundTopLeft)
                {
                    result = new CombinedGeometry(GeometryCombineMode.Union, result, new RectangleGeometry(new Rect(0, 0, halfWidth, halfHeight)));
                }
                if (!RoundTopRight)
                {
                    result = new CombinedGeometry(GeometryCombineMode.Union, result, new RectangleGeometry(new Rect(halfWidth, 0, halfWidth, halfHeight)));
                }
                if (!RoundBottomLeft)
                {
                    result = new CombinedGeometry(GeometryCombineMode.Union, result, new RectangleGeometry(new Rect(0, halfHeight, halfWidth, halfHeight)));
                }
                if (!RoundBottomRight)
                {
                    result = new CombinedGeometry(GeometryCombineMode.Union, result, new RectangleGeometry(new Rect(halfWidth, halfHeight, halfWidth, halfHeight)));
                }
                return result;
            }
        }
    }
}
