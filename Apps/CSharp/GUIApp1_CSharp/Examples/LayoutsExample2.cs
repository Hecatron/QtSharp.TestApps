using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QtWidgets;

namespace GUIApp1_CSharp.Examples
{
    class LayoutsExample2 : QDialog 
    {

        public const int NumGridRows = 3;
        public const int NumButtons = 4;
        QMenuBar menuBar;
        QGroupBox horizontalGroupBox;
        QGroupBox gridGroupBox;
        QGroupBox formGroupBox;
        QTextEdit smallEditor;
        QTextEdit bigEditor;
        QLabel[] labels;
        QLineEdit[] lineEdits;
        QPushButton[] buttons;
        QDialogButtonBox buttonBox;
        QMenu fileMenu;
        QAction exitAction;

        /// <summary> Test of a simple layout. </summary>
        public static void Run()
        {
            // taken from http://doc.qt.io/qt-4.8/qt-layouts-basiclayouts-dialog-cpp.html
            // TODO Tr statements removed

            App.SetupQApplication.Run();

            QDialog dia1 = new LayoutsExample2();
            dia1.Show();
            QApplication.Exec();
        }


        /// <summary> Default constructor. </summary>
        public LayoutsExample2()
        {
            createMenu();
            createHorizontalGroupBox();
            createGridGroupBox();
            createFormGroupBox();

            bigEditor = new QTextEdit();
            bigEditor.SetPlainText("This widget takes up all the remaining space in the top-level layout.");

            // TODO
            //buttonBox = new QDialogButtonBox(QtWidgets.QDialogButtonBox.Ok  QtWidgets.QDialogButtonBox.Cancel);

            // TODO
            //connect(buttonBox, SIGNAL(accepted()), this, SLOT(accept()));
            //connect(buttonBox, SIGNAL(rejected()), this, SLOT(reject()));

            QVBoxLayout mainLayout = new QVBoxLayout();
            mainLayout.MenuBar = menuBar;
            mainLayout.AddWidget(horizontalGroupBox);
            mainLayout.AddWidget(gridGroupBox);
            mainLayout.AddWidget(formGroupBox);
            mainLayout.AddWidget(bigEditor);
            //mainLayout.AddWidget(buttonBox);
            Layout = mainLayout;
            WindowTitle = "Basic Layouts";
        }

        private void createMenu()
        {
            menuBar = new QMenuBar();
            fileMenu = new QMenu("&File");
            exitAction = fileMenu.AddAction("E&xit");
            menuBar.AddMenu(fileMenu);
            // TODO
            //connect(exitAction, SIGNAL(triggered()), this, SLOT(accept()));
        }

        private void createHorizontalGroupBox()
        {
            buttons = new QPushButton[NumButtons];
            horizontalGroupBox = new QGroupBox("Horizontal layout");
            QHBoxLayout layout = new QHBoxLayout();

            for (int i = 0; i < NumButtons; ++i) {
                buttons[i] = new QPushButton("Button " + (i + 1));
                layout.AddWidget(buttons[i]);
            }
            horizontalGroupBox.Layout = layout;
        }

        private void createGridGroupBox()
        {
            gridGroupBox = new QGroupBox("Grid layout");
            QGridLayout layout = new QGridLayout();

            labels = new QLabel[NumGridRows];
            lineEdits = new QLineEdit[NumGridRows];
            
            for (int i = 0; i < NumGridRows; ++i) {
                labels[i] = new QLabel("Line " + (i + 1));
                lineEdits[i] = new QLineEdit();
                layout.AddWidget(labels[i], i + 1, 0);
                layout.AddWidget(lineEdits[i], i + 1, 1);
            }
        }

        private void createFormGroupBox()
        {
            formGroupBox = new QGroupBox("Form layout");
            QFormLayout layout = new QFormLayout();
            layout.AddRow(new QLabel("Line 1:"), new QLineEdit());
            layout.AddRow(new QLabel("Line 2, long text:"), new QComboBox());
            layout.AddRow(new QLabel("Line 3:"), new QSpinBox());
            formGroupBox.Layout = layout;
        }
    }
}
