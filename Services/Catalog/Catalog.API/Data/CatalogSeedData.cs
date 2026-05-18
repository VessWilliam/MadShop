using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogSeedData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();
        if (await session.Query<Product>().AnyAsync()) return;


        session.Store(SeedingData());
        await session.SaveChangesAsync(cancellation);
    }

    private static IEnumerable<Product> SeedingData() => new List<Product>()
      {
            new()
            {
                Id = new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
                Name = "iPhone X",
                Descriptions = "Apple flagship smartphone with edge-to-edge display and Face ID.",
                ImageFiles = "product-1.png",
                Price = 950.00M,
                Category = new List<string> { "Smart Phone" }
            },
            new()
            {
                Id = new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"),
                Name = "Samsung Galaxy S10",
                Descriptions = "Premium Samsung phone with AMOLED display and triple camera setup.",
                ImageFiles = "product-2.png",
                Price = 840.00M,
                Category = new List<string> { "Smart Phone" }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Google Pixel 7",
                Descriptions = "Pure Android experience with Google AI camera enhancements.",
                ImageFiles = "product-3.png",
                Price = 799.00M,
                Category = new List<string> { "Smart Phone" }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "OnePlus 11",
                Descriptions = "High performance phone with fast charging and smooth display.",
                ImageFiles = "product-4.png",
                Price = 699.00M,
                Category = new List<string> { "Smart Phone" }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Xiaomi Mi 13",
                Descriptions = "Affordable flagship with strong performance and camera system.",
                ImageFiles = "product-5.png",
                Price = 650.00M,
                Category = new List<string> { "Smart Phone" }
            },

            new()
            {
                Id = Guid.NewGuid(),
                Name = "MacBook Pro M2",
                Descriptions = "Apple laptop with M2 chip for professional workloads.",
                ImageFiles = "product-6.png",
                Price = 1999.00M,
                Category = new List<string> { "Laptop" }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Dell XPS 13",
                Descriptions = "Ultra portable Windows laptop with premium build quality.",
                ImageFiles = "product-7.png",
                Price = 1499.00M,
                Category = new List<string> { "Laptop" }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Asus ROG Strix G15",
                Descriptions = "Gaming laptop with RTX GPU and high refresh rate display.",
                ImageFiles = "product-8.png",
                Price = 1799.00M,
                Category = new List<string> { "Laptop", "Gaming" }
            },

            new()
            {
                Id = Guid.NewGuid(),
                Name = "Logitech MX Master 3S",
                Descriptions = "Premium ergonomic wireless mouse for productivity.",
                ImageFiles = "product-9.png",
                Price = 120.00M,
                Category = new List<string> { "Accessories" }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Keychron K2 Keyboard",
                Descriptions = "Mechanical wireless keyboard with RGB lighting.",
                ImageFiles = "product-10.png",
                Price = 99.00M,
                Category = new List<string> { "Accessories" }
            },

            new()
            {
                Id = Guid.NewGuid(),
                Name = "iPad Pro 12.9",
                Descriptions = "Powerful tablet for creative and productivity work.",
                ImageFiles = "product-11.png",
                Price = 1299.00M,
                Category = new List<string> { "Tablet" }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Samsung Galaxy Tab S9",
                Descriptions = "Android tablet with AMOLED display and S-Pen support.",
                ImageFiles = "product-12.png",
                Price = 999.00M,
                Category = new List<string> { "Tablet" }
            },

            new()
            {
                Id = Guid.NewGuid(),
                Name = "Sony WH-1000XM5",
                Descriptions = "Industry leading noise cancelling headphones.",
                ImageFiles = "product-13.png",
                Price = 399.00M,
                Category = new List<string> { "Audio" }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "AirPods Pro 2",
                Descriptions = "Apple wireless earbuds with active noise cancellation.",
                ImageFiles = "product-14.png",
                Price = 249.00M,
                Category = new List<string> { "Audio" }
            },

            new()
            {
                Id = Guid.NewGuid(),
                Name = "Canon EOS R10",
                Descriptions = "Mirrorless camera for photography and video creators.",
                ImageFiles = "product-15.png",
                Price = 1099.00M,
                Category = new List<string> { "Camera" }
            },

            new()
            {
                Id = Guid.NewGuid(),
                Name = "DJI Mini 3 Pro",
                Descriptions = "Compact drone with 4K camera and intelligent tracking.",
                ImageFiles = "product-16.png",
                Price = 759.00M,
                Category = new List<string> { "Drone" }
            },

            new()
            {
                Id = Guid.NewGuid(),
                Name = "Apple Watch Series 9",
                Descriptions = "Smartwatch with health tracking and fitness features.",
                ImageFiles = "product-17.png",
                Price = 429.00M,
                Category = new List<string> { "Wearable" }
            },

            new()
            {
                Id = Guid.NewGuid(),
                Name = "Samsung Galaxy Watch 6",
                Descriptions = "Android smartwatch with advanced fitness tracking.",
                ImageFiles = "product-18.png",
                Price = 399.00M,
                Category = new List<string> { "Wearable" }
            },
    };
}



