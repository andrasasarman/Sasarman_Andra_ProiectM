using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sasarman_Andra_Proiect.Models;

namespace Sasarman_Andra_Proiect.Data
{
    public class DbInitializer
    {
        public static void Seed(LibraryContext context)
        {
            context.Database.EnsureCreated();
            if (context.Courses.Any())
            {
                return; // BD a fost creata anterior
            }

            var courses = new Course[]
            {
         new Course{Title="Build a math game with JavaScript",Author="Lia Sue Kim",Price=Decimal.Parse("222")},
         new Course{Title="Life Coaching Certificate Course",Author="Kain Ramsay",Price=Decimal.Parse("118")},
         new Course{Title="Illustrator 2022 MasterClass",Author="Martin Perhiniak",Price=Decimal.Parse("127")},
         new Course{Title="Design Thinking in 3 steps",Author="Alan Cooper",Price=Decimal.Parse("189")},
         new Course{Title="The complete Oracle SQL Certification Course",Author="Imtiaz Ahmad",Price=Decimal.Parse("589")},
         new Course{Title="Web design for begginers: Real World Coding in HTML and CSS",Author="Brad Schiff",Price=Decimal.Parse("109")},
         new Course{Title="Flutter - Beginners Course",Author="Bryan Cairns",Price=Decimal.Parse("267")}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var customers = new Customer[]
            {

         new Customer{CustomerID=1050,Name="Pop Ioana",BirthDate=DateTime.Parse("1989-08-03")},
         new Customer{CustomerID=1045,Name="Man Claudiu",BirthDate=DateTime.Parse("1999-09-05")},
            };
            foreach (Customer cu in customers)
            {
                context.Customers.Add(cu);
            }
            context.SaveChanges();
            var orders = new Order[]
            {
         new Order{CourseID=1,CustomerID=1050,OrderDate=DateTime.Parse("02-25-2020")},
         new Order{CourseID=3,CustomerID=1045,OrderDate=DateTime.Parse("09-28-2020")},
         new Order{CourseID=1,CustomerID=1045,OrderDate=DateTime.Parse("10-28-2020")},
         new Order{CourseID=2,CustomerID=1050,OrderDate=DateTime.Parse("09-28-2020")},
         new Order{CourseID=4,CustomerID=1050,OrderDate=DateTime.Parse("09-28-2020")},
         new Order{CourseID=6,CustomerID=1050,OrderDate=DateTime.Parse("10-28-2020")},
         new Order{CourseID=5,CustomerID=1045,OrderDate=DateTime.Parse("10-21-2020")},
            };
            foreach (Order o in orders)
            {
               context.Orders.Add(o);
            }
            context.SaveChanges();

            var domains = new Domain[]
            {
                new Domain{DomainName = "IT",Description="The topics you can find here are variated."},
                new Domain{DomainName = "Design",Description="Here are some various courses regarding this domain."},
                new Domain{DomainName = "Personal development",Description="For the ones who are preoccupied about personal development."},

            };
            foreach (Domain d in domains)
            {
                context.Domains.Add(d);
            }
            context.SaveChanges();

            var publishedcourses = new PublishedCourse[]
            {
                new PublishedCourse{CourseID = courses.Single(c=>c.Title=="Build a math game with JavaScript").ID,
                    DomainID = domains.Single(d=>d.DomainName=="IT").ID},
                new PublishedCourse{CourseID = courses.Single(c=>c.Title=="Life Coaching Certificate Course").ID,
                    DomainID = domains.Single(d=>d.DomainName=="Personal Development").ID},
                new PublishedCourse{CourseID = courses.Single(c=>c.Title=="Illustrator 2022 MasterClass").ID,
                    DomainID = domains.Single(d=>d.DomainName=="Design").ID},
                new PublishedCourse{CourseID = courses.Single(c=>c.Title=="Design Thinking in 3 steps").ID,
                    DomainID = domains.Single(d=>d.DomainName=="Design").ID},
                new PublishedCourse{CourseID = courses.Single(c=>c.Title=="The complete Oracle SQL Certification Course").ID,
                    DomainID = domains.Single(d=>d.DomainName=="IT").ID},
                new PublishedCourse{CourseID = courses.Single(c=>c.Title=="Web design for begginers: Real World Coding in HTML and CSS").ID,
                    DomainID = domains.Single(d=>d.DomainName=="IT").ID},
                new PublishedCourse{CourseID = courses.Single(c=>c.Title=="Flutter - Beginners Course").ID,
                    DomainID = domains.Single(d=>d.DomainName=="IT").ID},
            };
            foreach (PublishedCourse pc in publishedcourses)
            {
                context.PublishedCourses.Add(pc);
            }
            context.SaveChanges();
        }
    }
}
