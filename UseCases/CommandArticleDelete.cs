namespace UseCases;

public record CommandArticleDelete(Guid ArticleId)
{
    public class Validator : AbstractValidator<CommandArticleDelete>
    {
        public Validator()
        {
            RuleFor(x => x.ArticleId).NotNull();
        }
    }
}

