public static class ServiceProviderEx
{ 
    public static T GetService<T>(this IServiceScopeFactory scopeFactory)
    {
        var scope = scopeFactory.CreateScope();
        return scope.ServiceProvider.GetService<T>(); 
    }
}