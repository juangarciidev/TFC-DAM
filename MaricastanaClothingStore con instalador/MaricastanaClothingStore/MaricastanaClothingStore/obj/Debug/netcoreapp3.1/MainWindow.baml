   M S B A M L   `   `   `  ����S  OMaricastanaClothingStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:MaricastanaClothingStoreMaricastanaClothingStore  5   	   Q-clr-namespace:MaricastanaClothingStore.VISTASMaricastanaClothingStore.VISTAS  5   	   )    #MaricastanaClothingStore.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:MaricastanaClothingStore     5   	   D
viewmodels-clr-namespace:MaricastanaClothingStore.VISTAS     5   	   -   5        $	��600��5	   	   $
��1000��5
   	     � WindowStartupLocation$  CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency$
 true��5   	   
������  � 	Resources 5       	  Focus �� Property �� Value �� StrokeDashArray J� SnapsToDevicePixels  Button.Static.Background �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground 
 BtnEncabezadoEstilo	 �� 
TargetType
 �� 
TargetType ��Name �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName  FocusVisual  Button.Static.Background1  Button.Static.Border1   Button.MouseOver.Background1  Button.MouseOver.Border1  Button.Pressed.Background1  Button.Pressed.Border1  Button.Disabled.Background1  Button.Disabled.Border1  Button.Disabled.Foreground1  BtnMostrarOcultar  FocusVisual1  Button.Static.Background2  Button.Static.Border2   Button.MouseOver.Background2  Button.MouseOver.Border2  Button.Pressed.Background2  Button.Pressed.Border2  Button.Disabled.Background2  Button.Disabled.Border2  Button.Disabled.Foreground2   BtnListaEstilo ! FocusVisual2 " Item.MouseOver.Background # Item.MouseOver.Border "$ Item.SelectedActive.Background % Item.SelectedActive.Border $&  Item.SelectedInactive.Background  ' Item.SelectedInactive.Border ( ListViewIEstilo �� RelativeSource �� AncestorType �� Padding ]� 
Conditions �� Property �� Value �� DataType.   (MaricastanaClothingStore.VISTAS.Usuarios � DataContext��%�  &	        &	 �     &	 �     &	 �     &	     &	 ?    &	 b    &	 �    &	 �    &		 �    &	
 �    7   7  7  7  7  7 	 &	     &	 �    &	 �    &	     &	 9    &	 \    &	     &	 �    &	 �    &	 �    &	 	    7  7  7  7  &	 �    &	 �    &	 �    &	 �    &	 �    &	      &	 C    &	 f    &	 �    &	 �    &	  �    7  7  7  7  7  7  &	! �    &	" �    &	# �    &	$ �    &	%     &	& (    &	' K    &	( n    7 ! (�� L    5�      *, +)�� 5   
   .�5      ��  � �� 5      �� 5      .R�5      �� $��2q�6$   $	 1 2R�6/   #��� ��6E   $
 true��6�   $��1��6�   5      5      5      �� 5   
   $ 	#FFDDDDDD��6;   �� 5   
   $ 	#FF707070��67   �� 5   
   $ 	#FFBEE6FD��6>   �� 5   
   $ 	#FF3C7FB1��6:   �� 5   
   $ 	#FFC4E5F6��6<   �� 5    
   $ 	#FF2C628B��68   �� 5!   
   $ 	#FFF4F4F4��6=   �� 5"   
   $ 	#FFADB2B5��69   �� 5#   
   $ 	#FF838383��6=   �� 5$   
   "	 ��6,   .�5%      ��  �@�FocusVisualStyle8   61   �� 5&       � ��8  6+   �� 5'       � ��8  6,   �� 5(       � ��# � ��6+   �� 5)       � ��$ 1q�60   �� 5*       � ��$ Center=�6;   �� 5+       � ��$ Center=�69   �� 5,       � ��$ 1q�6(   �� 5-       � �� 5.      �� 5/      "
 ��6&   .R�50      �� $ border��62   $ 9��6!   #��z��6B   #��z��6l   #��z��6�   $
 true��6�   ._�51      �� $ contentPresenter��6/   $|�False��6I   #��z��6[   #��z��6�   $
��True��6�   # z 6�   #��z��6  52       53      P� 54       �@��IsDefaulted$
 true��6=   . �55   "   �� $ border��6@   # � ��6T    � ��56      P� 57       �@J�IsMouseOver$
 true��6=   . �58   "   �� $ border��6?    � �� �#d1b7ad6S   �� 59   "   $ border��6@    � �� �#d1b7ad6T   5:      P� 5;       � ��$
 true��6;   . �5<   "   �� $ border��6?    � ��
 ���� 6S   �� 5=   "   $ border��6@    � ��
 ���� 6T   5>      P� 5?       � {�$ false��6;   . �5@   "   �� $ border��6?   8   � ��6S   �� 5A   "   $ border��6@   8   � ��6T   �� 5B   "   $ contentPresenter��6K   8   � ��6i   5C      5E      5G      5H      �� 5K   
   .�5L      ��  � �� 5M      �� 5N      .R�5O      �� $��2q�6$   $	 1 2R�6/   #��� ��6E   $
 true��6�   $��1��6�   5P      5R      5S      �� 5T   
   $ 	#FFDDDDDD��6<   �� 5U   
   $ 	#FF707070��68   �� 5V   
   $ 	#FFBEE6FD��6?   �� 5W   
   $ 	#FF3C7FB1��6;   �� 5X   
   $ 	#FFC4E5F6��6=   �� 5Y   
   $ 	#FF2C628B��69   �� 5Z   
   $ 	#FFF4F4F4��6>   �� 5[   
   $ 	#FFADB2B5��6:   �� 5\   
   $ 	#FF838383��6>   �� 5]   
   "	 d�6*   .�5^      ��  �@�FocusVisualStyle8   61   �� 5_       � ��
 ���� 6+   �� 5`       � ��
 ���� 6,   �� 5a       � ��# � ��6+   �� 5b       � ��$ 1q�60   �� 5c       � ��$ Center=�6;   �� 5d       � ��$ Center=�69   �� 5e       � ��$ 1q�6(   �� 5f       � �� 5g      �� 5h      "
 d�6&   .R�5i      �� $ border��6!   #��z��61   #��z��6[   #��z��6�   $
 true��6�   ._�5j      �� $ contentPresenter��6/   $|�False��6I   #��z��6[   #��z��6�   $
