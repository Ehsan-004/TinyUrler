﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TinyUrler_DotNetVersion.Models.Context
{
    public class TContext : IdentityDbContext<AppUser>
    {
        public TContext(DbContextOptions<TContext> options) : base(options)
        {

        }
        public DbSet<Link> Links { get; set; }
    }
}