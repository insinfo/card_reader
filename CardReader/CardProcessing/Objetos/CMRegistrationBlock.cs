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
    public class CMRegistrationBlock : Canvas
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
        public double ColLabelFontSize { get; set; }

        public CMRegistrationBlock(bool loadDef = false)
        {
            if (loadDef)
            {
                LoadDef();
            }
        }
        public void LoadDef()
        {
            LineStart = 0; LineCount = 3; ColCount = 10; CheckBoxWidth = 46; CheckBoxHeight = 46; CheckBoxColSpacing = 21; CheckBoxLineSpacing = 21;
            ColLabelFontSize = 30;

            base.Name = "CMRegistrationBlock";
            base.Width = (ColCount * CheckBoxWidth) + ((ColCount - 1) * CheckBoxColSpacing) + 60;
            base.Height = (LineCount * CheckBoxHeight) + ((LineCount - 1) * CheckBoxLineSpacing) + 70;
            this.X = 276;
            this.Y = 3143;
            base.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
          
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
                    if (positionY == CheckBoxStartPositionY)
                    {
                        CMLabel labelCol = new CMLabel();
                        labelCol.IsHitTestVisible = false;
                        labelCol.Name = "LBReg_" + checkBoxCount;
                        labelCol.Text = col.ToString();
                        labelCol.FontSize = ColLabelFontSize;
                        labelCol.X = positionX + ((CheckBoxWidth / 2) - ((ColLabelFontSize / 2) - 3)) ;
                        labelCol.Y = positionY ;
                        Children.Add(labelCol);
                        SetLeft(labelCol, positionX + ((CheckBoxWidth / 2) - ((ColLabelFontSize / 2) - 3)));
                        SetTop(labelCol, positionY);
                        UpdateLayout();
                    }
                    cmCheckBox.Line = line;
                    cmCheckBox.Name = "CBReg_" + checkBoxCount;
                    cmCheckBox.X = positionX;
                    cmCheckBox.Y = positionY + 55;
                    cmCheckBox.Width = CheckBoxWidth;
                    cmCheckBox.Height = CheckBoxHeight;
                    SetLeft(cmCheckBox, positionX);
                    SetTop(cmCheckBox, positionY + 55);
                    Children.Add(cmCheckBox);
                    UpdateLayout();
                    positionX += stepX;
                    checkBoxCount++;
                }
                positionX = CheckBoxStartPositionX;
                positionY += stepY;
            }
        }
    }
}
