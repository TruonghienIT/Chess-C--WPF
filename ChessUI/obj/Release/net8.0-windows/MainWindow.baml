   M S B A M L   `   `   `  ����   ChessUI    ChessUI.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  GAME CỜ VUA��$
��1120��$	��700�� � WindowState$ Normal=� � Icon$ Assets/icon.ico�� � WindowStartupLocation$ CenterScreen=�
�������.���  � ColumnDefinitions �� $��2* ��� $��6* ��� $��2* �.+��� $��0��
���   �$��3q� �� CornerRadius$ 10�����#6B4226 �� Padding$ 10q�$��5q�._�]� .+��� $	��Top=� �� Orientation$ Vertical=�$��Center=�.+��� $ 
Horizontal=�$��0,0,0,10q�.+�(� $��80��$��80��.���� $��Assets/player2.png���� -    ��Name$ BlackPlayerName��$��22(�$
��Bold"�
�������$��5,0,0,0q�$��Center=��� $ 
Horizontal=�$��0,0,0,20q�$��Center=�$��Center=�.+�(� $��24��$��24��.���� $��Assets/hourglass.png���� $��26(�$
��Bold"�
�������$��5,0,0,0q��� -   	 ��Name$	 BlackTimerRun��$�10:00���� -   
 ��Name$
 BlackCheckWarning��$��CHIẾU TƯỚNG!��
���  ��$
��Bold"�$��18(�$��Center=�
������ $��0q� �� 
IsReadOnly$
 True��$y�	Collapsed=�$��0,0,0,20q�(�  (� Stretch$ Uniform=�$��Bottom=�.��A�  A� Columns$ 2��$	��150��.+��� -   $-��'pack://application:,,,/Assets/happy.png��$��60��$��60��
 � Tag$ happy��$��5q��� -   $+��%pack://application:,,,/Assets/sad.png��$��60��$��60��$	 sad��$��5q��� -   $-��'pack://application:,,,/Assets/angry.png��$��60��$��60��$ angry��$��5q��� -   $.��(pack://application:,,,/Assets/crying.png��$��60��$��60��$	 cry��$��5q��� -	   $1��+pack://application:,,,/Assets/surprised.png��$��60��$��60��$ 	surprised��$��5q��� -
   $,��&pack://application:,,,/Assets/love.png��$��60��$��60��$
 love��$��5q��� -   $0��*pack://application:,,,/Assets/thinking.png��$��60��$��60��$ thinking��$��5q��� -   $0��*pack://application:,,,/Assets/laughing.png��$��60��$��60��$ laugh��$��5q��� -   $,��&pack://application:,,,/Assets/cool.png��$��60��$��60��$ smirk��$��5q��� -   $0��*pack://application:,,,/Assets/sleeping.png��$��60��$��60��$ confused��$��5q�(� $��1��$ Uniform=�.��� -    �Name$ 	BoardGrid��$	��600��$	��600������  �� ImageSource$ Assets/Board.png�� j� Stretch$
 Fill=�.+�A� -    A�Name$ HighlightGrid�� A� Rows$ 8��$ 8��A� -   $ 	PieceGrid��$ 8��$ 8�� �� BitmapScalingMode$ HighQuality=��� -    ��Name$ MenuContainer���� $��2��
���   �$��3q�$ 10�����	#FFEDE0C8$ 10q�$��5q�._�]� .+�(� $ Uniform=�$	��Top=�.��A� $ 2��$	��150��.+��� -   $-��'pack://application:,,,/Assets/happy.png��$��60��$��60��$ happy��$��5q��� -   $+��%pack://application:,,,/Assets/sad.png��$��60��$��60��$	 sad��$��5q��� -   $-��'pack://application:,,,/Assets/angry.png��$��60��$��60��$ angry��$��5q��� -   $.��(pack://application:,,,/Assets/crying.png��$��60��$��60��$	 cry��$��5q��� -   $1��+pack://application:,,,/Assets/surprised.png��$��60��$��60��$ 	surprised��$��5q��� -   $,��&pack://application:,,,/Assets/love.png��$��60��$��60��$
 love��$��5q��� -   $0��*pack://application:,,,/Assets/thinking.png��$��60��$��60��$ thinking��$��5q��� -   $0��*pack://application:,,,/Assets/laughing.png��$��60��$��60��$ laugh��$��5q��� -   $,��&pack://application:,,,/Assets/cool.png��$��60��$��60��$ smirk��$��5q��� -   $0��*pack://application:,,,/Assets/sleeping.png��$��60��$��60��$ confused��$��5q��� $��Bottom=�$ Vertical=�$��Center=�.+��� $ 
Horizontal=�$��	0,20,0,10q�.+�(� $��80��$��80��.���� $��Assets/player1.png���� -   $ WhitePlayerName��$��22(�$
��Bold"�
���   �$��5,0,0,0q�$��Center=��� $ 
Horizontal=�$��0,0,0,20q�$��Center=�$��Center=�.+�(� $��24��$��24��.���� $��Assets/hourglass.png���� $��26(�$
��Bold"�
���   �$��5,0,0,0q��� -   $	 WhiteTimerRun��$�10:00���� -   $
 WhiteCheckWarning��$��CHIẾU TƯỚNG!��
���  ��$
��Bold"�$��18(�$��Center=�
������ $��0q�$
 True��$y�	Collapsed=�$��0,0,0,20q