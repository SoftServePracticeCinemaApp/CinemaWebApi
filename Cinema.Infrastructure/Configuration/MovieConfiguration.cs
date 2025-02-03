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
                    Title = "Inception",
                    Overview = "A thief who steals corporate secrets through dream-sharing technology...",
                    ReleaseDate = "2010-07-16",
                    CinemaRating = 8.5,
                    PosterPath = "/poster/inception.jpg"
                },
                new MovieEntity
                {
                    Id = 2,
                    SearchId = 102,
                    Title = "The Matrix",
                    Overview = "A computer programmer discovers the true nature of his reality...",
                    ReleaseDate = "1999-03-31",
                    CinemaRating = 7.8,
                    PosterPath = "/poster/matrix.jpg"
                }
            );
        }
    }
}
