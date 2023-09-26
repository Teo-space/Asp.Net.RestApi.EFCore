namespace UseCases;

public record QueryArticleDisplay(Guid ArticleId, Guid ArticleVersionId)
{
    public class Validator : AbstractValidator<QueryArticleDisplay>
    {
        public Validator()
        {
            RuleFor(x =>  x.ArticleId).NotNull();
            RuleFor(x =>  x.ArticleVersionId).NotNull();
        }
    }
}

