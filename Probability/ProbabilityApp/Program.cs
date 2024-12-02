using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ProbabilityApp.Model;
using ProbabilityApp.Service;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddCors(options =>
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5000", "http://localhost:5289") // Allow specific origins
              .AllowAnyHeader()
              .AllowAnyMethod();
    }));

services.AddTransient<CalculationService>();

var app = builder.Build();
app.UseCors();

var serviceProvider = app.Services;

bool ValidateModel(object model)
{
    var results = new List<ValidationResult>();
    var validationContext = new ValidationContext(model, null, null);
    return Validator.TryValidateObject(model, validationContext, results, validateAllProperties: true);
}
   
app.MapPost("/calculateProbability", async (CalculationService calculationService, HttpContext httpContext) =>
{
    ContextHandler contextHandler = new(httpContext);

    string body = await contextHandler.GetBody();
    string functionType = contextHandler.GetHeader("Function-Type");
    var probability = JsonConvert.DeserializeObject<Probability>(body);

    if (!ValidateModel(probability))
        return Results.BadRequest("Invalid request data.");
    
    var res = calculationService.CalculateProbability(probability, functionType);
    return res;
})
.RequireCors("AllowSpecificOrigins");

app.Run();