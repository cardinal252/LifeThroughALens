namespace LifeThroughALens.Domain
{
    public interface ITheme : ISitecoreItem
    {
        string CssFile { get; set; }
    }
}
