Imports QtCore
Imports QtQml
Imports QtSharpHelper.Qt
Imports QtWidgets

Public Class Program

    ' Using the code from the QtQuick Gallery example included with Qt Creator / Q 5.5.1 as a basis
    ' Simple example to load a qml file

    Public Shared QApp As QApp
    Public Shared QmlEngine As QQmlApplicationEngine

    Public Shared Sub Main()
        ' Create a new QApplication object
        QApp = QApp.CreateQApp()

        ' File Access - System Access Violation Error
        'QmlEngine = New QQmlApplicationEngine("main.qml")
        'QmlEngine = New QQmlApplicationEngine("D:\SourceControl\GitRepos\QtSharp.TestApps\TestApps\QtQuick\BasicLayouts1_VB\main.qml")

        ' 2nd form of File Access
        'QmlEngine = New QQmlApplicationEngine()
        'QmlEngine.Load("main.qml") ' Fails

        ' Url Access - System Access Violation Error
        'Dim url1 As QUrl = QUrl.FromLocalFile("main.qml")
        'Dim url1 As QUrl = QUrl.FromLocalFile("D:\SourceControl\GitRepos\QtSharp.TestApps\TestApps\QtQuick\BasicLayouts1_VB\main.qml")
        'QmlEngine = New QQmlApplicationEngine(url1)

        ' Try LoadData instead - still fails
        'Dim qmltxt As String = System.IO.File.ReadAllText("main.qml")
        'Dim qarr As New QByteArray(qmltxt, Len(qmltxt))
        'QmlEngine = New QQmlApplicationEngine()
        'QmlEngine.LoadData(qarr)

        ' Try QResource method
        Debug.WriteLine(QResource.RegisterResource("qresource.rcc"))
        Dim url1 As new QUrl("qrc:/main.qml")
        QmlEngine = New QQmlApplicationEngine(url1)

        ' Run the QApplication Process
        QApplication.Exec()

    End Sub

End Class
