var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewOptions(options => options.HtmlHelperOptions.ClientValidationEnabled = true);

builder.Services.AddHttpClient("EventosAuth", client =>
{
    client.BaseAddress = new Uri("https://localhost:7015/");
});

builder.Services.AddHttpClient("EventosEvento", client =>
{
    client.BaseAddress = new Uri("https://localhost:7153/");
});

builder.Services.AddHttpClient("EventosCertificado", client =>
{
    client.BaseAddress = new Uri("http://localhost:8080/");
});

builder.Services.AddSession();

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
