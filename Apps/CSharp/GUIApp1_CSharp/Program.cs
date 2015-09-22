using System;
using System.Collections.Generic;
using QtCore;
using QtWidgets;

namespace GUIApp1_CSharp
{

    public class Program
    {

        private static void Main(string[] args)
        {
            BasicFormTest();
            //UILoadTest();
        }

        /// <summary> Show Basic Form. </summary>
        public static unsafe void BasicFormTest()
        {
            // Setup string array arguments for QApplication
            List<String> qappopts = new List<String>();
            qappopts.Add("");
            CStringArray carr = new CStringArray(qappopts);
            carr.Alloc();

            // Create a new QApplication
            int argc = qappopts.Count;
            char** argv = (char**)carr.Address();
            QApplication app = new QApplication(ref argc, argv);
            
            // Create a basic widget
            QWidget widg1 = new QWidget();
            widg1.Resize(250, 150);
            widg1.WindowTitle = "Simple example"; // System.AccessViolationException
            widg1.Show(); // System.AccessViolationException

            // This only errors out if we create a QWidget object, if we comment out the above QWidget decleration, this then works fine
            QApplication.Exec(); // System.AccessViolationException
        }


        /// <summary> See if we can load .ui files. </summary>
        public static unsafe void UILoadTest()
        {
            // Setup string array arguments for QApplication
            List<String> qappopts = new List<String>();
            qappopts.Add("");
            CStringArray carr = new CStringArray(qappopts);
            carr.Alloc();

            // Create a new QApplication
            int argc = qappopts.Count;
            char** argv = (char**)carr.Address();
            QApplication app = new QApplication(ref argc, argv);

            // QtUiTools is a smaller runtime, but requires manual compilation of the dll on the end users machine
            //QtUiTools.QUiLoader loader = new QtUiTools.QUiLoader();
            QtDesigner.QFormBuilder loader = new QtDesigner.QFormBuilder();

            // Load the widget from a ui file
            QFile file1 = new QFile("testform1.ui");
            file1.Open(QIODevice.OpenModeFlag.ReadOnly);
            // I think this is within the QWidget Load Function, so if we can get that to work, ui parsing may work as well
            QWidget widg1 = loader.Load(file1); // System.AccessViolationException
            file1.Close();

            // We never get this far
            widg1.ObjectName = "Simple example";
            widg1.Show();

            // Run the QApplication Process
            QApplication.Exec();
        }

    }

}
