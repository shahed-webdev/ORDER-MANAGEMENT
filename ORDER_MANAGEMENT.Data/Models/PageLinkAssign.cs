namespace ORDER_MANAGEMENT.Data
{
    public class PageLinkAssign
    {
        public int LinkAssignID { get; set; }
        public int RegistrationID { get; set; }
        public int LinkID { get; set; }
        public virtual Registration Registration { get; set; }
        public virtual PageLink PageLink { get; set; }
    }
}
