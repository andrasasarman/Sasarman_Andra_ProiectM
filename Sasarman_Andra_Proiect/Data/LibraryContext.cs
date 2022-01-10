using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sasarman_Andra_Proiect.Models;

namespace Sasarman_Andra_Proiect.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) :
base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<PublishedCourse> PublishedCourses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Domain>().ToTable("Domain");
            modelBuilder.Entity<PublishedCourse>().ToTable("PublishedCourses");
            modelBuilder.Entity<PublishedCourse>()
                       .HasKey(c => new { c.CourseID, c.DomainID });
        }
    }
}
