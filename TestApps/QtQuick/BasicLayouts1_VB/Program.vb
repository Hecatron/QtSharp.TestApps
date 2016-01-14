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

        'QApp = QApp.CreateQApp()
        QGuiApp = QGuiApp.CreateQGuiApp()

        ' 1. File Access
        QmlEngine = New QQmlApplicationEngine("main.qml")

        ' 2. File Access
        'QmlEngine = New QQmlApplicationEngine()
        'QmlEngine.Load("main.qml")

        ' 3. Url Access
        'Dim url1 As QUrl = QUrl.FromLocalFile("main.qml")
        'QmlEngine = New QQmlApplicationEngine(url1)

        ' 4. LoadData
        'Dim qmltxt As String = System.IO.File.ReadAllText("main.qml")
        'Dim qarr As New QByteArray(qmltxt, Len(qmltxt))
        'QmlEngine = New QQmlApplicationEngine()
        'QmlEngine.LoadData(qarr)

        ' This shows an empty form, perhaps it's supposed to
        'Dim quickView As New QQuickView()
        'quickView.Source = QUrl.FromLocalFile("main.qml")
        'quickView.Show()

        ' Run the QApplication Process
        QGuiApplication.Exec()

    End Sub

End Class
