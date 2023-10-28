using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopBasedOnMs.Services.Catalog.CatalogApi.Domain.Entities;

namespace ShopBasedOnMs.Services.Catalog.CatalogApi.Infrastructure.EntityConfiguration;

//Ignoring warnings those related with missing-xml-documents.
#pragma warning disable 1591
public class CatalogItemEntityConfiguration : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        //Ignoring virtual properties to avoid storing
        builder.Ignore(x => x.FinalPrice);

        builder.Property(x => x.Id)
                .UseHiLo("catalog_hilo")
                .IsRequired();


        //Relations
        builder.HasOne(x => x.Category)
                .WithMany(x => x.CatalogItems)
                .HasForeignKey(x => x.CategoryId);
    }
}