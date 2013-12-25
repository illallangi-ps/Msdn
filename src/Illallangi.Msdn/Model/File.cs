using System;

namespace Illallangi.Msdn.Model
{
    public sealed class File
    {
        public string[] BenefitLevels { get; set; }
        public string Description { get; set; }
        public int DownloadProvider { get; set; }
        public int FileId { get; set; }
        public string FileName { get; set; }
        public bool IsAuthorization { get; set; }
        public bool IsProductKeyRequired { get; set; }
        public string[] LanguageCodes { get; set; }
        public string[] Languages { get; set; }
        public int NotAuthorizedReadonId { get; set; }
        public string Notes { get; set; }
        public DateTime PostedDate { get; set; }
        public int ProductFamilyId { get; set; }
        public string Sha1Hash { get; set; }
        public string Size { get; set; }

        public override string ToString()
        {
            return this.Description.Trim(Environment.NewLine.ToCharArray()).Trim().Replace(":", string.Empty);
        }
    }
}