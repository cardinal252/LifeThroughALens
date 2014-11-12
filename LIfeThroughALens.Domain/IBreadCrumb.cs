namespace LifeThroughALens.Domain
{
    public interface IBreadCrumb : ISitecoreItem
    {
        string BreadcrumbTitle { get; set; }
    }
}
