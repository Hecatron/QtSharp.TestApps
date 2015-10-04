using QtCore;
using QtWidgets;

namespace GUIApp1_CSharp.Examples
{
    class UILoadExample
    {
        /// <summary> See if we can load .ui files. </summary>
        public static void Run()
        {

            App.SetupQApplication.Run();

            // QtUiTools is a smaller runtime, but requires manual compilation of the dll on the end users machine
            //QtUiTools.QUiLoader loader = new QtUiTools.QUiLoader();
            QtDesigner.QFormBuilder loader = new QtDesigner.QFormBuilder();

            // Load the widget from a ui file
            QFile file1 = new QFile("testform1.ui");
            file1.Open(QIODevice.OpenModeFlag.ReadOnly);
            //error shows up in QtCore.OnEvent
            QWidget widg1 = loader.Load(file1); // System.AccessViolationException
            file1.Close();

            widg1.ObjectName = "Simple example";
            widg1.Show();

            // Run the QApplication Process
            QApplication.Exec();
        }

    }
}
