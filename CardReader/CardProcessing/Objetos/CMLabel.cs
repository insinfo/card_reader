using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CardProcessing
{
    public class CMLabel : TextBox
    {       
        public double X { get; set; }
        public double Y { get; set; }

        public CMLabel()
        {
            Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            BorderThickness = new Thickness(0);           
            FontSize = 40;            
        }
    }
}
