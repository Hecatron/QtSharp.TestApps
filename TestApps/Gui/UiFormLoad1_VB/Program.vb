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

        ' Run the QApplication Process
        QApplication.Exec()

    End Sub

End Class
