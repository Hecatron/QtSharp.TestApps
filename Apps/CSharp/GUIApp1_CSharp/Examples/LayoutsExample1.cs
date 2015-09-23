using QtWidgets;

namespace GUIApp1_CSharp.Examples
{
    class LayoutsExample1
    {
        /// <summary> Test of a box layout. </summary>
        public static void Run()
        {
            // taken from http://www.java2s.com/Code/Cpp/Qt/CreatePushButtonandaddtowindow.htm

            App.SetupQApplication.Run();

            // Create a basic widget
            QWidget window1 = new QWidget();
            window1.WindowTitle = "Simple example2";

            QPushButton button1 = new QPushButton("One");
            QPushButton button2 = new QPushButton("Two");
            QPushButton button3 = new QPushButton("Three");
            QPushButton button4 = new QPushButton("Four");
            QPushButton button5 = new QPushButton("Five");

            QHBoxLayout layout = new QHBoxLayout();
            layout.AddWidget(button1);
            layout.AddWidget(button2);
            layout.AddWidget(button3);
            layout.AddWidget(button4);
            layout.AddWidget(button5);

            window1.Layout = layout;
            window1.Show();

            // Now works with a basic widget woo hoo
            QApplication.Exec();
        }

    }
}
