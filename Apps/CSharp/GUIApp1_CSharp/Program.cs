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
            List<String> qappopts = new List<String>();
            qappopts.Add("");
            CStringArray carr = new CStringArray(qappopts);
            carr.Alloc();

            int argc = qappopts.Count;
            char** argv = (char**)carr.Address();
            QApplication app = new QApplication(ref argc, argv);

            QWidget widg1 = new QWidget();
            widg1.Resize(250, 150);
            //widg1.WindowTitle = "Simple example";
            widg1.Show();
            //app.exec(); //Not Found?
        }

        /// <summary> See if we can load .ui files. </summary>
        public static unsafe void UILoadTest()
        {
            List<String> qappopts = new List<String>();
            qappopts.Add("");
            CStringArray carr = new CStringArray(qappopts);
            carr.Alloc();

            int argc = qappopts.Count;
            char** argv = (char**)carr.Address();
            QApplication app = new QApplication(ref argc, argv);

            QtUiTools.QUiLoader loader = new QtUiTools.QUiLoader();
            //QtDesigner.QFormBuilder loader = new QtDesigner.QFormBuilder();

            QFile file1 = new QFile("testform1.ui");
            //QFile file1 = new QFile(@"D:/SourceControl/GitExternal/QtSharp.TestApps/CSharp/GUIApp1_CSharp/testform1.ui");

            file1.Open(QIODevice.OpenModeFlag.ReadOnly);

            QWidget widg1 = loader.Load(file1);
            file1.Close();

            widg1.ObjectName = "Simple example";
            widg1.Show();
            //app.exec(); //Not Found?
        }

    }

}