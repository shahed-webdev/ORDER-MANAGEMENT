using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class Outlet_ImagesConfiguration : EntityTypeConfiguration<Outlet_Images>
    {
        public Outlet_ImagesConfiguration()
        {
            ToTable("Outlet_Images");
            HasKey(i => i.OutletImageID);
            Property(i => i.OutletImage).IsRequired();
            HasRequired(i => i.Outlet).WithMany(o => o.Outlet_Images).HasForeignKey(i => i.OutletID);
        }
    }
}
