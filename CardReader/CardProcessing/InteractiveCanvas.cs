using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace CardProcessing
{
    
    public class InteractiveCanvas : Canvas
    {
        private object elementSelected = null;
        private List<object> selectedElements;
        //private AdornerLayer myAdornerLayer;
        //private MoveAdorner moveAdorner;
        public event EventHandler OnSelectElement;
        public event EventHandler OnCancelSelectElement;
        private bool movingElementStates = true;
        private bool selectingElementsStates = false;//define o estado para selecionar varios elementos

        public InteractiveCanvas()
        {
            
            this.Background = new SolidColorBrush(Color.FromArgb(255,255,255,255));
            this.PreviewMouseDown += InteractiveCanvas_PreviewMouseDown;
            this.MouseMove += InteractiveCanvas_MouseMove;
            this.MouseUp += InteractiveCanvas_MouseUp;
            this.PreviewKeyDown += InteractiveCanvas_PreviewKeyDown;
            this.PreviewKeyUp += InteractiveCanvas_PreviewKeyUp;
            
        }

        private void InteractiveCanvas_PreviewKeyUp(object sender, KeyEventArgs e)
        {
             if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                selectingElementsStates = false;
                movingElementStates = true;
            }
             
        }

        private void InteractiveCanvas_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Decimal)
            {
                if (elementSelected != null)
                {
                    FrameworkElement el = (FrameworkElement)elementSelected;
                    this.Children.Remove(el);
                }
            }
            else if(e.Key == Key.LeftShift || e.Key == Key.RightShift )
            {
                selectingElementsStates = true;
                movingElementStates = false;
            }
        }
       
        public object GetSelectedElement()
        {
            return elementSelected;
        }

        public void SetZoom(double zoomFactor)
        {
            ScaleTransform st = new ScaleTransform();
            st.ScaleX = zoomFactor;
            st.ScaleY = zoomFactor;
            this.RenderTransform = st;
        }

        private void InteractiveCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            /* this.scale.ScaleX += e.Delta * 0.003;
            this.scale.ScaleY += e.Delta * 0.003;


            scroller.ScrollToHorizontalOffset(scroller.ScrollableWidth / 2);
            scroller.ScrollToVerticalOffset(scroller.ScrollableHeight / 2);*/

            this.SetZoom(e.Delta * 0.003);


        }

        private void RemoveAllMoveAdornerLayer()
         {
             for (int j = 0; j < this.Children.Count; j++)
             {
                 //(FrameworkElement)LogicalTreeHelper.FindLogicalNode(background, "sequence01");
                 FrameworkElement frameworkElement = (FrameworkElement)this.Children[j];
                 if (frameworkElement != null)
                 {
                    RemoveMoveAdorner(frameworkElement);
                 }
             }
         }

        private void RemoveAllAdornerLayer()
        {
            for (int j = 0; j < this.Children.Count; j++)
            {
                //(FrameworkElement)LogicalTreeHelper.FindLogicalNode(background, "sequence01");
                FrameworkElement frameworkElement = (FrameworkElement)this.Children[j];
                if (frameworkElement != null)
                {
                    RemoveAdorner(frameworkElement);
                }
            }
        }

        /** remove adorno de mover elemento **/
        private void RemoveMoveAdorner(FrameworkElement element)
        {
            if (element != null)
            {
                AdornerLayer ALayer = AdornerLayer.GetAdornerLayer(element);
                if (ALayer != null)
                {
                    Adorner[] ads = ALayer.GetAdorners(element);
                    if (ads != null)
                    {
                        foreach (Adorner ad in ads)
                        {
                            if(ad.GetType() == typeof(MoveAdorner))
                            {
                                ALayer.Remove(ad);
                            }
                        }
                    }
                }
            }
        }

        /** remove qualque adorno do elemento **/
        private void RemoveAdorner(FrameworkElement element)
        {
            if (element != null)
            {
                AdornerLayer ALayer = AdornerLayer.GetAdornerLayer(element);
                if (ALayer != null)
                {
                    Adorner[] ads = ALayer.GetAdorners(element);
                    if (ads != null)
                    {
                        foreach (Adorner ad in ads)
                        {
                           ALayer.Remove(ad);                            
                        }
                    }
                }
            }
        }

        private void  AddMoveAdorner(FrameworkElement frameworkElement)
        {
            if (frameworkElement.Name != this.Name)
            {
                AdornerLayer myAdornerLayer = AdornerLayer.GetAdornerLayer(frameworkElement);
                if (myAdornerLayer.GetAdorners(frameworkElement) == null)
                {
                    MoveAdorner moveAdorner = new MoveAdorner(frameworkElement, this);
                    myAdornerLayer.Add(moveAdorner);
                }
            }
        }

        private void AddSelectAdorner(FrameworkElement frameworkElement)
        {
            if (frameworkElement.Name != this.Name)
            {
                AdornerLayer myAdornerLayer = AdornerLayer.GetAdornerLayer(frameworkElement);
                if (myAdornerLayer.GetAdorners(frameworkElement) == null)
                {
                    SelectAdorner selectAdorner = new SelectAdorner(frameworkElement);
                    myAdornerLayer.Add(selectAdorner);
                }
                else
                {
                    RemoveAllAdornerLayer();                   
                }
                if (myAdornerLayer.GetAdorners(frameworkElement) == null)
                {
                    SelectAdorner selectAdorner = new SelectAdorner(frameworkElement);
                    myAdornerLayer.Add(selectAdorner);
                }
            }

        }

        private void InteractiveCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            FrameworkElement frameworkElement = (FrameworkElement)e.Source;
            if (movingElementStates)
            {
                RemoveAllMoveAdornerLayer();
                AddMoveAdorner(frameworkElement);
            }  
        }

        private void InteractiveCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //RemoveAllAdornerLayer();
        }

        private void InteractiveCanvas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.Focus(this);
            FrameworkElement frameworkElement = (FrameworkElement)e.Source;
            
            if (e.ChangedButton == MouseButton.Left)
            {
                if (frameworkElement.Name != this.Name)
                {
                    elementSelected = e.Source;

                    if (selectingElementsStates == true)
                    {
                        AddSelectAdorner(frameworkElement);
                        movingElementStates = false;
                        
                        if (selectedElements == null)
                        { selectedElements = new List<object>(); }

                        selectedElements.Add(e.Source);
                        if (OnSelectElement != null)
                        {
                            OnSelectElement(selectedElements, e);
                        }
                    }
                    else
                    {
                        if (OnSelectElement != null)
                        {
                            OnSelectElement(e.Source, e);
                        }
                    }
                }
                else
                {
                   
                    selectingElementsStates = false;
                    
                    if (selectingElementsStates == false)
                    {
                        RemoveAllAdornerLayer();
                        if (selectedElements != null) { selectedElements.Clear();
                        }                           
                    }
                    if (OnCancelSelectElement != null)
                    {
                        OnCancelSelectElement(this, e);
                    }
                }
            }
            
        }
    }
}
