public sealed class ContextHandler
{
    private readonly HttpContext httpContext;
    public ContextHandler(HttpContext _httpContext) => httpContext = _httpContext;

    public async Task<string> GetBody()
    {
        string body = "";

        using (var reader = new StreamReader(httpContext.Request.Body))
        {
            body = await reader.ReadToEndAsync();
        }

        return body;
    }

    public string GetHeader(string headerType)
    {
        string header = "";

        if (httpContext.Request.Headers.TryGetValue(headerType, out var _header))
        {
            header = _header;
        }

        return header;
    }

}