Imports QtCore
Imports QtGui
Imports QtQml
Imports QtQuick
Imports QtSharpHelper.Qt
Imports QtWidgets

Public Class Program

    ' Using the code from the QtQuick Gallery example included with Qt Creator / Q 5.5.1 as a basis
    ' Simple example to load a qml file

    Public Shared QApp As QApp
    Public Shared QGuiApp As QGuiApp
    Public Shared QmlEngine As QQmlApplicationEngine

    Public Shared Sub Main()

        QApp = QApp.CreateQApp()
        'QGuiApp = QGuiApp.CreateQGuiApp()

        Dim quickView As New QQuickView()
        quickView.Source = QUrl.FromLocalFile("main.qml")
        quickView.Show()

        quickView.

        ' Run the QApplication Process
        QApplication.Exec()

    End Sub

End Class
