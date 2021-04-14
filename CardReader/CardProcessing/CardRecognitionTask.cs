using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;

namespace CardProcessing
{
    //CLASSE QUE DEFINE UM ARGUMENTO DO EVENTO DE PROGRESSO DE UMA OPERAÇÃO
    public class EventArgsProgress : EventArgs
    {
        public Int64 corrent;
        public Int64 entire;
        public bool state;
        public string info;
    }
    //declaração de um delegate para o evento que mostra um progresso de uma operação em uma Thread
    public delegate void EventHandlerProgress(object sender, EventArgsProgress e);

    public class CardRecognitionTask
    {
        private string cardModelFilePath = null;
        private List<string> filesToRecognition = null;
        private string resultJsonFilePath;
        private string resultJson;
     
        private JavaScriptSerializer jss = null;
        private CardModelParse cardModelParse = null;
        private AsyncOperation operation;
        private Thread recognitionTaskThread;
        public event EventHandler OnComplete;
        public event EventHandlerProgress OnProgress;
    

        public CardRecognitionTask(string cardModelFilePath, List<string> filesToRecognition, string resultJsonFilePath)
        {            
            this.filesToRecognition = filesToRecognition;
            this.resultJsonFilePath = resultJsonFilePath;

            if (cardModelFilePath == null || cardModelFilePath.Trim() == "")
            {
                this.cardModelFilePath = "models/model.xml";
            }
            else
            {
                this.cardModelFilePath = cardModelFilePath;
            }

            
        }

        public void Start()
        {
            operation = AsyncOperationManager.CreateOperation(null);
            recognitionTaskThread = new Thread(new ThreadStart(Run));
            recognitionTaskThread.SetApartmentState(ApartmentState.STA);
            recognitionTaskThread.Start();
        }

        public void Run()
        {
            try
            {
                jss = new JavaScriptSerializer();
                jss.MaxJsonLength = int.MaxValue;
                cardModelParse = new CardModelParse();
                CardModel cardModel = cardModelParse.LoadFile(cardModelFilePath);
                CardRecognitionResult cardRecognitionResult = new CardRecognitionResult();
                //jss.Serialize(cardRecognitionResult);
                CreateJSONFile("{\"cards\":[");
                const string RESPONSE_STATE_BLANK = "blank";
                const string RESPONSE_STATE_NULL = "null";
                string comma = "";
                string responseState = "";

                if (cardModel != null)
                {                    
                    if (filesToRecognition != null && filesToRecognition.Count != 0)
                    {
                        int fileCount = filesToRecognition.Count;
                        int correntFile = 0;
                        bool status = true;
                        foreach (string file in filesToRecognition)
                        {
                            CardRecognition recognition = new CardRecognition(cardModel);
                            ImagePreprocessing imagePreprocessing = new ImagePreprocessing();

                            System.Drawing.Bitmap outputBitmap = null;
                            status = imagePreprocessing.Start(file, out outputBitmap);

                            if (status)
                            {
                                recognition.Init(outputBitmap);
                                Card correntCard = new Card();

                                correntCard.RegistrationNumber = recognition.GetRegistrationNumber();
                                correntCard.TextNumber = recognition.GetTestNumber();
                                correntCard.Lang = recognition.GetLangNumber();
                                correntCard.Day = recognition.GetDayNumber();
                                                               
                                recognition.ReadQuestions();

                                List<CMQuestionBlock> resultQuestionBlockList = recognition.GetResultQuestionBlockList();
                                
                                foreach (CMQuestionBlock resultQuestionBlock in resultQuestionBlockList)
                                {
                                    int colCount = resultQuestionBlock.AnswerCount;
                                    int lineCount = resultQuestionBlock.GetCheckBoxCount() / colCount;
                                    int questionStart = resultQuestionBlock.QuestionStart;

                                    for (int correntLine = 0; correntLine < lineCount; correntLine++)
                                    {
                                        List<CMCheckBox> cbList = resultQuestionBlock.GetAllCheckBoxByLine(correntLine + questionStart);
                                        List<CMCheckBox> cbTrueList = new List<CMCheckBox>();
                                        foreach (CMCheckBox cb in cbList)
                                        {
                                            if (cb.IsChecked)
                                            {
                                                cbTrueList.Add(cb);
                                            }
                                        }

                                        if (cbTrueList.Count == 0)
                                        {
                                            responseState = RESPONSE_STATE_BLANK;
                                        }
                                        else if (cbTrueList.Count >= 2)
                                        {
                                            responseState = RESPONSE_STATE_NULL;
                                        }
                                        else if (cbTrueList.Count == 1)
                                        {
                                            responseState = cbTrueList[0].Col;
                                        }
                                        QuestionAnswer questionAnswer = new QuestionAnswer();
                                        questionAnswer.Question = (correntLine + questionStart).ToString();
                                        questionAnswer.Answer = responseState;
                                        correntCard.AddQuestionAnswer(questionAnswer);
                                    }
                                }
                                //cardRecognitionResult.AddCard(correntCard);       
                                if (correntFile < fileCount-1)
                                {
                                    comma = ",";
                                }
                                else {
                                    comma = "";
                                }                                
                                AppendToJSONFile(jss.Serialize(correntCard)+ comma);
                                
                            }
                            
                            /*GC.Collect();
                            GC.WaitForPendingFinalizers();
                            GC.Collect();*/

                            //ACIONA O EVENTO OnProgress ENVIANDO COMO ARGUMENTO O valor ATUAL E O valor TOTAL 
                            operation.Post(new SendOrPostCallback(delegate (object state)
                            {
                                if (OnProgress != null)
                                {
                                    EventArgsProgress eventArgsProgress = new EventArgsProgress();
                                    eventArgsProgress.corrent = correntFile;
                                    eventArgsProgress.entire = fileCount;
                                    eventArgsProgress.state = status;
                                    eventArgsProgress.info = file;
                                    OnProgress(this, eventArgsProgress);
                                }
                            }), null);
                            correntFile++;
                        }                        
                        AppendToJSONFile("]}");
                       
                    }
                    else
                    {
                        MessageBox.Show("Selecione um arquivo ou pasta para ler");
                    }
                }
                else
                {
                    MessageBox.Show("Selecione um modelo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Complete();
        }

        //METODO EXECUTANDO QUADO A OPERAÇÃO CHEGAR AO FINAL
        private void Complete()
        {
            //ACIONA O EVENTO OnComplete
            operation.Post(new SendOrPostCallback(delegate (object state)
            {
                if (OnComplete != null)
                {
                    OnComplete(this, new EventArgs());
                }
            }), null);
            operation.OperationCompleted();
            Release();
        }

        //METODO PARA LIBERAR A THREAD NO FINAL DO PROCESSO
        public void Release()
        {
            try
            {
                if (recognitionTaskThread != null)
                {
                    //renderProcessThread.Abort();
                    recognitionTaskThread = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //METODO PARA ABORTAR A OPERAÇÃO 
        public void Abort()
        {
            try
            {
                if (recognitionTaskThread != null)
                {
                    recognitionTaskThread.Abort();
                    recognitionTaskThread = null;
                }
            }
            catch (Exception ex)
            {
                //throw new Exception();
            }
        }
        

        private void CreateJSONFile(string jsonString)
        {
            File.WriteAllText(resultJsonFilePath, jsonString + Environment.NewLine);            
        }
        private void AppendToJSONFile(string jsonString)
        {            
            File.AppendAllText(resultJsonFilePath, jsonString + Environment.NewLine);
        }
    }

    
}
