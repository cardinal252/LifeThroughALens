using System;

namespace LifeThroughALens
{
    public interface ILog
    {
        void LogException(string error, Exception ex);
    }
}
