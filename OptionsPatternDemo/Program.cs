using OptionsPatternDemo.Components;
using OptionsPatternDemo.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddOptions<CloudInfoOptions>()
    .BindConfiguration("CloudInfo")
    .Validate(opts =>
     !string.IsNullOrWhiteSpace(opts.Storage) &&
     !string.IsNullOrWhiteSpace(opts.Website) &&
     !string.IsNullOrWhiteSpace(opts.API),
     "Cloud Info options Failed Validation");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
