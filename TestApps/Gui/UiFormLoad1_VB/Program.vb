Imports QtCore
Imports QtSharpHelper.Qt
Imports QtWidgets

Public Class Program

    Public Shared QApp As QApp

    Public Shared Sub Main()
        ' Create a new QApplication object
        QApp = QApp.CreateQApp()

        ' QtUiTools is a smaller runtime, but requires manual compilation of the dll on the end users machine
        ' Dim loader As New QtUiTools.QUiLoader()
        Dim loader As New QtDesigner.QFormBuilder()

        ' Load the widget from a ui file
        Dim file1 As New QFile("testform1.ui")
        file1.Open(QIODevice.OpenModeFlag.ReadOnly)
        ' error shows up in QtCore.OnEvent
        Dim widg1 As QWidget = loader.Load(file1)
        file1.Close()

        widg1.ObjectName = "Simple example"
        widg1.Show()

        ' TODO missing QObject.findChild QBoject.findChildren
        ' This might be because the function calls in C++ use templates (generics) to specify the type
        'QWidget.Qt_qFindChild_helper(widg1, "pushButton", )

        ' Looks like we'll need to re-implement our own findchild / find children method
        ' using QWidget.Qt_qFindChild_helper
        ' see C:\Qt\Qt5.5.1\5.5\Src\qtbase\src\corelib\kernel\qobject.h
        'Dim test1 = findChild(Of QPushButton)(widg1,"pushButton")

        ' Run the QApplication Process
        QApplication.Exec()

    End Sub


    Public shared Function findChild(of T As QObject)(parent As QObject, name As string, _
            Optional options As Qt.FindChildOption = Qt.FindChildOption.FindChildrenRecursively) As QObject

        ' TODO StaticMetaObject returns Null reference exception
        Dim meta As QMetaObject = QPushButton.StaticMetaObject
        'Dim meta As QMetaObject = GetType(T).GetProperty("StaticMetaObject").GetValue(Nothing, Nothing)
        
        Dim ret = QWidget.Qt_qFindChild_helper(parent,name,meta,options)
        Return ret
    End Function



    Public shared Function findChildren(of T As QObject)(parent As QObject, name As string, _
            Optional options As Qt.FindChildOption = Qt.FindChildOption.FindChildrenRecursively) As List(Of T)

        Dim ret As New List(Of T)

        ' TODO need to find a way of passing types into QList, then getting the QMetaObject from it
        Dim x As New QList()

        Dim meta As QMetaObject = QPushButton.StaticMetaObject
        'Dim meta As QMetaObject = GetType(T).GetProperty("StaticMetaObject").GetValue(Nothing, Nothing)
        
        Dim test1 = QWidget.Qt_qFindChild_helper(parent,name,meta,options)

        Return ret
    End Function



End Class
