Imports QtGui
Imports QtSharpHelper.Qt

' Note make sure the build type of the application is set to x86 (32bit) not Any CPU

Public Class Program

    Public Shared QGuiApp As QGuiApp

    Public Shared Sub Main()
        ' Create a new QApplication object
        QGuiApp = QGuiApp.CreateQGuiApp()

        Dim clock As New AnalogClockWindow()
        clock.Show()

        QGuiApplication.Exec()

    End Sub

End Class
