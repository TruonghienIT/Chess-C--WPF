   M S B A M L   `   `   `  ����   ChessUI    ChessUI.PlayerNameWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$   Nhập tên người chơi��$	��500��$	��500�� � Icon$ Assets/icon.ico�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=�
������  � 	Resources    UnderlineComboBoxStyle �� 
TargetType �� Property �� Value �� 
TargetType	 � ColumnDefinitions
 ��Name �� SelectionBoxItem �� SelectionBoxItemTemplate d�Name d� 	IsChecked �� Mode �� RelativeSource �� Stretch �Name �� IsDropDownOpen � AllowsTransparency �� CornerRadius  ComboBoxItemStyle �� Triggers P� Property P� Value  	TextStyle��%D  &	        &	 �    &	 �    �� " ��.���  � ��
 ���� ��  � ��
 �������  � ��$ 20(���  � ��$ 40����  � ��$ Center=���  � ��$ 0,0,0,2q���  � ��
 �������  � ��$ 0,0,0,5q���  � �� �� " ��.R�� 	 �� �� $��20 �.+��� $
 ContentSite��#��z #��z $��Center=�$
��Left=�$��0,0,5,0q��� $��2��$��2��#��z��$��Bottom=�d� $ ToggleButton��$��1��
������ $��0q�$|�false�� �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+.��F� "����        � 	=  	= � z     	
�������$ Uniform=�$��Center=�� $ Popup��$��Bottom=�#��z $
 True��$|�False��.���� 
����������	#FFABADB3$��1q�$ 4��._��� .���� �� " ��.���  � ��
 �   ���  � ��$ 18(���  � ��$
 10,5q���  � �� �� " ��.R��� 
������ ._���  P�  �@��IsHighlighted$
 True��. ���  � �� �#EEE�� " ��.���  � ��
 �������  � ��$
 Bold"���  �@��TextAlignment$ Center=���  � ��$ Center=�.���� $	��475��$	��450��$ 20��
�������$��2q�����  �� ImageSource$ Assets/avatar.jpg�� �� Opacity$	 0.7Q� j� Stretch$ UniformToFill=�._��  � RowDefinitions �� $
��Auto ��� $
��Auto �$��98.95���� $
��Auto �$��120.95���� $
��Auto �$��50���� .+��� $ ��Nhập tên người chơi��#��[ $��30(�$��47��$	��301���� $��1��$��	20,0,20,5q�.+��� $��Người chơi 1 (Đen):��#��[ $��20(��� -    ��Name$ BlackPlayerName��
�������$��74��$��45(� �� TextAlignment$ Center=�$��Center=�
������ .������  �� Color$ White��.����u�0,0,0,2q�.���� $��2��$��	20,0,20,5q�.+��� $!��Người chơi 2 (Trắng):��#��[ $��20(��� -   $ WhitePlayerName��
�������$��74��$��45(�$ Center=�$��Center=�
������ .������ $ White��.����u�0,0,0,2q�.���� $��3��$��	20,0,20,5q�  �� Orientation$  
Horizontal=�$��Center=�.+��� $$��Chọn thời gian thi đấu:��#��[ $��20(�$��0,0,10,0q��� -   ! ��Name$! GameTimeComboBox��#��[  #��[ " �� SelectedIndex$" 1��$��80��.@��� $��5��
# � Tag$# 5���� $��10��$# 10���� $��15��$# 15���� $��30��$# 30���� $��phút��#��[ $��20(�$��10,0,0,0q��� $��4��$  
Horizontal=�$��Center=�$��	0,10,0,10q�$	��320��.+��� -   $��XÁC NHẬN��  ButtonStyle#��[ $	��155��$��53��$��20(��� -   $��HỦY��#��[ $	��155��$��53��$��20(�$��10,0,0,0q