using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class EquipmentTypeConfiguration : EntityTypeConfiguration<EquipmentType>
    {
        public EquipmentTypeConfiguration()
        {
            ToTable("EquipmentType");
            HasKey(e => e.EquipmentTypeID);
            Property(e => e.EquipmentTypeName).IsRequired().HasMaxLength(256);
        }
    }
}
