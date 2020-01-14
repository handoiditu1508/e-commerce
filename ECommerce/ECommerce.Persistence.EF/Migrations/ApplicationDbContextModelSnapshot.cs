﻿// <auto-generated />
using System;
using ECommerce.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ECommerce.Persistence.EF.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ECommerce.Models.Entities.Admins.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("ECommerce.Models.Entities.Categories.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("ECommerce.Models.Entities.Customers.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("ECommerce.Models.Entities.Customers.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("CurrentPrice");

                    b.Property<int>("CustomerId");

                    b.Property<int>("ProductTypeId");

                    b.Property<short>("Quantity");

                    b.Property<int>("SellerId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductTypeId");

                    b.HasIndex("SellerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ECommerce.Models.Entities.Customers.OrderAttribute", b =>
                {
                    b.Property<int>("OrderId");

                    b.Property<string>("Name");

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("OrderId", "Name");

                    b.ToTable("OrderAttribute");
                });

            modelBuilder.Entity("ECommerce.Models.Entities.ProductTypes.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductType");
                });

            modelBuilder.Entity("ECommerce.Models.Entities.ProductTypes.ProductTypeUpdateRequest", b =>
                {
                    b.Property<int>("SellerId");

                    b.Property<int>("ProductTypeId");

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Descriptions")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("SellerId", "ProductTypeId");

                    b.HasAlternateKey("ProductTypeId", "SellerId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductTypeUpdateRequest");
                });

            modelBuilder.Entity("ECommerce.Models.Entities.Sellers.Comment", b =>
                {
                    b.Property<int>("SellerId");

                    b.Property<int>("ProductTypeId");

                    b.Property<int>("CustomerId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("SerializedImages")
                        .HasColumnName("Images");

                    b.Property<byte>("Stars");

                    b.Property<string>("Subject")
                        .IsRequired();

                    b.HasKey("SellerId", "ProductTypeId", "CustomerId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("ECommerce.Models.Entities.Sellers.Product", b =>
                {
                    b.Property<int>("SellerId");

                    b.Property<int>("ProductTypeId");

                    b.Property<bool>("Active");

                    b.Property<int>("Model");

                    b.Property<decimal>("Price");

                    b.Property<short>("Quantity");

                    b.Property<string>("RepresentativeImage")
                        .IsRequired();

                    b.Property<string>("SerializedAttributesStates")
                        .HasColumnName("AttributesStates");

                    b.Property<string>("SerializedImages")
                        .HasColumnName("Images");

                    b.Property<int>("Status");

                    b.HasKey("SellerId", "ProductTypeId");

                    b.HasAlternateKey("ProductTypeId", "SellerId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("ECommerce.Models.Entities.Sellers.ProductAttribute", b =>
                {
                    b.Property<int>("SellerId");

                    b.Property<int>("ProductTypeId");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.Property<short>("Order");

                    b.HasKey("SellerId", "ProductTypeId", "Name", "Value");

                    b.ToTable("ProductAttribute");
                });

            modelBuilder.Entity("ECommerce.Models.Entities.Sellers.Seller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<int>("Status");

                    b.Property<string>("StoreName")
                        .IsRequired();

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Seller");
                });

            modelBuilder.Entity("ECommerce.Models.Entities.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("ECommerce.Models.Entities.Admins.Admin", b =>
                {
                    b.HasOne("ECommerce.Models.Entities.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("ECommerce.Models.Entities.Categories.Category", b =>
                {
                    b.HasOne("ECommerce.Models.Entities.Categories.Category", "ParentCategory")
                        .WithMany("ChildCategories")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("ECommerce.Models.Entities.Customers.Customer", b =>
                {
                    b.HasOne("ECommerce.Models.Entities.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("ECommerce.Models.Entities.Customers.Order", b =>
                {
                    b.HasOne("ECommerce.Models.Entities.Customers.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ECommerce.Models.Entities.ProductTypes.ProductType", "ProductType")
                        .WithMany("Orders")
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ECommerce.Models.Entities.Sellers.Seller", "Seller")
                        .WithMany("Orders")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ECommerce.Models.Entities.Customers.OrderAttribute", b =>
                {
                    b.HasOne("ECommerce.Models.Entities.Customers.Order", "Order")
                        .WithMany("Attributes")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ECommerce.Models.Entities.ProductTypes.ProductType", b =>
                {
                    b.HasOne("ECommerce.Models.Entities.Categories.Category", "Category")
                        .WithMany("ProductTypes")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ECommerce.Models.Entities.ProductTypes.ProductTypeUpdateRequest", b =>
                {
                    b.HasOne("ECommerce.Models.Entities.Categories.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ECommerce.Models.Entities.ProductTypes.ProductType", "ProductType")
                        .WithMany("UpdateRequests")
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ECommerce.Models.Entities.Sellers.Seller", "Seller")
                        .WithMany("ProductTypeUpdateRequests")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ECommerce.Models.Entities.Sellers.Comment", b =>
                {
                    b.HasOne("ECommerce.Models.Entities.Customers.Customer", "Customer")
                        .WithMany("Comments")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ECommerce.Models.Entities.Sellers.Product", "Product")
                        .WithMany("Comments")
                        .HasForeignKey("SellerId", "ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ECommerce.Models.Entities.Sellers.Product", b =>
                {
                    b.HasOne("ECommerce.Models.Entities.ProductTypes.ProductType", "ProductType")
                        .WithMany("Products")
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ECommerce.Models.Entities.Sellers.Seller", "Seller")
                        .WithMany("Products")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ECommerce.Models.Entities.Sellers.ProductAttribute", b =>
                {
                    b.HasOne("ECommerce.Models.Entities.Sellers.Product", "Product")
                        .WithMany("SplittedAttributes")
                        .HasForeignKey("SellerId", "ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ECommerce.Models.Entities.Sellers.Seller", b =>
                {
                    b.HasOne("ECommerce.Models.Entities.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("ECommerce.Models.Entities.Users.User", b =>
                {
                    b.OwnsOne("ECommerce.Models.Entities.FullName", "Name", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnName("FirstName")
                                .HasMaxLength(20);

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnName("LastName")
                                .HasMaxLength(20);

                            b1.Property<string>("MiddleName")
                                .HasColumnName("MiddleName")
                                .HasMaxLength(20);

                            b1.HasKey("UserId");

                            b1.ToTable("User");

                            b1.HasOne("ECommerce.Models.Entities.Users.User")
                                .WithOne("Name")
                                .HasForeignKey("ECommerce.Models.Entities.FullName", "UserId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
