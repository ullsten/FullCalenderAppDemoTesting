using System;
using System.Linq;
using FullCalenderApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FullCalenderApp.Data
{
    public static class SeedData
    {
        public static void Seed(ApplicationDbContext dbContext)
        {
            // Apply any pending migrations
            dbContext.Database.Migrate();

            // Check if the Color table is empty
            if (!dbContext.Colors.Any())
            {
                // Seed initial data to the Color table
                var colorsToAdd = new[]
                {
                    new Color { ColorName = "Red" },
                    new Color { ColorName = "Blue" },
                    new Color { ColorName = "Green" },
                    new Color { ColorName = "Yellow" },
                    new Color { ColorName = "Orange" },
                    new Color { ColorName = "Purple" },
                    new Color { ColorName = "Pink" },
                    new Color { ColorName = "Brown" },
                    new Color { ColorName = "Teal" },
                    new Color { ColorName = "Indigo" },
                    new Color { ColorName = "Cyan" },
                    new Color { ColorName = "Magenta" },
                    new Color { ColorName = "Lime" },
                    new Color { ColorName = "Silver" },
                    new Color { ColorName = "Gold" },
                    new Color { ColorName = "Gray" },
                    new Color { ColorName = "Darkgray" }
                };

                // Add colors to the database only if they don't exist
                foreach (var color in colorsToAdd)
                {
                    if (!dbContext.Colors.Any(c => c.ColorName == color.ColorName))
                    {
                        dbContext.Colors.Add(color);
                    }
                }

                dbContext.SaveChanges();
            }
        }
    }
}
