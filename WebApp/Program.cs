using Microsoft.FluentUI.AspNetCore.Components;
using WebApp;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IAesKeyGenerator, AesKeyGenerator>();
builder.Services.AddScoped<IAesService, AesService>();
builder.Services.AddScoped<IRsaKeyGenerator, RsaKeyGenerator>();
builder.Services.AddScoped<IRsaService, RsaService>();
builder.Services.AddScoped<IHashService, HashService>();
builder.Services.AddScoped<IDigitalSignatureService, DigitalSignatureService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddSingleton<Global>();

builder.Services.AddHttpClient();

builder.Services.AddFluentUIComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
