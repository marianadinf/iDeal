using System;
using Seterlund.CodeGuard;

namespace UIT.iDeal.Common.Data
{
    public static class GuidComb
    {
        static int _sequenceNumber = 1;

        public static void Reset(int seed = 1)
        {
            Guard.That(seed).IsGreaterThan(0);
            _sequenceNumber = seed;
        }
       
        public static Guid Generate()
        {
            var b = new byte[16];
            var bytes = BitConverter.GetBytes(_sequenceNumber++);
            b[12] = bytes[3];
            b[13] = bytes[2];
            b[14] = bytes[1];
            b[15] = bytes[0];
            return new Guid(b);
        }
    }
}