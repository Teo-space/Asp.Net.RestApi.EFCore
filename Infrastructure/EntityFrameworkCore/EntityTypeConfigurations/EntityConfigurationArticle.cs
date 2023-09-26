namespace Web.Forums.Infrastructure.EntityFrameworkCore.EntityTypeConfigurations;
using Domain;


public class EntityConfigurationArticle : IEntityTypeConfiguration<Article>
{
	public void Configure(EntityTypeBuilder<Article> builder)
	{
		builder.ToTable("Articles");

        builder.HasIndex(x => x.ArticleVersionId).IsDescending().IsClustered(true);
        builder.HasIndex(x => x.ArticleId).IsDescending();
        builder.HasIndex(x => x.Title).IsDescending();

        builder.Property(x => x.Title).HasMaxLength(100).IsConcurrencyToken();
        builder.Property(x => x.Description).HasMaxLength(500).IsConcurrencyToken();
        builder.Property(x => x.Text).HasMaxLength(500).IsConcurrencyToken();

        //Versions
        builder.HasMany(x => x.Versions)
            .WithOne()
            .HasPrincipalKey(x => x.ArticleId)
            .HasForeignKey(x => x.ArticleId)
            .OnDelete(DeleteBehavior.NoAction);
    }



}
