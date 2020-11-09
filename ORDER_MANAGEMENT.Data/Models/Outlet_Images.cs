namespace ORDER_MANAGEMENT.Data
{
    public class Outlet_Images
    {
        public int OutletImageID { get; set; }
        public int OutletID { get; set; }
        public byte[] OutletImage { get; set; }
        public virtual Outlet Outlet { get; set; }

    }
}
