using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WIA;


using WinForms = System.Windows.Forms;
using System.IO;
using System.Windows.Threading;
using System.Windows.Media.Imaging;

namespace CardProcessing
{
    public partial class MainWindow : MahApps.Metro.Controls.MetroWindow
    {       
        public MainWindow()
        {
            InitializeComponent();
            InitEvents();
            cbZoom.IsEditable = true;            
        }

        private void InitEvents()
        {
            btOpenModel.Click += btOpenModel_Click;
            btSaveModel.Click += btSaveModel_Click;
            btSaveAs.Click += btSaveAs_Click;
            btPrinter.Click += btPrinterModel_Click;
            btClearCanvas.Click += BtClearCanvas_Click;
            btAddRegBlock.Click += btAddRegBlock_Click;
            btAddTestBlock.Click += btAddTestBlock_Click;
            btAddQuestionBlock.Click += btAddQuestionBlock_Click;
            cbZoom.SelectionChanged += cbZoom_SelectionChanged;
            cbZoom.KeyUp += cbZoom_KeyUp;
            drawArea.OnSelectElement += DrawArea_OnSelectElement;
            elementPropPanel.OnSetProp += ElementPropPanel_OnSetProp;
            btAddDayBlock.Click += BtAddDayBlock_Click;
            btAddMultQuestionBlock.Click += BtAddMultQuestionBlock_Click;
            btAddLangBlock.Click += BtAddLangBlock_Click;
            btSelRecognitionModel.Click += BtSelRecognitionModel_Click;
            btSelRecognitionFolder.Click += btSelRecognitionFolder_Click;
            btSelRecognitionFile.Click += btSelRecognitionFile_Click;
            btStartRecognition.Click += btStartRecognition_Click;
        }
        

        /*************** EDITOR ******************/

        private void BtClearCanvas_Click(object sender, RoutedEventArgs e)
        {
            drawArea.Children.Clear();
        }

