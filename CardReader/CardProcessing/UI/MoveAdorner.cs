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
    class MoveAdorner : Adorner
    {
        // Para armazenar e gerenciar os filhos visuais do Adorner.
        VisualCollection visualChildren;

        private MoveThumb moveThumb;    
        private FrameworkElement adornedEle;
        private Canvas canvas;

        public MoveAdorner(UIElement adornedElement,Canvas canvas) : base(adornedElement)
        {
            this.canvas = canvas;
            visualChildren = new VisualCollection(this);
            adornedEle = this.AdornedElement as FrameworkElement;

            moveThumb = new MoveThumb(adornedEle);
            moveThumb.Style = (Style)this.FindResource("MoveThumbStyle");
            visualChildren.Add(moveThumb);

            //this.MouseLeftButtonDown += Adorner_AnyEvent;

            this.PreviewMouseDown += (s, e) =>
            {
                //InvalidateVisual();
                AdornedElement.RaiseEvent(e);
            };
        }
        
        void Adorner_AnyEvent(object sender, RoutedEventArgs e)
        {
        }

        protected override int VisualChildrenCount { get { return visualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return visualChildren[index]; }
               
        protected override Size ArrangeOverride(Size finalSize)
        {
            double adornerWidth = 46;
            double adornerHeight = 46;
            if (adornedEle.Width.ToString() == "NaN" || adornedEle.Height.ToString() == "NaN")
            {
                double desiredWidth = AdornedElement.DesiredSize.Width;
                double desiredHeight = AdornedElement.DesiredSize.Height;
                // adornerWidth & adornerHeight are used for placement as well.
                 adornerWidth = this.DesiredSize.Width;
                 adornerHeight = this.DesiredSize.Height;
            }
            else
            {
                adornerWidth = adornedEle.Width;
                adornerHeight = adornedEle.Height;
            }
            moveThumb.Arrange(new Rect(0, 0, adornerWidth, adornerHeight));            
            return finalSize;
        }



    }


}
