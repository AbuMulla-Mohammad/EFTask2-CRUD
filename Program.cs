using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Task2EF.Data;
using Task2EF.Models;

namespace Task2EF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            
             //--------------------------------- 1.a
            dbContext.Add(new Product { Name = "product 1", Price = 55.55 });
            dbContext.Add(new Product { Name = "product 2", Price = 66.55 });
            dbContext.Add(new Product { Name = "product 3", Price = 77.55 });
            dbContext.Add(new Product { Name = "product 4", Price = 88.55 });
            dbContext.Add(new Product { Name = "product 5", Price = 99.55 });
            dbContext.Add(new Product { Name = "product 6", Price = 99.55 });
            dbContext.Add(new Product { Name = "product 7", Price = 99.55 });
            dbContext.Add(new Product { Name = "product 8", Price = 99.55 });
            dbContext.Add(new Product { Name = "product 9", Price = 99.55 });
            dbContext.Add(new Product { Name = "product 10", Price = 99.55 });
            dbContext.SaveChanges();





            var productsIdLessThan5 = dbContext.Products.Where(p => p.Id < 5).ToList();
            var productsIdMoreThan5 = dbContext.Products.Where(p => p.Id >= 5).ToList();
            //--------------------------------- 1.b
            dbContext.Add(new Order
            {
                Address = "jenin",
                CreatedAt=DateTime.Now,
                Products=productsIdLessThan5,
            });
            dbContext.Add(new Order
            {
                Address = "Nablus",
                CreatedAt=DateTime.Now,
                Products= productsIdMoreThan5,
            });
            dbContext.Add(new Order
            {
                Address = "Tubas",
                CreatedAt=DateTime.Now,
                Products=productsIdLessThan5,
            });
            dbContext.SaveChanges();
            
            //--------------------------------- 2.a
            var allProducts =dbContext.Products.ToList();
            
            //--------------------------------- 2.b
            var orders =dbContext.Orders.Include(o=>o.Products).ToList();


            //--------------------------------- 3.a
            foreach (var product in allProducts)
            {
                product.Name = $"{product.Name} updated";
                Console.WriteLine(product.Name);
            }
            //--------------------------------- 3.b
            foreach (var order in orders)
            {

                Console.WriteLine(order.Address);
                Console.WriteLine(order.CreatedAt);
                if (order.Id == 2)
                {
                    order.CreatedAt = new DateTime(2024, 1, 25, 10, 30, 0);
                    
                    
                }
                foreach (var product in order.Products)
                {
                    Console.WriteLine(product.Name);
                }
            }
            //--------------------------------- 4.a
            var productToRemove = dbContext.Products.FirstOrDefault(p => p.Id == 2);
            if (productToRemove != null)
            {
                dbContext.Products.Remove(productToRemove);
            }
            else
            {
                Console.WriteLine("product not found");
            }
            //--------------------------------- 4.b
            var orderToRemove = dbContext.Orders.FirstOrDefault(o => o.Id == 3);
            if (orderToRemove != null)
            {
                dbContext.Orders.Remove(orderToRemove);
            }
            else
            {
                Console.WriteLine("Order not found");
            }
            foreach(var product in allProducts)
            {
                Console.WriteLine(product.Name);
            }
            foreach( var order in orders)
            {
                Console.WriteLine(order.Address);
            }
            dbContext.SaveChanges();




        }
    }
}
