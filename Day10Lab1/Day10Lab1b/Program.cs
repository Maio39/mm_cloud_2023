using Day10Lab1;
using Day10Lab1c;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Press Return to Start!");

Console.ReadLine();
/*/WeatherForecast
HttpClient MyCn = new HttpClient();
string URIBASE = "http://localhost:5068/WeatherForecast";

HttpResponseMessage response = await MyCn.GetAsync(URIBASE);
if (response.IsSuccessStatusCode)
{
    //ok
    WeatherForecast[] Antani = await response.Content.ReadFromJsonAsync<WeatherForecast[]>();
    foreach(var w in Antani)
    {
        Console.WriteLine("--------------- Weather Result ---------------");
        Console.WriteLine($"{w.Date.ToLocalTime()} temp {w.TemperatureC} ");
    }
}
else
{
    Console.WriteLine($"Sorry: {response.StatusCode}: {response.ReasonPhrase}");
}
//*/

HttpClient MyCn = new HttpClient();
string URIBASE = "http://localhost:5068/api/ToDoItems";

Console.WriteLine("Inserisci Un nuovo todo");
Console.WriteLine("Inserisci Titolo");
string Title = Console.ReadLine();
Console.WriteLine("Inserisci Descrizione");
string Description = Console.ReadLine();
Console.WriteLine("Inserisci priorita");
string prior = Console.ReadLine();
int Priority = Int32.Parse(prior);

ToDoItem mio = new ToDoItem()
{
    Title = Title,
    Description = Description,
    DueDate = DateTime.Now.AddDays(2),
    CreationDate = DateTime.Now,
    PriorityLevel = Priority,
    IsDone = false,
    IsMandatory = false
};

var response2 = await MyCn.PostAsJsonAsync<ToDoItem>(URIBASE, mio);
if (response2.IsSuccessStatusCode)
{
    Console.WriteLine("To Do Inserito Correttamente");    
}
else
{
    Console.WriteLine($"Sorry: {response2.StatusCode}: {response2.ReasonPhrase}");
}

HttpResponseMessage response = await MyCn.GetAsync(URIBASE);
if (response.IsSuccessStatusCode)
{
    ToDoItem[] todo = await response.Content.ReadFromJsonAsync<ToDoItem[]>();
    foreach (var td in todo)
    {
        Console.WriteLine("--------------- To Do ---------------");
        Console.WriteLine(td);
    }

}
else
{
    Console.WriteLine($"Sorry: {response.StatusCode}: {response.ReasonPhrase}");
}



Console.ReadLine();