Imports QtSharpHelper.Qt
Imports QtWidgets

' Note make sure the build type of the application is set to x86 (32bit) not Any CPU

Public Class Program
    Public Shared QApp As QApp

    Public Shared Sub Main()
        ' Create a new QApplication object
        QApp = QApp.CreateQApp()

        ' Create a Basic Widget
        Dim widg1 As New QWidget()
        widg1.Resize(250, 150)
        widg1.WindowTitle = "Simple example"
        widg1.Show()

        QApplication.Exec()
    End Sub
End Class
