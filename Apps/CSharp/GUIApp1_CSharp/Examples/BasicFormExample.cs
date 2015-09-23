using QtWidgets;

namespace GUIApp1_CSharp.Examples
{
    class BasicFormExample
    {
        /// <summary> Show Basic Form. </summary>
        public static void Run()
        {
            App.SetupQApplication.Run();

            // Create a basic widget
            QWidget widg1 = new QWidget();
            widg1.Resize(250, 150);
            widg1.WindowTitle = "Simple example";
            widg1.Show();

            // Now works with a basic widget woo hoo
            QApplication.Exec();
        }
    }
}
