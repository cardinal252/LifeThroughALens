using DateTime = System.DateTime;

namespace LifeThroughALens.Domain
{
    public interface IJob : ISitecoreItem
    {
        string Contact { get; set; }

        DateTime DeadlineDateTime { get;set; }
    }
}
