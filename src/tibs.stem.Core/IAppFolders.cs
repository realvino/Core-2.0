namespace tibs.stem
{
    public interface IAppFolders
    {
        string TempFileDownloadFolder { get; }

        string SampleProfileImagesFolder { get; }

        string WebLogsFolder { get; set; } 

        string ProductFilePath { get; set; }

        string FindFilePath { get; set; }

        string ColorCodeFilePath { get; set; }
        string ProductSpecificationFilePath { get; set; }
        string QuotationProductPath { get; set; }
        string ImportProductFilePath { get; set; }
        string StandardPDF { get; set; }
        string PhotoEmphasisPDF { get; set; }
        string ProductCategoryPDF { get; set; }
        string TempProductFilePath { get; set; }
        string ProfilePath { get; set; }


    }
}