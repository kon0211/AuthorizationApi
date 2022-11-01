using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationApi.Infrastructure
{
    public class DesignTimeDbAppContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<UserDbContext>();
            builder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=AuthApiDb; Integrated Security=True;");
            return new UserDbContext(builder.Options);
        }

    }
}
