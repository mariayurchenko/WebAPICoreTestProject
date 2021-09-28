﻿using System;
using Microsoft.EntityFrameworkCore;
using Core.Models;

namespace DataStore.EF
{
    public class BugsContext : DbContext
    {
        public BugsContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Tickets)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId);

            //seeding
            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    ProjectId = 1,
                    Name = "Project 1"
                },
                new Project
                {
                    ProjectId = 2,
                    Name = "Project 2"
                });
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket
                {
                    TicketId = 1,
                    ProjectId = 1,
                    Title = "Bug #1",
                    Owner = "Frank Liu",
                    ReportDate = new DateTime(2021, 1, 1),
                    DueDate = new DateTime(2021, 1, 1)
                },
                new Ticket
                {
                    TicketId = 2,
                    ProjectId = 1,
                    Title = "Bug #2",
                    Owner = "Maria Yurchenko",
                    ReportDate = new DateTime(2021,8,4),
                    DueDate = new DateTime(2021,8,4)
                },
                new Ticket
                {
                    TicketId = 3,
                    ProjectId = 2,
                    Title = "Bug #3",
                    Owner = "Maria Yurchenko",
                    ReportDate = new DateTime(2021, 10, 4),
                    DueDate = new DateTime(2021, 10, 4)
                });
        }
    }
}