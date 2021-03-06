﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.Quotationss.Dto;

namespace tibs.stem.Quotationss.Exporting
{
    public class AllTeamReportExcelExporter: EpPlusExcelExporterBase, IAllTeamReportExcelExporter
    {
        public FileDto ExportToFile(List<TeamReportListDto> AllTeamReportList, TeamReportListDto TotalReport)
        {
            return CreateExcelPackage(
                "ForecastReport.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ForecastReport"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Team"),
                        L("AEDValue"),
                        L("WeightedAED"),
                        L(AllTeamReportList[0].Total1ValueFormat),
                        L(AllTeamReportList[0].Total2ValueFormat),
                        L(AllTeamReportList[0].Total3ValueFormat),
                        L(AllTeamReportList[0].Total4ValueFormat),
                        L(AllTeamReportList[0].Total5ValueFormat),
                        L(AllTeamReportList[0].Total6ValueFormat),
                        L(AllTeamReportList[0].Total7ValueFormat),
                        L(AllTeamReportList[0].Total8ValueFormat),
                        L(AllTeamReportList[0].Total9ValueFormat),
                        L(AllTeamReportList[0].Total10ValueFormat),
                        L(AllTeamReportList[0].Total11ValueFormat),
                        L(AllTeamReportList[0].Total12ValueFormat)
                        );

                    AddObjects(
                        sheet, 2, AllTeamReportList,
                        _ => _.TeamName,
                        _ => _.TotalAEDValue,
                        _ => _.TotalWeightedAED,
                        _ => _.Total1Value,
                        _ => _.Total2Value,
                        _ => _.Total3Value,
                        _ => _.Total4Value,
                        _ => _.Total5Value,
                        _ => _.Total6Value,
                        _ => _.Total7Value,
                        _ => _.Total8Value,
                        _ => _.Total9Value,
                        _ => _.Total10Value,
                        _ => _.Total11Value,
                        _ => _.Total12Value
                        );

                    for (var i = 1; i <= 15; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }

                    sheet.InsertRow(AllTeamReportList.Count + 2, 2);

                    sheet.Row(AllTeamReportList.Count + 3).Style.Font.Bold = true;

                    sheet.Cells[AllTeamReportList.Count + 3, 1].Value = TotalReport.TeamName;
                    sheet.Cells[AllTeamReportList.Count + 3, 2].Value = TotalReport.TotalAEDValue;
                    sheet.Cells[AllTeamReportList.Count + 3, 3].Value = TotalReport.TotalWeightedAED;
                    sheet.Cells[AllTeamReportList.Count + 3, 4].Value = TotalReport.Total1Value;
                    sheet.Cells[AllTeamReportList.Count + 3, 5].Value = TotalReport.Total2Value;
                    sheet.Cells[AllTeamReportList.Count + 3, 6].Value = TotalReport.Total3Value;
                    sheet.Cells[AllTeamReportList.Count + 3, 7].Value = TotalReport.Total4Value;
                    sheet.Cells[AllTeamReportList.Count + 3, 8].Value = TotalReport.Total5Value;
                    sheet.Cells[AllTeamReportList.Count + 3, 9].Value = TotalReport.Total6Value;
                    sheet.Cells[AllTeamReportList.Count + 3, 10].Value = TotalReport.Total7Value;
                    sheet.Cells[AllTeamReportList.Count + 3, 11].Value = TotalReport.Total8Value;
                    sheet.Cells[AllTeamReportList.Count + 3, 12].Value = TotalReport.Total9Value;
                    sheet.Cells[AllTeamReportList.Count + 3, 13].Value = TotalReport.Total10Value;
                    sheet.Cells[AllTeamReportList.Count + 3, 14].Value = TotalReport.Total11Value;
                    sheet.Cells[AllTeamReportList.Count + 3, 15].Value = TotalReport.Total12Value;

                    int totalCol = 15;
                    for (var i = 4; i <= totalCol; i++)
                    {
                        decimal test = (decimal)sheet.Cells[AllTeamReportList.Count + 3, i].Value;

                        if (test <= 0.00M)
                        {
                            sheet.DeleteColumn(i);
                            totalCol = totalCol - 1;
                        }

                    }
                });
        }
    }
}
