   M S B A M L   `   `   `  ����F  BExpressoWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ExpressoWPFExpressoWPF  5   	       ExpressoWPF.Login   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:ExpressoWPF     5   	   _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Login��5   	   $	��500��6   $	��900��6$    � WindowStartupLocation$ CenterScreen=�5	   	    � WindowStyle$
 None=�6.   
������  � 
ResizeMode$ NoResize=�5   	    � AllowsTransparency . $��SF Pro Display)�5   	   .��5      ��    SecundaryBackgroundColor#���   6    �� CornerRadius$ 8��6E   ._�5   
   �  � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��0* �6#   .+�5      ��   PrimaryBackgroundColor#���  6   $ 8 0 0 8��6K   $��0��6b   $��	0,0,355,0q�6o   ._�5      � .+�5      �� $��
0,100,0,30q�5      $��Center=�5      $	��524��6<   ��5      �� 5       �� ImageSource$  Resources/Images/Login.png��6)    j� Stretch$ Uniform=�6R   5      �� 5       $��Center=�6    $��0,29,0,0q�6=   $
��Wrap=�6O   $��E X P R E S S O ��6c   $	��Top=�6{   $��40(�6�     PrimaryTextColor#���  6�   $��Monument Extended)�6�   5!      5"      � 5$      $��	545,0,0,0q�6   .+�5%      �� $��0��6   $��Right=�6*   $	��Top=�6F   $��0,5,0,0q�6^   $��30��6o   	 �� Orientation$	 
Horizontal=�6{   $��RightToLeft=�6�   $��2��6�   .+�5&      �� -   5        
 ��Name$
 btnClose��5&        IconButtonsStyle#��[ 6/    	 close#���  6Y   
 � Tag$ IsCloseButton��6�   �� 5'      -   5        $
 btnMinimize��5'      #���  62     minimize#���  6]    � Cursor$
 Hand��6�   5(      �� 5)      $��Center=�6   $��0,77,0,0q�69   $
��Wrap=�6K   $��I N I C I O��6_   $	��Top=�6r   $��20(�6�     AccentColor#���  6�   $��Monument Extended)�6�   �� 5*      -   5         ��Name$ txtUserName��5*      $��Center=�6/   $��	0,187,0,0q�6L    �� TextWrapping$
 Wrap=�6_   $	��Top=�6s   $	��204��6�   $��31��6�   #���  6�   #���  6�     TextFourthColor �� SelectionBrush# �  6  $ Arrow��6C  ��6�   Z� 6C  �� 5+      $��Center=�6   $��	0,149,0,0q�69   $
��Wrap=�6L   $��U S U A R I O��6`   $	��Top=�6u   #���  6�   $��SF Pro Display)�6�   G� 5,      -   5         G�Name$ txtPassword��5,      $��Center=�63   $��	0,303,0,0q�6P   $	��Top=�6c   $	��204��6{   $��31��6�   #���  6�   #���  6�    G� SelectionBrush# �  6   ��6�   Z� 6   �� 5-      $��Center=�6   $��	0,263,0,0q�69   $
��Wrap=�6L   $	��Top=�6`   #���  6x   $��SF Pro Display)�6�   �� 6�   $�C O N T R A S E��6�   �� 6�    � Language$ es-es�6�   $� Ñ A��6�   6  �� 5.      -   5        $
 btnLogin��5.      $��Iniciar Sesion��6+   $��Center=�6D   $��	0,380,0,0q�6a   $	��Top=�6t   $��31��6�   $	��204��6�   #���  6�   #���  6�   $
 Hand��6-  ��6�   Z� 6-  �� 5/      -   5        $
 btnReset��5/      $y�	Collapsed=�6+   $��Olvide Mi Contraseña��6B   $��Center=�6a   $��	0,430,0,0q�6~   $	��Top=�6�   $��31��6�   $	��204��6�   #���  6�   #���  6  $
 Hand��6J  ��6�   Z� 6J  51      52      55      57      