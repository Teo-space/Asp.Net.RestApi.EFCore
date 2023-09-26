namespace UseCases;

public record CommandArticleCreate(string Title, string Description, string Text)
{
    public class Validator : AbstractValidator<CommandArticleCreate>
    {
        public Validator()
        {
            RuleFor(x =>  x.Title).NotNull().NotEmpty().MinimumLength(8).MaximumLength(50);
            RuleFor(x =>  x.Description).NotNull().NotEmpty().MinimumLength(10).MaximumLength(100);
            RuleFor(x =>  x.Text).NotNull().NotEmpty().MinimumLength(100).MaximumLength(10000);



        }
    }
}

