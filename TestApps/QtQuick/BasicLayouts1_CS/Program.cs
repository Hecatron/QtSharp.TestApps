using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using QtCore;
using QtGui;
using QtQml;
using QtQuick;
using QtSharpHelper.Qt;
using QtWidgets;

namespace BasicLayouts1_CS
{
    static class Program
    {

        // Using the code from the QtQuick Gallery example included with Qt Creator / Q 5.5.1 as a basis
        // Simple example to load a qml file

        public static QApp QApp;
        public static QGuiApp QGuiApp;
        public static QQmlApplicationEngine QmlEngine;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static unsafe void Main()
        {

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //int argc = 0;
            //new QGuiApplication(ref argc, null);

            QGuiApp = QGuiApp.CreateQGuiApp();

            QQuickView quickView = new QQuickView();
            quickView.Source = QUrl.FromLocalFile(@"main.qml");
            quickView.Show();

            // Run the QApplication Process
            QApplication.Exec();
        }
    }
}
