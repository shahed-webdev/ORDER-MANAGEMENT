using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    class ChangeLogConfiguration : EntityTypeConfiguration<ChangeLog>
    {
        public ChangeLogConfiguration()
        {
            ToTable("ChangeLog");
            HasKey(c => c.Id);

        }
    }
}
