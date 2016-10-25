using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Forms;
using CppSharp;
using QtCore;
using QtCore.Qt;
using QtGui;
using QtSharpHelper.CHelper;
using QtSharpHelper.Qt;
using QtWidgets;

namespace UIFormLoad1_CS
{
    static class Program
    {

        public static QApp QApp;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public unsafe static void Main()
        {
            // Create a new QApplication object
            //QApp = QApp.CreateQApp();

            var argc = 0;
            new QApplication(ref argc, null);

            // QtUiTools is a smaller runtime, but requires manual compilation of the dll on the end users machine
            // Dim loader As New QtUiTools.QUiLoader()
            var loader = new QtDesigner.QFormBuilder();

            // Load the widget from a ui file
            var file1 = new QFile("testform1.ui");
            file1.Open(QIODevice.OpenModeFlag.ReadOnly);
            // error shows up in QtCore.OnEvent
            var widg1 = loader.Load(file1);
            file1.Close();

            widg1.ObjectName = "Simple example";
            widg1.Show();

            // TODO missing QObject.findChild QBoject.findChildren
            // This might be because the function calls in C++ use templates (generics) to specify the type
            //QWidget.QtQFindChildHelper(widg1, "pushButton", )

            // Looks like we'll need to re-implement our own findchild / find children method
            // using QWidget.Qt_qFindChild_helper
            // see C:\Qt\Qt5.7.0\5.7\Src\qtbase\src\corelib\kernel\qobject.h
            var test1 = findChild<QPushButton>(widg1, "pushbutton");
            //var test2 = getChildren(widg1);


            // Run the QApplication Process
            QApplication.Exec();

        }

        public static QObject findChild<T>(QObject parent, string name, QtCore.Qt.FindChildOption options = QtCore.Qt.FindChildOption.FindChildrenRecursively) where T : QObject {
            //QMetaObject meta = QPushButton.StaticMetaObject;
            QMetaObject meta = QObject.StaticMetaObject;
            //Dim meta As QMetaObject = GetType(T).GetProperty("StaticMetaObject").GetValue(Nothing, Nothing)

            //IntPtr symboltest = SymbolResolver.ResolveSymbol("Qt5Widgets", "_ZN11QPushButton16staticMetaObjectE");
            //QMetaObject meta = QMetaObject.__CreateInstance(*(QMetaObject.Internal*)(void*)symboltest, false);

            var ret = QObject.QtQFindChildHelper(parent, name, meta, options);
            //var ret = Qt_qFindChild_helper(parent, name, meta, options);
            return ret;
        }



        //public static List<T> findChildren<T>(QObject parent, string name, QtCore.Qt.FindChildOption options = QtCore.Qt.FindChildOption.FindChildrenRecursively) where T : QObject
        //{

        //    List<T> ret = new List<T>();

        //    // TODO need to find a way of passing types into QList, then getting the QMetaObject from it
        //    //QList x = new QList();

        //    QMetaObject meta = QPushButton.StaticMetaObject;
        //    //Dim meta As QMetaObject = GetType(T).GetProperty("StaticMetaObject").GetValue(Nothing, Nothing)

        //    var test1 = QWidget.Qt_qFindChild_helper(parent, name, meta, options);

        //    return ret;
        //}





        //public static unsafe QObject Qt_qFindChild_helper(QObject parent, string name, QMetaObject mo, FindChildOption options)
        //{
        //    IntPtr parent1 = object.ReferenceEquals((object)parent, (object)null) ? IntPtr.Zero : parent.__Instance;

        //    void** numPtr = (void**)(object.ReferenceEquals((object)name, (object)null) ? (IntPtr)null : (IntPtr)Marshal.StringToHGlobalUni(name).ToPointer());
        //    QString qstring = (IntPtr)numPtr == IntPtr.Zero ? (QString)null : QString.FromUtf16(numPtr, name.Length);
        //    IntPtr name1 = object.ReferenceEquals((object)qstring, (object)null) ? IntPtr.Zero : qstring.__Instance;

        //    if (object.ReferenceEquals((object)mo, (object)null))
        //        throw new ArgumentNullException("mo", "mo cannot be null because it is a C++ reference (&).");
        //    IntPtr instance = mo.__Instance;

        //    FindChildOption options1 = options;

        //    IntPtr childHelper0 = Qt_qFindChild_helper_0(parent1, name1, instance, options1);
        //    QObject qobject;
        //    if (childHelper0 == IntPtr.Zero)
        //        qobject = (QObject)null;
        //    else if (QObject.NativeToManagedMap.ContainsKey(childHelper0))
        //        qobject = QObject.NativeToManagedMap[childHelper0];
        //    else
        //        QObject.NativeToManagedMap[childHelper0] = qobject = QObject.__CreateInstance(childHelper0, false);
        //    return qobject;
        //}

        //[SuppressUnmanagedCodeSecurity]
        //[DllImport("Qt5Core", EntryPoint = "_Z20qt_qFindChild_helperPK7QObjectRK7QStringRK11QMetaObject6QFlagsIN2Qt15FindChildOptionEE", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr Qt_qFindChild_helper_0(IntPtr parent, IntPtr name, IntPtr mo, FindChildOption options);





        //public static unsafe Object getChildren(QObject parent)
        //{
        //    IntPtr parent1 = object.ReferenceEquals((object)parent, (object)null) ? IntPtr.Zero : parent.__Instance;

        //    //QList retlst = null;
        //    IntPtr retptr = QObject_Children_0(parent1);
        //    //// seems to work, interpret QList<QObject*> into a list of pointers then interate

        //    if (retptr != IntPtr.Zero)
        //    {
        //    //    //TODO no worky
        //    //    var y1 = new QList();

        //    //    var x1 = new QListData();
        //    //    var x2 = x1.__Instance;
        //    //    x2.d = retptr;


        //    //    //CStruct<QList> tmpcontainer = new CStruct<QList>(retptr, CStruct<QList>.AllocatedMode.viaDll);
        //    //    //retlst = tmpcontainer.Struct;
        //    }
        //    return null;
        //}

        //[SuppressUnmanagedCodeSecurity]
        //[DllImport("QtCore-inlines", EntryPoint = "_ZNK7QObject8childrenEv", CallingConvention = CallingConvention.ThisCall)]
        //internal static extern IntPtr QObject_Children_0(IntPtr parent);

    }
}
