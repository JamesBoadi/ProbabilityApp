namespace ProbabilityApp.Service;

using System;

using ProbabilityApp.Model;
using ProbabilityApp.Calculate;
using Microsoft.AspNetCore.Http.HttpResults;

public sealed class CalculationService
{
    decimal ans { get; set; }

    public Ok<decimal> CalculateProbability(Probability probability, string functionType)
    {
        if (string.IsNullOrEmpty(functionType))
            throw new ArgumentNullException();

        CalculateProbability calcuateProbability = new(probability);

        if (functionType.Equals("CombinedWithFunction"))
            ans = calcuateProbability.CombinedWith;
        else if (functionType.Equals("EitherFunction"))
            ans = calcuateProbability.Either;
        else
            throw new ArgumentException("Invalid Function");

        return TypedResults.Ok(ans);
    }
}