namespace jex_assessment.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "JEX Assessment API v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors();
            app.UseAuthorization();
            app.MapControllers();
            return app;
        }

        public static WebApplication ConfigureSpaFallback(this WebApplication app)
        {
            // Always serve built SPA when hitting non-API routes
            app.MapFallbackToFile("dist/index.html");
            return app;
        }
    }
}
