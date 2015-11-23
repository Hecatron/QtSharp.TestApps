using System;
using System.Collections.Generic;
using QtGui;
using QtSharpHelper.CHelper;

// QTGuiApplication should be used where widgets are not required, to avoid dependencies on QtWidget

namespace QtSharpHelper.Qt
{
    public class QGuiApp : IDisposable
    {

        #region "Properties"

        /// <summary> Readonly QGuiApplication object. </summary>
        /// <value> QGuiApplication object. </value>
        public QGuiApplication QGuiApplication
        {
            get { return _QGuiApplication; }
        }

        /// <summary> QGuiApplication object. </summary>
        /// <value> QGuiApplication object. </value>
        protected static QGuiApplication _QGuiApplication { get; set; }

        /// <summary> C Array for options passed to QGuiApplication. </summary>
        /// <value> C Array for options passed to QGuiApplication. </value>
        protected CStringArray COpts { get; set; }

        protected bool disposed = false;

        #endregion

        #region "Constructors"

        /// <summary> Specialised default constructor for use only by derived class. </summary>
        protected unsafe QGuiApp(List<String> opts)
        {

            // Convert the string list to a C String Array
            COpts = new CStringArray(opts);
            COpts.Alloc();

            // Create a new QGuiApplication
            int argc = opts.Count;
            char** argv = (char**)COpts.Address();
            _QGuiApplication = new QGuiApplication(ref argc, argv);
        }

        /// <summary> Creates QGuiApp object. </summary>
        /// <returns> The new QGuiApp object. </returns>
        public static QGuiApp CreateQGuiApp()
        {
            if (_QGuiApplication != null) throw new Exception("Only one QGuiApplication class may be created at a time");
            List<String> opts = new List<String>();
            opts.Add("");
            QGuiApp ret = new QGuiApp(opts);
            return ret;
        }

        /// <summary> Creates QGuiApp object. </summary>
        /// <returns> The new QGuiApp object. </returns>
        public static QGuiApp CreateQGuiApp(List<String> opts)
        {
            if (_QGuiApplication != null) throw new Exception("Only one QGuiApplication class may be created at a time");
            QGuiApp ret = new QGuiApp(opts);
            return ret;
        }

        #endregion

        #region "Destructors"

        /// <summary> Finaliser. </summary>
        ~QGuiApp()
        {
            Dispose(false);
        }

        /// <summary> Dispose / close the class. </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                    _QGuiApplication.Dispose();
                }

                // Free your own state (unmanaged objects).
                // Set large fields to null.
                //COpts.DeAlloc();
                COpts = null;
                _QGuiApplication = null;
                disposed = true;
            }
        }

        #endregion

    }
}
