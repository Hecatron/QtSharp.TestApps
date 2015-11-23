Imports QtSharpHelper.Qt
Imports QtWidgets

' Note make sure the build type of the application is set to x86 (32bit) not Any CPU

Public Class Program

    Public Shared QApp As QApp

    Public Shared Dia1 As LayoutExample

    Public Shared Sub Main()
        ' Create a new QApplication object
        QApp = QApp.CreateQApp()

        ' Show the Dialog
        Dia1 = New LayoutExample
        Dia1.Show()

        QApplication.Exec()

    End Sub

End Class
