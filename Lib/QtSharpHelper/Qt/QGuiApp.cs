using System;
using System.Collections.Generic;
using QtGui;
using QtSharpHelper.CHelper;

namespace QtSharpHelper.Qt {
    /// <summary> QTGuiApplication should be used where widgets are not required, to avoid dependencies on QtWidget. </summary>
    public class QGuiApp : IDisposable {
        /// <summary> Readonly QGuiApplication object. </summary>
        /// <value> QGuiApplication object. </value>
        public QGuiApplication QGuiApplication => _QGuiApplication;

        /// <summary> QGuiApplication object. </summary>
        /// <value> QGuiApplication object. </value>
        protected static QGuiApplication _QGuiApplication { get; set; }

        /// <summary> C Array for options passed to QGuiApplication. </summary>
        /// <value> C Array for options passed to QGuiApplication. </value>
        protected CStringArray COpts { get; set; }

        protected bool disposed;
        protected int argc;

        /// <summary> Specialised default constructor for use only by derived class. </summary>
        protected unsafe QGuiApp(IReadOnlyCollection<string> opts) {
            argc = opts.Count;
            char** argv = null;

            if (opts.Count > 0) {
                // Convert the string list to a C String Array
                COpts = new CStringArray(opts);
                COpts.Alloc();
                argv = (char**) COpts.Address();
            }
            _QGuiApplication = new QGuiApplication(ref argc, argv);
        }

        /// <summary> Creates QGuiApp object. </summary>
        /// <returns> The new QGuiApp object. </returns>
        public static QGuiApp CreateQGuiApp() {
            if (_QGuiApplication != null)
                throw new System.Exception("Only one QGuiApplication class may be created at a time");
            var opts = new List<string>();
            //opts.Add("");
            var ret = new QGuiApp(opts);
            return ret;
        }

        /// <summary> Creates QGuiApp object. </summary>
        /// <returns> The new QGuiApp object. </returns>
        public static QGuiApp CreateQGuiApp(List<string> opts) {
            if (_QGuiApplication != null)
                throw new System.Exception("Only one QGuiApplication class may be created at a time");
            var ret = new QGuiApp(opts);
            return ret;
        }

        /// <summary> Finaliser. </summary>
        ~QGuiApp() {
            Dispose(false);
        }

        /// <summary> Dispose / close the class. </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposed) return;
            if (disposing)
                // Free other state (managed objects).
                _QGuiApplication.Dispose();

            // Free your own state (unmanaged objects).
            // Set large fields to null.
            //COpts.DeAlloc();
            COpts = null;
            _QGuiApplication = null;
            disposed = true;
        }
    }
}