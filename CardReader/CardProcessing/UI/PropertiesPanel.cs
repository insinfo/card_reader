using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CardProcessing
{
    class PropertiesPanel : StackPanel
    {
        public event EventHandler OnSetProp;

        public void AddRow(StackPanel row)
        {
            this.Children.Add(row);
        }

        public void CreateProp(string labelText,string propIdentifie)
        {
            Label lb = new Label();
            lb.Content = labelText;
            lb.Foreground = (Brush)new BrushConverter().ConvertFrom("#fff"); ;

            TextBox textBox = new TextBox();
            textBox.Name = propIdentifie;
            textBox.Width = 100;
            textBox.Height = 22;
            textBox.Background = (Brush)new BrushConverter().ConvertFrom("#FF6C6C6C");
            textBox.Foreground = (Brush)new BrushConverter().ConvertFrom("#fff");
            textBox.KeyDown += TextBox_KeyDown;
            textBox.BorderThickness = new Thickness(0);

            PropStruct prop = new PropStruct(lb, textBox);
            AddRow(CreateRow(prop));
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (OnSetProp != null)
                {
                    OnSetProp(sender, e);
                }
            }
            
        }

        public StackPanel CreateRow(PropStruct propStruct)
        {
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.Children.Add(propStruct.Label);
            sp.Children.Add(propStruct.TextBox);

            return sp;
        }

    }
    public class PropStruct
    {
        public Label Label { get; set; }
        public TextBox TextBox { get; set; }
        public PropStruct(Label label, TextBox textBox)
        {
            this.Label = label;
            this.TextBox = textBox;
        }
    }
}
