var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string uri = builder.Configuration["InfoIp:uri"];
builder.Services.AddHttpClient("InfoIp",client =>
{
    client.BaseAddress = new Uri(uri);
    client.DefaultRequestHeaders.Add("USER-AGENT", "Mysearchspooter");
});

string uripoke = builder.Configuration["poke:uri"];
builder.Services.AddHttpClient("poke", client =>
{
    client.BaseAddress=new Uri(uripoke);
    client.DefaultRequestHeaders.Add("token", "mysuperkey");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
