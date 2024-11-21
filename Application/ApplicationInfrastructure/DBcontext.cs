using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace ApplicationInfrastructure
{
    public class DBContext : DbContext
    {
        // Constructor mặc định, thường không cần thiết nếu không có cấu hình đặc biệt.
        public DBContext() { }

        // Constructor nhận DbContextOptions và cấu hình cơ sở dữ liệu
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
            // Bạn không cần thực hiện việc tạo cơ sở dữ liệu và bảng ở đây nếu sử dụng migration
        }

        // Các DbSet cho các Entity của bạn (ví dụ User, Novel, v.v.)
        // public virtual DbSet<User> Users { get; set; }
        // public virtual DbSet<Novel> Novels { get; set; }
    }
}
