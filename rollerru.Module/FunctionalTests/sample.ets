;#DropDB rollerruEasyTest

#Application rollerruWin
#Application rollerruWeb

*FillForm
 User Name = Admin
*Action Log In

#IfDef rollerruWin

*Action About...
 
*Action OK

#EndIf

#IfDef rollerruWeb
*Action Navigation(My Details)
#EndIf
