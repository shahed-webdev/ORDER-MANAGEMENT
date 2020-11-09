using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class EquipmentConfiguration : EntityTypeConfiguration<Equipment>
    {
        public EquipmentConfiguration()
        {
            ToTable("Equipment");
            HasKey(e => e.EquipmentID);
            Property(e => e.Code).IsRequired().HasMaxLength(50);
            HasRequired(e => e.EquipmentType).WithMany(t => t.Equipments).HasForeignKey(e => e.EquipmentTypeID);
        }
    }
}
