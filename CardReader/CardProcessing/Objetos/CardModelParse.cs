using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Serialization;

namespace CardProcessing
{
    class CardModelParse
    {
        public CardModel LoadFile(string xmlFileURL)
        {
            CardModel cardModel = new CardModel();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileURL);
            XmlElement root = xmlDoc.DocumentElement;
            //XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/CardModel");

            // varre os elementos do xml
            foreach (XmlNode tag in root)
            {
                // quando o elemento for registrationblock
                #region registrationblock
                if (tag.Name.ToLower().Equals("registrationblock"))
                {
                    CMRegistrationBlock registrationBlock = new CMRegistrationBlock();

                    //varre os atributos da tag registrationblock
                    foreach (XmlAttribute attribute in tag.Attributes)
                    {
                        GetRegistrationBlockAttributes(attribute, ref registrationBlock);
                    }//fim foreach registrationblock atributos

                    // varre os filhos da tag registrationblock
                    foreach (XmlNode childs in tag)
                    {
                        if (childs.Name.ToLower().Equals("checkbox"))
                        {
                            CMCheckBox checkBox = new CMCheckBox();
                            foreach (XmlAttribute attributes in childs.Attributes)
                            {
                                GetCheckBoxAttributes(attributes, ref checkBox);
                            }
                            //adicona os checkBoxs no registrationBlock
                            registrationBlock.Children.Add(checkBox);
                        }
                        if (childs.Name.ToLower().Equals("label"))
                        {
                            CMLabel label = new CMLabel();
                            //varre os atributos da tag label
                            foreach (XmlAttribute attributes in childs.Attributes)
                            {
                                GetLabelAttributes(attributes, ref label);
                            }//fim foreach image atributos
                            registrationBlock.Children.Add(label);
                        }
                    }
                    //fim foreach

                    cardModel.addRegistrationBlock(registrationBlock);
                }//fim if 
                #endregion fim registrationblock
                               
                // quando o elemento for image
                #region image
                if (tag.Name.ToLower().Equals("image"))
                {
                    CMImage cmImage = new CMImage();

                    //varre os atributos da tag image
                    foreach (XmlAttribute attribute in tag.Attributes)
                    {
                        GetImageAttributes(attribute, ref cmImage);

                    }//fim foreach image atributos

                    cardModel.addImage(cmImage);

                } //fim if 
                #endregion fim image
                             
                // quando o elemento for label
                #region label
                if (tag.Name.ToLower().Equals("label"))
                {
                    CMLabel label = new CMLabel();
                    //varre os atributos da tag label
                    foreach (XmlAttribute attributes in tag.Attributes)
                    {
                        GetLabelAttributes(attributes, ref label);

                    }//fim foreach image atributos

                    cardModel.addLabel(label);

                } //fim if label
                #endregion fim label
                    
                // quando o elemento for testBlock
                #region testBlock
                if (tag.Name.ToLower().Equals("testblock"))
                {
                    CMTestBlock testBlock = new CMTestBlock();

                    //varre os atributos da tag testblock
                    foreach (XmlAttribute attribute in tag.Attributes)
                    {
                        GetTextBlockAttributes(attribute,ref testBlock);
                    }//fim foreach testblock atributos

                    // varre os filhos da tag testblock
                    foreach (XmlNode childs in tag)
                    {
                        if (childs.Name.ToLower().Equals("checkbox"))
                        {
                            CMCheckBox cmCheckBox = new CMCheckBox();
                            //varre os atributos da tag checkbox
                            foreach (XmlAttribute attributes in childs.Attributes)
                            {
                                GetCheckBoxAttributes(attributes, ref cmCheckBox);
                            }
                            //adicona os checkBoxs no testblock
                            testBlock.Children.Add(cmCheckBox);
                        }
                        if (childs.Name.ToLower().Equals("label"))
                        {
                            CMLabel label = new CMLabel();
                            //varre os atributos da tag label
                            foreach (XmlAttribute attributes in childs.Attributes)
                            {
                                GetLabelAttributes(attributes, ref label);

                            }//fim foreach image atributos
                            testBlock.Children.Add(label);
                        }
                    }
                    //fim foreach
                    cardModel.addTestBlock(testBlock);
                }//fim if 
                #endregion fim registrationblock
                             
                // quando o elemento for questionblock
                #region questionblock
                if (tag.Name.ToLower().Equals("questionblock"))
                {
                    CMQuestionBlock questionBlock = new CMQuestionBlock();

                    //varre os atributos da tag questionblock
                    foreach (XmlAttribute attributes in tag.Attributes)
                    {
                        GetQuestionBlockAttributes(attributes, ref questionBlock);

                    }//fim foreach questionblock atributos

                    // varre os filhos da tag questionblock
                    foreach (XmlNode childs in tag)
                    {
                        if (childs.Name.ToLower().Equals("checkbox"))
                        {
                            CMCheckBox subItem = new CMCheckBox();
                            //varre os atributos da tag checkbox
                            foreach (XmlAttribute attributes in childs.Attributes)
                            {
                                GetCheckBoxAttributes(attributes, ref subItem);
                            }

                            //adicona os checkBoxs no questionblock
                            questionBlock.Children.Add(subItem);
                        }
                        if (childs.Name.ToLower().Equals("label"))
                        {
                            CMLabel label = new CMLabel();
                            //varre os atributos da tag label
                            foreach (XmlAttribute attributes in childs.Attributes)
                            {
                                GetLabelAttributes(attributes, ref label);

                            }//fim foreach image atributos
                            questionBlock.Children.Add(label);
                        }
                    }
                    //fim foreach

                    cardModel.addQuestionBlock(questionBlock);
                }//fim if 
                #endregion fim questionblock

                // quando o elemento for langblock
                #region langblock
                if (tag.Name.ToLower().Equals("langblock"))
                {
                    CMLangBlock langBlock = new CMLangBlock();

                    //varre os atributos da tag questionblock
                    foreach (XmlAttribute attributes in tag.Attributes)
                    {
                        GetLangBlockAttributes(attributes, ref langBlock);

                    }//fim foreach questionblock atributos

                    // varre os filhos da tag questionblock
                    foreach (XmlNode childs in tag)
                    {
                        if (childs.Name.ToLower().Equals("checkbox"))
                        {
                            CMCheckBox subItem = new CMCheckBox();
                            //varre os atributos da tag checkbox
                            foreach (XmlAttribute attributes in childs.Attributes)
                            {
                                GetCheckBoxAttributes(attributes, ref subItem);
                            }

                            //adicona os checkBoxs no questionblock
                            langBlock.Children.Add(subItem);
                        }
                        if (childs.Name.ToLower().Equals("label"))
                        {
                            CMLabel label = new CMLabel();
                            //varre os atributos da tag label
                            foreach (XmlAttribute attributes in childs.Attributes)
                            {
                                GetLabelAttributes(attributes, ref label);

                            }//fim foreach image atributos
                            langBlock.Children.Add(label);
                        }
                    }
                    //fim foreach

                    cardModel.addLangBlock(langBlock);
                }//fim if 
                #endregion fim langBlock

                // quando o elemento for dayBlock
                #region langblock
                if (tag.Name.ToLower().Equals("dayblock"))
                {
                    CMDayBlock dayBlock = new CMDayBlock();

                    //varre os atributos da tag dayBlock
                    foreach (XmlAttribute attributes in tag.Attributes)
                    {
                        GetDayBlockAttributes(attributes, ref dayBlock);

                    }//fim foreach questionblock atributos

                    // varre os filhos da tag questionblock
                    foreach (XmlNode childs in tag)
                    {
                        if (childs.Name.ToLower().Equals("checkbox"))
                        {
                            CMCheckBox subItem = new CMCheckBox();
                            //varre os atributos da tag checkbox
                            foreach (XmlAttribute attributes in childs.Attributes)
                            {
                                GetCheckBoxAttributes(attributes, ref subItem);
                            }

                            //adicona os checkBoxs no questionblock
                            dayBlock.Children.Add(subItem);
                        }
                        if (childs.Name.ToLower().Equals("label"))
                        {
                            CMLabel label = new CMLabel();
                            //varre os atributos da tag label
                            foreach (XmlAttribute attributes in childs.Attributes)
                            {
                                GetLabelAttributes(attributes, ref label);

                            }//fim foreach image atributos
                            dayBlock.Children.Add(label);
                        }
                    }
                    //fim foreach

                    cardModel.addDayBlock(dayBlock);
                }//fim if 
                #endregion fim questionblock

            } //fim do foreach que varre os elementos do CardModel

