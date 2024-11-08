using Microsoft.FluentUI.AspNetCore.Components;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IAesKeyGenerator, AesKeyGenerator>();
builder.Services.AddScoped<IAesService, AesService>();
builder.Services.AddScoped<IRsaKeyGenerator, RsaKeyGenerator>();
builder.Services.AddScoped<IRsaService, RsaService>();
builder.Services.AddScoped<IHashService, HashService>();
builder.Services.AddScoped<IDigitalSignatureService, DigitalSignatureService>();
builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddHttpClient();

// Fluent UI
builder.Services.AddFluentUIComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
