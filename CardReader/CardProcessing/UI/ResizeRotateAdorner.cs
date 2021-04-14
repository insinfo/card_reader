using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Collections.Generic;
using System.Windows.Input;

namespace CardProcessing
{
    class CardModelAdorner : Adorner
    {
        // Para armazenar e gerenciar os filhos visuais do Adorner.
        VisualCollection visualChildren;

       
        private RotateThumb rotateThumb;
        private ResizeThumb resiseThumbTop, resiseThumbLeft, resiseThumbRight, resiseThumbBottom, resiseThumbTopLeft, resiseThumbTopRight, resiseThumbBottomLeft, resiseThumbBottomRight;
        private MainWindow mainWindow;
        private FrameworkElement adornedEle;

        public CardModelAdorner(UIElement adornedElement, MainWindow mainWindow) : base(adornedElement)
        {
            this.mainWindow = mainWindow;
            visualChildren = new VisualCollection(this);
            adornedEle = this.AdornedElement as FrameworkElement;

         

            rotateThumb = new RotateThumb(adornedEle);
            rotateThumb.Cursor = Cursors.ScrollAll;
            rotateThumb.Style = (Style)mainWindow.FindResource("RotateThumbStyle");

            visualChildren.Add(rotateThumb);


            resiseThumbTop = new ResizeThumb(adornedEle);
            resiseThumbTop.Height = 2;
            resiseThumbTop.Cursor = Cursors.SizeNS;
            resiseThumbTop.Style = (Style)mainWindow.FindResource("ResiseThumbStyle");
            resiseThumbTop.VerticalAlignment = VerticalAlignment.Top;
            resiseThumbTop.HorizontalAlignment = HorizontalAlignment.Stretch;
            resiseThumbTop.Margin = new Thickness(0, -4, 0, 0);
            visualChildren.Add(resiseThumbTop);

            resiseThumbLeft = new ResizeThumb(adornedEle);
            resiseThumbLeft.Width = 2;
            resiseThumbLeft.Cursor = Cursors.SizeWE;
            resiseThumbLeft.Style = (Style)mainWindow.FindResource("ResiseThumbStyle");
            resiseThumbLeft.VerticalAlignment = VerticalAlignment.Stretch;
            resiseThumbLeft.HorizontalAlignment = HorizontalAlignment.Left;
            resiseThumbLeft.Margin = new Thickness(-4, 0, 0, 0);
            visualChildren.Add(resiseThumbLeft);

            resiseThumbRight = new ResizeThumb(adornedEle);
            resiseThumbRight.Width = 2;
            resiseThumbRight.Cursor = Cursors.SizeWE;
            resiseThumbRight.Style = (Style)mainWindow.FindResource("ResiseThumbStyle");
            resiseThumbRight.VerticalAlignment = VerticalAlignment.Stretch;
            resiseThumbRight.HorizontalAlignment = HorizontalAlignment.Right;
            resiseThumbRight.Margin = new Thickness(0, 0, -4, 0);
            visualChildren.Add(resiseThumbRight);

            resiseThumbBottom = new ResizeThumb(adornedEle);
            resiseThumbBottom.Height = 2;
            resiseThumbBottom.Cursor = Cursors.SizeNS;
            resiseThumbBottom.Style = (Style)mainWindow.FindResource("ResiseThumbStyle");
            resiseThumbBottom.VerticalAlignment = VerticalAlignment.Bottom;
            resiseThumbBottom.HorizontalAlignment = HorizontalAlignment.Stretch;
            resiseThumbBottom.Margin = new Thickness(0, 0, 0, -4);
            visualChildren.Add(resiseThumbBottom);

            resiseThumbTopLeft = new ResizeThumb(adornedEle);
            resiseThumbTopLeft.Width = 7;
            resiseThumbTopLeft.Height = 7;
            resiseThumbTopLeft.Cursor = Cursors.SizeNWSE;
            resiseThumbTopLeft.Style = (Style)mainWindow.FindResource("ResiseThumbStyle");
            resiseThumbTopLeft.VerticalAlignment = VerticalAlignment.Top;
            resiseThumbTopLeft.HorizontalAlignment = HorizontalAlignment.Left;
            resiseThumbTopLeft.Margin = new Thickness(-6, -6, 0, 0);
            visualChildren.Add(resiseThumbTopLeft);

            resiseThumbTopRight = new ResizeThumb(adornedEle);
            resiseThumbTopRight.Width = 7;
            resiseThumbTopRight.Height = 7;
            resiseThumbTopRight.Cursor = Cursors.SizeNESW;
            resiseThumbTopRight.Style = (Style)mainWindow.FindResource("ResiseThumbStyle");
            resiseThumbTopRight.VerticalAlignment = VerticalAlignment.Top;
            resiseThumbTopRight.HorizontalAlignment = HorizontalAlignment.Right;
            resiseThumbTopRight.Margin = new Thickness(0, -6, -6, 0);
            visualChildren.Add(resiseThumbTopRight);

            resiseThumbBottomLeft = new ResizeThumb(adornedEle);
            resiseThumbBottomLeft.Width = 7;
            resiseThumbBottomLeft.Height = 7;
            resiseThumbBottomLeft.Cursor = Cursors.SizeNESW;
            resiseThumbBottomLeft.Style = (Style)mainWindow.FindResource("ResiseThumbStyle");
            resiseThumbBottomLeft.VerticalAlignment = VerticalAlignment.Bottom;
            resiseThumbBottomLeft.HorizontalAlignment = HorizontalAlignment.Left;
            resiseThumbBottomLeft.Margin = new Thickness(-6, 0, 0, -6);
            visualChildren.Add(resiseThumbBottomLeft);

            resiseThumbBottomRight = new ResizeThumb(adornedEle);
            resiseThumbBottomRight.Width = 7;
            resiseThumbBottomRight.Height = 7;
            resiseThumbBottomRight.Cursor = Cursors.SizeNWSE;
            resiseThumbBottomRight.Style = (Style)mainWindow.FindResource("ResiseThumbStyle");
            resiseThumbBottomRight.VerticalAlignment = VerticalAlignment.Bottom;
            resiseThumbBottomRight.HorizontalAlignment = HorizontalAlignment.Right;
            resiseThumbBottomRight.Margin = new Thickness(0, 0, -6, -6);
            visualChildren.Add(resiseThumbBottomRight);


        }