            return cardModel;

        }//fim loadFile

        //varre os atributos da tag 
        private void GetImageAttributes(XmlAttribute attributes, ref CMImage cmImage)
        {
            #region switch
            switch (attributes.Name.ToLower().Trim())
            {
                case "src":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            BitmapImage src = new BitmapImage();
                            src.BeginInit();
                            src.UriSource = new Uri(attributes.Value.Trim(), UriKind.Relative);
                            src.CacheOption = BitmapCacheOption.OnLoad;
                            src.EndInit();
                            cmImage.Source = src;
                        }
                        break;
                    }
                case "name":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            cmImage.Name = attributes.Value.Trim();
                        }
                        break;
                    }
                case "x":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            cmImage.X = Convert.ToInt32(attributes.Value.Trim());
                            Canvas.SetLeft(cmImage, cmImage.X);
                        }
                        break;
                    }
                case "y":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            cmImage.Y = Convert.ToInt32(attributes.Value.Trim());
                            Canvas.SetTop(cmImage, cmImage.Y);
                        }
                        break;
                    }
                case "width":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            cmImage.Width = Convert.ToInt32(attributes.Value.Trim());
                        }
                        break;
                    }
                case "height":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            cmImage.Height = Convert.ToInt32(attributes.Value.Trim());
                        }
                        break;
                    }

            }
            #endregion
        }
        private void GetCheckBoxAttributes(XmlAttribute attributes, ref CMCheckBox subItem)
        {
            switch (attributes.Name.ToLower().Trim())
            {
                case "col":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            subItem.Col = attributes.Value.Trim();
                        }
                        break;
                    }
                case "line":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            subItem.Line = Convert.ToInt32(attributes.Value.Trim());
                        }
                        break;
                    }
                case "name":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            subItem.Name = "_" + attributes.Value.Trim();
                        }
                        break;
                    }
                case "backgroundcolor":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            try
                            {
                                subItem.Fill = (Brush)new BrushConverter().ConvertFrom(attributes.Value.Trim());
                            }
                            catch
                            {
                                subItem.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                            }
                        }
                        break;
                    }
                case "bordercolor":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            try
                            {
                                subItem.Stroke = (Brush)new BrushConverter().ConvertFrom(attributes.Value.Trim());
                            }
                            catch
                            {
                                subItem.Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                            }
                        }
                        break;
                    }
                case "bordersize":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            subItem.StrokeThickness = Convert.ToDouble(attributes.Value.Trim());
                        }
                        break;
                    }
                case "x":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            subItem.X = Convert.ToDouble(attributes.Value.Trim());
                            Canvas.SetLeft(subItem, subItem.X);
                        }
                        break;
                    }
                case "y":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            subItem.Y = Convert.ToDouble(attributes.Value.Trim());
                            Canvas.SetTop(subItem, subItem.Y);
                        }
                        break;
                    }
                case "width":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            subItem.Width = Convert.ToDouble(attributes.Value.Trim());
                        }
                        break;
                    }
                case "height":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            subItem.Height = Convert.ToDouble(attributes.Value.Trim());
                        }
                        break;
                    }

            }//fim switch checkbox

        }
        private void GetLabelAttributes(XmlAttribute attributes, ref CMLabel label)
        {
            switch (attributes.Name.ToLower().Trim())
            {
                case "text":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            label.Text = attributes.Value.Trim();
                        }
                        break;
                    }
                case "fontsize":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            label.FontSize = Convert.ToDouble(attributes.Value.Trim());
                        }
                        break;
                    }
                case "fontface":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            label.FontFamily = new FontFamily(attributes.Value.Trim());
                        }
                        break;
                    }
                case "color":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            try
                            {
                                label.Foreground = (Brush)new BrushConverter().ConvertFrom(attributes.Value.Trim());
                            }
                            catch
                            {
                                label.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                            }
                        }
                        break;
                    }
                case "name":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            label.Name = attributes.Value.Trim();
                        }
                        break;
                    }
                case "backgroundcolor":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            try
                            {
                                label.Background = (Brush)new BrushConverter().ConvertFrom(attributes.Value.Trim());
                            }
                            catch
                            {
                                label.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                            }
                        }
                        break;
                    }
                case "bordercolor":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            try
                            {
                                label.BorderBrush = (Brush)new BrushConverter().ConvertFrom(attributes.Value.Trim());
                            }
                            catch
                            {
                                label.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                            }
                        }
                        break;
                    }
                case "bordersize":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            label.BorderThickness = new Thickness(Convert.ToDouble(attributes.Value.Trim()));
                        }
                        break;
                    }
                case "x":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            label.X = Convert.ToDouble(attributes.Value.Trim());
                            Canvas.SetLeft(label, label.X);
                        }
                        break;
                    }
                case "y":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            label.Y = Convert.ToDouble(attributes.Value.Trim());
                            Canvas.SetTop(label, label.Y);
                        }
                        break;
                    }
                case "width":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "" && attributes.Value.Trim() != "NaN")
                        {
                            try
                            {
                                label.Width = Convert.ToDouble(attributes.Value.Trim());
                            }
                            catch
                            {
                            }
                        }
                        break;
                    }
                case "height":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "" && attributes.Value.Trim() != "NaN")
                        {
                            try
                            {
                                label.Height = Convert.ToDouble(attributes.Value.Trim());
                            }
                            catch
                            {
                            }
                        }
                        break;
                    }

            }
        }
        private void GetRegistrationBlockAttributes(XmlAttribute attributes, ref CMRegistrationBlock item)
        {
            switch (attributes.Name.ToLower().Trim())
            {
                case "name":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.Name = attributes.Value.Trim();
                        }
                        break;
                    }
                case "backgroundcolor":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            try
                            {
                                item.Background = (Brush)new BrushConverter().ConvertFrom(attributes.Value.Trim());
                            }
                            catch
                            {
                                item.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                            }
                        }
                        break;
                    }
                case "x":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.X = Convert.ToDouble(attributes.Value.Trim());
                            Canvas.SetLeft(item, item.X);
                        }
                        break;
                    }
                case "y":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.Y = Convert.ToDouble(attributes.Value.Trim());
                            Canvas.SetTop(item, item.Y);
                        }
                        break;
                    }
                case "width":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.Width = Convert.ToDouble(attributes.Value.Trim());
                        }
                        break;
                    }
                case "height":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.Height = Convert.ToDouble(attributes.Value.Trim());
                        }
                        break;
                    }
                
            }
        }
        private void GetTextBlockAttributes(XmlAttribute attributes, ref CMTestBlock item)
        {
            switch (attributes.Name.ToLower().Trim())
            {
                case "name":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.Name = attributes.Value.Trim();
                        }
                        break;
                    }
                case "backgroundcolor":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            try
                            {
                                item.Background = (Brush)new BrushConverter().ConvertFrom(attributes.Value.Trim());
                            }
                            catch
                            {
                                item.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                            }
                        }
                        break;
                    }
                case "x":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.X = Convert.ToDouble(attributes.Value.Trim());
                            Canvas.SetLeft(item, item.X);
                        }
                        break;
                    }
                case "y":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.Y = Convert.ToDouble(attributes.Value.Trim());
                            Canvas.SetTop(item, item.Y);
                        }
                        break;
                    }
                case "width":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.Width = Convert.ToDouble(attributes.Value.Trim());
                        }
                        break;
                    }
                case "height":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.Height = Convert.ToDouble(attributes.Value.Trim());
                        }
                        break;
                    }

            }
        }
        private void GetQuestionBlockAttributes(XmlAttribute attributes, ref CMQuestionBlock item)
        {
            switch (attributes.Name.ToLower().Trim())
            {
                case "name":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.Name = attributes.Value.Trim();
                        }
                        break;
                    }
                case "backgroundcolor":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            try
                            {
                                item.Background = (Brush)new BrushConverter().ConvertFrom(attributes.Value.Trim());
                            }
                            catch
                            {
                                item.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                            }
                        }
                        break;
                    }
                case "x":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.X = Convert.ToDouble(attributes.Value.Trim());
                            Canvas.SetLeft(item, item.X);
                        }
                        break;
                    }
                case "y":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.Y = Convert.ToDouble(attributes.Value.Trim());
                            Canvas.SetTop(item, item.Y);
                        }
                        break;
                    }
                case "width":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.Width = Convert.ToDouble(attributes.Value.Trim());
                        }
                        break;
                    }
                case "height":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.Height = Convert.ToDouble(attributes.Value.Trim());
                        }
                        break;
                    }
                case "answercount":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.AnswerCount = Convert.ToInt32(attributes.Value.Trim());
                        }
                        break;
                    }
                case "questionstart":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.QuestionStart = Convert.ToInt32(attributes.Value.Trim());
                        }
                        break;
                    }

            }
        }
        private void GetLangBlockAttributes(XmlAttribute attributes, ref CMLangBlock item)
        {
            switch (attributes.Name.ToLower().Trim())
            {
                case "name":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.Name = attributes.Value.Trim();
                        }
                        break;
                    }
                case "backgroundcolor":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            try
                            {
                                item.Background = (Brush)new BrushConverter().ConvertFrom(attributes.Value.Trim());
                            }
                            catch
                            {
                                item.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                            }
                        }
                        break;
                    }
                case "x":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.X = Convert.ToDouble(attributes.Value.Trim());
                            Canvas.SetLeft(item, item.X);
                        }
                        break;
                    }
                case "y":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.Y = Convert.ToDouble(attributes.Value.Trim());
                            Canvas.SetTop(item, item.Y);
                        }
                        break;
                    }
                case "width":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.Width = Convert.ToDouble(attributes.Value.Trim());
                        }
                        break;
                    }
                case "height":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.Height = Convert.ToDouble(attributes.Value.Trim());
                        }
                        break;
                    }
              

            }
        }
        private void GetDayBlockAttributes(XmlAttribute attributes, ref CMDayBlock item)
        {
            switch (attributes.Name.ToLower().Trim())
            {
                case "name":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.Name = attributes.Value.Trim();
                        }
                        break;
                    }
                case "backgroundcolor":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            try
                            {
                                item.Background = (Brush)new BrushConverter().ConvertFrom(attributes.Value.Trim());
                            }
                            catch
                            {
                                item.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                            }
                        }
                        break;
                    }
                case "x":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.X = Convert.ToDouble(attributes.Value.Trim());
                            Canvas.SetLeft(item, item.X);
                        }
                        break;
                    }
                case "y":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.Y = Convert.ToDouble(attributes.Value.Trim());
                            Canvas.SetTop(item, item.Y);
                        }
                        break;
                    }
                case "width":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.Width = Convert.ToDouble(attributes.Value.Trim());
                        }
                        break;
                    }
                case "height":
                    {
                        if (attributes.Value != null && attributes.Value.Trim() != "")
                        {
                            item.Height = Convert.ToDouble(attributes.Value.Trim());
                        }
                        break;
                    }


            }
        }

        public void CreateFile(Canvas canvas,string fileName)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);
            XmlNode rootNode = doc.CreateElement("CardModel");
            doc.AppendChild(rootNode);

            string name;
            double width;
            double height;
            double x;
            double y;
            double borderSize;
            string backGroundColor;
            string borderColor;
            string col;
            int line;           
            bool isChecked;
            string text;
            string fontFace;
            double fontSize;
            string color;
            string src;
            int answerCount;
            int questionStart;
            bool isHitTestVisible;

            foreach (FrameworkElement element in canvas.Children)
            {
                #region cmImage
                if (element.GetType() == typeof(CMImage))
                {
                    CMImage cmImage = (CMImage)element;                 
                    name = cmImage.Name;
                    width = cmImage.Width;
                    height = cmImage.Height;
                    x = Canvas.GetLeft(cmImage);
                    y = Canvas.GetTop(cmImage);
                    src = cmImage.Source.ToString();

                    XmlNode imageNode = doc.CreateElement("Image");

                    XmlAttribute attributeNameImage = doc.CreateAttribute("Name");
                    XmlAttribute attributeWidthImage = doc.CreateAttribute("Width");
                    XmlAttribute attributeHeightImage = doc.CreateAttribute("Height");
                    XmlAttribute attributeXImage = doc.CreateAttribute("X");
                    XmlAttribute attributeYImage = doc.CreateAttribute("Y");                  
                    XmlAttribute attributeSrcImage = doc.CreateAttribute("Src");
                                       
                    attributeNameImage.Value = name;
                    attributeWidthImage.Value = width.ToString();
                    attributeHeightImage.Value = height.ToString();
                    attributeXImage.Value = x.ToString();
                    attributeYImage.Value = y.ToString();
                    attributeSrcImage.Value = src.ToString();

                    imageNode.Attributes.Append(attributeNameImage);                
                    imageNode.Attributes.Append(attributeWidthImage);
                    imageNode.Attributes.Append(attributeHeightImage);
                    imageNode.Attributes.Append(attributeXImage);
                    imageNode.Attributes.Append(attributeYImage);
                    imageNode.Attributes.Append(attributeSrcImage);

                    rootNode.AppendChild(imageNode);
                }
                #endregion

                #region registrationBlock
                if (element.GetType() == typeof(CMRegistrationBlock))
                {
                    CMRegistrationBlock registrationBlock = (CMRegistrationBlock)element;                 
                    name = registrationBlock.Name;
                    width = registrationBlock.Width;
                    height = registrationBlock.Height;
                    x = Canvas.GetLeft(registrationBlock);
                    y = Canvas.GetTop(registrationBlock);
                    backGroundColor = registrationBlock.Background.ToString();
                    
                    XmlNode registrationBlockNode = doc.CreateElement("RegistrationBlock");
                
                    XmlAttribute attributeNameReg = doc.CreateAttribute("Name");
                    XmlAttribute attributeWidthReg = doc.CreateAttribute("Width");
                    XmlAttribute attributeHeightReg = doc.CreateAttribute("Height");
                    XmlAttribute attributeXReg = doc.CreateAttribute("X");
                    XmlAttribute attributeYReg = doc.CreateAttribute("Y");                
                    XmlAttribute attributeBackGroundColorReg = doc.CreateAttribute("BackGroundColor");
                                     
                    attributeNameReg.Value = name;
                    attributeWidthReg.Value = width.ToString();
                    attributeHeightReg.Value = height.ToString();
                    attributeXReg.Value = x.ToString();
                    attributeYReg.Value = y.ToString();
                    attributeBackGroundColorReg.Value = backGroundColor.ToString();
                                      
                    registrationBlockNode.Attributes.Append(attributeNameReg);
                    registrationBlockNode.Attributes.Append(attributeWidthReg);
                    registrationBlockNode.Attributes.Append(attributeHeightReg);
                    registrationBlockNode.Attributes.Append(attributeXReg);
                    registrationBlockNode.Attributes.Append(attributeYReg);
                    registrationBlockNode.Attributes.Append(attributeBackGroundColorReg);

                    foreach (FrameworkElement children in registrationBlock.Children)
                    {
                        #region cmLabel
                        if (children.GetType() == typeof(CMLabel))
                        {
                            CMLabel cmLabel = (CMLabel)children;
                                                       
                            name = cmLabel.Name;
                            width = cmLabel.Width;
                            height = cmLabel.Height;
                            x = Canvas.GetLeft(cmLabel);
                            y = Canvas.GetTop(cmLabel);
                            borderSize = cmLabel.BorderThickness.Top;
                            backGroundColor = cmLabel.Background.ToString();
                            borderColor = cmLabel.BorderBrush.ToString();
                            text = cmLabel.Text;
                            fontFace = cmLabel.FontFamily.ToString();
                            fontSize = cmLabel.FontSize;
                            color = cmLabel.Foreground.ToString();
                                                       
                            XmlAttribute attributeNameLabel = doc.CreateAttribute("Name");
                            XmlAttribute attributeWidthLabel = doc.CreateAttribute("Width");
                            XmlAttribute attributeHeightLabel = doc.CreateAttribute("Height");
                            XmlAttribute attributeXLabel = doc.CreateAttribute("X");
                            XmlAttribute attributeYLabel = doc.CreateAttribute("Y");
                            XmlAttribute attributeBackGroundColorLabel = doc.CreateAttribute("BackGroundColor");
                            XmlAttribute attributeTextLabel = doc.CreateAttribute("Text");
                            XmlAttribute attributeFontFaceLabel = doc.CreateAttribute("FontFace");
                            XmlAttribute attributeFontSizeLabel = doc.CreateAttribute("FontSize");
                            XmlAttribute attributeColorLabel = doc.CreateAttribute("Color");
                            XmlAttribute attributeBorderSizeLabel = doc.CreateAttribute("BorderSize");
                            XmlAttribute attributeBorderColorLabel = doc.CreateAttribute("BorderColor");

                            XmlNode cmLabelNode = doc.CreateElement("Label");
                           
                            attributeNameLabel.Value = name;
                            attributeWidthLabel.Value = width.ToString();
                            attributeHeightLabel.Value = height.ToString();
                            attributeXLabel.Value = x.ToString();
                            attributeYLabel.Value = y.ToString();
                            attributeBorderSizeLabel.Value = borderSize.ToString();
                            attributeBackGroundColorLabel.Value = backGroundColor.ToString();
                            attributeBorderColorLabel.Value = borderColor.ToString();
                            attributeTextLabel.Value = text;
                            attributeFontFaceLabel.Value = fontFace;
                            attributeFontSizeLabel.Value = fontSize.ToString();
                            attributeColorLabel.Value = color;
                                                 
                            cmLabelNode.Attributes.Append(attributeNameLabel);
                            cmLabelNode.Attributes.Append(attributeWidthLabel);
                            cmLabelNode.Attributes.Append(attributeHeightLabel);
                            cmLabelNode.Attributes.Append(attributeXLabel);
                            cmLabelNode.Attributes.Append(attributeYLabel);
                            cmLabelNode.Attributes.Append(attributeBorderSizeLabel);
                            cmLabelNode.Attributes.Append(attributeBackGroundColorLabel);
                            cmLabelNode.Attributes.Append(attributeBorderColorLabel);
                            cmLabelNode.Attributes.Append(attributeTextLabel);
                            cmLabelNode.Attributes.Append(attributeFontFaceLabel);
                            cmLabelNode.Attributes.Append(attributeFontSizeLabel);
                            cmLabelNode.Attributes.Append(attributeColorLabel);

                            registrationBlockNode.AppendChild(cmLabelNode);
                        }
                        #endregion
                        #region cmCheckBox
                        if (children.GetType() == typeof(CMCheckBox))
                        {
                            CMCheckBox cmCheckBox = (CMCheckBox)children;                           
                            name = cmCheckBox.Name;
                            width = cmCheckBox.Width;
                            height = cmCheckBox.Height;
                            x = Canvas.GetLeft(cmCheckBox);
                            y = Canvas.GetTop(cmCheckBox);
                            borderSize = cmCheckBox.StrokeThickness;
                            backGroundColor = cmCheckBox.Fill.ToString();
                            borderColor = cmCheckBox.Stroke.ToString();
                            col = cmCheckBox.Col;
                            line = cmCheckBox.Line;
                            isChecked = cmCheckBox.IsChecked;
                                                      
                            XmlAttribute attributeNameCheck = doc.CreateAttribute("Name");
                            XmlAttribute attributeWidthCheck = doc.CreateAttribute("Width");
                            XmlAttribute attributeHeightCheck = doc.CreateAttribute("Height");
                            XmlAttribute attributeXCheck = doc.CreateAttribute("X");
                            XmlAttribute attributeYCheck = doc.CreateAttribute("Y");
                            XmlAttribute attributeBackGroundColorCheck = doc.CreateAttribute("BackGroundColor");
                            XmlAttribute attributeBorderSizeCheck = doc.CreateAttribute("BorderSize");
                            XmlAttribute attributeBorderColorCheck = doc.CreateAttribute("BorderColor");

                            XmlAttribute attributeColCheck = doc.CreateAttribute("Col");
                            XmlAttribute attributeLineCheck = doc.CreateAttribute("Line");
                            XmlAttribute attributeIsCheckedCheck = doc.CreateAttribute("IsChecked");                          

                            XmlNode cmCheckBoxNode = doc.CreateElement("CheckBox");
                                                      
                            attributeNameCheck.Value = name;
                            attributeWidthCheck.Value = width.ToString();
                            attributeHeightCheck.Value = height.ToString();
                            attributeXCheck.Value = x.ToString();
                            attributeYCheck.Value = y.ToString();
                            attributeBorderSizeCheck.Value = borderSize.ToString();
                            attributeBackGroundColorCheck.Value = backGroundColor.ToString();
                            attributeBorderColorCheck.Value = borderColor.ToString();
                            attributeColCheck.Value = col;
                            attributeLineCheck.Value = line.ToString();
                            attributeIsCheckedCheck.Value = isChecked.ToString();
                                                        
                            cmCheckBoxNode.Attributes.Append(attributeNameCheck);
                            cmCheckBoxNode.Attributes.Append(attributeWidthCheck);
                            cmCheckBoxNode.Attributes.Append(attributeHeightCheck);
                            cmCheckBoxNode.Attributes.Append(attributeXCheck);
                            cmCheckBoxNode.Attributes.Append(attributeYCheck);
                            cmCheckBoxNode.Attributes.Append(attributeBorderSizeCheck);
                            cmCheckBoxNode.Attributes.Append(attributeBackGroundColorCheck);
                            cmCheckBoxNode.Attributes.Append(attributeBorderColorCheck);
                            cmCheckBoxNode.Attributes.Append(attributeColCheck);
                            cmCheckBoxNode.Attributes.Append(attributeLineCheck);
                            cmCheckBoxNode.Attributes.Append(attributeIsCheckedCheck);

                            registrationBlockNode.AppendChild(cmCheckBoxNode);
                        }
                        #endregion
                    }

                    rootNode.AppendChild(registrationBlockNode);
                }
                #endregion

                #region testBlock
                if (element.GetType() == typeof(CMTestBlock))
                {
                    CMTestBlock testBlock = (CMTestBlock)element;
                  
                    name = testBlock.Name;
                    width = testBlock.Width;
                    height = testBlock.Height;
                    x = Canvas.GetLeft(testBlock);
                    y = Canvas.GetTop(testBlock);
                    backGroundColor = testBlock.Background.ToString();

                    XmlNode testBlockNode = doc.CreateElement("TestBlock");
                                      
                    XmlAttribute attributeName = doc.CreateAttribute("Name");
                    XmlAttribute attributeWidth = doc.CreateAttribute("Width");
                    XmlAttribute attributeHeight = doc.CreateAttribute("Height");
                    XmlAttribute attributeX = doc.CreateAttribute("X");
                    XmlAttribute attributeY = doc.CreateAttribute("Y");
                    XmlAttribute attributeBackGroundColor = doc.CreateAttribute("BackGroundColor");                   
                    XmlAttribute attributeSrc = doc.CreateAttribute("Src");
                                        
                    attributeName.Value = name;
                    attributeWidth.Value = width.ToString();
                    attributeHeight.Value = height.ToString();
                    attributeX.Value = x.ToString();
                    attributeY.Value = y.ToString();
                    attributeBackGroundColor.Value = backGroundColor;

                    testBlockNode.Attributes.Append(attributeName);
                    testBlockNode.Attributes.Append(attributeWidth);
                    testBlockNode.Attributes.Append(attributeHeight);
                    testBlockNode.Attributes.Append(attributeX);
                    testBlockNode.Attributes.Append(attributeY);
                    testBlockNode.Attributes.Append(attributeBackGroundColor);

                 
                    foreach (FrameworkElement children in testBlock.Children)
                    {
                        #region cmLabel
                        if (children.GetType() == typeof(CMLabel))
                        {
                            CMLabel cmLabel = (CMLabel)children;
                                                      
                            name = cmLabel.Name;
                            width = cmLabel.Width;
                            height = cmLabel.Height;
                            x = Canvas.GetLeft(cmLabel);
                            y = Canvas.GetTop(cmLabel);
                            borderSize = cmLabel.BorderThickness.Top;
                            backGroundColor = cmLabel.Background.ToString();
                            borderColor = cmLabel.BorderBrush.ToString();
                            text = cmLabel.Text;
                            fontFace = cmLabel.FontFamily.ToString();
                            fontSize = cmLabel.FontSize;
                            color = cmLabel.Foreground.ToString();
                                                     
                            XmlAttribute attributeNameLabel = doc.CreateAttribute("Name");
                            XmlAttribute attributeWidthLabel = doc.CreateAttribute("Width");
                            XmlAttribute attributeHeightLabel = doc.CreateAttribute("Height");
                            XmlAttribute attributeXLabel = doc.CreateAttribute("X");
                            XmlAttribute attributeYLabel = doc.CreateAttribute("Y");
                            XmlAttribute attributeBackGroundColorLabel = doc.CreateAttribute("BackGroundColor");
                            XmlAttribute attributeTextLabel = doc.CreateAttribute("text");
                            XmlAttribute attributeFontFaceLabel = doc.CreateAttribute("fontFace");
                            XmlAttribute attributeFontSizeLabel = doc.CreateAttribute("fontSize");
                            XmlAttribute attributeColorLabel = doc.CreateAttribute("color");
                            XmlAttribute attributeBorderSizeLabel = doc.CreateAttribute("BorderSize");
                            XmlAttribute attributeBorderColorLabel = doc.CreateAttribute("BorderColor");

                            XmlNode cmLabelNode = doc.CreateElement("Label");
                        
                            attributeNameLabel.Value = name;
                            attributeWidthLabel.Value = width.ToString();
                            attributeHeightLabel.Value = height.ToString();
                            attributeXLabel.Value = x.ToString();
                            attributeYLabel.Value = y.ToString();
                            attributeBorderSizeLabel.Value = borderSize.ToString();
                            attributeBackGroundColorLabel.Value = backGroundColor.ToString();
                            attributeBorderColorLabel.Value = borderColor.ToString();
                            attributeTextLabel.Value = text;
                            attributeFontFaceLabel.Value = fontFace;
                            attributeFontSizeLabel.Value = fontSize.ToString();
                            attributeColorLabel.Value = color;
                                               
                            cmLabelNode.Attributes.Append(attributeNameLabel);
                            cmLabelNode.Attributes.Append(attributeWidthLabel);
                            cmLabelNode.Attributes.Append(attributeHeightLabel);
                            cmLabelNode.Attributes.Append(attributeXLabel);
                            cmLabelNode.Attributes.Append(attributeYLabel);
                            cmLabelNode.Attributes.Append(attributeBorderSizeLabel);
                            cmLabelNode.Attributes.Append(attributeBackGroundColorLabel);
                            cmLabelNode.Attributes.Append(attributeBorderColorLabel);
                            cmLabelNode.Attributes.Append(attributeTextLabel);
                            cmLabelNode.Attributes.Append(attributeFontFaceLabel);
                            cmLabelNode.Attributes.Append(attributeFontSizeLabel);
                            cmLabelNode.Attributes.Append(attributeColorLabel);

                            testBlockNode.AppendChild(cmLabelNode);
                        }
                        #endregion
                        #region cmCheckBox
                        if (children.GetType() == typeof(CMCheckBox))
                        {
                            CMCheckBox cmCheckBox = (CMCheckBox)children;
                         
                            name = cmCheckBox.Name;
                            width = cmCheckBox.Width;
                            height = cmCheckBox.Height;
                            x = Canvas.GetLeft(cmCheckBox);
                            y = Canvas.GetTop(cmCheckBox);
                            borderSize = cmCheckBox.StrokeThickness;
                            backGroundColor = cmCheckBox.Fill.ToString();
                            borderColor = cmCheckBox.Stroke.ToString();
                            col = cmCheckBox.Col;
                            line = cmCheckBox.Line;
                            isChecked = cmCheckBox.IsChecked;

                            XmlAttribute attributeNameCheck = doc.CreateAttribute("Name");
                            XmlAttribute attributeWidthCheck = doc.CreateAttribute("Width");
                            XmlAttribute attributeHeightCheck = doc.CreateAttribute("Height");
                            XmlAttribute attributeXCheck = doc.CreateAttribute("X");
                            XmlAttribute attributeYCheck = doc.CreateAttribute("Y");
                            XmlAttribute attributeBackGroundColorCheck = doc.CreateAttribute("BackGroundColor");
                            XmlAttribute attributeBorderSizeCheck = doc.CreateAttribute("BorderSize");
                            XmlAttribute attributeBorderColorCheck = doc.CreateAttribute("BorderColor");

                            XmlAttribute attributeColCheck = doc.CreateAttribute("Col");
                            XmlAttribute attributeLineCheck = doc.CreateAttribute("Line");
                            XmlAttribute attributeIsCheckedCheck = doc.CreateAttribute("IsChecked");

                            XmlNode cmCheckBoxNode = doc.CreateElement("CheckBox");
                                                     
                            attributeNameCheck.Value = name;
                            attributeWidthCheck.Value = width.ToString();
                            attributeHeightCheck.Value = height.ToString();
                            attributeXCheck.Value = x.ToString();
                            attributeYCheck.Value = y.ToString();
                            attributeBorderSizeCheck.Value = borderSize.ToString();
                            attributeBackGroundColorCheck.Value = backGroundColor.ToString();
                            attributeBorderColorCheck.Value = borderColor.ToString();
                            attributeColCheck.Value = col;
                            attributeLineCheck.Value = line.ToString();
                            attributeIsCheckedCheck.Value = isChecked.ToString();
                                                    
                            cmCheckBoxNode.Attributes.Append(attributeNameCheck);
                            cmCheckBoxNode.Attributes.Append(attributeWidthCheck);
                            cmCheckBoxNode.Attributes.Append(attributeHeightCheck);
                            cmCheckBoxNode.Attributes.Append(attributeXCheck);
                            cmCheckBoxNode.Attributes.Append(attributeYCheck);
                            cmCheckBoxNode.Attributes.Append(attributeBorderSizeCheck);
                            cmCheckBoxNode.Attributes.Append(attributeBackGroundColorCheck);
                            cmCheckBoxNode.Attributes.Append(attributeBorderColorCheck);
                            cmCheckBoxNode.Attributes.Append(attributeColCheck);
                            cmCheckBoxNode.Attributes.Append(attributeLineCheck);
                            cmCheckBoxNode.Attributes.Append(attributeIsCheckedCheck);

                            testBlockNode.AppendChild(cmCheckBoxNode);
                        }
                        #endregion
                    }

                    rootNode.AppendChild(testBlockNode);
                }
                #endregion

                #region questioBlockList
                if (element.GetType() == typeof(CMQuestionBlock))
                {
                    CMQuestionBlock questionBlock = (CMQuestionBlock)element;
                                     
                    name = questionBlock.Name;
                    width = questionBlock.Width;
                    height = questionBlock.Height;
                    x = Canvas.GetLeft(questionBlock);
                    y = Canvas.GetTop(questionBlock);
                    backGroundColor = questionBlock.Background.ToString();
                    answerCount = questionBlock.AnswerCount;
                    questionStart = questionBlock.QuestionStart;

                    XmlNode questionBlockNode = doc.CreateElement("QuestionBlock");
                                       
                    XmlAttribute attributeName = doc.CreateAttribute("Name");
                    XmlAttribute attributeWidth = doc.CreateAttribute("Width");
                    XmlAttribute attributeHeight = doc.CreateAttribute("Height");
                    XmlAttribute attributeX = doc.CreateAttribute("X");
                    XmlAttribute attributeY = doc.CreateAttribute("Y");                  
                    XmlAttribute attributeBackGroundColor = doc.CreateAttribute("BackGroundColor");
                    XmlAttribute attributeAnswerCount = doc.CreateAttribute("AnswerCount");
                    XmlAttribute attributeQuestionStart = doc.CreateAttribute("QuestionStart");

                    attributeName.Value = name;
                    attributeWidth.Value = width.ToString();
                    attributeHeight.Value = height.ToString();
                    attributeX.Value = x.ToString();
                    attributeY.Value = y.ToString();
                    attributeBackGroundColor.Value = backGroundColor.ToString();
                    attributeAnswerCount.Value = answerCount.ToString();
                    attributeQuestionStart.Value = questionStart.ToString();

                    questionBlockNode.Attributes.Append(attributeName);
                    questionBlockNode.Attributes.Append(attributeWidth);
                    questionBlockNode.Attributes.Append(attributeHeight);
                    questionBlockNode.Attributes.Append(attributeX);
                    questionBlockNode.Attributes.Append(attributeY);
                    questionBlockNode.Attributes.Append(attributeBackGroundColor);
                    questionBlockNode.Attributes.Append(attributeAnswerCount);
                    questionBlockNode.Attributes.Append(attributeQuestionStart);

                    foreach (FrameworkElement children in questionBlock.Children)
                    {
                        #region cmLabel
                        if (children.GetType() == typeof(CMLabel))
                        {
                            CMLabel cmLabel = (CMLabel)children;
                            questionBlockNode.AppendChild(CreateLabelNode(doc, cmLabel));
                        }
                        #endregion
                        #region cmCheckBox
                        if (children.GetType() == typeof(CMCheckBox))
                        {
                            CMCheckBox cmCheckBox = (CMCheckBox)children;
                            questionBlockNode.AppendChild(CreateCheckBoxNode(doc,cmCheckBox));
                        }
                        #endregion
                    }
                    
                    rootNode.AppendChild(questionBlockNode);
                }
                #endregion

                #region langBlockList
                if (element.GetType() == typeof(CMLangBlock))
                {
                    CMLangBlock questionBlock = (CMLangBlock)element;

                    name = questionBlock.Name;
                    width = questionBlock.Width;
                    height = questionBlock.Height;
                    x = Canvas.GetLeft(questionBlock);
                    y = Canvas.GetTop(questionBlock);
                    backGroundColor = questionBlock.Background.ToString();                    

                    XmlNode questionBlockNode = doc.CreateElement("LangBlock");

                    XmlAttribute attributeName = doc.CreateAttribute("Name");
                    XmlAttribute attributeWidth = doc.CreateAttribute("Width");
                    XmlAttribute attributeHeight = doc.CreateAttribute("Height");
                    XmlAttribute attributeX = doc.CreateAttribute("X");
                    XmlAttribute attributeY = doc.CreateAttribute("Y");
                    XmlAttribute attributeBackGroundColor = doc.CreateAttribute("BackGroundColor");                    

                    attributeName.Value = name;
                    attributeWidth.Value = width.ToString();
                    attributeHeight.Value = height.ToString();
                    attributeX.Value = x.ToString();
                    attributeY.Value = y.ToString();
                    attributeBackGroundColor.Value = backGroundColor.ToString();                    

                    questionBlockNode.Attributes.Append(attributeName);
                    questionBlockNode.Attributes.Append(attributeWidth);
                    questionBlockNode.Attributes.Append(attributeHeight);
                    questionBlockNode.Attributes.Append(attributeX);
                    questionBlockNode.Attributes.Append(attributeY);
                    questionBlockNode.Attributes.Append(attributeBackGroundColor);                    

                    foreach (FrameworkElement children in questionBlock.Children)
                    {
                        #region cmLabel
                        if (children.GetType() == typeof(CMLabel))
                        {
                            CMLabel cmLabel = (CMLabel)children;
                            questionBlockNode.AppendChild(CreateLabelNode(doc, cmLabel));
                        }
                        #endregion
                        #region cmCheckBox
                        if (children.GetType() == typeof(CMCheckBox))
                        {
                            CMCheckBox cmCheckBox = (CMCheckBox)children;
                            questionBlockNode.AppendChild(CreateCheckBoxNode(doc, cmCheckBox));
                        }
                        #endregion
                    }

                    rootNode.AppendChild(questionBlockNode);
                }
                #endregion

                #region dayBlockList
                if (element.GetType() == typeof(CMDayBlock))
                {
                    CMDayBlock questionBlock = (CMDayBlock)element;

                    name = questionBlock.Name;
                    width = questionBlock.Width;
                    height = questionBlock.Height;
                    x = Canvas.GetLeft(questionBlock);
                    y = Canvas.GetTop(questionBlock);
                    backGroundColor = questionBlock.Background.ToString();

                    XmlNode questionBlockNode = doc.CreateElement("DayBlock");

                    XmlAttribute attributeName = doc.CreateAttribute("Name");
                    XmlAttribute attributeWidth = doc.CreateAttribute("Width");
                    XmlAttribute attributeHeight = doc.CreateAttribute("Height");
                    XmlAttribute attributeX = doc.CreateAttribute("X");
                    XmlAttribute attributeY = doc.CreateAttribute("Y");
                    XmlAttribute attributeBackGroundColor = doc.CreateAttribute("BackGroundColor");

                    attributeName.Value = name;
                    attributeWidth.Value = width.ToString();
                    attributeHeight.Value = height.ToString();
                    attributeX.Value = x.ToString();
                    attributeY.Value = y.ToString();
                    attributeBackGroundColor.Value = backGroundColor.ToString();

                    questionBlockNode.Attributes.Append(attributeName);
                    questionBlockNode.Attributes.Append(attributeWidth);
                    questionBlockNode.Attributes.Append(attributeHeight);
                    questionBlockNode.Attributes.Append(attributeX);
                    questionBlockNode.Attributes.Append(attributeY);
                    questionBlockNode.Attributes.Append(attributeBackGroundColor);

                    foreach (FrameworkElement children in questionBlock.Children)
                    {
                        #region cmLabel
                        if (children.GetType() == typeof(CMLabel))
                        {
                            CMLabel cmLabel = (CMLabel)children;
                            questionBlockNode.AppendChild(CreateLabelNode(doc, cmLabel));
                        }
                        #endregion
                        #region cmCheckBox
                        if (children.GetType() == typeof(CMCheckBox))
                        {
                            CMCheckBox cmCheckBox = (CMCheckBox)children;
                            questionBlockNode.AppendChild(CreateCheckBoxNode(doc, cmCheckBox));
                        }
                        #endregion
                    }

                    rootNode.AppendChild(questionBlockNode);
                }
                #endregion

                #region cmLabel
                if (element.GetType() == typeof(CMLabel))
                {
                    CMLabel cmLabel = (CMLabel)element;
                                       
                    name = cmLabel.Name;
                    width = cmLabel.Width;
                    height = cmLabel.Height;
                    x = Canvas.GetLeft(cmLabel);
                    y = Canvas.GetTop(cmLabel);
                    borderSize = cmLabel.BorderThickness.Top;
                    backGroundColor = cmLabel.Background.ToString();
                    borderColor = cmLabel.BorderBrush.ToString();
                    text = cmLabel.Text;
                    fontFace = cmLabel.FontFamily.ToString();
                    fontSize = cmLabel.FontSize;
                    color = cmLabel.Foreground.ToString();
                                    
                    XmlNode cmLabelNode = doc.CreateElement("Label");

                    XmlAttribute attributeText = doc.CreateAttribute("Text");
                    XmlAttribute attributeFontFace = doc.CreateAttribute("FontFace");
                    XmlAttribute attributeFontSize = doc.CreateAttribute("FontSize");
                    XmlAttribute attributeColor = doc.CreateAttribute("Color");

                    XmlAttribute attributeName = doc.CreateAttribute("Name");
                    XmlAttribute attributeWidth = doc.CreateAttribute("Width");
                    XmlAttribute attributeHeight = doc.CreateAttribute("Height");
                    XmlAttribute attributeX = doc.CreateAttribute("X");
                    XmlAttribute attributeY = doc.CreateAttribute("Y");
                    XmlAttribute attributeBorderSize = doc.CreateAttribute("BorderSize");
                    XmlAttribute attributeBackGroundColor = doc.CreateAttribute("BackGroundColor");
                    XmlAttribute attributeBorderColor = doc.CreateAttribute("BorderColor");
                    XmlAttribute attributeSrc = doc.CreateAttribute("Src");
                                       
                    attributeName.Value = name;
                    attributeWidth.Value = width.ToString();
                    attributeHeight.Value = height.ToString();
                    attributeX.Value = x.ToString();
                    attributeY.Value = y.ToString();
                    attributeBorderSize.Value = borderSize.ToString();
                    attributeBackGroundColor.Value = backGroundColor.ToString();
                    attributeBorderColor.Value = borderColor.ToString();
                    attributeText.Value = text;
                    attributeFontFace.Value = fontFace;
                    attributeFontSize.Value = fontSize.ToString();
                    attributeColor.Value = color;
                                      
                    cmLabelNode.Attributes.Append(attributeName);
                    cmLabelNode.Attributes.Append(attributeWidth);
                    cmLabelNode.Attributes.Append(attributeHeight);
                    cmLabelNode.Attributes.Append(attributeX);
                    cmLabelNode.Attributes.Append(attributeY);
                    cmLabelNode.Attributes.Append(attributeBorderSize);
                    cmLabelNode.Attributes.Append(attributeBackGroundColor);
                    cmLabelNode.Attributes.Append(attributeBorderColor);

                    rootNode.AppendChild(cmLabelNode);
                }
                #endregion
            }

            doc.Save(fileName);
        }

        public string CanvasToXML(Canvas canvas)
        {
            string xml = "";
            XmlSerializer xsSubmit = new XmlSerializer(typeof(Canvas));
            
            using (StringWriter sww = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                xsSubmit.Serialize(writer, canvas);
                xml = sww.ToString(); // Your XML
            }
            return xml;
        }

        public CardModel CanvasToCardModel(Canvas canvas)
        {
            CardModel cardModel = new CardModel();

            #region canvas.Children
            foreach (FrameworkElement element in canvas.Children)
            {
                #region cmImage
                if (element.GetType() == typeof(CMImage))
                {
                    CMImage cmImage = (CMImage)element;
                    cardModel.addImage(cmImage);
                }
                #endregion
                #region registrationBlock
                if (element.GetType() == typeof(CMRegistrationBlock))
                {
                    CMRegistrationBlock registrationBlock = (CMRegistrationBlock)element;
                    cardModel.addRegistrationBlock(registrationBlock);
                }
                #endregion
                #region testBlock
                if (element.GetType() == typeof(CMTestBlock))
                {
                    CMTestBlock testBlock = (CMTestBlock)element;
                    cardModel.addTestBlock(testBlock);
                }
                #endregion
                #region questioBlockList
                if (element.GetType() == typeof(CMQuestionBlock))
                {
                    CMQuestionBlock questionBlock = (CMQuestionBlock)element;
                    cardModel.addQuestionBlock(questionBlock);
                }
                #endregion
                #region cmLabel
                if (element.GetType() == typeof(CMLabel))
                {
                    CMLabel cmLabel = (CMLabel)element;
                    cardModel.addLabel(cmLabel);
                }
                #endregion
            }
            #endregion

            return cardModel;
        }

        public void CreateFileFalho(Canvas canvas, string fileName)
        {
            CardModel cardModel = CanvasToCardModel(canvas);
            //string xml = "";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CardModel));

            /*using (StringWriter sww = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                xmlSerializer.Serialize(writer, cardModel);
                xml = sww.ToString(); // Your XML
            }*/

            FileStream file = File.Create(fileName);
            xmlSerializer.Serialize(file, cardModel);
            file.Close();
        }

        public CardModel LoadFileFalho(string xmlFileURL)
        {
            CardModel cardModel = new CardModel();
            XmlSerializer xmlSerializer = new XmlSerializer(cardModel.GetType());          
            cardModel = (CardModel)xmlSerializer.Deserialize(new XmlTextReader(xmlFileURL));
            return cardModel;
        }

        public XmlAttribute CreateXmlAttribute(XmlDocument doc,string name,string value)
        {
            XmlAttribute attribute = doc.CreateAttribute(name);
            attribute.Value = value;
            return attribute;
        }

        public XmlNode CreateLabelNode(XmlDocument doc, CMLabel item)
        {
            XmlNode node = doc.CreateElement("Label");

            node.Attributes.Append(CreateXmlAttribute(doc, "Name", item.Name));
            node.Attributes.Append(CreateXmlAttribute(doc, "Width", item.Width.ToString()));
            node.Attributes.Append(CreateXmlAttribute(doc, "Height", item.Height.ToString()));            
            node.Attributes.Append(CreateXmlAttribute(doc, "X", Canvas.GetLeft(item).ToString()));
            node.Attributes.Append(CreateXmlAttribute(doc, "Y", Canvas.GetTop(item).ToString()));
            node.Attributes.Append(CreateXmlAttribute(doc, "BorderSize", item.BorderThickness.Top.ToString()));
            node.Attributes.Append(CreateXmlAttribute(doc, "BackGroundColor", item.Background.ToString()));
            node.Attributes.Append(CreateXmlAttribute(doc, "BorderColor", item.BorderBrush.ToString()));
            node.Attributes.Append(CreateXmlAttribute(doc, "Text", item.Text));
            node.Attributes.Append(CreateXmlAttribute(doc, "FontFace", item.FontFamily.ToString()));
            node.Attributes.Append(CreateXmlAttribute(doc, "FontSize", item.FontSize.ToString()));
            node.Attributes.Append(CreateXmlAttribute(doc, "Color", item.Foreground.ToString()));            
          
            return node;
        }

        public XmlNode CreateCheckBoxNode(XmlDocument doc, CMCheckBox item)
        {
            XmlNode node = doc.CreateElement("CheckBox");

            node.Attributes.Append(CreateXmlAttribute(doc, "Name", item.Name));
            node.Attributes.Append(CreateXmlAttribute(doc, "Width", item.Width.ToString()));
            node.Attributes.Append(CreateXmlAttribute(doc, "Height", item.Height.ToString()));
            node.Attributes.Append(CreateXmlAttribute(doc, "X", Canvas.GetLeft(item).ToString()));
            node.Attributes.Append(CreateXmlAttribute(doc, "Y", Canvas.GetTop(item).ToString()));
            node.Attributes.Append(CreateXmlAttribute(doc, "BorderSize", item.StrokeThickness.ToString()));
            node.Attributes.Append(CreateXmlAttribute(doc, "BackGroundColor", item.Fill.ToString()));
            node.Attributes.Append(CreateXmlAttribute(doc, "BorderColor", item.Stroke.ToString()));
            node.Attributes.Append(CreateXmlAttribute(doc, "IsHitTestVisible", item.IsHitTestVisible.ToString()));

            node.Attributes.Append(CreateXmlAttribute(doc, "Col", item.Col));
            node.Attributes.Append(CreateXmlAttribute(doc, "Line", item.Line.ToString()));
            node.Attributes.Append(CreateXmlAttribute(doc, "IsChecked", item.IsChecked.ToString()));

            return node;
        }
    }//fim class





/*XmlSerializer serializer = new XmlSerializer(typeof(msg));
msg resultingMessage = (msg)serializer.Deserialize(new XmlTextReader("yourfile.xml"));

XmlSerializer serializer = new XmlSerializer(typeof(msg));
MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(inputString));
msg resultingMessage = (msg)serializer.Deserialize(memStream);


XmlSerializer serializer = new XmlSerializer(typeof(msg));
StringReader rdr = new StringReader(inputString);
msg resultingMessage = (msg)serializer.Deserialize(rdr);*/

}//fim namespace
