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
    Public Shared QV As QQuickView
    Public Shared Comp As QQmlComponent
    Public Shared CompObj As QObject

    Public Shared Sub Main()

        ' http://doc.qt.io/qt-5/qtqml-cppintegration-interactqmlfromcpp.html

        QGuiApp = QGuiApp.CreateQGuiApp()

        LoadQml()

        ' Run the QApplication Process
        QGuiApplication.Exec()

    End Sub

    Public Shared Sub LoadQml()

        ' These work fine

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
        'QV = New QQuickView(QmlEngine, Nothing)
        'QV.Source = QUrl.FromLocalFile("main.qml")
        ''quickView.Title = "test title"
        'QV.Show()

        ' This seems to bomb out horribly
        ' QmlEngine = New QQmlApplicationEngine
        'Comp = New QQmlComponent(QmlEngine, QUrl.FromLocalFile("main.qml"))
        'CompObj = Comp.Create()

        ' http://stackoverflow.com/questions/23936169/whats-the-difference-between-qquickview-and-qquickwindow
        'QmlEngine = New QQmlApplicationEngine()
        'QmlEngine.Load("main.qml")
        ' To access the QQuickView inside QmlEngine we need to access the rootObjects()
        ' rootObjects appears to be a function which returns a QList of pointers to QObject, not visible because it's templated
    End Sub

End Class
