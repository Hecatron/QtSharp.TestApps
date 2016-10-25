using QtSharpHelper.Qt;
using QtWidgets;

namespace BasicForm1_CS {
    public class Program {
        public static QApp QApp;

        public static void Main() {
            // Create a new QApplication object
            QApp = QApp.CreateQApp();

            // Create a basic widget
            var widg1 = new QWidget();
            widg1.Resize(250, 150);
            widg1.WindowTitle = "Simple example";
            widg1.Show();

            QApplication.Exec();
        }
    }
}