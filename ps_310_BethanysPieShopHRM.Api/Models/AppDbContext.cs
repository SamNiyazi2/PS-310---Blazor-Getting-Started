using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

// 05/10/2021 03:33 am - SSN - [20210510-0323] - [002] - M03-03 - Demo: Exploring the API
// using ps_310_BethanysPieShopHRM.Shared;
using ps_310_BethanysPieShopHRM.Shared;


namespace ps_310_BethanysPieShopHRM.Api.Models
{
    public class AppDbContext : DbContext
    {
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options , ILogger<AppDbContext> _logger, IConfiguration _configuration) : base(options)
        {

            // 04/09/2022 10:09 pm - SSN - [20220409-2151] - [002] - Add RowVersion to Employee
            #region [20220409-2151] - [002] 
            logger = _logger;
            configuration = _configuration;

            bool do_database_Migration = false;
            bool.TryParse(configuration["Database_Migration"], out do_database_Migration);

            
            logger.Log(LogLevel.Information, $"ps-310-20220409-2203 - BethanyPieShopHRM Migration check? [{do_database_Migration}]");

            if (do_database_Migration)
            {

                try
                {

                    logger.Log(LogLevel.Warning, "ps-310-20220409-2203-B - BethanyPieShopHRM Migration Running check ");

                    Database.SetCommandTimeout(6000);
                    Database.Migrate();


                    logger.Log(LogLevel.Information, "ps-310-20220409-2203-C - BethanyPieShopHRM applied migration");

                }
                catch (Exception ex)
                { 
                    try
                    {
                        logger.Log(LogLevel.Error, ex, "ps-310-20220409-2203-D - BethanyPieShopHRM migration failed.");
                    }
                    catch (Exception)
                    {
                        // Do nothing
                    }
                }

            }

            #endregion [20220409-2151] - [002] 

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("PS_310");

            base.OnModelCreating(modelBuilder);

            //seed categories
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 1, Name = "Belgium" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 2, Name = "Germany" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 3, Name = "Netherlands" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 4, Name = "USA" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 5, Name = "Japan" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 6, Name = "China" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 7, Name = "UK" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 8, Name = "France" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 9, Name = "Brazil" });

            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 1, JobCategoryName = "Pie research" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 2, JobCategoryName = "Sales" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 3, JobCategoryName = "Management" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 4, JobCategoryName = "Store staff" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 5, JobCategoryName = "Finance" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 6, JobCategoryName = "QA" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 7, JobCategoryName = "IT" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 8, JobCategoryName = "Cleaning" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 9, JobCategoryName = "Bakery" });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 1,
                CountryId = 1,
                MaritalStatus = MaritalStatus.Single,
                BirthDate = new DateTime(1979, 1, 16),
                City = "Brussels",
                Email = "bethany@bethanyspieshop.com",
                FirstName = "Bethany",
                LastName = "Smith",
                Gender = Gender.Female,
                PhoneNumber = "324777888773",
                Smoker = false,
                Street = "Grote Markt 1",
                Zip = "1000",
                JobCategoryId = 1,
                Comment = "Lorem Ipsum",
                ExitDate = null,
                JoinedDate = new DateTime(2015, 3, 1),
                Latitude = 50.8503,
                Longitude = 4.3517
            });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                CountryId = 2,
                MaritalStatus = MaritalStatus.Married,
                BirthDate = new DateTime(1979, 1, 16),
                City = "Antwerp",
                Email = "gill@bethanyspieshop.com",
                EmployeeId = 2,
                FirstName = "Gill",
                LastName = "Cleeren",
                Gender = Gender.Male,
                PhoneNumber = "33999909923",
                Smoker = false,
                Street = "New Street",
                Zip = "2000",
                JobCategoryId = 1,
                Comment = "Lorem Ipsum",
                ExitDate = null,
                JoinedDate = new DateTime(2017, 12, 24),
                Latitude = 50.8503,
                Longitude = 4.3517
            });
        }
    }
}
