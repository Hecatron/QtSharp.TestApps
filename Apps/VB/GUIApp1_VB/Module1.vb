Imports QtCore
Imports QtWidgets

Module Module1

    Sub Main()
        Test1()
    End Sub

    ''' <summary> See if we can load .ui files. </summary>
    Public Sub Test1()
        'Dim qapp As New QApplication()
        'Dim loader As new QtUiTools.QUiLoader
        Dim loader As New QtDesigner.QFormBuilder

        Dim file1 As New QFile("testform1.ui")
        file1.open(QIODevice.OpenModeFlag.ReadOnly)

        Dim widg1 As QtWidgets.QWidget = loader.load(file1)
        file1.Close()

        widg1.ObjectName = "Slim Shady"
        widg1.Show()
    End Sub

End Module
