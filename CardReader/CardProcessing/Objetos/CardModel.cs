using System.Collections.Generic;

namespace CardProcessing
{
    public class CardModel
    {
        public List<CMImage> imageList { set; get; }
        public List<CMRegistrationBlock> registrationBlockList { set; get; }
        public List<CMTestBlock> testBlockList { set; get; }
        public List<CMQuestionBlock> questionBlockList { set; get; }
        public List<CMLabel> labelList { set; get; }
        public List<CMLangBlock> langBlockList { set; get; }
        public List<CMDayBlock> dayBlockList { set; get; }
        public int paperWidth { set; get; }
        public int paperHeight { set; get; }

        public void addLangBlock(CMLangBlock langBlock)
        {
            if (langBlockList == null)
            {
                langBlockList = new List<CMLangBlock>();
                langBlockList.Add(langBlock);
            }
            else
            {
                langBlockList.Add(langBlock);
            }
        }

        public void addDayBlock(CMDayBlock dayBlock)
        {
            if (dayBlockList == null)
            {
                dayBlockList = new List<CMDayBlock>();
                dayBlockList.Add(dayBlock);
            }
            else
            {
                dayBlockList.Add(dayBlock);
            }
        }

        public void addQuestionBlock(CMQuestionBlock questionBlock)
        {
            if (questionBlockList == null)
            {
                questionBlockList = new List<CMQuestionBlock>();
                questionBlockList.Add(questionBlock);
            }
            else
            {
                questionBlockList.Add(questionBlock);
            }
        }

        public void addRegistrationBlock(CMRegistrationBlock registrationBlock)
        {
            if (registrationBlockList == null)
            {
                registrationBlockList = new List<CMRegistrationBlock>();
                registrationBlockList.Add(registrationBlock);
            }
            else
            {
                registrationBlockList.Add(registrationBlock);
            }
        }

        public void addLabel(CMLabel label)
        {
            if (labelList == null)
            {
                labelList = new List<CMLabel>();
                labelList.Add(label);
            }
            else
            {
                labelList.Add(label);
            }
        }

        public void addTestBlock(CMTestBlock testBlock)
        {
            if (testBlockList == null)
            {
                testBlockList = new List<CMTestBlock>();
                testBlockList.Add(testBlock);
            }
            else
            {
                testBlockList.Add(testBlock);
            }
        }

        public void addImage(CMImage cmImage)
        {
            if (imageList == null)
            {
                imageList = new List<CMImage>();
                imageList.Add(cmImage);
            }
            else
            {
                imageList.Add(cmImage);
            }
        }
    }

     
   
       


}
