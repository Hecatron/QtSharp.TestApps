// Used for containing representations of low level C Structures
// Also keeps tabs on memory pointers, and if the underlying memory was allocated
// via us (.Net) or the DLL

// The best way to think of unmanaged memory is a block of no-mans land that sits between VB .Net / the DLL
// Typically you have a memory pointer which points to the first byte of the block
// and a way of letting the operating system know that you want to allocate a block for use

// If a block is allocated by something outside of the .Net platform then it needs to be the one to unallocate it
// In the same way if .Net allocates memory then .Net needs to free it up
// Because the Garbage collector can't always capture this automatically for unmanaged code areas
// We use the below class as a sort of wrapper to auto clean up after itself, and to interpret the data into
// a structure of some kind that we can read

using System;
using System.Runtime.InteropServices;

namespace QtSharpHelper.CHelper
{

	/// <summary>
	///  Represents an unmanaged C Structure held in memory
	///  
	///  Note Pointer is a memory address pointer to the block of unmanaged memory and struct is a
	///  representation of what the data looks like (a copy)
	///  
	///  Unmanaged means the block of data was ether created outside of the .Net platform (e.g.
	///  allocated via a DLL) or allocated via UnmgdMem_Alloc Any memory allocated via a DLL must be
	///  freed via the DLL (usually via a function call)
	///  and any memory allocated via UnmgdMem_Alloc in .Net must be freed via UnmgdMem_DeAlloc.
	/// </summary>
	public class CStruct<TCustomtype>
	{

#region Types

		/// <summary> Determines how the unmanaged memory that is pointed to was allocated. </summary>
		public enum AllocatedMode
		{

			/// <summary>
			/// No Memory allocated at the pointer
			/// </summary>
			None = 0,

			/// <summary>
			/// Memory was allocated by .Net via UnmgdMem_Alloc
			/// and must be freed via UnmgdMem_DeAlloc
			/// </summary>
			viaNet = 1,

			/// <summary>
			/// Memory was allocated by a DLL or external source via C new
			/// and must be freed via the Dll (usually via a function call)
			/// </summary>
			viaDll = 2,

			/// <summary>
			/// The memory pointer points to something, but it's just a piece of a larger memory block
			/// which means we're not worried about if the memory is allocated / de-allocated
			/// </summary>
			Nested = 3

		}

#endregion

#region Properties

		/// <summary> Memory Address pointer to the block of memory which is unmanaged. </summary>
		/// <value> The pointer. </value>
		public IntPtr Pointer {get; set;}

		/// <summary> Keep track of how the memory was allocated. </summary>
		/// <value> The pointer allocated. </value>
		public AllocatedMode Pointer_Allocated {get; set;}

		/// <summary>
		///  Structure which is a direct copy of the unamanged data stored in a managed object (structure)
		///  Note accessing this structure does not directly access the unamanged data you still need to
		///  copy from this to / from the above memory pointer to access / change the unmanaged memory.
		/// </summary>
		/// <remarks>
		///  any structures used here should be declared StructLayout(LayoutKind.Sequential)
		/// </remarks>
		/// <value> The structure. </value>
		public TCustomtype Struct {get; set;}

#endregion

#region Constructors

		/// <summary> Default Empty Constructor. </summary>
		public CStruct()
		{
			Pointer = IntPtr.Zero;
			Pointer_Allocated = AllocatedMode.None;
			Struct = default(TCustomtype);
		}

		/// <summary> Constructor based on a memory pointer. </summary>
		/// <param name="ptr_param">   The pointer parameter. </param>
		/// <param name="Alloc_param"> The allocate parameter. </param>
		public CStruct(IntPtr ptr_param, AllocatedMode Alloc_param)
		{
			Pointer = ptr_param;
			Pointer_Allocated = Alloc_param;
			CopyPtrToStruct();
		}

