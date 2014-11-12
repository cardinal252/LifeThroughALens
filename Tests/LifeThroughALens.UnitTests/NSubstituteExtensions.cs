using System;
using NSubstitute;

namespace LifeThroughALens.UnitTests
{
    public static class NSubstituteExtensions
    {
        public static void Throws<T>(this object value) where T : Exception, new()
        {
            value.Returns((info) => { throw new T(); });
        }
    }
}
