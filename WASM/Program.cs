using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using WASM.Services;

namespace WASM
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");
			builder.RootComponents.Add<HeadOutlet>("head::after");

			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped<IAesKeyGenerator, AesKeyGenerator>();
            builder.Services.AddScoped<IAesService, AesService>();
            builder.Services.AddScoped<IRsaKeyGenerator, RsaKeyGenerator>();
            builder.Services.AddScoped<IRsaService, RsaService>();
            builder.Services.AddScoped<IHashService, HashService>();
            builder.Services.AddScoped<IDigitalSignatureService, DigitalSignatureService>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddSingleton<Global>();

            builder.Services.AddFluentUIComponents();

			await builder.Build().RunAsync();
		}
	}
}
