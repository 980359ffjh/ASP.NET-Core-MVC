using RongProject1.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RongProject1.Entities
{
    public class RongDbContext : IdentityDbContext<User>
    {
        public RongDbContext(DbContextOptions options) : base(options)
        {

        }

        //在資料庫新增名為 Restaurants 的 table 表
        //名為Restaurants 的 table 表格式會照著 Dbset<???> 中的 ??? 的格式
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
