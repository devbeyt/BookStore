using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider) 
        {
            using (var context = new BookStoreDBContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDBContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }
                context.Books.AddRange
                     (
                     new Book 
                     {
                         //Id = 1, 
                         Title = "Learn Html",
                         GenreId = 1, PageCount = 250,
                         PublisDate = new DateTime(2022, 5, 21) 
                     },
                     new Book 
                     {   //Id = 2, 
                         Title = "Cyber security",
                         GenreId = 2,
                         PageCount = 300,
                         PublisDate = new DateTime(2021, 2, 5)
                     },
                     new Book 
                     { 
                         //Id = 3,
                         Title = "Unity", 
                         GenreId = 1,
                         PageCount = 120,
                         PublisDate = new DateTime(2010, 3, 31)
                     }
                     );
                context.SaveChanges();
            }

        }
    }
}
