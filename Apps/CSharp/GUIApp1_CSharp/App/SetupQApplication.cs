using System;
using System.Collections.Generic;
using QtWidgets;

namespace GUIApp1_CSharp.App
{
    class SetupQApplication
    {
        public static QApplication app { get; set; }

        /// <summary> Setup the main QApplication instance. </summary>
        public static unsafe void Run()
        {
            // Setup string array arguments for QApplication
            List<String> qappopts = new List<String>();
            qappopts.Add("");
            CStringArray carr = new CStringArray(qappopts);
            carr.Alloc();

            // Create a new QApplication
            int argc = qappopts.Count;
            char** argv = (char**)carr.Address();
            app = new QApplication(ref argc, argv);
        }
    }
}
