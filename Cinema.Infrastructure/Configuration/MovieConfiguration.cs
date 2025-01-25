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
    public class MovieConfiguration : IEntityTypeConfiguration<MovieEntity>
    {
        public void Configure(EntityTypeBuilder<MovieEntity> builder)
        {
            builder.HasData(
                new MovieEntity
                {
                    Id = 1,
                    SearchId = 101,
                    CinemaRating = 8.5
                },
                new MovieEntity
                {
                    Id = 2,
                    SearchId = 102,
                    CinemaRating = 7.8
                }
            );
        }
    }
}
