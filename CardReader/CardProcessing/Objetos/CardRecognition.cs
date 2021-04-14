using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;


namespace CardProcessing
{
    class CardRecognition
    {
        private CardModel inputCardModel = null;
        private CardModel outputCardModel = null;
        private Bitmap inputBitmap = null;

        public CardRecognition(CardModel cardModel)
        {
            inputCardModel = null;
            inputCardModel = cardModel;            
        }

        public void Init(Bitmap inputBitmap)
        {
            outputCardModel = null;
            outputCardModel = new CardModel();
            this.inputBitmap = inputBitmap;
        }        
        
        public void ReadQuestions()
        {  
            foreach (CMQuestionBlock inputQuestionBlock in inputCardModel.questionBlockList)
            {
                CMQuestionBlock outputQuestionBlock = new CMQuestionBlock();              
                outputQuestionBlock.Name = inputQuestionBlock.Name;
                outputQuestionBlock.Width = inputQuestionBlock.Width;
                outputQuestionBlock.Height = inputQuestionBlock.Height;
                outputQuestionBlock.X = inputQuestionBlock.X;
                outputQuestionBlock.Y = inputQuestionBlock.Y;
                outputQuestionBlock.Background = inputQuestionBlock.Background;
                outputQuestionBlock.AnswerCount = inputQuestionBlock.AnswerCount;
                outputQuestionBlock.QuestionStart = inputQuestionBlock.QuestionStart;

                foreach (System.Windows.FrameworkElement children in inputQuestionBlock.Children)
                {
                    if (children.GetType() == typeof(CMCheckBox))
                    {
                        CMCheckBox inputCheckBox = (CMCheckBox)children;
                        CMCheckBox outCheckBox = new CMCheckBox();
                        outCheckBox.Name = inputCheckBox.Name;
                        outCheckBox.Width = inputCheckBox.Width;
                        outCheckBox.Height = inputCheckBox.Height;
                        outCheckBox.X = inputCheckBox.X;
                        outCheckBox.Y = inputCheckBox.Y;
                        outCheckBox.Fill = inputCheckBox.Fill;
                        outCheckBox.Stroke = inputCheckBox.Stroke;
                        outCheckBox.StrokeThickness = inputCheckBox.StrokeThickness;
                        outCheckBox.Line = inputCheckBox.Line;
                        outCheckBox.Col = inputCheckBox.Col;
                        outCheckBox.IsChecked = false;

                        Point startPosition = new Point();
                        Point endPosition = new Point();
                        startPosition.X = Convert.ToInt32(inputQuestionBlock.X + inputCheckBox.X);
                        startPosition.Y = Convert.ToInt32(inputQuestionBlock.Y + inputCheckBox.Y);
                        endPosition.X = Convert.ToInt32(inputQuestionBlock.X + (inputCheckBox.X + inputCheckBox.Width));
                        endPosition.Y = Convert.ToInt32(inputQuestionBlock.Y + (inputCheckBox.Y + inputCheckBox.Height));

                        if (IsFill(startPosition, endPosition))
                        {                            
                            outCheckBox.IsChecked = true;
                        }
                        outputQuestionBlock.Children.Add(outCheckBox);
                    }
                }
                outputCardModel.addQuestionBlock(outputQuestionBlock);
            }           
        }

        public string GetDayNumber()
        {
            string dayNumber = "";
            foreach (CMDayBlock inputDayBlock in inputCardModel.dayBlockList)
            {
                CMDayBlock outDayBlock = new CMDayBlock();
                outDayBlock.Name = inputDayBlock.Name;
                outDayBlock.Width = inputDayBlock.Width;
                outDayBlock.Height = inputDayBlock.Height;
                outDayBlock.X = inputDayBlock.X;
                outDayBlock.Y = inputDayBlock.Y;
                outDayBlock.Background = inputDayBlock.Background;

                foreach (System.Windows.FrameworkElement children in inputDayBlock.Children)
                {
                    if (children.GetType() == typeof(CMCheckBox))
                    {
                        CMCheckBox inputCheckBox = (CMCheckBox)children;
                        CMCheckBox outCheckBox = new CMCheckBox();
                        outCheckBox.Name = inputCheckBox.Name;
                        outCheckBox.Width = inputCheckBox.Width;
                        outCheckBox.Height = inputCheckBox.Height;
                        outCheckBox.X = inputCheckBox.X;
                        outCheckBox.Y = inputCheckBox.Y;
                        outCheckBox.Fill = inputCheckBox.Fill;
                        outCheckBox.Stroke = inputCheckBox.Stroke;
                        outCheckBox.StrokeThickness = inputCheckBox.StrokeThickness;
                        outCheckBox.Line = inputCheckBox.Line;
                        outCheckBox.Col = inputCheckBox.Col;
                        outCheckBox.IsChecked = false;

                        Point startPosition = new Point();
                        Point endPosition = new Point();
                        startPosition.X = Convert.ToInt32(inputDayBlock.X + inputCheckBox.X);
                        startPosition.Y = Convert.ToInt32(inputDayBlock.Y + inputCheckBox.Y);
                        endPosition.X = Convert.ToInt32(inputDayBlock.X + (inputCheckBox.X + inputCheckBox.Width));
                        endPosition.Y = Convert.ToInt32(inputDayBlock.Y + (inputCheckBox.Y + inputCheckBox.Height));

                        if (IsFill(startPosition, endPosition))
                        {
                            dayNumber += inputCheckBox.Col;
                            outCheckBox.IsChecked = true;
                        }
                        outDayBlock.Children.Add(outCheckBox);
                    }
                }
                outputCardModel.addDayBlock(outDayBlock);

            }
            return dayNumber;
        }

