   M S B A M L   `   `   `  ���� I  ECardProcessing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:CardProcessingCardProcessing  /clr-namespace:CardProcessingCardProcessing  E ADragablz, Version=0.0.3.160, Culture=neutral, PublicKeyToken=null5(clr-namespace:Dragablz;assembly=DragablzDragablz T PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls     CardProcessing.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:CardProcessing     /custonclr-namespace:CardProcessing     =dragablz(clr-namespace:Dragablz;assembly=Dragablz     OmahApps;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro       � Title$  
CardReader��$	��600��$	��800��   MaterialDesignBody#���     MaterialDesignPaper#���  $X��Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)�.���� .@��� $��EDITAR CARTÃO��.��� |�.  .+��� -    ��Name$ 
svDrawArea��$��91,44,200,0q�$��Visible=�$��Visible=�|�.  .��&    CardProcessing.InteractiveCanvas  -     Name$ drawArea��$��Center=�$��Center=�$
��2550��$
��3510�� J� RenderTransformOrigin$ 0.5,0.5&�z�V� .~��� �� �� T� �� -    ��Name$ ToolBarEditModel��$��0,44,0,0q�$��91��$
��Left=�.+��� -    ��Name$ 
btAddImage��$��IMAGE��$��5q��� -   $ btAddDayBlock��$	��DAY��$��5,0,5,5q��� -   $ btAddTestBlock��$
��TEST��$��5,0,5,5q��� -   $ btAddLangBlock��$
��LANG��$��5,0,5,5q��� -   $ btAddRegBlock��$	��REG��$��5,0,5,5q��� -	   $ btAddQuestionBlock��$��QUEST��$��5,0,5,5q��� -
   $ btAddMultQuestionBlock��$��MQUEST��$��5,0,5,5q��� -   $ MenuEditModel��$��44��$	��Top=� �� Orientation$ 
Horizontal=�$��0q�.+��� -   $ btOpenModel��$��Abrir��$	��5,0q��� -   $ btSaveModel��$��Salvar��$��0,0,5,0q��� -   $ 	btPrinter��$��Imprimir��$��0,0,5,0q��� -   $ btSaveAs��$��Exportar��$��0,0,5,0q��� -    ��Name$ cbZoom��$��85��$��0,0,5,0q� �� Text$
 Zoom��.@��� $
��100%���� $	��50%���� $	��25%���� $	��10%���� $��5%���� -   $ btClearCanvas��$��Excluir Tudo���� $��Right=�$��0,44,0,0q�$	��200��$
��Auto=�$��Hidden=�.���� $	��336��$	��199��$
��Left=�$	��Top=�.+��� -   	 ��Name$	 labe2��$��Propriedades do elemento��$  CardProcessing.PropertiesPanel  -   
  Name$
 elementPropPanel���� $��LER CARTÕES�� �� 
IsSelected . .��� .+��� -   $ MenuCardReader��$��46��$	��Top=�$ 
Horizontal=�$��0q�.+��� -   $ btSelRecognitionModel��$��Selecione Modelo��$	��5,0q��� -   $ btSelRecognitionFolder��$��Selecionar Pasta��$��0,0,5,0q��� -   $ btSelRecognitionFile��$��Selecionar Arquivo��$��0,0,5,0q��� -   $ btStartRecognition��$��Iniciar Leitura��$��0,0,5,0q��� -   $ btSenderResult��$��Enviar Dados��$��0,7,5,7q��� -    ��Name$ rtbRecognitionLog��$��10,103,10,43q�$��1q�$
��Auto=�$
��Auto=����	#FF252525� -    �Name$ pbRecognitionState��$��15��$��
10,83,10,0q�$	��Top=��� $��Tempo Estimado:��$
��Left=�$��	10,54,0,0q�$	��Top=�$ 0.474,0&��� -   $	 	lbTimeEst��$�� ��$
��Left=�$��
115,54,0,0q�$	��Top=�$ 0.474,0&�$	��100���� $��Tempo Atual:��$��Right=�$��583,54,100,0q�$	��Top=�$ 0.474,0&��� -   $	 lbTimeCount��$�� ��$��Right=�$��	0,54,10,0q�$	��Top=�$ 0.474,0&�$��85���� -   $	 
lbProgress��$��0%��$��Center=�$��0,54,0,0q�$	��Top=�$ 0.474,0&�$��55���� $��DIGITALIZAÇÃO��.��� .+��� -   $ btStartScan��$��Iniciar Digitalização��$��10,10,574,485q�� -    $ pbScan��$��15��$��
10,57,10,0q�$	��Top=��� -!   $ 	rtbScaLog��$��10,103,10,43q�$��1q�$
��Auto=�$
��Auto=����	#FF252525