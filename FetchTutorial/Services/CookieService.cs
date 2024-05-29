using FetchTutorial.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json;

public interface ICookieService
{
    string Get(string key);
    void Set(string key, string value, int? expireTime);
    bool Exists(string key);
}

public class CookieService : ICookieService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CookieService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool Exists(string key)
    {
        return _httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(key);
    }

    public string Get(string key)
    {
        return _httpContextAccessor.HttpContext.Request.Cookies[key];
    }

    public void Set(string key, string value, int? expireTime)
    {
        CookieOptions option = new CookieOptions();

        if (expireTime.HasValue)
            option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
        else
            option.Expires = DateTime.Now.AddMilliseconds(10);

        _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, option);
    }

   
}
