using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ContactDesignation.Dto;
using tibs.stem.Dto;

namespace tibs.stem.ContactDesignation.Exporting
{
    public interface IContactDesignationExcelExporter
    {
        FileDto ExportToFile(List<ContactDesignationInput> ContactDesignationList);
    }
}
