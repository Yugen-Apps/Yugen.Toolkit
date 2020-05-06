using System;

namespace Yugen.Toolkit.Standard.Helpers
{
    public static class GuidHelper
    {
        public static Guid Int2Guid(int value) =>
            new Guid(value, 0, 0, new byte[8]);

        public static int Guid2Int(Guid value)
        {
            var b = value.ToByteArray();
            var bint = BitConverter.ToInt32(b, 0);
            return bint;
        }
    }
}