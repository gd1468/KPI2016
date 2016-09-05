using MoneyManagement.DomainModel.Complex_types;
using MoneyManagement.DomainModel.Domain;

namespace MoneyManagement.Persistance.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MoneyManagement.Persistance.MoneyManagementDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MoneyManagement.Persistance.MoneyManagementDbContext context)
        {
            context.Users.AddOrUpdate(x=>x.KeyId,
                new User()
                {
                    Username = "dainguyen",
                    Password = "#123",
                    CreatedById = Guid.NewGuid(),
                    CreatedOn = DateTime.Now,
                    LastTime = DateTime.Now,
                    LastUserId = Guid.NewGuid(),
                    ShortName = "Dai Nguyen",
                    UserInformation = new UserInformation
                    {
                        Address = new Address
                        {
                            AddressNumber = "1126",
                            Street = "Quang Trung"
                        },
                        FirstName = "Dai",
                        LastName = "Nguyen Huynh Gia"
                    }
                });
        }
    }
}
