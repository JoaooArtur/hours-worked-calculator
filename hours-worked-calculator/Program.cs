using APIs.WorkedHoursCalculator;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.ConfigureServices((context, services) =>
{
    services.AddApiVersioning(options => options.ReportApiVersions = true)
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
    services.AddFluentValidationAutoValidation()
        .AddFluentValidationClientsideAdapters()
        .AddValidatorsFromAssemblyContaining<Program>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.NewVersionedApi("WorkedHoursCalculator").MapWorkedHoursCalculatorApiV1();

app.MapControllers();
try
{
    await app.RunAsync();
}
catch (Exception ex)
{
    await app.StopAsync();
}
finally
{
    await app.DisposeAsync();
}
