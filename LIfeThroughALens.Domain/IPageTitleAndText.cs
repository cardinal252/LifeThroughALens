using Glass.Mapper.Sc.Fields;

namespace LifeThroughALens.Domain
{
    public interface IPageTitleAndText : ISitecoreItem
    {
        string Title { get; set; }

        string Text { get; set; }

        Image SectionImage { get; set; }
    }
}
