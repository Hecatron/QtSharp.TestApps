Imports QtWidgets

' TODO try signaling and slotting buttons

Public Class LayoutExample
    Inherits QDialog

    Public Const NumGridRows As Integer = 3
    Public Const NumButtons As Integer = 4
    Private menuBar As QMenuBar
    Private horizontalGroupBox As QGroupBox
    Private gridGroupBox As QGroupBox
    Private formGroupBox As QGroupBox
    Private smallEditor As QTextEdit
    Private bigEditor As QTextEdit
    Private labels() As QLabel
    Private lineEdits() As QLineEdit
    Private buttons() As QPushButton
    Private buttonBox As QDialogButtonBox
    Private fileMenu As QMenu
    Private exitAction As QAction

    ''' <summary> Default constructor. </summary>
    Public Sub New()
        createMenu()
        createHorizontalGroupBox()
        createGridGroupBox()
        createFormGroupBox()

        ' Causes an error on dialog close
        bigEditor = New QTextEdit()
        bigEditor.SetPlainText("This widget takes up all the remaining space in the top-level layout.")

        ' TODO
        ' QDialogButtonBox.StandardButton is ambiguous, both function and enum
        'buttonBox = new QDialogButtonBox(QtWidgets.QDialogButtonBox.Ok,  QtWidgets.QDialogButtonBox.Cancel);

        ' TODO
        'connect(buttonBox, SIGNAL(accepted()), this, SLOT(accept()));
        'connect(buttonBox, SIGNAL(rejected()), this, SLOT(reject()));

        Dim mainLayout As New QVBoxLayout()
        mainLayout.MenuBar = menuBar
        mainLayout.AddWidget(horizontalGroupBox)
        mainLayout.AddWidget(gridGroupBox)
        mainLayout.AddWidget(formGroupBox)
        mainLayout.AddWidget(bigEditor)
        'mainLayout.AddWidget(buttonBox)
        Layout = mainLayout
        WindowTitle = "Basic Layouts"

    End Sub

    Private Sub createMenu()
        menuBar = New QMenuBar()
        fileMenu = New QMenu("&File")
        exitAction = fileMenu.AddAction("E&xit")
        menuBar.AddMenu(fileMenu)

        ' TODO
        ' Singal and slot just turn parameters into string literals, so in theory this should work, but doesn't seem to
        Connect(exitAction, "triggered()", Me, "accept()")
        'connect(exitAction, SIGNAL(triggered()), this, SLOT(accept()));
    End Sub


    Private Sub createHorizontalGroupBox()
        buttons = New QPushButton(NumButtons - 1) {}
        horizontalGroupBox = New QGroupBox("Horizontal layout")
        Dim layout As New QHBoxLayout()

        For i As Integer = 0 To NumButtons - 1
            buttons(i) = New QPushButton("Button " & (i + 1))
            layout.AddWidget(buttons(i))
        Next i
        horizontalGroupBox.Layout = layout
    End Sub


    Private Sub createGridGroupBox()
        gridGroupBox = New QGroupBox("Grid layout")
        Dim layout As New QGridLayout()

        labels = New QLabel(NumGridRows - 1) {}
        lineEdits = New QLineEdit(NumGridRows - 1) {}

        For i As Integer = 0 To NumGridRows - 1
            labels(i) = New QLabel("Line " & (i + 1))
            lineEdits(i) = New QLineEdit()
            layout.AddWidget(labels(i), i + 1, 0)
            layout.AddWidget(lineEdits(i), i + 1, 1)
        Next i
    End Sub

    Private Sub createFormGroupBox()
        formGroupBox = New QGroupBox("Form layout")
        Dim Layout As New QFormLayout()
        Layout.AddRow(New QLabel("Line 1:"), New QLineEdit())
        Layout.AddRow(New QLabel("Line 2, long text:"), New QComboBox())
        Layout.AddRow(New QLabel("Line 3:"), New QSpinBox())
        formGroupBox.Layout = Layout
    End Sub

End Class
