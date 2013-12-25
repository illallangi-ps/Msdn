namespace Illallangi.Msdn.Model
{
    public sealed class ProductFamily
    {
        public int ProductFamilyId { get; set; }
        public string Title { get; set; }
        public int ProductGroupId { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }


}