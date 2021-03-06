﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using courses.Entities;


namespace courses.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser

    {
        public string FirstName { get; set; }
        public bool IsActive { get; set; }
        public DateTime Registered { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<ModuleType> ModuleTypes { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseType> CourseTypes { get; set; }
        public DbSet<CourseLinkText> CourseLinkTexts { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<CourseModule> courseModules { get; set; }
        public DbSet<CourseSubscription> CourseSubscriptions { get; set; }
        public DbSet<StudentSubscription> StudentSubscriptions { get; set; }

        
    }
}