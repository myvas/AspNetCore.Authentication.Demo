using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Myvas.AspNetCore.Weixin;
using Myvas.AspNetCore.Weixin.EntityFrameworkCore.Options;
using Myvas.AspNetCore.Weixin.EntityFrameworkCore.Storage.Extensions;
using Myvas.AspNetCore.Weixin.Models;
using System;

namespace Demo.Data
{
    public class DemoDbContext : IdentityDbContext, IWeixinDbContext<Subscriber>
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {
        }

        public DbSet<Subscriber> WeixinSubscribers { get; set; }
        public DbSet<AuditEntry> AuditEntires { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<MessageReceivedEntry> MessageReceivedEntries { get; set; }
        public DbSet<EventReceivedEntry> EventReceivedEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ConfigureWeixinDbContext<Subscriber>(null);
            base.OnModelCreating(builder);
        }
    }
}
