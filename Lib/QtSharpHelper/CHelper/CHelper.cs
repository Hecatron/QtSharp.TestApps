using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

//TODO string formatting is ascii only
//http://stackoverflow.com/questions/721201/whats-the-equivalent-of-vbs-asc-and-chr-functions-in-c

namespace QtSharpHelper.CHelper {
    /// <summary> Helper Code for Low Level C Unmanaged structures. </summary>
    public class CHelper {
        /// <summary> Return True if we're on a 64 bit Operating system. </summary>
        /// <returns> true if 64bit, false if not. </returns>
        public static bool Is64bit() {
            return (Marshal.SizeOf(typeof (IntPtr))*8) == 64;
        }

        /// <summary> Convert a C Byte Array to a String. </summary>
        /// <param name="ByteArr"> Array of bytes. </param>
        /// <returns> A String. </returns>
        public static string CByteArrToString(byte[] ByteArr) {
            var retval = "";
            if (ByteArr == null)
                return retval;
            foreach (var item in ByteArr) {
                retval += ((char) item).ToString();
            }
            retval = retval.TrimEnd(' ');
            return retval;
        }

        /// <summary> Convert a String to a C Byte Array, Terminated with a Null (0) value. </summary>
        /// <param name="input"> The input. </param>
        /// <returns> A Byte() </returns>
        public static byte[] StringToCByteArr(string input) {
            if (string.IsNullOrEmpty(input))
                return null;
            var retval = new byte[input.Length + 1];
            var tempVar = input.Length - 1;
            for (var count1 = 0; count1 <= tempVar; count1++) {
                retval[count1] = Convert.ToByte((input.Substring(count1, 1)[0]));
            }
            return retval;
        }

        /// <summary>
        ///  Adds an offset to a memory pointer Typically not supported in the framework until .Net 4, so
        ///  we implement our own methods here.
        /// </summary>
        /// <exception cref="NotSupportedException"> Thrown when the requested operation is not supported. </exception>
        /// <param name="src">    Source for the. </param>
        /// <param name="offset"> The offset. </param>
        /// <returns> An IntPtr. </returns>
        public static IntPtr MemOffset(IntPtr src, int offset) {
            var bitsize = Marshal.SizeOf(typeof (IntPtr))*8;
            switch (bitsize) {
                case 64:
                    return new IntPtr(src.ToInt64() + offset);
                case 32:
                    return new IntPtr(src.ToInt32() + offset);
                default:
                    throw new NotSupportedException("This is running on a machine where pointers are " + IntPtr.Size +
                                                    " bytes which is currently unsupported");
            }
        }

        /// <summary>
        ///  Adds an offset to a memory pointer Typically not supported in the frameword until .Net 4, so
        ///  we implement our own methods here.
        /// </summary>
        /// <exception cref="NotSupportedException"> Thrown when the requested operation is not supported. </exception>
        /// <param name="src">    Source for the. </param>
        /// <param name="offset"> The offset. </param>
        /// <returns> An IntPtr. </returns>
        public static IntPtr MemOffset(IntPtr src, long offset) {
            var bitsize = Marshal.SizeOf(typeof (IntPtr))*8;
            switch (bitsize) {
                case 64:
                    return new IntPtr(src.ToInt64() + offset);
                case 32:
                    // May generate funny results
                    // And Not the "funny ha ha" kind
                    return new IntPtr(src.ToInt32() + offset);
                default:
                    throw new NotSupportedException("This is running on a machine where pointers are " + IntPtr.Size +
                                                    " bytes which is currently unsupported");
            }
        }

        /// <summary> Convert an InPtr to a String. </summary>
        /// <param name="Pointer"> The pointer. </param>
        /// <returns> A String. </returns>
        public static string Umg_PtrToString(IntPtr Pointer) {
            var retval = "";
            if (Pointer.ToInt64() != 0)
                retval = Marshal.PtrToStringAnsi(Pointer);
            return retval;
        }

        /// <summary>
        ///  Copy an IntPtr memory pointer to an actual Data Structure, Typically used for unamanged data
        ///  Note the data is copied from the pointer (unmanaged) to the Structure (managed)
        /// </summary>
        /// <remarks>
        ///  The pointer can be freed after the copy, but if the memory was allocated via an external
        ///  source (e.g. a DLL) then it must also be freed via the external source.
        /// </remarks>
        /// <param name="Pointer"> The pointer. </param>
        /// <returns> A customtype. </returns>
        public static TCustomtype Umg_PtrToStructure<TCustomtype>(IntPtr Pointer) {
            if (Pointer.ToInt64() == 0)
                return default(TCustomtype);
            var retval = (TCustomtype) Marshal.PtrToStructure(Pointer, typeof (TCustomtype));
            return retval;
        }

        /// <summary>
        ///  Copy a Data Structure to an IntPtr, Typically used for unamanged data Note the data is copied
        ///  from the structure (managed) to the Pointer (unmanaged)
        /// </summary>
        /// <remarks>
        ///  The pointer must point to a block of memory that has already been allocated ether via an
        ///  external source (e.g. a DLL) or via managed code in .net (e.g. UnmgdMemAlloc)
        /// </remarks>
        /// <param name="ptr">   The pointer. </param>
        /// <param name="Struc"> [in,out] The struc. </param>
        /// <returns> An IntPtr. </returns>
        public static IntPtr Umg_StructureToPtr<TCustomtype>(IntPtr ptr, ref TCustomtype Struc) {
            var retval = ptr;
            if (Struc == null)
                return retval;
            Marshal.StructureToPtr(Struc, retval, false);
            return retval;
        }

