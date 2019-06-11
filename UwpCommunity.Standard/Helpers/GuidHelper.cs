using System;

namespace UwpCommunity.Standard.Helpers
{
    public static class GuidHelper
    {
        public static Guid Int2Guid(int value) => 
            new Guid(value, 0, 0, new byte[8]);

        public static int Guid2Int(Guid value)
        {
            byte[] b = value.ToByteArray();
            int bint = BitConverter.ToInt32(b, 0);
            return bint;
        }
    }
}