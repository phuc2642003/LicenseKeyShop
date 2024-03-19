    using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LicenseKeyShop.Models;

public partial class Prn211He176850Context : DbContext
{
    public static Prn211He176850Context INSTANCE = new Prn211He176850Context();
    public Prn211He176850Context()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
    }

    public Prn211He176850Context(DbContextOptions<Prn211He176850Context> options)
        : base(options)
    {
    }

    public virtual DbSet<BalanceHistory> BalanceHistories { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<DepositHistory> DepositHistories { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderHistory> OrderHistories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductKey> ProductKeys { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserBalance> UserBalances { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=(local);database=PRN211_HE176850;uid=sa;pwd=phuc2642003;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BalanceHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BalanceHistory_HE173252_pk");

            entity.ToTable("BalanceHistory");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ChangeDate).HasColumnType("datetime");
            entity.Property(e => e.Reason)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserUsername)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("User_Username");

            entity.HasOne(d => d.UserUsernameNavigation).WithMany(p => p.BalanceHistories)
                .HasForeignKey(d => d.UserUsername)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BalanceHistory_HE173252_User_HE173252");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.UserUsername).HasName("Cart_HE173252_pk");

            entity.ToTable("Cart");

            entity.Property(e => e.UserUsername)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("User_Username");

            entity.HasOne(d => d.UserUsernameNavigation).WithOne(p => p.Cart)
                .HasForeignKey<Cart>(d => d.UserUsername)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cart_HE173252_User_HE173252");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("CartItem_HE173252_pk");

            entity.ToTable("CartItem");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.ProductProductId).HasColumnName("Product_ProductID");
            entity.Property(e => e.UserUsername)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("User_Username");

            entity.HasOne(d => d.ProductProduct).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CartItem_HE173252_Product_HE173252");

            entity.HasOne(d => d.UserUsernameNavigation).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.UserUsername)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CartItem_HE173252_Cart_HE173252");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("Category_HE173252_pk");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(255);
        });

        modelBuilder.Entity<DepositHistory>(entity =>
        {
            entity.HasKey(e => e.DepositId).HasName("DepositRequest_HE173252_pk");

            entity.ToTable("DepositHistory");

            entity.Property(e => e.DepositId).HasColumnName("DepositID");
            entity.Property(e => e.ActionBy)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.ActionDate).HasColumnType("datetime");
            entity.Property(e => e.UserUsername)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("User_Username");

            entity.HasOne(d => d.UserUsernameNavigation).WithMany(p => p.DepositHistories)
                .HasForeignKey(d => d.UserUsername)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DepositRequest_HE173252_User_HE173252");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("OrderDetail_HE173252_pk");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.ExpirationDate)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.OrderHistoryOrderId).HasColumnName("OrderHistory_OrderID");
            entity.Property(e => e.ProductKey)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProductSoldName).HasMaxLength(255);

            entity.HasOne(d => d.OrderHistoryOrder).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderHistoryOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderDetail_HE173252_OrderHistory_HE173252");
        });

        modelBuilder.Entity<OrderHistory>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("OrderHistory_HE173252_pk");

            entity.ToTable("OrderHistory");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.UserUsername)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("User_Username");

            entity.HasOne(d => d.UserUsernameNavigation).WithMany(p => p.OrderHistories)
                .HasForeignKey(d => d.UserUsername)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderHistory_HE173252_User_HE173252");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("Product_HE173252_pk");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CategoryCategoryId).HasColumnName("Category_CategoryID");
            entity.Property(e => e.ProductName).HasMaxLength(255);

            entity.HasOne(d => d.CategoryCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_Category");
        });

        modelBuilder.Entity<ProductKey>(entity =>
        {
            entity.HasKey(e => e.KeyId).HasName("ProductKey_HE173252_pk");

            entity.ToTable("ProductKey");

            entity.Property(e => e.KeyId).HasColumnName("KeyID");
            entity.Property(e => e.ExpirationDate)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IsExpired).HasColumnName("isExpired");
            entity.Property(e => e.ProductKey1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ProductKey");
            entity.Property(e => e.ProductProductId).HasColumnName("Product_ProductID");

            entity.HasOne(d => d.ProductProduct).WithMany(p => p.ProductKeys)
                .HasForeignKey(d => d.ProductProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProductKey_HE173252_Product_HE173252");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("Role_HE173252_pk");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("User_HE173252_pk");

            entity.ToTable("User");

            entity.Property(e => e.Username)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(75);
            entity.Property(e => e.LastName).HasMaxLength(75);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RoleRoleId).HasColumnName("Role_RoleID");

            entity.HasOne(d => d.RoleRole).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_Role");
        });

        modelBuilder.Entity<UserBalance>(entity =>
        {
            entity.HasKey(e => e.UserUsername).HasName("UserBalance_HE173252_pk");

            entity.ToTable("UserBalance");

            entity.Property(e => e.UserUsername)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("User_Username");

            entity.HasOne(d => d.UserUsernameNavigation).WithOne(p => p.UserBalance)
                .HasForeignKey<UserBalance>(d => d.UserUsername)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserBalance_HE173252_User_HE173252");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
