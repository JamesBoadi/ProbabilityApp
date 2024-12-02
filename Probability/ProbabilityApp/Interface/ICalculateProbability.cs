namespace ProbabilityApp.Interface;

public interface ICalculateProbability
{
    public decimal CombinedWith { get; }

    public decimal Either { get; }
}