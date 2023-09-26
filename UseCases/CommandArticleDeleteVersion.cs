namespace UseCases;

public record CommandArticleDeleteVersion(Guid ArticleId, Guid ArticleVersionId)
{
    public class Validator : AbstractValidator<CommandArticleDeleteVersion>
    {
        public Validator()
        {
            RuleFor(x => x.ArticleId).NotNull();
            RuleFor(x => x.ArticleVersionId).NotNull();
        }
    }
}

