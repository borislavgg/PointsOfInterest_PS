   M S B A M L   `   `   `  ���� K  GPointsOfInterest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:PointsOfInterestPointsOfInterest      PointsOfInterest.Museums   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:PointsOfInterest       � Title$  Museums��$	��450��$	��800��.��� $��0,20,0,0q�.+�&   System.Windows.Controls.DataGrid  -   $��museums��$
��Left=�$	��179��
�������$��4q�$��	18,18,0,0q�$	��Top=�$	��712�� J� RenderTransformOrigin$ 1.043,0.889&�   AutoGenerateColumns .   �� SelectedItem ��  �� Path$ SelectedCustomer� �� Mode$ TwoWay=����� $ OneWay=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Id��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Id+  $
 Name�� �� *
MuseumName+  $ Description�� �� *Descripiton+  $ Average Rate�� �� *AverageRate+�� -   $��Select Museum��$
��Left=�$��266,213,0,0q�$	��Top=�$	��100��$ 0.601,6.893&��� -   $��MusName��$
��Left=�$��23��$��110,249,0,0q�	 �� TextWrapping$
	 Wrap=�$�� ��$	��Top=�$	��186��$ 0.485,0.484&��� -   $��MusDes��$
��Left=�$��23��$��110,292,0,0q�$
	 Wrap=�$�� ��$	��Top=�$	��186��$ 
0.53,0.591&��� -   $��MusImageName��$
��Left=�$��28��$��110,330,0,0q�$
	 Wrap=�$�� ��$	��Top=�$	��186��$ 0.265,0.426&��� -   $��AddMus��$��
Add Museum��$
��Left=�$��332,338,0,0q�$	��Top=�$��79���� -   $��	NameLabel��$
��Name��$
��Left=�$��
18,246,0,0q�$	��Top=�$��96���� -   $��DesLabel��$��Description��$
��Left=�$��
18,289,0,0q�$	��Top=�$��96���� -	   $��BrowseButton��$��18,330,687,41q�.��Upload Image�� -
   $��	DeleteMus��$��Delete Museum��$
��Left=�$��454,213,0,0q�$	��Top=�$	��134��$ 0.434,-0.821&��� -   $��
SearchTerm��$
��Left=�$��20��$��
18,213,0,0q�$
	 Wrap=�$�� ��$	��Top=�$	��138���� -   $��Search��$
��Left=�$��172,213,0,0q�$	��Top=�$��75���� -   $��Back To Home Page��$
��Left=�$��38��$��548,343,0,0q�$	��Top=�$	��224���� -   $��SeedFromFile��$��Seed from File in Db��$
��Left=�$��37��$��612,205,0,0q�$	��Top=�$	��148���� -   $��ErrrorMessage��$�� ��
���  ��$
��Left=�$��43��$��332,272,0,0q�$	��Top=�$	��428���� -   
 ��Name$
 BrowseButton_Copy��$��18,330,687,36q�$��Upload Image���� -   $
 BrowseButton_Copy1��$��18,330,687,36q�$��Upload Image��