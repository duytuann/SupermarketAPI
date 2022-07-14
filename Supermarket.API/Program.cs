using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Supermarket.API.Persistence.Contexts;
using Supermarket.API.Domain.Models;

namespace Supermarket.API
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();
      using (var scope = host.Services.CreateScope())
      using (var context = scope.ServiceProvider.GetService<AppDbContext>())
      {
        context.Database.EnsureCreated();

        context.Categories.Add(new Category
        {
          Id = 101,
          Name = "Dairy"
        });

        context.Categories.Add(new Category
        {
          Id = 100,
          Name = "Fruits and Vegetables"
        });

        context.SaveChanges();
      }

      host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
