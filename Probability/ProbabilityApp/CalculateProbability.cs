namespace ProbabilityApp.Calculate;

using ProbabilityApp.Interface;
using ProbabilityApp.Model;
public class CalculateProbability : ICalculateProbability
{
    private readonly Probability probability;

    public CalculateProbability(Probability _probability)
    {
        probability = _probability;
    }
    
    public decimal CombinedWith => probability.probabilityA * probability.probabilityB;   
    
    public decimal Either => probability.probabilityA + probability.probabilityB - CombinedWith;

}