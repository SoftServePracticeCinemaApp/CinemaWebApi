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
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            var name = "John";
            var lastName = "Doe";
            var name1 = "Jane";
            var lastName1 = "Smith";


            builder.HasData(
                new UserEntity
                {
                    Id = "1",
                    Name = name,
                    LastName = lastName,
                    UserName = $"{name}_{lastName}",
                    Tickets = []
                },
                new UserEntity
                {
                    Id = "2",
                    Name = name1,
                    LastName = lastName1,
                    UserName = $"{name}_{lastName}",
                    Tickets = []
                }
            );
        }
    }
}
