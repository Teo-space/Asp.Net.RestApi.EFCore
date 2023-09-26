namespace Asp.Net.RestApi.EFCore.Controllers;

using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UseCases;
using Web.Forums.Infrastructure.EntityFrameworkCore;




[ApiController]
[Route("[controller]")]
public class ArticlesController(AppDbContext appDbContext) : ControllerBase
{
    /// <summary>
    /// Получение одиночной статьи
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    public Task<Article> ArticleDisplay(QueryArticleDisplay request)
    {
        return appDbContext.Set<Article>()
            .Where(x => x.ArticleId         == request.ArticleId && 
                        x.ArticleVersionId  == request.ArticleVersionId)
            .FirstAsync();
    }

    /// <summary>
    /// Получение всех версий
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("Versions/")]
    public async Task<IReadOnlyCollection<Article>> ArticleVersions(QueryArticleVersions request)
    {
        return (await appDbContext.Set<Article>()
            .Where(x => x.ArticleId == request.ArticleId)
            .ToListAsync());
    }

    /// <summary>
    /// Создание базовой версии
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Article> ArticleCreate(CommandArticleCreate request)
    {
        var article= Article.Create(request.Title, request.Description, request.Text);
        await appDbContext.AddAsync(article);
        await appDbContext.SaveChangesAsync();
        return article;
    }

    /// <summary>
    /// Создание дочерней версии от базовой
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("Version/")]
    public async Task<Article> ArticleCreate(CommandArticleCreateVersion request)
    {
        var baseArticle = await appDbContext.Set<Article>()
            .Where(x => x.ArticleId         == request.ArticleId &&
                        x.ArticleVersionId  == request.ArticleId)
            .FirstOrDefaultAsync();
        if(baseArticle == null)
        {
            return default(Article);
        }

        var articleVersion = baseArticle.CreateVersion(/*request.Title,*/ request.Description, request.Text);
        await appDbContext.AddAsync(articleVersion);
        await appDbContext.SaveChangesAsync();
        return articleVersion;
    }

    /// <summary>
    /// Удаление всех версий
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<Article> ArticleDelete(CommandArticleDelete request)
    {
        var baseArticle = await appDbContext.Set<Article>()
            .Where(x => x.ArticleId == request.ArticleId &&
                        x.ArticleVersionId == request.ArticleId)
            .Include(x => x.Versions)
            .FirstOrDefaultAsync();
        if (baseArticle == null)
        {
            return default(Article);
        }
        appDbContext.Set<Article>().RemoveRange(baseArticle.Versions);
        await appDbContext.SaveChangesAsync();
        return baseArticle;
    }

    /// <summary>
    /// Удаление конкретной версии
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpDelete("Version/")]
    public async Task<Article> ArticleDelete(CommandArticleDeleteVersion request)
    {
        if(request.ArticleId == request.ArticleVersionId)
        {
            return await ArticleDelete(new CommandArticleDelete(request.ArticleId));
        }

        var article = await appDbContext.Set<Article>()
            .Where(x => x.ArticleId == request.ArticleId &&
                        x.ArticleVersionId == request.ArticleVersionId)
            .FirstOrDefaultAsync();
        if (article == null)
        {
            return default(Article);
        }
        appDbContext.Set<Article>().Remove(article);
        await appDbContext.SaveChangesAsync();
        return article;
    }



}