��True��6�   # z 6�   #��z��6  5k       5l      P� 5m       �@��IsDefaulted$
 true��6D   . �5n   "   �� $ border��6@   # � ��6T    � ��5o      P� 5p       � {�$ false��6;   . �5q   "   �� $ border��6?   8   � ��6S   �� 5r   "   $ border��6@   8   � ��6T   �� 5s   "   $ contentPresenter��6K   8   � ��6i   5t      5v      5x      5y      �� 5}   
   .�5~      ��  � �� 5      �� 5�      .R�5�      �� $��2q�6$   $	 1 2R�6/   #��� ��6E   $
 true��6�   $��1��6�   5�      5�      5�      �� 5�   
   $ 	#FFDDDDDD��6<   �� 5�   
   $ 	#FF707070��68   �� 5�   
   $ 	#FFBEE6FD��6?   �� 5�   
   $ 	#FF3C7FB1��6;   �� 5�   
   $ 	#FFC4E5F6��6=   �� 5�   
   $ 	#FF2C628B��69   �� 5�   
   $ 	#FFF4F4F4��6>   �� 5�   
   $ 	#FFADB2B5��6:   �� 5�   
   $ 	#FF838383��6>   �� 5�   
   "	 ��6'   .�5�      ��  �@�FocusVisualStyle8   61   �� 5�       � ��8  6+   �� 5�       � ��8  6,   �� 5�       � ��# � ��6+   �� 5�       � ��$ 1q�60   �� 5�       � ��$ Center=�6;   �� 5�       � ��$ Center=�69   �� 5�       � ��$ 1q�6(   �� 5�       � �� 5�      �� 5�      "
 ��6&   .R�5�      �� $ border��6!   #��z��61   #��z��6[   #��z��6�   $
 true��6�   ._�5�      �� $ contentPresenter��6/   $|�False��6I   #��z��6[   #��z��6�   $
