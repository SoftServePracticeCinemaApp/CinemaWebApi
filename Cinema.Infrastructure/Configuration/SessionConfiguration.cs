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
    public class SessionConfiguration : IEntityTypeConfiguration<SessionEntity>
    {
        public void Configure(EntityTypeBuilder<SessionEntity> builder)
        {
            builder.HasData(
                new SessionEntity
                {
                    Id = 1,
                    MovieId = 1,
                    Date = DateTime.Now.AddDays(1),
                    HallId = 1
                },
                new SessionEntity
                {
                    Id = 2,
                    MovieId = 2,
                    Date = DateTime.Now.AddDays(2),
                    HallId = 2
                }
            );
        }
    }
}
