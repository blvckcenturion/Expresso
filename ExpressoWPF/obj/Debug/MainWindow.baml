   M S B A M L   `   `   `  ����F  BExpressoWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ExpressoWPF.ControlsExpressoWPF.Controls  5   	   )clr-namespace:ExpressoWPFExpressoWPF  5   	   d `Microsoft.Maps.MapControl.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tRclr-namespace:Microsoft.Maps.MapControl.WPF;assembly=Microsoft.Maps.MapControl.WPFMicrosoft.Maps.MapControl.WPF 5   	       ExpressoWPF.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   1ctls"clr-namespace:ExpressoWPF.Controls    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:ExpressoWPF     5   	   `mRclr-namespace:Microsoft.Maps.MapControl.WPF;assembly=Microsoft.Maps.MapControl.WPF     5   	   -   5        $��SF Pro Display)�5	   	      PrimaryBackgroundColor#���   5   	     � Title$  
MainWindow��5   	   $	��850��6   $
��1200��6)    � WindowStartupLocation$ CenterScreen=�66    � WindowStyle$
 None=�6[    � WindowState$ 	Maximized=�6n   .��5      �  � RowDefinitions 5   
   �� 5      $
��121* �6   �� 5      $	��49* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6     SecundaryBackgroundColor#���  5      $��2��6I   ._�5      � -   5         �Name$ GridNav��5      $��60��6$    5      �� 5      $��60 �6$   �� 5      $��* �6$   .+�5!      � $��0��6     AccentColor#���  6$   .+�5"      �� $��
10,10,0,10q�5#      $��40��5$      $��40��6*   $
��Left=�5%      ��5&      �� 5'       j� Stretch$ Uniform=�6)    �� ImageSource$# /Resources/Images/Isotype.png��6;   5)      �� 5+      $��EXPRESSO��6     PrimaryTextColor#���  5,      $
��Bold"�5-      $��16(�5.      $��Monument Extended)�5/      $��Center=�50      $
��Left=�51      $��60 0 0 0q�52      53      �� 56      $��1��6   $��
-1 80 0 20q�6*   .+�57      �� -   5        	 ��Name$	 btnHome��57        PopupButtonStyle#���  6.   .��58      #   ExpressoWPF.Controls.MenuItem  
   	GroupName$
 MenuItem��6(      IndicatorBrush# �  6=     home   Icon# �  6l      	IconWidth$ 24��6�      Text$ Inicio��6�   $��Center=�6�   59      �� 5:      -   5        $	 btnPointOfSale��5:      #���  65   .��5;        $
 MenuItem��6(   # �  6=     cart# �  6l   $ 24��6�   $ Nueva Orden��6�   $��Center=�6�   5<      �� 5=      -   5        $	 btnPreviousSales��5=      #���  67   .��5>        $
 MenuItem��6(   # �  6=    
 orders# �  6l   $ 24��6�   $ Ordenes Previas��6�   $��Center=�6�   5?      �� 5@      -   5        $	 btnProducts��5@      #���  62   .��5A        $
 MenuItem��6(   # �  6=     products# �  6l   $ 24��6�   $ 	Productos��6�   $��Center=�6�   5B      �� 5C      -   5        $	 btnProductCategories��5C      #���  6;   .��5D        $
 MenuItem��6(   # �  6=    	 
categories# � 	 6l   $ 24��6�   $ 
Categorias��6�   $��Center=�6�   5E      �� 5F      -   5        $	 btnLocations��5F      #���  63   .��5G        $
 MenuItem��6(   # �  6=    
 location# � 
 6l   $ 24��6�   $ Ubicaciones��6�   $��Center=�6�   5H      �� 5I      -	   5        $	 btnUsers��5I      #���  6/   .��5J        $
 MenuItem��6(     PrimaryBlueColor# �  6=    	 users# �  6q   $ 24��6�   $ Usuarios��6�   $��Center=�6�   5K      �� 5L      -
   5        $	 
btnClients��5L      #���  61   .��5M        $
 MenuItem��6(   # �  6=     clients# �  6q   $ 24��6�   $ Clientes��6�   $��Center=�6�   5N      �� 5O      -   5        $	 btnSettings��5O      #���  62   .��5P        $
 MenuItem��6(   # �  6=     settings# �  6q   $ 24��6�   $ Configuracion��6�   $��Center=�6�   5Q      5S      � 5X      -   5         �Name$ Popup��5X       � AllowsTransparency .  � HorizontalOffset$ 0��6C    � VerticalOffset$ 10��6X   .��5Y      $   ExpressoWPF.Controls.UserPopup  -   5          Name$ Header��5Y   %   #���  65   5Z      5]      5^      � 5`   
   $��1��6   $��2��6    5a      �� 5b      $��70 �6    �� 5c      $��* �6    .+�5f      d� -   5         d�Name$ Tg_Btn��5f      $��1��6+   $��Center=�6;     iToggleButtonStyle#���  6V   $
��Left=�6�   $��0q�6�   
������ $��
10,15,0,15q�6�   $��30��6�   $��30��6�    � Triggers 5g      :� 5h       :� RoutedEvent$ ToggleButton.Unchecked��6#   .[�5i      �� .��5j      �� -   5         ��Name$ HideStackPanel��5j   )   .��5k   "   W�  �� 
TargetName$ GridNav��62    �� TargetProperty$ Width�6R    g� 	BeginTime$ 0:0:0X�6t    W� From$	 210X�6�   	 W� To$ 60X�6�    g� Duration$ 0:0:0.5D�6�   5l   #   5m      5n      5o      :� 5p      $ ToggleButton.Checked��6#   .[�5q      �� .��5r      �� -   5        $ ShowStackPanel��5r   )   .��5s   "   W� $ GridNav��62   $ Width�6R   $ 0:0:0X�6t   $ 60X�6�   $	 210X�6�   $ 0:0:0.5D�6�   5t   #   5u      5v      5w      5y      �� 5|      $��0��6   $��Right=�6&   $	��Top=�6B   $��0,5,10,0q�6Z   $��30��6l    �� Orientation$ 
Horizontal=�6x   $��RightToLeft=�6�   $��2��6�   .+�5}      �� -   5        $	 btnClose��5}        IconButtonsStyle#��[ 6+    	 close#���  6U   
 � Tag$ IsCloseButton��6�   �� 5~      -   5        $	 btnMinimize��5~      #���  6.     minimize#���  6Y     � Cursor$
  Hand��6�   5       � 5�      -   5        $��
fContainer��5�      $��1��5�      $��20q�5�      !  � NavigationUIVisibility$! Hidden=�5�      5�      � 5�      $	��300��6   $��0 10 0 0q�6     5�      �� 5�      $��1* �6'   �� 5�      $��4* �6'   .+�5�      �� " �� CornerRadius$" 50��6   $��5q�6+   $��0��66   ��5�      �� 5�      -   5        
profileImg��$ UniformToFill=�5�   9   5�      � 5�      $��1��6   $��10 0 0 0q�6'    5�      �� 5�      $��30 �6(   �� 5�      .+�5�      �� -   5        # ��Name$# txtRole��5�       $��0��61   $��Administrador��6>   $��Center=�6S   #���  6n   $��20(�6�   �� 5�      -   5        $# txtName��5�       $��1��61   $��Santiago Sarabia Sainz��6>   $��Center=�6\   #���  6w   $��20(�6�   5�      5�      5�      5�      5�      