        public string GetLangNumber()
        {
            string langNumber = "";
            foreach (CMLangBlock inputLangBlock in inputCardModel.langBlockList)
            {
                CMLangBlock outLangBlock = new CMLangBlock();
                outLangBlock.Name = inputLangBlock.Name;
                outLangBlock.Width = inputLangBlock.Width;
                outLangBlock.Height = inputLangBlock.Height;
                outLangBlock.X = inputLangBlock.X;
                outLangBlock.Y = inputLangBlock.Y;
                outLangBlock.Background = inputLangBlock.Background;

                foreach (System.Windows.FrameworkElement children in inputLangBlock.Children)
                {
                    if (children.GetType() == typeof(CMCheckBox))
                    {
                        CMCheckBox inputCheckBox = (CMCheckBox)children;
                        CMCheckBox outCheckBox = new CMCheckBox();
                        outCheckBox.Name = inputCheckBox.Name;
                        outCheckBox.Width = inputCheckBox.Width;
                        outCheckBox.Height = inputCheckBox.Height;
                        outCheckBox.X = inputCheckBox.X;
                        outCheckBox.Y = inputCheckBox.Y;
                        outCheckBox.Fill = inputCheckBox.Fill;
                        outCheckBox.Stroke = inputCheckBox.Stroke;
                        outCheckBox.StrokeThickness = inputCheckBox.StrokeThickness;
                        outCheckBox.Line = inputCheckBox.Line;
                        outCheckBox.Col = inputCheckBox.Col;
                        outCheckBox.IsChecked = false;

                        Point startPosition = new Point();
                        Point endPosition = new Point();
                        startPosition.X = Convert.ToInt32(inputLangBlock.X + inputCheckBox.X);
                        startPosition.Y = Convert.ToInt32(inputLangBlock.Y + inputCheckBox.Y);
                        endPosition.X = Convert.ToInt32(inputLangBlock.X + (inputCheckBox.X + inputCheckBox.Width));
                        endPosition.Y = Convert.ToInt32(inputLangBlock.Y + (inputCheckBox.Y + inputCheckBox.Height));

                        if (IsFill(startPosition, endPosition))
                        {
                            langNumber += inputCheckBox.Col;
                            outCheckBox.IsChecked = true;
                        }
                        outLangBlock.Children.Add(outCheckBox);
                    }
                }
                outputCardModel.addLangBlock(outLangBlock);

            }
            return langNumber;
        }

        public CardModel GetResultModel()
        {
            return this.outputCardModel;
        }

        public List<CMQuestionBlock> GetResultQuestionBlockList()
        {
            return outputCardModel.questionBlockList;
        }
        
        public string GetTestNumber()
        {
            string testNumber = "";
            foreach (CMTestBlock inputTestBlock in inputCardModel.testBlockList)
            {
                CMTestBlock outTestBlock = new CMTestBlock();
                outTestBlock.Name = inputTestBlock.Name;
                outTestBlock.Width = inputTestBlock.Width;
                outTestBlock.Height = inputTestBlock.Height;
                outTestBlock.X = inputTestBlock.X;
                outTestBlock.Y = inputTestBlock.Y;
                outTestBlock.Background = inputTestBlock.Background;

                foreach (System.Windows.FrameworkElement children in inputTestBlock.Children)
                {
                    if (children.GetType() == typeof(CMCheckBox))
                    {
                        CMCheckBox inputCheckBox = (CMCheckBox)children;
                        CMCheckBox outCheckBox = new CMCheckBox();
                        outCheckBox.Name = inputCheckBox.Name;
                        outCheckBox.Width = inputCheckBox.Width;
                        outCheckBox.Height = inputCheckBox.Height;
                        outCheckBox.X = inputCheckBox.X;
                        outCheckBox.Y = inputCheckBox.Y;
                        outCheckBox.Fill = inputCheckBox.Fill;
                        outCheckBox.Stroke = inputCheckBox.Stroke;
                        outCheckBox.StrokeThickness = inputCheckBox.StrokeThickness;
                        outCheckBox.Line = inputCheckBox.Line;
                        outCheckBox.Col = inputCheckBox.Col;
                        outCheckBox.IsChecked = false;

                        Point startPosition = new Point();
                        Point endPosition = new Point();
                        startPosition.X = Convert.ToInt32(inputTestBlock.X + inputCheckBox.X);
                        startPosition.Y = Convert.ToInt32(inputTestBlock.Y + inputCheckBox.Y);
                        endPosition.X = Convert.ToInt32(inputTestBlock.X + (inputCheckBox.X + inputCheckBox.Width));
                        endPosition.Y = Convert.ToInt32(inputTestBlock.Y + (inputCheckBox.Y + inputCheckBox.Height));

                        if (IsFill(startPosition, endPosition))
                        {
                            testNumber += inputCheckBox.Col;
                            outCheckBox.IsChecked = true;
                        }
                        outTestBlock.Children.Add(outCheckBox);
                    }
                }
                outputCardModel.addTestBlock(outTestBlock);

            }
            return testNumber;
        }

