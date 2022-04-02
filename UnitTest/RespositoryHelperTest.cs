using AssesmentAPI.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UnitTest
{
   
    public class CategoryCollection
    {
        public IList<Category> categories { get; set; }
    }

    public class ProductCollection
    {
        public IList<Product> products { get; set; }
    }
    public static class RespositoryHelperTest
    {
         public static DbContextOptions<InventoryDBContext> DbContextOptionsLocalDb()
        {
            var options = new DbContextOptionsBuilder<InventoryDBContext>().
             UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=InventoryDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;Integrated Security=SSPI;MultipleActiveResultSets=True;").Options;
            return options;
        }

        public static void CreateData(DbContextOptions<InventoryDBContext> options)
        {


            using (var inventoryDbContext = new InventoryDBContext(options))
            {
                inventoryDbContext.Database.EnsureDeleted();
                inventoryDbContext.Database.EnsureCreated();

                var jsonString = File.ReadAllText(System.AppContext.BaseDirectory + "\\Product_Data.json");
                var productCollection = JsonConvert.DeserializeObject<ProductCollection>(jsonString);

                jsonString = File.ReadAllText(System.AppContext.BaseDirectory + "\\Category_Data.json");
                var categoryCollection = JsonConvert.DeserializeObject<CategoryCollection>(jsonString);

                inventoryDbContext.Categories.AddRange(categoryCollection.categories);
                inventoryDbContext.SaveChanges();

                inventoryDbContext.Products.AddRange(productCollection.products);
                inventoryDbContext.SaveChanges();

            }



        }

    }

}
