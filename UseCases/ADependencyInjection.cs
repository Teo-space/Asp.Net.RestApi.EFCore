using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class ADependencyInjection
{
    public static WebApplicationBuilder AddUseCases(this WebApplicationBuilder builder)
    {
        builder.Services.AddLogging();

        builder.Services.AddFluentValidation();
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();





        return builder;
    }







}
