using Cinema.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Configuration
{
    public class HallConfiguration : IEntityTypeConfiguration<HallEntity>
    {
        public void Configure(EntityTypeBuilder<HallEntity> builder)
        {
            builder.HasData(
                new HallEntity
                {
                    Id = 1,
                    Seats = new List<List<int>>
                    {
                        new List<int> { 1, 2, 3 },
                        new List<int> { 4, 5, 6 }
                    }
                },
                new HallEntity
                {
                    Id = 2,
                    Seats = new List<List<int>>
                    {
                        new List<int> { 1, 2 },
                        new List<int> { 3, 4 }
                    }
                }
            );
        }
    }
}
