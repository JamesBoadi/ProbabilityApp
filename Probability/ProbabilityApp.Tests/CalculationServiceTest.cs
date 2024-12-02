namespace ProbabilityApp.Tests;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;


public class CalculationServiceTest
{
    string functionType { get; set; }
    Probability probabilityObj { get; set; }
    CalculationService calculationService = new();
    
    private void SetProbabilityAndFunction(decimal _probabilityA,
    decimal _probabilityB, string _functionType)
    {

        Probability _probabilityObj = new()
        {
            probabilityA = _probabilityA,
            probabilityB = _probabilityB
        };

        functionType = _functionType;
        probabilityObj = _probabilityObj;
    }

    public static IList<ValidationResult> ValidateModel(object model)
    {
        var results = new List<ValidationResult>();
        var validationContext = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, validationContext, results, validateAllProperties: true);

        return results;
    }

    [Fact]
    public void Validate_If_Probability_Is_Between_0_And_1()
    {
        Probability _probabilityObj = new()
        {
            probabilityA = 0.4m,
            probabilityB = -1m
        };

        var validationResults = ValidateModel(_probabilityObj);

        Assert.NotEmpty(validationResults);
        Assert.Single(validationResults);
        Assert.Equal("probabilityB must be between 0 and 1", validationResults[0].ErrorMessage);
    }

    [Fact]
    public void Throws_Exception_If_Invalid_Function_Passed()
    {
        SetProbabilityAndFunction(0.4m, 0.3m, "GameFunction");
        var result = Assert.Throws<ArgumentException>(() => calculationService.CalculateProbability(probabilityObj, functionType));
        Assert.Equal("Invalid Function", result.Message);

        SetProbabilityAndFunction(0.4m, 0.3m, "");
        result = Assert.Throws<ArgumentNullException>(() => calculationService.CalculateProbability(probabilityObj, functionType));
        Assert.Equal("Value cannot be null.", result.Message);

        SetProbabilityAndFunction(0.4m, 0.3m, null);
        result = Assert.Throws<ArgumentNullException>(() => calculationService.CalculateProbability(probabilityObj, functionType));
        Assert.Equal("Value cannot be null.", result.Message);
    }

  
    [Fact]
    public void EitherFunction_Returns_Correct_Result()
    {
        SetProbabilityAndFunction(0.4m, 0.3m, "EitherFunction");
        var result = calculationService.CalculateProbability(probabilityObj, functionType);

        Assert.IsType<Ok<decimal>>(result);
        Assert.Equal(0.58m, result.Value);

        SetProbabilityAndFunction(0m, 0m, "EitherFunction");
        result = calculationService.CalculateProbability(probabilityObj, functionType);

        Assert.Equal(0m, result.Value);
    }

    [Fact]
    public void CombinedFunction_Returns_Correct_Result()
    {
        SetProbabilityAndFunction(0.4m, 0.3m, "CombinedWithFunction");
        var result = calculationService.CalculateProbability(probabilityObj, functionType);

        Assert.IsType<Ok<decimal>>(result);
        Assert.Equal(0.12m, result.Value);

        SetProbabilityAndFunction(0.5m, 0.5m, "CombinedWithFunction");
        result = calculationService.CalculateProbability(probabilityObj, functionType);
        Assert.Equal(0.25m, result.Value);

        SetProbabilityAndFunction(1m, 0.1m, "CombinedWithFunction");
        result = calculationService.CalculateProbability(probabilityObj, functionType);
        Assert.Equal(0.1m, result.Value);
    }
}