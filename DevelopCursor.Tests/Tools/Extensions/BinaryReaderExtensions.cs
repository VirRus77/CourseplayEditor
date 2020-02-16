using System.IO;
using System.Runtime.InteropServices;

namespace DevelopCursor.Tests.Tools.Extensions
{
    public static class BinaryReaderExtensions
    {
        public static T ByteToType<T>(this BinaryReader reader)
        {
            byte[] bytes = reader.ReadBytes(Marshal.SizeOf(typeof(T)));

            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            var theStructure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();

            return theStructure;
        }
    }
}