		/// <summary> Constructor based on a Structure. </summary>
		/// <exception cref="ArgumentException"> Thrown when one or more arguments have unsupported or
		///                                      illegal values. </exception>
		/// <param name="struc_param"> The struc parameter. </param>
		/// <param name="Alloc_param"> The allocate parameter. </param>
		public CStruct(TCustomtype struc_param, AllocatedMode Alloc_param = AllocatedMode.None)
		{
			// Make a copy of the supplied Structure
			Struct = struc_param;

			if (Alloc_param == AllocatedMode.None)
			{
				Pointer = IntPtr.Zero;
				Pointer_Allocated = AllocatedMode.None;
			}
			else if (Alloc_param == AllocatedMode.viaNet)
			{
				// Do we need to allocate some memory to copy this into (unmanaged)
				MemoryAllocate();
				CopyStructToPtr();
			}
			else if (Alloc_param == AllocatedMode.viaDll)
			{
				throw new ArgumentException("AllocatedMode = viaDll not supported in this context");
			}
			else if (Alloc_param == AllocatedMode.Nested)
			{
				throw new ArgumentException("AllocatedMode = Nested not supported in this context");
			}
		}


#endregion

#region Destructors

		/// <summary> Called by the user to close / de-allocate all memory. </summary>
		public void Dispose()
		{
			// If the memory has been allocated via .Net then make sure we free it
			if (Pointer_Allocated == AllocatedMode.viaNet)
			{
				MemoryFree();
			}
			// If the memory has been allocated outside .Net then the pointer should already have been freed
			// therefore mark the block as freed as Dispose is called by the user
			if (Pointer_Allocated == AllocatedMode.viaDll)
			{
				Pointer_Allocated = AllocatedMode.None;
				Pointer = IntPtr.Zero;
				Struct = default(TCustomtype);
			}
		}

		/// <summary>
		///  Called by the .Net platform when the class goes out of scope and needs to be destroyed by the
		///  garbage collector.
		/// </summary>
		~CStruct()
		{
			// If the memory has been allocated via .Net then make sure we free it
			if (Pointer_Allocated == AllocatedMode.viaNet)
			{
				MemoryFree();
			}
			// If the memory has been allocated outside .Net then the
			// pointer should already have been freed and this should never happend
			// therefore constitutes a memory leak
			if (Pointer_Allocated == AllocatedMode.viaDll)
			{
				//Throw New Exception("Memory Leak detected with the dll, class has been closed but memory is still allocated")
			}
		}

#endregion

#region Methods

		/// <summary> Allocate memory for the unmanaged data block. </summary>
		/// <returns> true if it succeeds, false if it fails. </returns>
		public bool MemoryAllocate()
		{
			if (Pointer_Allocated == AllocatedMode.None)
			{
				Pointer = CHelper.UmgMem_Alloc(Marshal.SizeOf(Struct));
				Pointer_Allocated = AllocatedMode.viaNet;
				return true;
			}
			return false;
		}

		/// <summary> Free up any Allocated unmanaged memory. </summary>
		/// <returns> true if it succeeds, false if it fails. </returns>
		public bool MemoryFree()
		{
			if (Pointer_Allocated == AllocatedMode.viaNet)
			{
				CHelper.UmgMem_DeAlloc(Pointer);
				Pointer_Allocated = AllocatedMode.None;
				Pointer = IntPtr.Zero;
				return true;
			}
			return false;
		}

		/// <summary> Copy the unmanaged memory from the address pointer into the structure. </summary>
		/// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
		public void CopyPtrToStruct()
		{
			if (Pointer_Allocated == AllocatedMode.None || Pointer.ToInt64() == 0)
			{
				throw new Exception("Error Pointer undefined, no data to copy from");
			}
			Struct = CHelper.Umg_PtrToStructure<TCustomtype>(Pointer);
		}

		/// <summary>
		///  Copy the structure into the memory allocated at the address pointer Note the memory pointed
		///  to via pointer must be allocated ether via the dll or via .net.
		/// </summary>
		public void CopyStructToPtr()
		{
			if (Pointer_Allocated == AllocatedMode.None)
			{
				MemoryAllocate();
			}
		    var customtype = Struct;
		    Pointer = CHelper.Umg_StructureToPtr(Pointer, ref customtype);
		}

#endregion

	}

}
