namespace Domain;


public class Article
{
    public Guid ArticleId { get; set; }
    public Guid ArticleVersionId { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }

    public string Text { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<Article> Versions { get; set; } = new List<Article>();



    public static Article Create(string Title, string Description, string Text)
    {
        var article = new Article();
        article.ArticleId = NUlid.Ulid.NewUlid().ToGuid();
        article.ArticleVersionId = article.ArticleId;

        article.Title = Title;
        article.Description = Description;
        article.Text = Text;
        article.CreatedAt = DateTime.Now;
        article.UpdatedAt = DateTime.Now;
        return article;
    }

    public Article CreateVersion(/*string Title,*/ string Description, string Text)
        => CreateVersion(this.ArticleId, this.Title, Description, Text);
    public static Article CreateVersion(Guid ArticleId, string Title, string Description, string Text)
    {
        var article = new Article();
        article.ArticleId = ArticleId;
        article.ArticleVersionId = NUlid.Ulid.NewUlid().ToGuid();

        article.Title = Title;
        article.Description = Description;
        article.Text = Text;
        article.CreatedAt = DateTime.Now;
        article.UpdatedAt = DateTime.Now;


        return article;
    }



    public Article Edit(string Description, string Text)
    {
        this.Description = Description;
        this.Text = Text;
        this.UpdatedAt = DateTime.Now;
        return this;
    }










}