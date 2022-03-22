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

        public virtual DbSet<Base> Bases { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Drink> Drinks { get; set; }
        public virtual DbSet<Extra> Extras { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MemberVoucher> MemberVouchers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Pizza> Pizzas { get; set; }
        public virtual DbSet<PizzaBase> PizzaBases { get; set; }
        public virtual DbSet<PizzaSize> PizzaSizes { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<Sauce> Sauces { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Topping> Toppings { get; set; }
        public virtual DbSet<ToppingDetail> ToppingDetails { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer("");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Base>(entity =>
            {
                entity.ToTable("Base");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Drink>(entity =>
            {
                entity.Property(e => e.Brand).HasMaxLength(200);

                entity.Property(e => e.Image)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.Drinks)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DrinkSize");
            });

            modelBuilder.Entity<Extra>(entity =>
            {
                entity.Property(e => e.ExtraName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Image)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Avatar)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Point).HasDefaultValueSql("((0))");

                entity.Property(e => e.Role).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Rank)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.RankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberRank");
            });

            modelBuilder.Entity<MemberVoucher>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.Member)
                    .WithMany()
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MemberVou__Membe__440B1D61");

                entity.HasOne(d => d.Voucher)
                    .WithMany()
                    .HasForeignKey(d => d.VoucherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MemberVou__Vouch__4316F928");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Freight).HasColumnType("money");

                entity.Property(e => e.Note).HasMaxLength(1000);

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.RequiredDate).HasColumnType("date");

                entity.Property(e => e.ShippedDate).HasColumnType("date");

                entity.Property(e => e.Status)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Pending')");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdersMember");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("OrderDetail");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Size).HasMaxLength(20);

                entity.HasOne(d => d.Drink)
                    .WithMany()
                    .HasForeignKey(d => d.DrinkId)
                    .HasConstraintName("FK__OrderDeta__Drink__7B5B524B");

                entity.HasOne(d => d.Extra)
                    .WithMany()
                    .HasForeignKey(d => d.ExtraId)
                    .HasConstraintName("FK__OrderDeta__Extra__7C4F7684");

                entity.HasOne(d => d.Order)
                    .WithMany()
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__Order__797309D9");

                entity.HasOne(d => d.Pizza)
                    .WithMany()
                    .HasForeignKey(d => d.PizzaId)
                    .HasConstraintName("FK__OrderDeta__Pizza__7A672E12");
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Image)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Pizzas)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PizzaCategory");

                entity.HasOne(d => d.Sauce)
                    .WithMany(p => p.Pizzas)
                    .HasForeignKey(d => d.SauceId)
                    .HasConstraintName("FK_PizzaSauce");
            });

            modelBuilder.Entity<PizzaBase>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Pizza_Base");

                entity.HasOne(d => d.Base)
                    .WithMany()
                    .HasForeignKey(d => d.BaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pizza_Bas__BaseI__5165187F");

                entity.HasOne(d => d.Pizza)
                    .WithMany()
                    .HasForeignKey(d => d.PizzaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pizza_Bas__Pizza__5070F446");
            });

            modelBuilder.Entity<PizzaSize>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Pizza_Size");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Pizza)
                    .WithMany()
                    .HasForeignKey(d => d.PizzaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pizza_Siz__Pizza__4BAC3F29");

                entity.HasOne(d => d.Size)
                    .WithMany()
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pizza_Siz__SizeI__4CA06362");
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Sauce>(entity =>
            {
                entity.ToTable("Sauce");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("Size");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Topping>(entity =>
            {
                entity.Property(e => e.Image)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ToppingName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Toppings)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ToppingCategory");
            });

            modelBuilder.Entity<ToppingDetail>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.Pizza)
                    .WithMany()
                    .HasForeignKey(d => d.PizzaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ToppingDe__Pizza__398D8EEE");

                entity.HasOne(d => d.Topping)
                    .WithMany()
                    .HasForeignKey(d => d.ToppingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ToppingDe__Toppi__3A81B327");
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.VoucherCode)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}