using Abp.Dependency;

namespace tibs.stem
{
    public class AppFolders : IAppFolders, ISingletonDependency
    {
        public string TempFileDownloadFolder { get; set; }

        public string SampleProfileImagesFolder { get; set; }

        public string WebLogsFolder { get; set; }

        public string ProductFilePath { get; set; }

        public string FindFilePath { get; set; }

        public string ColorCodeFilePath { get; set; }

        public string ProductSpecificationFilePath { get; set; }
        public string QuotationProductPath { get; set; }
        public string ImportProductFilePath { get; set; }
        public string StandardPDF { get; set; }
        public string PhotoEmphasisPDF { get; set; }
        public string ProductCategoryPDF { get; set; }
        public string TempProductFilePath { get; set; }
        public string ProfilePath { get; set; }
    }
}