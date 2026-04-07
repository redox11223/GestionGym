using System;
using System.Net.Http.Headers;

namespace Autenticacion.API.Services;

public class AuthHeaderHandler:DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AuthHeaderHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization=AuthenticationHeaderValue.Parse(token);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
