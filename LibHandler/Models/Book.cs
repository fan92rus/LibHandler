namespace LibHandler.Models
{
    #pragma warning disable IDE1006
    #pragma warning disable CS8618

    public class Article
    {
        public string Title { get; set; }
        public string LibgenTopic { get; set; }
        public string Type { get; set; }
        public string SeriesName { get; set; }
        public string TitleAdd { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string City { get; set; }
        public string Edition { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Pages { get; set; }
        public string EditionsAddInfo { get; set; }
        public string CoverUrl { get; set; }
        public string IssueSId { get; set; }
        public string IssueNumberInYear { get; set; }
        public string IssueYearNumber { get; set; }
        public string IssueNumber { get; set; }
        public string IssueVolume { get; set; }
        public string IssueSplit { get; set; }
        public string IssueTotalNumber { get; set; }
        public string IssueFirstPage { get; set; }
        public string IssueLastPage { get; set; }
        public string IssueYearEnd { get; set; }
        public string IssueMonthEnd { get; set; }
        public string IssueDayEnd { get; set; }
        public string IssueClosed { get; set; }
        public string Doi { get; set; }
        public string TimeAdded { get; set; }
        public string TimeLastModified { get; set; }
        public string Visible { get; set; }
        public string Editable { get; set; }
        public string Commentary { get; set; }
        public string CoverExists { get; set; }

        public Dictionary<string, ArticleFile> Files { get; set; }
    }

    public class ArticleFile
    {
        public string FId { get; set; }
        public string Md5 { get; set; }
        public string TimeAdded { get; set; }
        public string TimeLastModified { get; set; }
    }

// Корневой словарь: id статьи -> объект
    public class ArticlesRoot : Dictionary<string, Article>
    {
    }
}
