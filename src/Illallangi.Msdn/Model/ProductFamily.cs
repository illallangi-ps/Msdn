namespace Illallangi.Msdn.Model
{
    public sealed class ProductFamily
    {
        private string currentTitle;

        public int ProductFamilyId { get; set; }

        public string Title
        {
            get
            {
                return this.currentTitle.Replace('"', '\'');
            }
            set
            {
                this.currentTitle = value;
            }
        }

        public int ProductGroupId { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}