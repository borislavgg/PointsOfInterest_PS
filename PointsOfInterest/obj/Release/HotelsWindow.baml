   M S B A M L   `   `   `  ���� K  GPointsOfInterest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:PointsOfInterestPointsOfInterest  #    PointsOfInterest.HotelsWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:PointsOfInterest       � Title$  HotelsWindow��$	��450��$	��800��.��� .+�&   System.Windows.Controls.DataGrid  -   $��hotels��$
��Left=�$	��179��$��	18,18,0,0q�
����� �$��4q�$	��Top=�$	��712�� J� RenderTransformOrigin$ 1.043,0.889&�   AutoGenerateColumns .   �� SelectedItem ��  �� Path$ SelectedCustomer� �� Mode$ TwoWay=����� $ OneWay=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Id��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Id+  $
 Name�� �� *	HotelName+  $ Place�� �� *Place+  $ Description�� �� *Descripiton+  $ Price�� �� *Price+  $ Average Rate�� �� *Rate+�� -   $��Select Hotel��$
��Left=�$��331,213,0,0q�$	��Top=�$	��100��$ 0.601,6.893&��� -   $��	HotelName��$
��Left=�$��23��$��110,249,0,0q�	 �� TextWrapping$
	 Wrap=�$�� ��$	��Top=�$	��186��$ 0.485,0.484&��� -   $��HotelDes��$
��Left=�$��23��$��110,292,0,0q�$
	 Wrap=�$�� ��$	��Top=�$	��186��$ 
0.53,0.591&��� -   $��HotelImageName��$
��Left=�$��23��$��110,330,0,0q�$
	 Wrap=�$�� ��$	��Top=�$	��186��$ 0.265,0.426&��� -   $��AddHotel��$��	Add Hotel��$
��Left=�$��339,259,0,0q�$	��Top=�$��79���� -   $��	NameLabel��$
��Name��$
��Left=�$��
18,246,0,0q�$	��Top=�$��96���� -   $��DesLabel��$��Description��$
��Left=�$��
18,289,0,0q�$	��Top=�$��96���� -	   $��DeleteHotel��$��Delete Hotel��$
��Left=�$��458,213,0,0q�$	��Top=�$	��134���� -
   $��
HotelPlace��$
��Left=�$��23��$��110,370,0,0q�$
	 Wrap=�$�� ��$	��Top=�$	��186���� -   $��
PlaceLabel��$��Place��$
��Left=�$��
18,367,0,0q�$	��Top=�$��78���� -   $��
HotelPrice��$
��Left=�$��23��$��110,210,0,0q�$
	 Wrap=�$�� ��$	��Top=�$	��186���� -   $��
PriceLabel��$��Price��$
��Left=�$��
18,210,0,0q�$	��Top=�$��78���� -   $��
SearchTerm��$
��Left=�$��23��$��470,249,0,0q�$
	 Wrap=�$�� ��$	��Top=�$	��141���� -   $��Search��$
��Left=�$��640,247,0,0q�$	��Top=�$��90��$��23���� -   $��Back To Home Page��$
��Left=�$��39��$��505,354,0,0q�$	��Top=�$	��207���� -   $��SeedFromFile��$��Seed From File In Db��$
��Left=�$��39��$��321,367,0,0q�$	��Top=�$	��160���� -   $��BrowseButton��$��18,330,687,57q�.��Upload Image�� -   $��ErrrorMessage��$�� ��
���  ��$
��Left=�$��41��$��331,292,0,0q�$	��Top=�$	��438��