        /// <summary>
        ///  Copy a Byte Array to an IntPtr, Typically used for unamanged data Note the data is copied
        ///  from the structure (managed) to the Pointer (unmanaged)
        /// </summary>
        /// <remarks>
        ///  The pointer must point to a block of memory that has already been allocated ether via an
        ///  external source (e.g. a DLL) or via managed code in .net (e.g. UnmgdMemAlloc)
        /// </remarks>
        /// <param name="pointer"> The pointer. </param>
        /// <param name="input">   The input. </param>
        /// <returns> An IntPtr. </returns>
        public static IntPtr Umg_CByteArrToPtr(IntPtr pointer, byte[] input) {
            var retval = pointer;
            if (input == null)
                throw (new System.Exception("Byte Array conversion Error"));
            Marshal.Copy(input, 0, retval, input.Length);
            return retval;
        }

        /// <summary> Copy Data from an unmanaged memory block to a Byte Array. </summary>
        /// <param name="pointer"> The pointer. </param>
        /// <param name="length">  The length. </param>
        /// <param name="Offset">  The offset. </param>
        /// <returns> A Byte() </returns>
        public static byte[] Umg_PtrToCByteArr(IntPtr pointer, int length, int Offset = 0) {
            var ret = new byte[length + 1];
            Marshal.Copy(pointer, ret, Offset, length);
            return ret;
        }

        /// <summary> Convert a Unmanaged Pointer to a List of Objects. </summary>
        /// <param name="Pointer">  The pointer. </param>
        /// <param name="MaxCount"> Number of maximums. </param>
        /// <returns> A list of. </returns>
        public static ReadOnlyCollection<TCustomtype> Umg_PtrToList<TCustomtype>(IntPtr Pointer,
            int MaxCount = int.MaxValue) {
            var retval = new List<TCustomtype>();

            // Set the Value Pointer to the First Item in the List
            var arrayPtr = Pointer;
            var valPtr = Umg_PtrToStructure<IntPtr>(arrayPtr);

            // Loop Until no more items are left
            while (valPtr.ToInt64() != 0 && MaxCount > 0) {
                // Pull the Next value from the Array
                object tmpobj;
                if (typeof (TCustomtype) == typeof (string))
                    tmpobj = Umg_PtrToString(valPtr);
                else
                    tmpobj = Umg_PtrToStructure<TCustomtype>(valPtr);
                retval.Add((TCustomtype) tmpobj);
                // Increment the Array Pointer to the next value in the array
                arrayPtr = new IntPtr(arrayPtr.ToInt64() + IntPtr.Size);
                valPtr = Umg_PtrToStructure<IntPtr>(arrayPtr);
                MaxCount -= 1;
            }

            return retval.AsReadOnly();
        }

        /// <summary>
        ///  Allocates unmanaged memory, and returns a pointer This form just allocates the memory block,
        ///  without copying data in A memory Pointer of the type IntPtr is returned Note this memory must
        ///  be freed via Marshal.FreeHGlobal.
        /// </summary>
        /// <param name="length"> The length. </param>
        /// <returns> An IntPtr. </returns>
        public static IntPtr UmgMem_Alloc(int length) {
            if (length < 1)
                throw new System.Exception("Memory Length conversion Error");
            var retval = Marshal.AllocHGlobal(length);
            return retval;
        }

        /// <summary>
        ///  Allocates unmanaged memory, and returns a pointer This form copies a managed Structure into
        ///  the memory that has been allocated A memory Pointer of the type IntPtr is returned Note this
        ///  memory must be freed via Marshal.FreeHGlobal.
        /// </summary>
        /// <param name="Struc"> The struc. </param>
        /// <returns> An IntPtr. </returns>
        public static IntPtr UmgMem_Alloc<TCustomtype>(TCustomtype Struc) {
            if (Struc == null)
                throw new System.Exception("Structure conversion Error");
            var retval = Marshal.AllocHGlobal(Marshal.SizeOf(Struc));
            Marshal.StructureToPtr(Struc, retval, false);
            return retval;
        }

        /// <summary>
        ///  Allocates unmanaged memory, and returns a pointer This form copies a managed Byte Array into
        ///  the memory that has been allocated A memory Pointer of the type IntPtr is returned Note this
        ///  memory must be freed via Marshal.FreeHGlobal.
        /// </summary>
        /// <param name="input"> The input. </param>
        /// <returns> An IntPtr. </returns>
        public static IntPtr UmgMem_Alloc(byte[] input) {
            if (input == null)
                throw new System.Exception("Byte Array conversion Error");
            var retval = Marshal.AllocHGlobal(input.Length);
            Marshal.Copy(input, 0, retval, input.Length);
            return retval;
        }

        /// <summary> De-Allocate / Free memory allocated via the above. </summary>
        /// <param name="pointer"> The pointer. </param>
        public static void UmgMem_DeAlloc(IntPtr pointer) {
            Marshal.FreeHGlobal(pointer);
        }
    }
}