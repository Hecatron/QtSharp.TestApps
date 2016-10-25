using System;
using System.Collections.Generic;
using QtSharpHelper.CHelper;
using QtWidgets;

namespace QtSharpHelper.Qt {
    public class QApp : IDisposable {
        /// <summary> Readonly QApplication object. </summary>
        /// <value> QApplication object. </value>
        public QApplication QApplication => _QApplication;

        /// <summary> QApplication object. </summary>
        /// <value> QApplication object. </value>
        protected static QApplication _QApplication { get; set; }

        /// <summary> C Array for options passed to QApplication. </summary>
        /// <value> C Array for options passed to QApplication. </value>
        protected CStringArray COpts { get; set; }

        protected bool disposed;
        protected int argc;

        /// <summary> Specialised default constructor for use only by derived class. </summary>
        protected unsafe QApp(IReadOnlyCollection<string> opts) {
            argc = opts.Count;
            char** argv = null;

            if (opts.Count > 0) {
                // Convert the string list to a C String Array
                COpts = new CStringArray(opts);
                COpts.Alloc();
                argv = (char**) COpts.Address();
            }
            _QApplication = new QApplication(ref argc, argv);
        }

        /// <summary> Creates QApp object. </summary>
        /// <returns> The new QApp object. </returns>
        public static QApp CreateQApp() {
            if (_QApplication != null)
                throw new System.Exception("Only one Qapplication class may be created at a time");
            var opts = new List<string> {""};
            var ret = new QApp(opts);
            return ret;
        }

        /// <summary> Creates QApp object. </summary>
        /// <returns> The new QApp object. </returns>
        public static QApp CreateQApp(List<string> opts) {
            if (_QApplication != null)
                throw new System.Exception("Only one Qapplication class may be created at a time");
            var ret = new QApp(opts);
            return ret;
        }

        /// <summary> Finaliser. </summary>
        ~QApp() {
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
                _QApplication.Dispose();

            // Free your own state (unmanaged objects).
            // Set large fields to null.
            //COpts.DeAlloc();
            COpts = null;
            _QApplication = null;
            disposed = true;
        }
    }
}