        private void btPrinterModel_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() == true)
            {
                dialog.PrintVisual(drawArea, "My Canvas");
            }
        }
                
        private FrameworkElement elementSelected = null;

        private void ElementPropPanel_OnSetProp(object sender, EventArgs e)
        {
            TextBox tbProp = null;
            if (sender is TextBox)
            {
                tbProp = (TextBox)sender;
            }
            if (elementSelected is CMQuestionBlock)
            {
                CMQuestionBlock el =(CMQuestionBlock)elementSelected;
                if (tbProp.Name == CMQuestionBlock.PROP_KEY_QUESTION_COUNT)
                {
                    el.QuestionCount = Convert.ToInt32(tbProp.Text);
                    el.Update();
                } else if (tbProp.Name == CMQuestionBlock.PROP_KEY_NAME)
                {
                    el.Name = tbProp.Text;
                    el.Update();
                }
                else if (tbProp.Name == CMQuestionBlock.PROP_KEY_QUESTION_START)
                {
                    el.QuestionStart = Convert.ToInt32(tbProp.Text);
                    el.Update();
                    
                }else if (tbProp.Name == CMQuestionBlock.PROP_KEY_X)
                {
                    el.X = Convert.ToDouble(tbProp.Text);
                    Canvas.SetLeft(el, Convert.ToDouble(tbProp.Text));
                    el.Update();
                }
                else if (tbProp.Name == CMQuestionBlock.PROP_KEY_Y)
                {
                    el.Y = Convert.ToDouble(tbProp.Text);
                    Canvas.SetTop(el, Convert.ToDouble(tbProp.Text));
                    el.Update();
                }else if (tbProp.Name == CMQuestionBlock.PROP_KEY_ANSWER_COUNT)
                {
                    el.AnswerCount = Convert.ToInt32(tbProp.Text);                    
                    el.Update();
                }

            }  
        }

        private void DrawArea_OnSelectElement(object sender, EventArgs e)
        {
            //ElementPropPanel.Children.Clear();
            if (sender is FrameworkElement)
            {
                elementSelected = (FrameworkElement)sender;              
                if (elementSelected is CMQuestionBlock)
                {
                    CMQuestionBlock el = (CMQuestionBlock)elementSelected;
                    QuestionPropertiesPanel questionProp = new QuestionPropertiesPanel();
                    questionProp.CreatePropOptions(ref elementPropPanel, el);
                }
                
            }
        }

        private string correntModelFile = "";
        
        private void btOpenModel_Click(object sender, RoutedEventArgs e)
        {
            CardModelParse cardModelParse = new CardModelParse();
            CardModelRender cardModelRender = new CardModelRender(drawArea, this);

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog().Value)
            {
                correntModelFile = ofd.FileName;
                CardModel cardModel = cardModelParse.LoadFile(correntModelFile);
                cardModelRender.setModel(cardModel);
            }
        }
                    
        private void btSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog().Value)
            {
                CardModelRender cardModelRender = new CardModelRender(drawArea, this);
                cardModelRender.RenderToBitmap(sfd.FileName);
            }
        }

        //ADICIONA UM BLOCO DE DIA DE PROVA
        private void BtAddDayBlock_Click(object sender, RoutedEventArgs e)
        {
            CMDayBlock dayBlock = new CMDayBlock(true);
            drawArea.Children.Add(dayBlock);
            Canvas.SetLeft(dayBlock, dayBlock.X);
            Canvas.SetTop(dayBlock, dayBlock.Y);
            drawArea.UpdateLayout();
        }
        // ADICIONA UM BLOCO DE REGISTRO NA PAGINA 
        private void btAddRegBlock_Click(object sender, RoutedEventArgs e)
        {
            CMRegistrationBlock regBlock = new CMRegistrationBlock(true);
            drawArea.Children.Add(regBlock);
            Canvas.SetLeft(regBlock, regBlock.X);
            Canvas.SetTop(regBlock, regBlock.Y);
            drawArea.UpdateLayout();
        }

        private void btAddTestBlock_Click(object sender, RoutedEventArgs e)
        {
            CMTestBlock testBlock = new CMTestBlock(true);
            drawArea.Children.Add(testBlock);
            Canvas.SetLeft(testBlock, testBlock.X);
            Canvas.SetTop(testBlock, testBlock.Y);
            drawArea.UpdateLayout();
        }

        private void BtAddLangBlock_Click(object sender, RoutedEventArgs e)
        {
            CMLangBlock langBlock = new CMLangBlock(true);
            drawArea.Children.Add(langBlock);
            Canvas.SetLeft(langBlock, langBlock.X);
            Canvas.SetTop(langBlock, langBlock.Y);
            drawArea.UpdateLayout();
        }

        private void btAddQuestionBlock_Click(object sender, RoutedEventArgs e)
        {
            CMQuestionBlock questionBlock = new CMQuestionBlock(true);
            drawArea.Children.Add(questionBlock);
            Canvas.SetLeft(questionBlock, questionBlock.X);
            Canvas.SetTop(questionBlock, questionBlock.Y);
            drawArea.UpdateLayout();
        }

        private void BtAddMultQuestionBlock_Click(object sender, RoutedEventArgs e)
        {
            CMQuestionBlock questionBlock = new CMQuestionBlock(true);
            questionBlock.X = 80;
            questionBlock.Y = 1593;
            questionBlock.QuestionStart = 1;
            drawArea.Children.Add(questionBlock);
            Canvas.SetLeft(questionBlock, questionBlock.X);
            Canvas.SetTop(questionBlock, questionBlock.Y);
            drawArea.UpdateLayout();
            questionBlock.Update();

            CMQuestionBlock questionBlock2 = new CMQuestionBlock(true);
            questionBlock2.X = 547;
            questionBlock2.Y = 1593;
            questionBlock2.QuestionStart = 19;
            drawArea.Children.Add(questionBlock2);           
            Canvas.SetLeft(questionBlock2, questionBlock2.X);
            Canvas.SetTop(questionBlock2, questionBlock2.Y);
            drawArea.UpdateLayout();
            questionBlock2.Update();

            CMQuestionBlock questionBlock3 = new CMQuestionBlock(true);
            questionBlock3.X = 1014;
            questionBlock3.Y = 1593;
            questionBlock3.QuestionStart = 37;
            drawArea.Children.Add(questionBlock3);
            Canvas.SetLeft(questionBlock3, questionBlock3.X);
            Canvas.SetTop(questionBlock3, questionBlock3.Y);
            drawArea.UpdateLayout();
            questionBlock3.Update();

            CMQuestionBlock questionBlock4 = new CMQuestionBlock(true);
            questionBlock4.X = 1481;
            questionBlock4.Y = 1593;
            questionBlock4.QuestionStart = 55;
            drawArea.Children.Add(questionBlock4);
            Canvas.SetLeft(questionBlock4, questionBlock4.X);
            Canvas.SetTop(questionBlock4, questionBlock4.Y);
            drawArea.UpdateLayout();
            questionBlock4.Update();

            CMQuestionBlock questionBlock5 = new CMQuestionBlock(true);
            questionBlock5.X = 1948;
            questionBlock5.Y = 1593;
            questionBlock5.QuestionStart = 73;
            drawArea.Children.Add(questionBlock5);
            Canvas.SetLeft(questionBlock5, questionBlock5.X);
            Canvas.SetTop(questionBlock5, questionBlock5.Y);
            drawArea.UpdateLayout();
            questionBlock5.Update();
        }

        private void btSaveModel_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog().Value)
            {
                CardModelParse cardModelParse = new CardModelParse();
                cardModelParse.CreateFile(drawArea, sfd.FileName);
            }

        }

        private void cbZoom_KeyUp(object sender, KeyEventArgs e)
        {
            string zoomValueStrinh = cbZoom.Text;
            double zoomValue = 0.2;

            if (e.Key == Key.Enter)
            {
                if (zoomValueStrinh.IndexOf('%') != -1)
                {
                    zoomValueStrinh = zoomValueStrinh.TrimEnd('%');                   
                }
                if (zoomValueStrinh.Length < 3)
                {
                    zoomValue = Convert.ToDouble(zoomValueStrinh) / 100;
                }
                drawArea.SetZoom(Convert.ToDouble(zoomValue));
                svDrawArea.ScrollToHorizontalOffset(svDrawArea.ScrollableWidth / 2);
                svDrawArea.ScrollToVerticalOffset(svDrawArea.ScrollableHeight / 2);
            }

        }

        private void cbZoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbZoom.SelectedIndex == 0)
            {
                drawArea.SetZoom(1);
                svDrawArea.ScrollToHorizontalOffset(0);
                svDrawArea.ScrollToVerticalOffset(0);
            }
            else if (cbZoom.SelectedIndex == 1)
            {
                drawArea.SetZoom(0.5);
                svDrawArea.ScrollToHorizontalOffset(svDrawArea.ScrollableWidth / 2);
                svDrawArea.ScrollToVerticalOffset(svDrawArea.ScrollableHeight / 2);
            }
            else if (cbZoom.SelectedIndex == 2)
            {
                drawArea.SetZoom(0.25);
                svDrawArea.ScrollToHorizontalOffset(svDrawArea.ScrollableWidth / 2);
                svDrawArea.ScrollToVerticalOffset(svDrawArea.ScrollableHeight / 2);

            }
            else if (cbZoom.SelectedIndex == 3)
            {
                drawArea.SetZoom(0.10);
                svDrawArea.ScrollToHorizontalOffset(svDrawArea.ScrollableWidth / 2);
                svDrawArea.ScrollToVerticalOffset(svDrawArea.ScrollableHeight / 2);
            }
            else if (cbZoom.SelectedIndex == 4)
            {                
                drawArea.SetZoom(0.05);
                svDrawArea.ScrollToHorizontalOffset(svDrawArea.ScrollableWidth / 2);
                svDrawArea.ScrollToVerticalOffset(svDrawArea.ScrollableHeight / 2);
            }
        }

        private void btClearCanvas_Click(object sender, RoutedEventArgs e)
        {
            drawArea.Children.Clear();
        }

        // LEITOR DE CARTÕES 
        private string folderForRecognition = null;
        private string fileModelForRecognition = null;
        private List<string> filesToRecognition = null;
        private TimeSpan timeSpan;
        private DispatcherTimer timer;
        private long timeCount = 0;
       

        private void BtSelRecognitionModel_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "Modelo de Cartão (*.xml)|*.xml|Todos os arquivos (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            if (openFileDialog.ShowDialog().Value)
            {
                fileModelForRecognition = openFileDialog.FileName;
            }
        }

        private void btSelRecognitionFolder_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog fbd = new WinForms.FolderBrowserDialog();
            WinForms.DialogResult result = fbd.ShowDialog();
            if (result == WinForms.DialogResult.OK)
            {
                folderForRecognition = fbd.SelectedPath;
                string[] files = Directory.GetFiles(folderForRecognition);
                if (filesToRecognition == null) { filesToRecognition = new List<string>(); }
                filesToRecognition.Clear();
                // cria uma lista contendo apenas arquivos validos 
                foreach (string file in files)
                {
                    if ( Path.GetExtension(file) == ".jpeg" || Path.GetExtension(file) ==  ".jpg" || Path.GetExtension(file) == ".png" || Path.GetExtension(file) == ".bmp")
                    {
                        filesToRecognition.Add(file);
                    }
                }
            }
        }
        
        private void btSelRecognitionFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "Arquivos de imagem (*.bmp, *.jpg,*.jpeg, *.png)|*.bmp;*.jpg;*.jpeg;*.png|Todos os arquivos (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            if (openFileDialog.ShowDialog().Value)
            {
                if (filesToRecognition == null) { filesToRecognition = new List<string>(); }
                filesToRecognition.Add(openFileDialog.FileName); 
            }
        }
                
        private void btStartRecognition_Click(object sender, RoutedEventArgs e)
        {           
            try
            {
                if (filesToRecognition != null && filesToRecognition.Count != 0)
                {
                    lbTimeEst.Content = TimeSpan.FromSeconds(filesToRecognition.Count * 7).ToString("c");
                    lbProgress.Content = "0%";
                    timeSpan = TimeSpan.FromSeconds(0);
                    timer = new DispatcherTimer();
                    timer.Interval = new TimeSpan(0, 0, 1);
                    timer.Tick += Timer_Tick;
                    timer.Start();

                    btStartRecognition.IsEnabled = false;

                    CardRecognitionTask cardRecognitionTask = new CardRecognitionTask(fileModelForRecognition, filesToRecognition, "output/resultado.json");
                    cardRecognitionTask.Start();
                    cardRecognitionTask.OnProgress += CardRecognitionTask_OnProgress;
                    cardRecognitionTask.OnComplete += CardRecognitionTask_OnComplete;
                }
                else {
                    MessageBox.Show("Selecione um arquivo para ler");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha ao iniciar o Leitor de cartões! "+ex.ToString(),"Erro",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lbTimeCount.Content = TimeSpan.FromSeconds(timeCount).ToString("c");
            timeCount++;
        }

        private void CardRecognitionTask_OnComplete(object sender, EventArgs e)
        {
            rtbRecognitionLog.AppendText("Leitura dos cartões concluida.  \r");
            btStartRecognition.IsEnabled = true;
            timer.Stop();
        }

        private void CardRecognitionTask_OnProgress(object sender, EventArgsProgress e)
        {
            pbRecognitionState.Value = e.corrent * 100.0 / e.entire;
            lbProgress.Content = (Math.Round(e.corrent * 100.0 / e.entire,2))+"%";
            if (e.state)
            {
                rtbRecognitionLog.AppendText("Arquivo: " + e.info.Substring(e.info.LastIndexOf('\\')+1) + " lido com sucesso. \r");
            }
            else
            {
                rtbRecognitionLog.AppendText("Arquivo: " + e.info.Substring(e.info.LastIndexOf('\\') + 1) + " erro na leitura. \r");
            }
            
        }

        private void btStartScan_Click(object sender, RoutedEventArgs e)
        {
            try {
                Scan();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }

            /*WiaDeviceType.ScannerDeviceType,
            WiaImageIntent.ColorIntent,
            WiaImageBias.MaximizeQuality, 
            "{B96B3CAF-0728-11D3-9D7B-0000F81EF32E}",
            true,true,false);*/

            //image.SaveFile("teste.jpeg");
        }

       
        public static List<string> Scan()
        {
            WIA.ICommonDialog dialog = new WIA.CommonDialog();
            WIA.Device device = dialog.ShowSelectDevice(WIA.WiaDeviceType.UnspecifiedDeviceType, true, false);
            if (device != null)
            {
                return Scan(device.DeviceID);
            }
            else
            {
                throw new Exception("Você deve selecionar um dispositivo para a digitalização.");
            }
        }
        const string wiaFormatBMP = "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}";
        class WIA_DPS_DOCUMENT_HANDLING_SELECT
        {
            public const uint FEEDER = 0x00000001;
            public const uint FLATBED = 0x00000002;
        }
        class WIA_DPS_DOCUMENT_HANDLING_STATUS
        {
            public const uint FEED_READY = 0x00000001;
        }
        class WIA_PROPERTIES
        {
            public const uint WIA_RESERVED_FOR_NEW_PROPS = 1024;
            public const uint WIA_DIP_FIRST = 2;
            public const uint WIA_DPA_FIRST = WIA_DIP_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
            public const uint WIA_DPC_FIRST = WIA_DPA_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
            //
            // Scanner only device properties (DPS)
            //
            public const uint WIA_DPS_FIRST = WIA_DPC_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
            public const uint WIA_DPS_DOCUMENT_HANDLING_STATUS = WIA_DPS_FIRST + 13;
            public const uint WIA_DPS_DOCUMENT_HANDLING_SELECT = WIA_DPS_FIRST + 14;
        }

        public static List<string> Scan(string scannerId)
        {
            List<string> images = new List<string>();
            bool hasMorePages = true;
            while (hasMorePages)
            {
                // select the correct scanner using the provided scannerId parameter
                WIA.DeviceManager manager = new WIA.DeviceManager();
                WIA.Device device = null;
                foreach (WIA.DeviceInfo info in manager.DeviceInfos)
                {
                    if (info.DeviceID == scannerId)
                    {
                        // connect to scanner
                        device = info.Connect();
                        break;
                    }
                }
                // device was not found
                if (device == null)
                {
                    // enumerate available devices
                    string availableDevices = "";
                    foreach (WIA.DeviceInfo info in manager.DeviceInfos)
                    {
                        availableDevices += info.DeviceID + "\n";
                    }
                    // show error with available devices
                    throw new Exception("The device with provided ID could not be found.Available Devices:\n" + availableDevices);
                }
                WIA.Item item = device.Items[1] as WIA.Item;
                try
                {
                    // scan image
                    WIA.ICommonDialog wiaCommonDialog = new WIA.CommonDialog();
                    WIA.ImageFile image = (WIA.ImageFile)wiaCommonDialog.ShowTransfer(item, wiaFormatBMP, false);
                    // save to temp file
                    string fileName = Path.GetTempFileName();
                    File.Delete(fileName);
                    image.SaveFile(fileName);
                    image = null;
                    // add file to output list
                    images.Add(fileName);
                }
                catch (Exception exc)
                {
                    throw exc;
                }
                finally
                {
                    item = null;
                    //determine if there are any more pages waiting
                    WIA.Property documentHandlingSelect = null;
                    WIA.Property documentHandlingStatus = null;
                    foreach (WIA.Property prop in device.Properties)
                    {
                        if (prop.PropertyID == WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_SELECT)
                            documentHandlingSelect = prop;
                        if (prop.PropertyID == WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_STATUS)
                            documentHandlingStatus = prop;
                    }
                    // assume there are no more pages
                    hasMorePages = false;
                    // may not exist on flatbed scanner but required for feeder
                    if (documentHandlingSelect != null)
                    {
                        // check for document feeder
                        if ((Convert.ToUInt32(documentHandlingSelect.get_Value()) &
                        WIA_DPS_DOCUMENT_HANDLING_SELECT.FEEDER) != 0)
                        {
                            hasMorePages = ((Convert.ToUInt32(documentHandlingStatus.get_Value()) &
                            WIA_DPS_DOCUMENT_HANDLING_STATUS.FEED_READY) != 0);
                        }
                    }
                }
            }
            return images;
        }
       
        public static List<string> GetDevices()
        {
            List<string> devices = new List<string>();
            WIA.DeviceManager manager = new WIA.DeviceManager();
            foreach (WIA.DeviceInfo info in manager.DeviceInfos)
            {
                devices.Add(info.DeviceID);
            }
            return devices;
        }

        private void btSenderResult_Click(object sender, RoutedEventArgs e)
        {

        }
    }



}
