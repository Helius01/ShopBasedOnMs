using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopBasedOnMs.Services.Catalog.CatalogApi.Domain.AggregatesModel;

namespace ShopBasedOnMs.Services.Catalog.CatalogApi.Infrastructure.EntityConfiguration;

//Ignoring warnings those related with missing-xml-documents.
#pragma warning disable 1591
public class CatalogItemEntityConfiguration : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        //Ignoring virtual properties to avoid storing
        builder.Ignore(x => x.FinalPrice);
        builder.Ignore(x => x.DomainEvents);


        builder.HasKey(x => x.Id);
        builder.Property(ci => ci.Id)
            .UseHiLo("catalog_hilo");

        builder.UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Property<string>("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Name")
                .HasMaxLength(1024);

        builder.Property<string>("_description")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Description")
                .HasMaxLength(4096);

        builder.Property<decimal>("_price")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Price");

        builder.Property<decimal>("_discount")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Discount");

        builder.Property<int>("_categoryId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CategoryId");

        builder.Property<string>("_imageUrl")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ImageUrl");

        builder.Property<string>("_description")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Description");

        builder.HasOne(x => x.Category)
                .WithMany(x => x.CatalogItems)
                .HasForeignKey("_categoryId");
    }
}