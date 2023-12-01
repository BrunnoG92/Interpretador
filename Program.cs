using Microsoft.AspNetCore.Diagnostics;
#pragma warning disable

namespace TextDecoderAPI
{   
    public class Program
    {   
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((hostContext, services) =>
                    {
                        services.AddControllers();
                        services.AddEndpointsApiExplorer();
                        services.AddSwaggerGen();
                     

                        // Configuração do CORS
                        services.AddCors(options =>
                        {
                            options.AddPolicy("AllowAnyOriginPolicy",
                                builder =>
                                {
                                    builder.AllowAnyOrigin()
                                        .AllowAnyMethod()
                                        .AllowAnyHeader();
                                });
                        });
                    })
                    .Configure(app =>
                    {
                        // Configuração CORS
                        app.UseCors("AllowAnyOriginPolicy");

                        if (app.ApplicationServices.GetRequiredService<IHostEnvironment>().IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                        }
                        else
                        {
                            // Middleware de tratamento de erros personalizado
                            _ = app.UseExceptionHandler(errorApp =>
                            {
                                errorApp.Run(async context =>
                                {
                                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                                    var exception = exceptionHandlerPathFeature.Error;

                                    // Tratamento para a exceção BadHttpRequestException
                                    if (exception is BadHttpRequestException badHttpRequestEx)
                                    {
                                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                                        await context.Response.WriteAsync("Erro na requisição HTTP.");
                                    }
                                    else
                                    {
                                        // Tratamento padrão para outras exceções
                                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                                        await context.Response.WriteAsync("Erro interno do servidor.");
                                    }
                                });
                            });
                        }

                        // Adicione o middleware personalizado aqui
                        // app.UseMiddleware<RejectMalformedRequestMiddleware>();

                        app.UseRouting();

                        app.UseAuthorization();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();


                        });
                        
                    });
                    //  webBuilder.UseUrls("http://localhost:5120");
                    webBuilder.UseUrls("http://servertropical.ddns.net:5555");
                });
    }
}
