using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopBasedOnMs.Services.Catalog.CatalogApi.Domain.AggregatesModel;

namespace ShopBasedOnMs.Services.Catalog.CatalogApi.Infrastructure.EntityConfiguration;
//Ignoring warnings those related with missing-xml-documents. Who cares? :)
#pragma warning disable 1591
public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
            .UseHiLo("category_hilo")
            .IsRequired();

        builder.Property<string>("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(512);

        builder.Property<string>("_imageUrl")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ImageUrl");

        builder.UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Ignore(b => b.DomainEvents);
    }
}