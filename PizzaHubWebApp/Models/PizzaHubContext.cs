using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class PizzaHubContext : DbContext
    {
        public PizzaHubContext()
        {
        }

        public PizzaHubContext(DbContextOptions<PizzaHubContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Drink> Drinks { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrdersDetail> OrderDetails { get; set; }
        public virtual DbSet<Pizza> Pizzas { get; set; }
        public virtual DbSet<PizzaBasis> PizzaBases { get; set; }
        public virtual DbSet<PizzaToppingDetail> PizzaToppingDetails { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<Sauce> Sauces { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Topping> Toppings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer("");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Cart");

                entity.Property(e => e.Base).HasDefaultValueSql("((1))");

                entity.Property(e => e.SizeId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.BaseNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Base)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Cart_PizzaBases_BaseId_fk");

                entity.HasOne(d => d.Member)
                    .WithMany()
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Cart_Members_MemberId_fk");

                entity.HasOne(d => d.Pizza)
                    .WithMany()
                    .HasForeignKey(d => d.PizzaId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Cart_Pizzas_PizzaId_fk");

                entity.HasOne(d => d.Size)
                    .WithMany()
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Cart_Sizes_SizeId_fk");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Drink>(entity =>
            {
                entity.Property(e => e.Brand).HasMaxLength(200);

                entity.Property(e => e.DrinkName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Avatar)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.City).HasMaxLength(255);

                entity.Property(e => e.Country).HasMaxLength(255);

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber).HasMaxLength(255);

                entity.Property(e => e.Point).HasDefaultValueSql("((0))");

                entity.Property(e => e.RankId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Role).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Rank)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.RankId)
                    .HasConstraintName("Members_Ranks_RankId_fk");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Freight).HasColumnType("money");

                entity.Property(e => e.Note).HasMaxLength(255);

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.RequiredDate).HasColumnType("datetime");

                entity.Property(e => e.ShippedDate).HasColumnType("datetime");

                entity.Property(e => e.StatusId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Orders_Members_MemberId_fk");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Orders_Status_StatusId_fk");
            });

            modelBuilder.Entity<OrdersDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailId)
                    .HasName("OrderDetail_pk");

                entity.ToTable("OrdersDetail");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.Property(e => e.TotalPrice).HasColumnType("money");

                entity.HasOne(d => d.Base)
                    .WithMany(p => p.OrdersDetails)
                    .HasForeignKey(d => d.BaseId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("OrderDetail_PizzaBases_BaseId_fk");

                entity.HasOne(d => d.Drink)
                    .WithMany(p => p.OrdersDetails)
                    .HasForeignKey(d => d.DrinkId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("OrderDetail_Drinks_DrinkId_fk");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrdersDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("OrderDetail_Orders_OrderId_fk");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.OrdersDetails)
                    .HasForeignKey(d => d.PizzaId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("OrderDetail_Pizzas_PizzaId_fk");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.OrdersDetails)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("OrderDetail_Sizes_SizeId_fk");
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PizzaName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Pizzas)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("Pizzas_Categories_CategoryId_fk");

                entity.HasOne(d => d.Sauce)
                    .WithMany(p => p.Pizzas)
                    .HasForeignKey(d => d.SauceId)
                    .HasConstraintName("Pizzas_Sauces_SauceId_fk");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Pizzas)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("Pizzas_Status_StatusId_fk");
            });

            modelBuilder.Entity<PizzaBasis>(entity =>
            {
                entity.HasKey(e => e.BaseId)
                    .HasName("PizzaBases_pk");

                entity.Property(e => e.Base)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<PizzaToppingDetail>(entity =>
            {
                entity.HasKey(e => e.PizzaTopping)
                    .HasName("Pizza_Topping_Detail_pk");

                entity.ToTable("Pizza_Topping_Detail");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.PizzaToppingDetails)
                    .HasForeignKey(d => d.PizzaId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Pizza_Topping_Detail_Pizzas_PizzaId_fk");

                entity.HasOne(d => d.Topping)
                    .WithMany(p => p.PizzaToppingDetails)
                    .HasForeignKey(d => d.ToppingId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Pizza_Topping_Detail_Toppings_ToppingId_fk");
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Rank1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Rank");
            });

            modelBuilder.Entity<Sauce>(entity => { entity.Property(e => e.SauceName).HasMaxLength(50); });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.Property(e => e.Size1)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Size");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.Status1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Status");
            });

            modelBuilder.Entity<Topping>(entity =>
            {
                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ToppingName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UnitPrice).HasColumnType("money");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}