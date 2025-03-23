using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Myvas.AspNetCore.Weixin;

namespace Demo.Data
{
    public class DemoDbContext : IdentityDbContext, IWeixinDbContext
    {
        public DemoDbContext(DbContextOptions options) : base(options)
        {
        }

        protected DemoDbContext()
        {
        }

        public DbSet<WeixinSubscriberEntity> WeixinSubscribers { get; set; }
        public DbSet<WeixinReceivedEventEntity> WeixinReceivedEvents { get; set; }
        public DbSet<WeixinReceivedMessageEntity> WeixinReceivedMessages { get; set; }
        public DbSet<WeixinResponseMessageEntity> WeixinResponseMessages { get; set; }
        public DbSet<WeixinSendMessageEntity> WeixinSendMessages { get; set; }
    }
}
