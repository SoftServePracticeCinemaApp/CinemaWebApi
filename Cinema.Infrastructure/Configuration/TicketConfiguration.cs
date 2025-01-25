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
    public class TicketConfiguration : IEntityTypeConfiguration<TicketEntity>
    {
        public void Configure(EntityTypeBuilder<TicketEntity> builder)
        {
            builder.HasData(
                new TicketEntity
                {
                    Id = 1,
                    SessionId = 1,
                    UserId = "1",
                    MovieId = 1,
                    Row = 1
                },
                new TicketEntity
                {
                    Id = 2,
                    SessionId = 2,
                    UserId = "2",
                    MovieId = 2,
                    Row = 2
                }
            );
        }
    }
}
