using Echo.Bot.Bots;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Echo.Bot;

public class Startup
{
	private readonly IConfiguration _configuration;

	public Startup(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services
			.AddHttpClient()
			.AddControllers()
			.AddNewtonsoftJson(options =>
			{
				options.SerializerSettings.MaxDepth = HttpHelper
					.BotMessageSerializerSettings
					.MaxDepth;
			});
		services.AddSingleton<
			BotFrameworkAuthentication,
			ConfigurationBotFrameworkAuthentication>();
		services.AddSingleton<
			IBotFrameworkHttpAdapter,
			AdapterWithErrorHandler>();
		services.AddTransient<
			IBot,
			EchoBot>();
	}

	public void Configure(
		IApplicationBuilder app,
		IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		app.UseDefaultFiles()
			.UseStaticFiles()
			.UseWebSockets()
			.UseRouting()
			.UseAuthorization()
			.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
	}
}
