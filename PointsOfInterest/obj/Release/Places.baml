   M S B A M L   `   `   `  ���� K  GPointsOfInterest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:PointsOfInterestPointsOfInterest      PointsOfInterest.Places   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:PointsOfInterest       � Title$  Places��$	��450��$	��800��.��� .+�&   System.Windows.Controls.DataGrid  -   $��places��$
��Left=�$	��179��$��	18,18,0,0q�
���  ��$��4q�$	��Top=�$	��712�� J� RenderTransformOrigin$ 1.043,0.889&�   AutoGenerateColumns .   �� SelectedItem ��  �� Path$ SelectedCustomer� �� Mode$ TwoWay=����� $ OneWay=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Id��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Id+  $
 Name�� �� *	PlaceName+  $ Description�� �� *Descripiton+  $ Average Rate�� �� *Rate+�� -   $��Select Place��$
��Left=�$��331,213,0,0q�$	��Top=�$	��100��$ 0.601,6.893&��� -   $��	PlaceName��$
��Left=�$��23��$��110,249,0,0q�	 �� TextWrapping$
	 Wrap=�$�� ��$	��Top=�$	��186��$ 0.485,0.484&��� -   $��PlaceDes��$
��Left=�$��23��$��110,292,0,0q�$
	 Wrap=�$�� ��$	��Top=�$	��186��$ 
0.53,0.591&��� -   $��PlaceImageName��$
��Left=�$��33��$��110,330,0,0q�$
	 Wrap=�$�� ��$	��Top=�$	��186��$ 0.265,0.426&��� -   $��AddPlace��$��	Add Place��$
��Left=�$��331,252,0,0q�$	��Top=�$��79���� -   $��	NameLabel��$
��Name��$
��Left=�$��
18,246,0,0q�$	��Top=�$��96���� -   $��DesLabel��$��Description��$
��Left=�$��
18,289,0,0q�$	��Top=�$��96���� -	   $��BrowseButton��$��18,330,687,56q�.��Upload Image�� -
   $��DeletePlace��$��Delete Place��$
��Left=�$��458,213,0,0q�$	��Top=�$	��134���� -   $��
SearchTerm��$
��Left=�$��20��$��
28,213,0,0q�$
	 Wrap=�$�� ��$	��Top=�$	��148���� -   $��Search��$
��Left=�$��191,213,0,0q�$	��Top=�$��94���� -   $��Back To Home Page��$
��Left=�$��31��$��503,353,0,0q�$	��Top=�$	��243���� -   $��SeedFromFile��$��Seed from file in Db��$
��Left=�$��31��$��614,212,0,0q�$	��Top=�$	��141���� -   $��ErrrorMessage��$�� ��
���  ��$
��Left=�$��41��$��
18,368,0,0q�$	��Top=�$	��458��