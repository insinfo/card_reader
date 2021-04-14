using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CardProcessing
{
    class QuestionPropertiesPanel
    {
        private int propCunt = 0;

        public void CreatePropOptions(ref PropertiesPanel proPanel, CMQuestionBlock el)
        {
            proPanel.Children.Clear();
            proPanel.CreateProp("Nome:", CMQuestionBlock.PROP_KEY_NAME);
            proPanel.CreateProp("X:", CMQuestionBlock.PROP_KEY_X);
            proPanel.CreateProp("Y:", CMQuestionBlock.PROP_KEY_Y);
            proPanel.CreateProp("Questões:", CMQuestionBlock.PROP_KEY_QUESTION_COUNT);
            proPanel.CreateProp("Respostas:", CMQuestionBlock.PROP_KEY_ANSWER_COUNT);
            proPanel.CreateProp("Inicia:", CMQuestionBlock.PROP_KEY_QUESTION_START);
            propCunt++;
        }
    }
}
