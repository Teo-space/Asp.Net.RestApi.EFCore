namespace UseCases;

public record QueryArticleVersions(Guid ArticleId)
{
    public class Validator : AbstractValidator<QueryArticleVersions>
    {
        public Validator()
        {
            RuleFor(x =>  x.ArticleId).NotNull();
        }
    }
}

