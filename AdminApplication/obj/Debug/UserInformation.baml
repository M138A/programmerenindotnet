   M S B A M L   `   `   `  ����K  GAdminApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:AdminApplicationAdminApplication  5   	   !    AdminApplication.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:AdminApplication     5   	     � 
ResizeMode$  NoResize=�5   	    � Title$ AdminApplication��5	   	   $	��602��6"   $	��800��6/   .��5
      � -   5        $��maingrid��5
      .+�5   
   �� -   5         ��Name$ UsersBox��5      $
��Left=�6$   $	��529��6?   $	��Top=�6L   $	��201��6d   $��0,40,0,0q�6p   6�   �� 5   
   -   5         ��Name$ label��5      $��Users��6   $
��Left=�6/   $��	67,-2,0,0q�6J   $	��Top=�6]   $��24(�6u    J� RenderTransformOrigin$ 0.625,0.487&�6�   �� 5   
   -   5        $ 
label_Copy��5      $��User Information��6$   $
��Left=�6?   $��	406,0,0,0q�6Z   $	��Top=�6m   $��24(�6�   $ 0.625,0.487&�6�   �� 5   
   
���   �$��1q�6%   $
��Left=�69   $	��529��6T   $��
201,40,0,0q�6a   $	��Top=�6u   $	��591��6�   ._�5      � .+�5      �� -   5        $ description��5      $;��5Select a user you want to know more about on the left��6-   $
��Left=�6m   $��
89,209,0,0q�6�   $	��Top=�6�   $��16(�6�   �� 5      -   5        $ status��5      $��Status:��6(   $
��Left=�6:   $��
181,29,0,0q�6U   $	��Top=�6i   $��16(�6�   $y�	Collapsed=�6�   �� 5      -   5        $
 name��5      $��Name:��6&   $
��Left=�66   $��181,100,0,0q�6Q   $	��Top=�6f   $��16(�6~   $y�	Collapsed=�6�   �� 5      -   5        $ email��5      $��Email:��6'   $
��Left=�68   $��186,136,0,0q�6S   $	��Top=�6h   $��16(�6�   $y�	Collapsed=�6�   �� 5      -	   5        $
 city��5      $��City:��6&   $
��Left=�66   $��197,172,0,0q�6Q   $	��Top=�6f   $��16(�6~   $y�	Collapsed=�6�   �� 5      -
   5        $ accountnumber��5      $��Account number:��6/   $
��Left=�6I   $��106,245,0,0q�6d   $	��Top=�6y   $��16(�6�   $y�	Collapsed=�6�   �� 5      -   5        $ balance��5      $��Balance:��6)   $
��Left=�6<   $��169,281,0,0q�6W   $	��Top=�6l   $��16(�6�   $y�	Collapsed=�6�   �� 5      -   5        $ 
statusData��5      $��placeholder��6,   $
��Left=�6B   $��
260,29,0,0q�6]   $	��Top=�6q   $��16(�6�   $y�	Collapsed=�6�   �� 5      -   5        $ nameData��5      $��placeholder��6*   $
��Left=�6@   $��260,100,0,0q�6[   $	��Top=�6p   $��16(�6�   $y�	Collapsed=�6�   �� 5      -   5        $ 	emailData��5      $��placeholder��6+   $
��Left=�6A   $��260,136,0,0q�6\   $	��Top=�6q   $��16(�6�   $y�	Collapsed=�6�   �� 5      -   5        $ cityData��5      $��placeholder��6*   $
��Left=�6@   $��260,172,0,0q�6[   $	��Top=�6p   $��16(�6�   $y�	Collapsed=�6�   �� 5      -   5        $ accountnumberData��5      $��placeholder��63   $
��Left=�6I   $��260,245,0,0q�6d   $	��Top=�6y   $��16(�6�   $y�	Collapsed=�6�   �� 5      -   5        $ balanceData��5      $��placeholder��6-   $
��Left=�6C   $��260,281,0,0q�6^   $	��Top=�6s   $��16(�6�   $y�	Collapsed=�6�   �� 5      -   5         ��Name$ TransactionButton��5      $��Transactions��64   $
��Left=�6K   $��227,380,0,0q�6f   $	��Top=�6{   $	��143��6�   $��16(�6�   $��39��6�   $y�	Collapsed=�6�   �� 5      -   5        $ DeleteButton��5      $��Delete Account��6/   $
��Left=�6H   $��227,441,0,0q�6c   $	��Top=�6x   $	��143��6�   $��16(�6�   $��39��6�   
����p��$y�	Collapsed=�6�   5      5       �� 5!   
   
���   �$��1q�6%   $
��Left=�69   $	��529��6T   $��
-61,40,0,0q�6a   $	��Top=�6u   $	��262��6�   5#      5$      