using ApplicationCore.Entities;
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


        }
        public DbSet<User> Users { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Quan hệ giữa User và Story: Một User có thể có nhiều Story, mỗi Story thuộc một User
            modelBuilder.Entity<Story>()
                .HasOne(s => s.User)
                .WithMany(u => u.Stories)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);  // Xóa Story khi User bị xóa

            // Quan hệ giữa Story và Chapter: Một Story có thể có nhiều Chapter, mỗi Chapter thuộc một Story
            modelBuilder.Entity<Chapter>()
                .HasOne(c => c.Story)
                .WithMany(s => s.Chapters)
                .HasForeignKey(c => c.StoryId)
                .OnDelete(DeleteBehavior.Cascade);  // Xóa Chapter khi Story bị xóa

            // Quan hệ giữa User và Comment: Một User có thể có nhiều Comment, mỗi Comment thuộc một User
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);  // Xóa Comment khi User bị xóa

            // Quan hệ giữa Story và Comment: Một Story có thể có nhiều Comment, mỗi Comment thuộc một Story
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Story)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.StoryId)
                .OnDelete(DeleteBehavior.Cascade);  // Xóa Comment khi Story bị xóa

            // Thêm các cấu hình khác nếu cần
        }

    }
}
