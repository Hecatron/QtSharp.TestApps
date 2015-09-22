Imports System.Runtime.InteropServices

' Based on code from http://stackoverflow.com/questions/13317931/marshal-an-array-of-strings-from-c-sharp-to-c-code-using-p-invoke

''' <summary> Helper class for generating C String arrays. </summary>
Public Class CStringArray

#Region "Properties"

    ''' <summary> List of Managed Strings. </summary>
    ''' <value> List of Managed Strings. </value>
    Public ReadOnly Property ManagedArray As String()
        Get
            Return _ManagedArray
        End Get
    End Property
    Protected Property _ManagedArray As String()

    ''' <summary> Handle to the allocated memory block. </summary>
    ''' <value> Handle to the allocated memory block. </value>
    Public ReadOnly Property Handle As GCHandle
        Get
            Return _Handle
        End Get
    End Property
    Protected Property _Handle As GCHandle

    ''' <summary> Array of Pointers. </summary>
    ''' <value> Array of Pointers. </value>
    Public ReadOnly Property PointerArray As IntPtr()
        Get
            Return _PointerArray
        End Get
    End Property
    Protected Property _PointerArray As IntPtr()

#End REgion

#Region "Methods"

    Public sub New (strarray As IEnumerable(Of string))
        _ManagedArray = strarray.ToArray
        _PointerArray = Nothing
    End sub

    Public Sub Alloc
        Dim PointerList as New List(Of IntPtr)
        For each item In ManagedArray
            PointerList.Add(Marshal.StringToHGlobalAnsi(item))
        Next
        _PointerArray = PointerList.ToArray
        _Handle = GCHandle.Alloc(PointerArray, GCHandleType.Pinned)
    End Sub

    Public Sub DeAlloc
        If _PointerArray Is Nothing then Exit Sub
        _Handle.Free
        For each item In _PointerArray
            Marshal.FreeHGlobal(item)
        Next
    End Sub

    ''' <summary> Return the Address of the String Array. </summary>
    Public Function Address As IntPtr
        If _PointerArray Is Nothing then Throw new ArgumentException("Memory has not been allocated")
        Return Handle.AddrOfPinnedObject
    End Function

#End Region

End Class
