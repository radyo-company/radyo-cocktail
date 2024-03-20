using Cocktail.Domain;
using Cocktail.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cocktail.Infrastructure.Configurations;

public abstract class EntityTypeConfiguration< TEntity, TKeyType > : IEntityTypeConfiguration< TEntity > where TEntity : Entity< TKeyType >
{
    public virtual void Configure( EntityTypeBuilder< TEntity > entityTypeBuilder )
    {
        entityTypeBuilder.Property<DateTimeOffset>(AuditField.CreatedOn).IsRequired();
        entityTypeBuilder.Property< DateTimeOffset? >( AuditField.LastModifiedOn );
        entityTypeBuilder.Property< DateTimeOffset? >( AuditField.DeletedOn );
        entityTypeBuilder.HasQueryFilter(x => x.DeletedOn == null);
    }
}