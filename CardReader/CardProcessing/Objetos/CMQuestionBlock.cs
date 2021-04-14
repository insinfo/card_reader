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
    public class CMQuestionBlock : Canvas
    {
        public static string PROP_KEY_NAME = "name";
        public static string PROP_KEY_WIDTH = "whidt";
        public static string PROP_KEY_HEIGHT = "height";
        public static string PROP_KEY_X = "x";
        public static string PROP_KEY_Y = "y";
        public static string PROP_KEY_QUESTION_START = "QuestionStart";
        public static string PROP_KEY_QUESTION_COUNT = "QuestionCount";
        public static string PROP_KEY_ANSWER_COUNT = "AnswerCount";
        public static string PROP_KEY_CHECKBOX_WIDTH = "CheckBoxWidth";
        public static string PROP_KEY_CHECKBOX_HEIGHT = "CheckBoxHeight";
        public static string PROP_KEY_CHECKBOX_COL_SPACING = "CheckBoxColSpacing";
        public static string PROP_KEY_CHECKBOX_LINE_SPACING = "CheckBoxLineSpacing";
        public static string PROP_KEY_CHECKBOX_INNER_LABEL_STATUS = "CheckBoxInnerLabelStatus";
        public static string PROP_KEY_CHECKBOX_INNER_LABEL_FONT_SIZE = "CheckBoxInnerLabelFontSize";
        public static string PROP_KEY_CHECKBOX_MARGIN_LEFT = "CheckBoxMarginLeft";
        public static string PROP_KEY_CHECKBOX_MARGIN_TOP = "CheckBoxMarginTop";
        public static string PROP_KEY_QUESTION_LABEL_FONT_SIZE = "QuestionLabelFontSize";
        
        public double X { get; set; }
        public double Y { get; set; }        
        public int QuestionStart { get; set; }
        public int QuestionCount { get; set; }
        public int AnswerCount { get; set; }
        public double CheckBoxWidth { get; set; }
        public double CheckBoxHeight { get; set; }
        public int CheckBoxColSpacing { get; set; }
        public int CheckBoxLineSpacing { get; set; }
        public bool CheckBoxInnerLabelStatus { get; set; }
        public double CheckBoxInnerLabelFontSize { get; set; }
        public double CheckBoxMarginLeft { get; set; }
        public double CheckBoxMarginTop { get; set; }
        public double QuestionLabelFontSize { get; set; }

        private string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P" };

        public CMQuestionBlock(bool loadDef = false)
        {
            if (loadDef)
            {
                LoadDef();
            }
        }

        private double CalcWitht()
        {
            return (AnswerCount * CheckBoxWidth) + ((AnswerCount - 1) * CheckBoxColSpacing) + 100;
        }

        private double CalcHeight()
        {
            return (QuestionCount * (CheckBoxHeight + 2)) + ((QuestionCount - 1) * CheckBoxLineSpacing) + 70;
        }

        public void LoadDef()
        {
            QuestionStart = 1; QuestionCount = 18; AnswerCount = 5; CheckBoxWidth = 46; CheckBoxHeight = 46; CheckBoxColSpacing = 22; CheckBoxLineSpacing = 34;
            CheckBoxInnerLabelStatus = true;
            CheckBoxInnerLabelFontSize = 30;
            CheckBoxMarginLeft = 93;
            CheckBoxMarginTop = 93;
            QuestionLabelFontSize = 35;
            double width = CalcWitht();
            double height = CalcHeight();

            base.Name = "CMQuestionBlock";
            base.Width = width;
            base.Height = height;
            this.X = 84;
            this.Y = 1591;
            base.Background = new SolidColorBrush(Color.FromArgb(0, 150, 150, 150));
            base.Arrange(new Rect(0, 0, width, height));
            Drawing();

        }
        public void Update()
        {
            this.Children.Clear();
            double width = CalcWitht();
            double height = CalcHeight();
            base.Width = width;
            base.Height = height;
            Drawing();

        }
        private void Drawing()
        {
            double stepY = CheckBoxHeight + CheckBoxLineSpacing;
            double stepX = CheckBoxWidth + CheckBoxColSpacing;

            double positionX = CheckBoxMarginLeft;
            double positionY = CheckBoxMarginTop;
            int checkBoxCount = 0;

            for (int line = 0; line < QuestionCount; line++)
            {
                CMLabel labelLine = new CMLabel();
                labelLine.Name = "LBLineQuestion_" + checkBoxCount;
                labelLine.IsHitTestVisible = false;
                labelLine.X = positionX - 83;
                labelLine.Y = positionY;
                labelLine.Text = (line + QuestionStart).ToString();
                labelLine.FontSize = QuestionLabelFontSize;
                Children.Add(labelLine);
                SetLeft(labelLine, positionX - 83);
                SetTop(labelLine, positionY);
                UpdateLayout();

                for (int col = 0; col < AnswerCount; col++)
                {
                    CMCheckBox cmCheckBox = new CMCheckBox();
                    cmCheckBox.IsHitTestVisible = false;
                    cmCheckBox.Col = alphabet[col];
                    cmCheckBox.Line = (line + QuestionStart);
                    cmCheckBox.Name = "CBQuestion_" + checkBoxCount;
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

                    if (CheckBoxInnerLabelStatus)
                    {
                        CMLabel labelCol = new CMLabel();
                        labelCol.Name = "LBColQuestion_" + checkBoxCount;
                        labelCol.IsHitTestVisible = false;
                        labelCol.Text = alphabet[col];
                        Children.Add(labelCol);
                        labelCol.X = positionX + ((CheckBoxWidth / 2) - ((CheckBoxInnerLabelFontSize / 2) - 3));
                        labelCol.Y = positionY + ((CheckBoxHeight / 2) - ((CheckBoxInnerLabelFontSize / 2)+3 ));
                        labelCol.FontSize = CheckBoxInnerLabelFontSize;
                        SetLeft(labelCol, positionX + ((CheckBoxWidth / 2) - ((CheckBoxInnerLabelFontSize / 2) - 3)));
                        SetTop(labelCol, positionY + ((CheckBoxHeight / 2) - ((CheckBoxInnerLabelFontSize / 2) +3)));
                        UpdateLayout();

                    }
                    positionX += stepX;
                    checkBoxCount++;
                }
                positionX = CheckBoxMarginLeft;
                positionY += stepY;
            }
        }

        public List<CMCheckBox> GetAllCheckBoxByLine(int line)
        {
            List<CMCheckBox> cbList = new List<CMCheckBox>();
            foreach (FrameworkElement element in this.Children)
            {
                if (element is CMCheckBox)
                {
                    CMCheckBox cb = (CMCheckBox)element;
                    if (cb.Line == line)
                    {
                        cbList.Add(cb);
                    }

                }
            }
            return cbList;
        }

        public int GetCheckBoxCount()
        {
            int count = 0;
            foreach (FrameworkElement element in this.Children)
            {
                if (element is CMCheckBox)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