        protected override int VisualChildrenCount { get { return visualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return visualChildren[index]; }

        // Arrange the Adorners.
        protected override Size ArrangeOverride(Size finalSize)
        {
            // desiredWidth and desiredHeight are the width and height of the element that's being adorned.  
            // These will be used to place the ResizingAdorner at the corners of the adorned element.  
            double desiredWidth = AdornedElement.DesiredSize.Width;
            double desiredHeight = AdornedElement.DesiredSize.Height;
            // adornerWidth & adornerHeight are used for placement as well.
            double adornerWidth = this.DesiredSize.Width;
            double adornerHeight = this.DesiredSize.Height;
            
            rotateThumb.Arrange(new Rect((-adornerWidth / 2) - 10, (-adornerHeight / 2) - 10, adornerWidth, adornerHeight));

            resiseThumbTop.Arrange(new Rect(0, 0, adornerWidth, adornerHeight));
            resiseThumbLeft.Arrange(new Rect(0, 0, adornerWidth, adornerHeight));
            resiseThumbRight.Arrange(new Rect(0, 0, adornerWidth, adornerHeight));
            resiseThumbBottom.Arrange(new Rect(0, 0, adornerWidth, adornerHeight));
            resiseThumbTopLeft.Arrange(new Rect(0, 0, adornerWidth, adornerHeight));
            resiseThumbTopRight.Arrange(new Rect(0, 0, adornerWidth, adornerHeight));
            resiseThumbBottomLeft.Arrange(new Rect(0, 0, adornerWidth, adornerHeight));
            resiseThumbBottomRight.Arrange(new Rect(0, 0, adornerWidth, adornerHeight));


            // Return the final size.
            return finalSize;
        }



    }


}