        public string GetRegistrationNumber()
        {
            string registrationNumber = "";
            foreach (CMRegistrationBlock inputRegistrationBlock in inputCardModel.registrationBlockList)
            {
                CMRegistrationBlock outRegistrationBlock = new CMRegistrationBlock();
                outRegistrationBlock.Name = inputRegistrationBlock.Name;
                outRegistrationBlock.Width = inputRegistrationBlock.Width;
                outRegistrationBlock.Height = inputRegistrationBlock.Height;
                outRegistrationBlock.X = inputRegistrationBlock.X;
                outRegistrationBlock.Y = inputRegistrationBlock.Y;
                outRegistrationBlock.Background = inputRegistrationBlock.Background;
                
                foreach (System.Windows.FrameworkElement children in inputRegistrationBlock.Children)
                {
                    if (children.GetType() == typeof(CMCheckBox))
                    {
                        CMCheckBox inputCheckBox = (CMCheckBox)children;
                        CMCheckBox outCheckBox = new CMCheckBox();
                        outCheckBox.Name = inputCheckBox.Name;
                        outCheckBox.Width = inputCheckBox.Width;
                        outCheckBox.Height = inputCheckBox.Height;
                        outCheckBox.X = inputCheckBox.X;
                        outCheckBox.Y = inputCheckBox.Y;
                        outCheckBox.Fill = inputCheckBox.Fill;
                        outCheckBox.Stroke = inputCheckBox.Stroke;
                        outCheckBox.StrokeThickness = inputCheckBox.StrokeThickness;
                        outCheckBox.Line = inputCheckBox.Line;
                        outCheckBox.Col = inputCheckBox.Col;
                        outCheckBox.IsChecked = false;

                        Point startPosition = new Point();
                        Point endPosition = new Point();
                        startPosition.X = Convert.ToInt32(inputRegistrationBlock.X + inputCheckBox.X);
                        startPosition.Y = Convert.ToInt32(inputRegistrationBlock.Y + inputCheckBox.Y);
                        endPosition.X = Convert.ToInt32(inputRegistrationBlock.X + (inputCheckBox.X + inputCheckBox.Width));
                        endPosition.Y = Convert.ToInt32(inputRegistrationBlock.Y + (inputCheckBox.Y + inputCheckBox.Height));

                        if (IsFill(startPosition, endPosition))
                        {
                            registrationNumber += inputCheckBox.Col;
                            outCheckBox.IsChecked = true;
                        }
                        outRegistrationBlock.Children.Add(outCheckBox);
                    }
                }
                outputCardModel.addRegistrationBlock(outRegistrationBlock); 

            }
            return registrationNumber;
        }

        /** a posição foi preenchida sim ou não **/
        private bool IsFill(Point startPosition, Point endPosition)
        {           
            int inputBitmapWidth = endPosition.X - startPosition.X;
            int inputBitmapHeight = endPosition.Y - startPosition.Y;
            int inputBitmapPixelCount = inputBitmapWidth * inputBitmapHeight;
            double tolarencia = inputBitmapPixelCount /4; // tolerancia 35%
            bool result = false;
            int pixelBlackCount = 0;

            for (int x = startPosition.X; x < endPosition.X; x++)
            {
                for (int y = startPosition.Y; y < endPosition.Y; y++)
                {                   
                    if (inputBitmap.GetPixel(x, y).R == 0)                       
                    {                       
                        if (pixelBlackCount >= tolarencia)
                        {
                            result = true;
                        }
                        pixelBlackCount++;
                    }                   
                }
            }
            return result;
        }

        
    }

}
