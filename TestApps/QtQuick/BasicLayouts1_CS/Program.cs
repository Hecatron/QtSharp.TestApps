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

namespace BasicLayouts1_CS {
    internal static class Program {
        // Using the code from the QtQuick Gallery example included with Qt Creator / Q 5.5.1 as a basis
        // Simple example to load a qml file

        public static QApp QApp;
        public static QGuiApp QGuiApp;
        public static QQmlApplicationEngine QmlEngine;
        public static QQuickView QV;
        public static QQmlComponent Comp;
        public static QObject CompObj;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static unsafe void Main() {
            QGuiApp = QGuiApp.CreateQGuiApp();

            LoadQml();

            // Run the QApplication Process
            QGuiApplication.Exec();
        }

        private static void LoadQml() {
            // 1. File Access
            QmlEngine = new QQmlApplicationEngine("main.qml");

            // 2. File Access
            //QmlEngine = new QQmlApplicationEngine();
            //QmlEngine.Load("main.qml");

            // 3. Url Access
            //QUrl url1 = QUrl.FromLocalFile("main.qml");
            //QmlEngine = new QQmlApplicationEngine(url1);

            // 4. LoadData
            //string qmltxt = System.IO.File.ReadAllText("main.qml");
            //QByteArray qarr = new QByteArray(qmltxt, qmltxt.Length);
            //QmlEngine = new QQmlApplicationEngine();
            //QmlEngine.LoadData(qarr);

            // This shows an empty form, perhaps it's supposed to
            //QQuickView quickView = new QQuickView();
            //quickView.Source = QUrl.FromLocalFile(@"main.qml");
            //quickView.Show();

            //This seems to bomb out horribly
            //QmlEngine = new QQmlApplicationEngine();
            //Comp = new QQmlComponent(QmlEngine, QUrl.FromLocalFile("main.qml"));
            //CompObj = Comp.Create();
            //QQmlApplicationEngine.SetObjectOwnership(CompObj, QQmlEngine.ObjectOwnership.CppOwnership);

            // http://stackoverflow.com/questions/23936169/whats-the-difference-between-qquickview-and-qquickwindow
            //QmlEngine = new QQmlApplicationEngine();
            //QmlEngine.Load ("main.qml");
            // To access the QQuickView inside QmlEngine we need to access the rootObjects()
            // rootObjects appears to be a function which returns a QList of pointers to QObject, not visible because it's templated
        }
    }
}