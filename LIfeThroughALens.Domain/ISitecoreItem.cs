using Sitecore.Data;

namespace LifeThroughALens.Domain
{
    public interface ISitecoreItem
    {
        string Name { get; set; }

        string Url { get; set; }

        ID Id { get; set; }
    }
}
