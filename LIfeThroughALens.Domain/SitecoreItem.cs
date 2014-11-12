using Sitecore.Data;

namespace LifeThroughALens.Domain
{
    public class SitecoreItem : ISitecoreItem
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public ID Id { get; set; }
    }
}
