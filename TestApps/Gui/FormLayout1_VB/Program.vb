Imports QtSharpHelper.Qt
Imports QtWidgets

' Note make sure the build type of the application is set to x86 (32bit) not Any CPU

Public Class Program
    Public Shared QApp As QApp

    Public Shared Sub Main()
        ' Create a new QApplication object
        QApp = QApp.CreateQApp()

        ' Create a basic widget
        Dim window1 As New QWidget()
        window1.WindowTitle = "Simple example2"

        Dim button1 As New QPushButton("One")
        Dim button2 As New QPushButton("Two")
        Dim button3 As New QPushButton("Three")
        Dim button4 As New QPushButton("Four")
        Dim button5 As New QPushButton("Five")

        AddHandler button1.Clicked, AddressOf Button1Clicked

        Dim layout As New QHBoxLayout()
        layout.AddWidget(button1)
        layout.AddWidget(button2)
        layout.AddWidget(button3)
        layout.AddWidget(button4)
        layout.AddWidget(button5)

        window1.Layout = layout
        window1.Show()

        QApplication.Exec()
    End Sub

    Public Shared Sub Button1Clicked(clicked As Boolean)
        Debug.WriteLine(clicked)
    End Sub
End Class
