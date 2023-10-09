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
        public DbSet<NoteEntity> Note {  get; set; }

        public DbSet<CollabEntity> Collab { get; set; }



        /* protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             modelBuilder.Entity<UserEntity>().HasData(new UserEntity
             {

                 FirstName = "Uncle",
                 LastName = "Bob",
                 Email = "uncle.bob@gmail.com",
                 Password = "password",
             }, new UserEntity
             {

                 FirstName = "Jan",
                 LastName = "Kirsten",
                 Email = "jan.kirsten@gmail.com",
                 Password= "pass123",
             },new  UserEntity
             {
                 FirstName = "smith",
                 LastName = "Doe",
                 Email = "jane.doe@gmail.com",
                 Password = "password",
             }, new UserEntity
             {
                 FirstName = "alex",
                 LastName = "Doe",
                 Email = "john.doe@gmail.com",
                 Password = "password",
             }
             );
         }*/
    }
}

