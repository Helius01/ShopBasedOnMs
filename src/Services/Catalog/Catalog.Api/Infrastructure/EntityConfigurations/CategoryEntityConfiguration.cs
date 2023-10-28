using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopBasedOnMs.Services.Catalog.CatalogApi.Domain.Entities;

namespace ShopBasedOnMs.Services.Catalog.CatalogApi.Infrastructure.EntityConfiguration;
//Ignoring warnings those related with missing-xml-documents. Who cares? :)
#pragma warning disable 1591
public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
                .UseHiLo("category_hilo")
                .IsRequired();
    }
}