namespace Illallangi.Msdn.Model
{
    public sealed class ProductCategory
    {
        public string Brand { get; set; }
        public string Name { get; set; }
        public int ProductGroupId { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}