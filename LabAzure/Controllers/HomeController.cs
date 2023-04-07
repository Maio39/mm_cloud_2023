using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LabAzure.Models;
using System.Text.Json.Nodes;
using System;

namespace LabAzure.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _httpClient;
    private readonly HttpClient _httpPoke;

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory FactoryApi)
    {
        _logger = logger;
        _httpClient = FactoryApi.CreateClient("InfoIp");
        _httpPoke = FactoryApi.CreateClient("poke");
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult GetPokeInfo()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> GetPokeInfo(string pokeName)
    {
        string url = $"{_httpPoke.BaseAddress}/pokemon/{pokeName}";
        var res = await _httpClient.GetAsync(url);
        if (res.IsSuccessStatusCode)
        {
            string info = await res.Content.ReadAsStringAsync();
            var json = JsonObject.Parse(info);
            List<string> infoPoke = new List<string>();
            if (json != null)
            {
                if (json!["abilities"] != null)
                {
                    infoPoke.Add("Name: "+json!["name"].AsValue().ToString());
                    infoPoke.Add("Height: "+json!["height"].AsValue().ToString());
                    infoPoke.Add("Weight: "+json!["weight"].AsValue().ToString());
                    infoPoke.Add("Base Experience: "+json!["base_experience"].AsValue().ToString());
                }
                else
                {
                    infoPoke.Add(info);
                }
                ViewData["pokeinfo"] = infoPoke;
            }
        }
        return View();
    }

    public IActionResult GetIP()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> GetIP(string IP)
    {
        /*
        var _httpIp = new HttpClient();
        _httpIp.BaseAddress = new Uri($"https://ipapi.co/{IP}/json/");
        _httpIp.DefaultRequestHeaders.Add("ntanti", "biriguda");
        */
        string url = $"{_httpClient.BaseAddress}{IP}/json/";
        var res = await _httpClient.GetAsync(url);
        if(res.IsSuccessStatusCode)
        {
            string info = await res.Content.ReadAsStringAsync();
            //
            var json = JsonObject.Parse(info);
            List<string> infoIp = new List<string>();
            if(json != null)
            {
                if (json!["city"] != null)
                {
                    infoIp.Add(json!["ip"].AsValue().ToString());
                    infoIp.Add(json!["version"].AsValue().ToString());
                    infoIp.Add(json!["city"].AsValue().ToString());
                    infoIp.Add(json!["region"].AsValue().ToString());
                }
                else
                {
                    infoIp.Add(info);
                }
            }
            //*/
            ViewData["IP"] = info;
            ViewData["IpList"] = infoIp;
            //ViewData["IPAddress"] = json!["ip"].AsValue().ToString();
        }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
