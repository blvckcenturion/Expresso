   M S B A M L   `   `   `  ����F  BExpressoWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:ExpressoWPF.Pages.ProductCategoryPages&ExpressoWPF.Pages.ProductCategoryPages  5      1    +ExpressoWPF.Pages.ProductCategoryPages.List   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Flocal4clr-namespace:ExpressoWPF.Pages.ProductCategoryPages     5      _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5        $��SF Pro Display)�5	      $��20(�5
        N� Title$
  List��5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��2* �6   �� 5      $��1* �6   .+�5   
   �� -   5         ��Name$ MainContent��5   5   $��0��6   $��2��6!    �� CornerRadius$ 10��6J      AccentColor#���   6\   $��3q�6�   $��30q�6�   ._�5      � $��5q�6    � RowDefinitions 5      �� 5      $��2* �6$   �� 5      $	��10* �6$   �� 5      $��1* �6$   .+�5      � $��0��6   $��0q�6$    5      �� 5      $��3* �6+   �� 5      $��1* �6+   �� 5      $��1* �6+   .+�5      �� -   5         ��Name$ 	txtFilter��5   .   $��0��6   $
��5 20q�6A     PrimaryTextColor#���  6O   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Nombre de la Categoria��6   $��Center=�6�   #���   6�    � Cursor$ Arrow��6�   �� 5       -   5         ��Name$ 	btnFilter��5    A   $��1��6   $��2��6-   $
��20 5q�6T   $��Normal"�6b   $
 Hand��6v   .��6�   	Filtrar5"      �� 5#      -   5        $ 
btnShowAll��5#   -   $��2��6   $
��20 5q�6A   $��Normal"�6O   $
 Hand��6c   $y�	Collapsed=�6�   .��6�   Mostrar Todos5%      5&      &   System.Windows.Controls.DataGrid  5'      -   5        	  Name$	 dgvData��5'      
   
IsReadOnly
 .    CanUserAddRows .  $��1��6U     FourthIconColor#���  6b   #���  6�   $��SF Pro Display)�6�   $��18(�6�   5)      �� 5*      $��2��6    �� Orientation$ 
Horizontal=�6*   .+�5+      �� -   5         ��Name$ txtInfo��5+       $��
Categoria:��61   $��Center=�6C   $��Stretch=�6^    �� TextAlignment$ Center=�6|   #���  6�   $��SF Pro Display)�6�   $��20(�6�   5,      5-      5.      �� 5/   
   -   5        $ OptionsContent��5/      $��1��6)   $y�	Collapsed=�69   $ 10��6P   #���   6b   $��3q�6�   $��30q�6�   ._�50      � $��1��6   $��5q�6#    51      �� 52      $��1* �6$   �� 53      $��6* �6$   �� 54      $��1* �6$   �� 55      $��1* �6$   .+�57      �� $��Detalles��6   $��Center=�6,   $��Center=�6G   $��30(�6d     SecondaryAccentColor#���  6r   $��Monument Extended)�6�   � 58      $��1��6    59      �� 5:      $��1* �6+   �� 5;      $��2* �6+    5=      �� 5>      $��1* �6(   �� 5?      $��1* �6(   .+�5A      �� $��0��6    $��0��60   $��Nombre:��6=   #���  6L   $��Center=�6}   $��SF Pro Display)�6�   $
 Left=�6�   $��18(�6�   �� 5B      -	   5        $ txtName��5B   ;   $��1��6   $��0��6.   $ Arrow��6L   $��Center=�6q   #���   6�   $��0 0 5 0q�6�     TextFourthColor �� 
CaretBrush# �  6�    �� SelectionBrush# �   6�   #���  6'  ��6[   Z� 6'  �� 5C      $��0��6    $��1��60   $��Descripcion:��6=   #���  6Q   $��Center=�6�   $��SF Pro Display)�6�   $��18(�6�   �� 5D      -
   5        $ txtDescription��5D   ;   $��1��6   $��1��6.   $ Arrow��6S   $��Center=�6x   #���   6�   $��0 0 5 0q�6�   # �  6�   # �   6�   #���  6.  ��6b   Z� 6.  5E      �� 5F      -   5        $ 	btnUpdate��5F   &   $��2��6   $
��20 5q�69   $��Normal"�6G   $
 Hand��6[   .��6�   
Actualizar5H      �� 5I      -   5        $ 	btnDelete��5I   &   $��3��6   $
��20 5q�69   $��Normal"�6G   $
 Hand��6[   .��6�   
Eliminar5K      5L      5M      5N      5O      