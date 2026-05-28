using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Data;

public static class Extensions
{
    public static IApplicationBuilder UseMigrations(this IApplicationBuilder app)
    {
        Directory.CreateDirectory("db");

        using var scope = app.ApplicationServices.CreateScope();
        using var dbcontext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
        dbcontext.Database.MigrateAsync();

        return app;
    }
}
