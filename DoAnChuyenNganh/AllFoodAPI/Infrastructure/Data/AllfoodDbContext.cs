using System;
using System.Collections.Generic;
using AllFoodAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace AllFoodAPI.Infrastructure.Data;

public partial class AllfoodDbContext : DbContext
{
    public AllfoodDbContext()
    {
    }

    public AllfoodDbContext(DbContextOptions<AllfoodDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartDetail> CartDetails { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderFromCusDetail> OrderFromCusDetails { get; set; }

    public virtual DbSet<OrdersFromCu> OrdersFromCus { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseMySql("server=localhost;database=allfood_db;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PRIMARY");

            entity.ToTable("address");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.AddressId)
                .HasColumnType("int(11)")
                .HasColumnName("address_id");
            entity.Property(e => e.Address1)
                .HasColumnType("text")
                .HasColumnName("address");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("address_ibfk_1");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PRIMARY");

            entity.ToTable("cart");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.CartId)
                .HasColumnType("int(11)")
                .HasColumnName("cart_id");
            entity.Property(e => e.Total)
                .HasColumnType("int(11)")
                .HasColumnName("total");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("cart_ibfk_1");
        });

        modelBuilder.Entity<CartDetail>(entity =>
        {
            entity.HasKey(e => new { e.CartId, e.ProductId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("cart_detail");

            entity.HasIndex(e => e.ProductId, "product_id");

            entity.HasIndex(e => e.ShopId, "shop_id");

            entity.Property(e => e.CartId)
                .HasColumnType("int(11)")
                .HasColumnName("cart_id");
            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.Price)
                .HasColumnType("int(11)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity)
                .HasColumnType("int(11)")
                .HasColumnName("quantity");
            entity.Property(e => e.ShopId)
                .HasColumnType("int(11)")
                .HasColumnName("shop_id");
            entity.Property(e => e.Total)
                .HasColumnType("int(11)")
                .HasColumnName("total");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartDetails)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("cart_detail_ibfk_1");

            entity.HasOne(d => d.Product).WithMany(p => p.CartDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("cart_detail_ibfk_2");

            entity.HasOne(d => d.Shop).WithMany(p => p.CartDetails)
                .HasForeignKey(d => d.ShopId)
                .HasConstraintName("cart_detail_ibfk_3");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("category");

            entity.HasIndex(e => e.ShopId, "fk_category_shop");

            entity.Property(e => e.CategoryId)
                .HasColumnType("int(11)")
                .HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasColumnType("text")
                .HasColumnName("category_name");
            entity.Property(e => e.ShopId)
                .HasColumnType("int(11)")
                .HasColumnName("shop_id");

            entity.HasOne(d => d.Shop).WithMany(p => p.Categories)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_category_shop");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PRIMARY");

            entity.ToTable("image");

            entity.HasIndex(e => e.ProductId, "product_id");

            entity.Property(e => e.ImageId)
                .HasColumnType("int(11)")
                .HasColumnName("image_id");
            entity.Property(e => e.ImageUrl)
                .HasColumnType("text")
                .HasColumnName("image_url");
            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("product_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Images)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("image_ibfk_1");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("order");

            entity.HasIndex(e => e.ShopId, "shop_id");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.OrderId)
                .HasColumnType("int(11)")
                .HasColumnName("order_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DeliveryAddress)
                .HasColumnType("text")
                .HasColumnName("delivery_address");
            entity.Property(e => e.PaymentMethod)
                .HasColumnType("text")
                .HasColumnName("payment_method");
            entity.Property(e => e.ShopId)
                .HasColumnType("int(11)")
                .HasColumnName("shop_id");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
            entity.Property(e => e.Total)
                .HasColumnType("int(11)")
                .HasColumnName("total");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
            entity.Property(e => e.VoucherId)
                .HasColumnType("int(11)")
                .HasColumnName("voucher_id");

            entity.HasOne(d => d.Shop).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShopId)
                .HasConstraintName("order_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("order_ibfk_1");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("order_detail");

            entity.HasIndex(e => e.ProductId, "product_id");

            entity.Property(e => e.OrderId)
                .HasColumnType("int(11)")
                .HasColumnName("order_id");
            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.OrderStatus)
                .HasColumnType("int(11)")
                .HasColumnName("order_status");
            entity.Property(e => e.Price)
                .HasColumnType("int(11)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity)
                .HasColumnType("int(11)")
                .HasColumnName("quantity");
            entity.Property(e => e.Total)
                .HasColumnType("int(11)")
                .HasColumnName("total");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("order_detail_ibfk_1");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("order_detail_ibfk_2");
        });

        modelBuilder.Entity<OrderFromCusDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("order_from_cus_detail");

            entity.HasIndex(e => e.OrderId, "order_id");

            entity.HasIndex(e => e.ProductId, "product_id");

            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.OrderId)
                .HasColumnType("int(11)")
                .HasColumnName("order_id");
            entity.Property(e => e.Price)
                .HasColumnType("int(11)")
                .HasColumnName("price");
            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasColumnType("int(11)")
                .HasColumnName("quantity");
            entity.Property(e => e.Total)
                .HasColumnType("int(11)")
                .HasColumnName("total");

            entity.HasOne(d => d.Order).WithMany()
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_from_cus_detail_ibfk_1");

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_from_cus_detail_ibfk_2");
        });

        modelBuilder.Entity<OrdersFromCu>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("orders_from_cus");

            entity.HasIndex(e => e.ShopId, "fk_shop_id");

            entity.Property(e => e.OrderId)
                .HasColumnType("int(11)")
                .HasColumnName("order_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DeliveryAddress)
                .HasColumnType("text")
                .HasColumnName("delivery_address");
            entity.Property(e => e.ShopId)
                .HasColumnType("int(11)")
                .HasColumnName("shop_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Total)
                .HasColumnType("int(11)")
                .HasColumnName("total");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Shop).WithMany(p => p.OrdersFromCus)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shop_id");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.ToTable("product");

            entity.HasIndex(e => e.CategoryId, "category_id");

            entity.HasIndex(e => e.ShopId, "shop_id");

            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.Available)
                .HasColumnType("int(11)")
                .HasColumnName("available");
            entity.Property(e => e.CategoryId)
                .HasColumnType("int(11)")
                .HasColumnName("category_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Price)
                .HasColumnType("int(11)")
                .HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasColumnType("tinytext")
                .HasColumnName("product_name");
            entity.Property(e => e.SalesCount)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)")
                .HasColumnName("sales_count");
            entity.Property(e => e.ShopId)
                .HasColumnType("int(11)")
                .HasColumnName("shop_id");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("product_ibfk_2");

            entity.HasOne(d => d.Shop).WithMany(p => p.Products)
                .HasForeignKey(d => d.ShopId)
                .HasConstraintName("product_ibfk_1");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PRIMARY");

            entity.ToTable("report");

            entity.HasIndex(e => e.ProductId, "product_id");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.ReportId)
                .HasColumnType("int(11)")
                .HasColumnName("report_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.Reason)
                .HasColumnType("text")
                .HasColumnName("reason");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("report_ibfk_1");

            entity.HasOne(d => d.User).WithMany(p => p.Reports)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("report_ibfk_2");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PRIMARY");

            entity.ToTable("review");

            entity.HasIndex(e => e.ProductId, "product_id");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.ReviewId)
                .HasColumnType("int(11)")
                .HasColumnName("review_id");
            entity.Property(e => e.Comment)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("review_ibfk_1");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("review_ibfk_2");
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.ShopId).HasName("PRIMARY");

            entity.ToTable("shop");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.ShopId)
                .HasColumnType("int(11)")
                .HasColumnName("shop_id");
            entity.Property(e => e.Address)
                .HasColumnType("text")
                .HasColumnName("address");
            entity.Property(e => e.Phone)
                .HasColumnType("tinytext")
                .HasColumnName("phone");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ShopName)
                .HasColumnType("tinytext")
                .HasColumnName("shop_name");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Shops)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("shop_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasColumnType("tinytext")
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasColumnType("tinytext")
                .HasColumnName("full_name");
            entity.Property(e => e.ImageUrl)
                .HasColumnType("text")
                .HasColumnName("image_url");
            entity.Property(e => e.Password)
                .HasColumnType("tinytext")
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasColumnType("tinytext")
                .HasColumnName("phone");
            entity.Property(e => e.Salt)
                .HasColumnType("tinytext")
                .HasColumnName("salt");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
            entity.Property(e => e.Username)
                .HasColumnType("tinytext")
                .HasColumnName("username");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PRIMARY");

            entity.ToTable("voucher");

            entity.HasIndex(e => e.ShopId, "shop_id");

            entity.Property(e => e.VoucherId)
                .HasColumnType("int(11)")
                .HasColumnName("voucher_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Discount)
                .HasColumnType("int(11)")
                .HasColumnName("discount");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.PaymentMethod)
                .HasColumnType("int(11)")
                .HasColumnName("payment_method");
            entity.Property(e => e.Quantity)
                .HasColumnType("int(11)")
                .HasColumnName("quantity");
            entity.Property(e => e.ShopId)
                .HasColumnType("int(11)")
                .HasColumnName("shop_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasColumnType("text")
                .HasColumnName("title");

            entity.HasOne(d => d.Shop).WithMany(p => p.Vouchers)
                .HasForeignKey(d => d.ShopId)
                .HasConstraintName("voucher_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
