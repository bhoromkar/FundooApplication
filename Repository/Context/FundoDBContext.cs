using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Repository.Context
{
    public class FundoDBContext : DbContext
    {
        public FundoDBContext(DbContextOptions<FundoDBContext> options) : base(options)
        {

        }
        public DbSet<UserEntity> Users { get; set; }
    }
}

