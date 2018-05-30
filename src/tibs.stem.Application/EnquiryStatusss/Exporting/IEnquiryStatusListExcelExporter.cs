using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.EnquiryStatusss.Dto;

namespace tibs.stem.EnquiryStatusss.Exporting
{
    public interface IEnquiryStatusListExcelExporter
    {
        FileDto ExportToFile(List<EnquiryStatusListDto> enquiryStatusListDtos);   
    }
}
