using BlazorApp1.Components;
using BlazorApp1.Data;
using BlazorApp1.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.JSInterop;
using QuestPDF.Infrastructure;
using System.Globalization;
using AppDbContext = BlazorApp1.Data.AppDbContext;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpContextAccessor();


builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions => sqlOptions.CommandTimeout(60)));

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddSingleton<DialogService>();
builder.Services.AddScoped<PdfExportService>();
builder.Services.AddScoped<CsvExportService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<UserSessionManager>();

QuestPDF.Settings.License = LicenseType.Community;





builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("tr"), new CultureInfo("mk") };
    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
});


var app = builder.Build();

app.MapGet("/set-culture", (string culture, string returnUrl, HttpContext httpContext) =>
{
    // Culture cookie’sini ayarla
    httpContext.Response.Cookies.Append(
        CookieRequestCultureProvider.DefaultCookieName,
        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
    );
    return Results.Redirect(returnUrl);
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

var supportedCultures2 = new[] { "en", "tr", "mk" };
var localizationOptions = new RequestLocalizationOptions();
localizationOptions.SetDefaultCulture(supportedCultures2[0]);
localizationOptions.AddSupportedCultures(supportedCultures2);
localizationOptions.AddSupportedUICultures(supportedCultures2);

app.UseStaticFiles(); 
app.UseRouting();
app.UseRequestLocalization(localizationOptions);
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
