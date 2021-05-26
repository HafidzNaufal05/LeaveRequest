using LeaveRequest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Context
{
    public class MyContext : DbContext
    {
        public MyContext()
        {
        }
        public MyContext(DbContextOptions<MyContext>options): base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public DbSet<NationalHoliday> NationalHolidays { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            //SelfJoin Employee-EmployeeManager
            modelBuilder.Entity<Employee>()
                .HasOne(Employee => Employee.Manager)
                .WithMany()
                .HasForeignKey(Manager => Manager.NIK_Manager);

            //Employe-EmployeeRole
            modelBuilder.Entity<EmployeeRole>()
                .HasOne(EmployeeRole => EmployeeRole.Employee)
                .WithMany(Employee => Employee.EmployeeRoles)
                .OnDelete(DeleteBehavior.Cascade);

            //Employee -Account
            modelBuilder.Entity<Employee>()
                .HasOne(Employee => Employee.Account)
                .WithOne(Account => Account.Employee)
                .HasForeignKey<Account>(Account => Account.NIK)
                .OnDelete(DeleteBehavior.Cascade);

            //EmployeeRole-Role
            modelBuilder.Entity<EmployeeRole>()
                .HasOne(EmployeeRole => EmployeeRole.Role)
                .WithMany(Role => Role.EmployeeRoles)
                .OnDelete(DeleteBehavior.Cascade);

            //Employee-Request
            modelBuilder.Entity<Request>()
                .HasOne(Request => Request.Employee)
                .WithMany(Employee => Employee.Requests)
                .OnDelete(DeleteBehavior.Cascade);

            //Employee-Department
            modelBuilder.Entity<Employee>()
                .HasOne(Employee => Employee.Department)
                .WithMany(Department => Department.Employees)
                .OnDelete(DeleteBehavior.Cascade);
        } 
    }
}
