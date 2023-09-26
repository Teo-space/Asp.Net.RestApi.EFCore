using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Web.Forums.Infrastructure.EntityFrameworkCore;

public static class DependencyInjection__ForumsInfrastructure
{
	public static void AddForumInfrastructure(this WebApplicationBuilder builder)
	{
		builder.Services.AddLogging();

		builder.Services.AddFluentValidationAutoValidation();
		builder.Services.AddFluentValidationClientsideAdapters();
		builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


		builder.Services.AddDbContext<AppDbContext>(options =>
		{
			options.UseSqlite($"FileName=Data/Forum.DbContext.db");
			//options.UseSqlServer(connectionString)
		});

	}

	public static void AddForumTestingInfrastructure(this WebApplicationBuilder builder)
	{
        builder.Services.AddLogging();

        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


        builder.Services.AddDbContext<AppDbContext>(options =>
		{
			options.UseInMemoryDatabase("ForumTest");
		});

	}

}