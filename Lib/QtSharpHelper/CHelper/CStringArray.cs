using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

// Based on code from http://stackoverflow.com/questions/13317931/marshal-an-array-of-strings-from-c-sharp-to-c-code-using-p-invoke

namespace QtSharpHelper.CHelper
{

    /// <summary> Helper class for generating C String arrays. </summary>
    public class CStringArray
    {
        #region Properties

        /// <summary> List of Managed Strings. </summary>
        /// <value> List of Managed Strings. </value>
        public string[] ManagedArray
        {
            get { return _ManagedArray; }
        }

        protected string[] _ManagedArray { get; set; }

        /// <summary> Handle to the allocated memory block. </summary>
        /// <value> Handle to the allocated memory block. </value>
        public GCHandle Handle
        {
            get { return _Handle; }
        }

        protected GCHandle _Handle { get; set; }

        /// <summary> Array of Pointers. </summary>
        /// <value> Array of Pointers. </value>
        public IntPtr[] PointerArray
        {
            get { return _PointerArray; }
        }

        protected IntPtr[] _PointerArray { get; set; }

        #endregion

        #region Methods

        public CStringArray(IEnumerable<string> strarray)
        {
            _ManagedArray = strarray.ToArray();
            _PointerArray = null;
        }

        ~CStringArray()
        {
            DeAlloc();
        }

        public void Alloc() {
            List<IntPtr> PointerList = new List<IntPtr>();
            foreach (var item in ManagedArray)
            {
                PointerList.Add(Marshal.StringToHGlobalAnsi(item));
            }
            _PointerArray = PointerList.ToArray();
            _Handle = GCHandle.Alloc(PointerArray, GCHandleType.Pinned);
        }

        public void DeAlloc()
        {
            if (_PointerArray == null)
            {
                return;
            }
            _Handle.Free();
            foreach (var item in _PointerArray)
            {
                Marshal.FreeHGlobal(item);
            }
        }

        /// <summary> Return the Address of the String Array. </summary>
        public IntPtr Address()
        {
            if (_PointerArray == null)
            {
                throw new ArgumentException("Memory has not been allocated");
            }
            return Handle.AddrOfPinnedObject();
        }

        #endregion
    }

}
