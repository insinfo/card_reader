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
    public class CMTestBlock : Canvas
    {     
        public double X { get; set; }
        public double Y { get; set; }
        public int LineStart { get; set; }
        public int LineCount { get; set; }
        public int ColCount { get; set; }
        public double CheckBoxWidth { get; set; }
        public double CheckBoxHeight { get; set; }
        public double CheckBoxColSpacing { get; set; }
        public double CheckBoxLineSpacing { get; set; }
        public double CheckBoxInnerLabelFontSize { get; set; }
        public bool CheckBoxInnerLabelStatus { get; set; }
        public bool CheckBoxLabelDescriptionStatus { get; set; }
        public double ColLabelFontSize { get; set; }

        private string[] cores = { "ROSA", "AMARELO", "AZUL", "ROCHO", "VERDE" };

        public CMTestBlock(bool loadDef = false)
        {
            if (loadDef)
            {
                LoadDef();
            }
        }

        public void LoadDef()
        {
            LineStart = 0; LineCount = 1; ColCount = 3; CheckBoxWidth = 59; CheckBoxHeight = 59; CheckBoxColSpacing = 781; CheckBoxLineSpacing = 21;
            CheckBoxInnerLabelFontSize = 30;
            CheckBoxInnerLabelStatus = true;
            CheckBoxLabelDescriptionStatus = true;
            ColLabelFontSize = 30;

            base.Name = "CMTestBlock";
            base.Width = (ColCount * CheckBoxWidth) + ((ColCount-1) * CheckBoxColSpacing)+250;
            base.Height = (LineCount * CheckBoxHeight) + ((LineCount - 1) * CheckBoxLineSpacing);
            this.X = 185;
            this.Y = 1213;
            base.Background = new SolidColorBrush(Color.FromArgb(0,180, 180, 180));

            double stepY = CheckBoxHeight + CheckBoxLineSpacing;
            double stepX = CheckBoxWidth + CheckBoxColSpacing;
            double CheckBoxStartPositionX = 0;
            double CheckBoxStartPositionY = 0;
            double positionX = CheckBoxStartPositionX;
            double positionY = CheckBoxStartPositionY;
            int checkBoxCount = 0;
            for (int line = 0; line < LineCount; line++)
            {               
                for (int col = 0; col < ColCount; col++)
                {
                    CMCheckBox cmCheckBox = new CMCheckBox();
                    cmCheckBox.IsHitTestVisible = false;
                    cmCheckBox.Col = col.ToString();                                      
                    cmCheckBox.Line = line;
                    cmCheckBox.Name = "CBTest_" + checkBoxCount;
                    cmCheckBox.Width = CheckBoxWidth;
                    cmCheckBox.Height = CheckBoxHeight;
                    cmCheckBox.RadiusX = 0;
                    cmCheckBox.RadiusY = 0;
                    cmCheckBox.RoundBottomLeft = true;
                    cmCheckBox.RoundBottomRight = true;
                    cmCheckBox.RoundTopLeft = true;
                    cmCheckBox.RoundTopRight = true;
                    cmCheckBox.StrokeThickness = 2;
                    cmCheckBox.X = positionX;
                    cmCheckBox.Y = positionY;
                    SetLeft(cmCheckBox, positionX);
                    SetTop(cmCheckBox, positionY);                                   
                    Children.Add(cmCheckBox);
                    UpdateLayout();

                    if (CheckBoxLabelDescriptionStatus)
                    {
                        CMLabel labelColDescription = new CMLabel();
                        labelColDescription.IsHitTestVisible = false;
                        labelColDescription.Name = "LabelDescriptionTest_" + checkBoxCount;
                        labelColDescription.IsHitTestVisible = false;
                        labelColDescription.Text = cores[col]+"- "+(col+1).ToString()+"ª Série";
                        Children.Add(labelColDescription);
                        labelColDescription.X = positionX + CheckBoxWidth+21 ;
                        labelColDescription.Y = positionY + ((CheckBoxHeight / 2) - ((CheckBoxInnerLabelFontSize / 2) + 3));
                        labelColDescription.FontSize = ColLabelFontSize;
                        SetLeft(labelColDescription, positionX + CheckBoxWidth +21 );                        
                        SetTop(labelColDescription, positionY + ((CheckBoxHeight / 2) - ((CheckBoxInnerLabelFontSize / 2) + 3)));
                        UpdateLayout();
                    }
                    
                    if (CheckBoxInnerLabelStatus)
                    {
                        CMLabel labelColNumber = new CMLabel();
                        labelColNumber.IsHitTestVisible = false;
                        labelColNumber.Name = "InnerLabelTest_" + checkBoxCount;
                        labelColNumber.IsHitTestVisible = false;
                        labelColNumber.Text = col.ToString();
                        Children.Add(labelColNumber);
                        labelColNumber.X = positionX + ((CheckBoxWidth / 2) - ((CheckBoxInnerLabelFontSize / 2) - 3));
                        labelColNumber.Y = positionY + ((CheckBoxHeight / 2) - ((CheckBoxInnerLabelFontSize / 2) + 3));
                        labelColNumber.FontSize = CheckBoxInnerLabelFontSize;
                        SetLeft(labelColNumber, positionX + ((CheckBoxWidth / 2) - ((CheckBoxInnerLabelFontSize / 2) - 3)));                       
                        SetTop(labelColNumber, positionY + ((CheckBoxHeight / 2) - ((CheckBoxInnerLabelFontSize / 2) + 3)));
                        UpdateLayout();
                    }
                    positionX += stepX;
                    checkBoxCount++;
                }
                positionX = 30;
                positionY += stepY;
            }

        }
    }
}