��True��6�   # z 6�   #��z��6  5�       5�      P� 5�       �@��IsDefaulted$
 true��6=   . �5�   "   �� $ border��6@   # � ��6T    � ��5�      P� 5�       �@J�IsMouseOver$
 true��6=   . �5�   "   �� $ border��6?    � ��
 ���� 6S   �� 5�   "   $ border��6@    � ��
 ���� 6T   5�      P� 5�       � ��$
 true��6;   . �5�   "   �� $ border��6?    � ��
 �����6S   �� 5�   "   $ border��6@    � ��
 �����6T   5�      P� 5�       � {�$ false��6;   . �5�   "   �� $ border��6?   8   � ��6S   �� 5�   "   $ border��6@   8   � ��6T   �� 5�   "   $ contentPresenter��6K   8   � ��6i   5�      5�      5�      5�      �� 5�   
   .�5�      ��  � �� 5�      �� 5�      .R�5�      �� $��2q�6$   $	 1 2R�6/   #��� ��6E   $
 true��6�   $��1��6�   5�      5�      5�      �� 5�   
   $ 	#1F26A0DA��6<   �� 5�   
   $ 	#a826A0Da��68   �� 5�   
   $ 	#3D26A0DA��6A   �� 5�   
   $ 	#FF26A0DA��6=   �� 5�   
   $ 	#3DDADADA��6C   �� 5�   
   $ 	#FFDADADA��6?   �� 5�   
   "	 ��6(   .�5�      ��  �@J�SnapsToDevicePixels$
 True��64   �� 5�       � ��$	 4,1q�6(   �� 5�       � �� 6;   �� *HorizontalContentAlignment+ �� " ���� 5�       � �� 69   �� *VerticalContentAlignment+ �� " ���� 5�       � ��
 ���� 6+   �� 5�       � ��
 ���� 6,   �� 5�       � ��$ 1q�60   �� 5�       �@�FocusVisualStyle8   61   �� 5�       � �� 5�      �� 5�      "
 ��6&   .R�5�      �� $ Bd��6!   #��z��6-   #��z��6W   #��z��6�   # z��6�   $
 true��6�   ._�5�      �� #��z��6/   # z 6r   #��z��6�   5�       5�      ]� 5�       5�   "   �� 5�   &    �@J�IsMouseOver$
 True��6G   ./�5�   "   �� $ Bd��6?    � ��
 �����6O   �� 5�   "   $ Bd��6@    � ��
 �����6P   5�      P� 5�       � {�$ False��6;   . �5�   "   �� $ Bd��6K   # � ��6[    � ��5�      5�      5�      5�      �� 5�   
   Usuarios��"  5�      .R�5�         6"   �� 5�      5�      .��5�      �� $ 10��6   
�������._�5�   
   � -   5        $��	Principal��5�      
������  � ColumnDefinitions 5�      �� 5�      $
��203* �6#   �� 5�      $
��797* �6#   .+�5�      � -   5        $��
Encabezado��5�      $��35��5�      $	��Top=�5�      
������ $��2��5�      .+�5�      �� $ 	10,10,0,0��6    J� Opacity$ .9Q�5�      ���#715a525�      � 5�       5      �� 5     �� 5     $	��200 �6+   �� 5     $	��150 �6+   .+�5     �� $!��Maricastaña Clothing Store��6    $��0��5  !   
���   �$��16(�5	  !   $��10,0,0,0q�5
  !   $
��Left=�5  !   $��Center=�5  !   $��SemiBold"�6<   �� 5     -   5         ��Name$ 	BtnCuenta��5  K   #��� 
 6   
������ 
������ $��15(�5     
���   �$��Center=�5     $
��Left=�5      � Cursor$
 Hand��5     $��1��6+   $��SemiBold"�6;   $��1��6i   .��5     �  5     �� 5  "   $��15 �63   �� 5  "   .+�5     �� $��1��6(   $��	Mi Cuenta��5  (   $��Center=�5  (   $��Center=�5  (   $��5,0,0,0q�5   (   �� 5!     $��0��6$   $��Center=�5"  $   $��Center=�5#  $   $��/IMAGENES/MiCuenta.png��5$  $   5%     5&     �� 5)     -   5        $ BtnAyuda��5)  K   #��� 
 6   $��1��5*     
������ 
������ 
���   �$��15(�5.     $��Center=�5/     $
��Left=�50     $
 Hand��51     $��SemiBold"�6+   $��	133,0,0,0q�6A   $��2��6k   .��52     �  53     �� 54  "   $��15 �63   �� 55  "   .+�57     �� $��/IMAGENES/ayuda.png��6$   $��0��58  $   $��Center=�59  $   �� 5:     $��Ayuda��6(   $��1��5;  (   $��Center=�5<  (   $��Center=�5=  (   $��5,0,0,0q�5>  (   5?     5@     �� 5C     -   5        $ BtnMinimizar��5C  K   #��� 
 6   $��2��5D     
������ 
������ $��25��5G     $��25��5H     $��Right=�5I     $��Center=�5J     $��0,0,65,0q�5K     $��3��6J   .��5L     �� $��/IMAGENES/Minimizar.png��6    $
 Hand��5M     $
��Fill=�6+   $��14��6:   5N     �� 5Q     -   5        $ BtnMaximizar��5Q  K   #��� 
 6   $��2��5R     
������ 
������ $��25��5U     $��25��5V     $��Right=�5W     $	��top=�5X     $��0,5,35,0q�5Y     $��4��6J   .��5Z     �� $��/IMAGENES/Maximizar.png��6    $
 Hand��5[     $
��Fill=�6+   5\     �� 5_     -   5        $ 	BtnCerrar��5_  K   #��� 
 6   $��2��5`     
������ 
������ $��25��5c     $
��Left=�5d     $��	120,5,0,5q�5e     $��5��6H   .��5f     �� $��/IMAGENES/Salir.png��6    $
 Hand��5g     $
��Fill=�6+   5h     5j     5k     � 5n     -	   5         �Name$ GridContenido��5n     
������ $��	75,35,0,0q�5q     $��2��5r      5s     �� 5t     $��* �6'   .+�5v     �� $ .9Q�6   ���#d1b7ad$��0��5w     �� 5x     ��6!   �� 5y     � 5|     -
   5        $ GridMenu��5|     
������ $��75��5~     $
��Left=�5     $��0,35,0,0q�5�     .+�5�     �� ���#a0877e$ .9Q�5�     $ 0,0,0,10��5�     $��2��5�     �� 5�     
������ $	��Top=�5�     .+�5�     � 
������ $	��Top=�5�     $��Right=�5�     $��	0,10,10,0q�5�     .+�5�     d� -   5         d�Name$ BtnMostrarOcultar��5�  '   $��30��5�  '   $��30��5�  '   $
 Hand��5�  '   
������ #���  5�  '   $��6��6S   ��5�     �� 5�  "     �� ImageSource$  /IMAGENES/Menu.png��6-   ! � Triggers! 5�     :� 5�  "   " :� RoutedEvent$" ToggleButton.Unchecked��6/   .[�5�  &   �� .��5�  *   �� -   5        # ��Name$# OcultarPanel��5�  5   .��5�  .   W� $ �� 
TargetName$$ GridMenu��6>   % �� TargetProperty$% Width�5�  >   & g� 	BeginTime	& 0:0:05�  >   ' W� From' 2005�  >   	( W� To( 756I   ) g� Duration$) 	0:0:0:0.3D�5�  >   5�  +   5�  '   5�  #   :� 5�  "   $" ToggleButton.Checked��6/   .[�5�  &   �� .��5�  *   �� -   5        $# MostrarPanel��5�  5   .��5�  .   W� $$ GridMenu��6>   $% Width�5�  >   	& 0:0:05�  >   ' 755�  >   ( 2006H   $) 	0:0:0:0.3D�5�  >   5�  +   5�  '   5�  #   5�     5�     �� 5�     -   5        * ��Name$* lvMenu��5�     
������ 
������ $��0,30,0,0q�5�     $��Disabled=�5�     .@�5�     �� #��� ( 6'   $��70��6Q   .��5�     �� -   5        $ 	BtnInicio��5�  N   #���   6%   $��70��5�  %   $	��200��5�  %   $
��Left=�5�  %   $
 Hand��5�  %   
������ 
������ $��7��5�  W   .��5�  "   � $	��200��6'    5�  &   �� 5�  *   $��60 �6;   �� 5�  *   .+�5�  &   �� $��50��6,   $��0��5�  -   $��Center=�5�  -   $��/IMAGENES/Inicio.png��5�  -   �� 5�  &   $��1��60   $��Inicio��5�  0   $��SemiBold"�5�  0   $��14(�5�  0   $��Center=�5�  0   $��Center=�5�  0   5�  #   5�     5�     �� 5�     #��� ( 6'   $��70��6Q   .��5�     �� -   5        $ BtnPuntoDeVentas��5�  N   #���   6%   $��70��5�  %   $	��200��5�  %   $
��Left=�5�  %   $
 Hand��5�  %   
������ 
������ $��8��5�  ^   .��5�  "   � $	��200��6'    5�  &   �� 5�  *   $��60 �6;   �� 5�  *   .+�5�  &   �� $��50��6,   $��0��5�  -   $��Center=�5�  -   $!��/IMAGENES/PuntoDeVentas.png��5�  -   �� 5�  &   $��1��60   $��Punto de Ventas��5�  0   $��SemiBold"�5�  0   $��14(�5�  0   $��Center=�5�  0   $��Center=�5�  0   5�  #   5�     5�     �� 5�     #��� ( 6'   $��70��6Q   .��5�     �� -   5        $ BtnArticulos��5�  N   #���   6%   $��70��5�  %   $	��200��5�  %   $
��Left=�5�  %   $
 Hand��5�  %   
������ 
������ $��9��5�  Z   .��5�  "   � $	��200��6'    5�  &   �� 5�  *   $��60 �6;   �� 5   *   .+�5  &   �� $��50��6,   $��Center=�5  -   $��/IMAGENES/Articulos.png��5  -   $��40��6N   $	��Top=�6Z   �� 5  &   $��1��60   $��
Artículos��5  0   $��SemiBold"�5  0   $��14(�5  0   $��Center=�5	  0   $��Center=�5
  0   5  #   5     5     �� 5     #��� ( 6'   $��70��6Q   .��5     �� -   5        $ BtnUsuarios��5  N   #���   6%   $��70��5  %   $	��200��5  %   $
��Left=�5  %   $
 Hand��5  %   
������ 
������ $��10��5  Y   .��5  "   � $	��200��6'    5  &   �� 5  *   $��60 �6;   �� 5  *   .+�5  &   �� $��50��6,   $��0��5  -   $��Center=�5  -   $��/IMAGENES/Usuarios.png��5   -   �� 5!  &   $��1��60   $��Usuarios��5"  0   $��SemiBold"�5#  0   $��14(�5$  0   $��Center=�5%  0   $��Center=�5&  0   5'  #   5(     5)     5+     5-     5/     51     52     53     