using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class OutletProductReturnConfiguration : EntityTypeConfiguration<OutletProductReturn>
    {
        public OutletProductReturnConfiguration()
        {
            ToTable("OutletProductReturn");
            HasKey(o => o.OutletProductReturnID);
            Property(o => o.ReturnQuantity).IsRequired();
            Property(o => o.ReturnDate).IsRequired();

        }
    }
}
