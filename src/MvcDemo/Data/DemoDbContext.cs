using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Myvas.AspNetCore.Weixin;

namespace Demo.Data
{
    public class DemoDbContext : IdentityDbContext, IWeixinDbContext<WeixinSubscriber, string>
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {
        }

        public DbSet<WeixinSubscriber> WeixinSubscribers { get; set; }
        public DbSet<WeixinReceivedEvent> WeixinReceivedEvents { get; set; }
        public DbSet<WeixinReceivedMessage> WeixinReceivedMessages { get; set; }
        public DbSet<WeixinResponseMessage> WeixinResponseMessages { get; set; }
        public DbSet<WeixinSendMessage> WeixinSendMessages { get; set; }
    }
}
