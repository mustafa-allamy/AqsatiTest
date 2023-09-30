using Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Departments
{
    public class DepartmentReportSetting : BaseEntity<int>
    {
        public int DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))] public Department Department { get; set; }


        #region ExcelSettings


        public bool ExcelHasBorder { get; set; }
        public int ExcelHeaderFontSize { get; set; }
        public int ExcelHeaderHigh { get; set; }
        public bool ExcelHeaderFontBold { get; set; }
        public int ExcelCellsFontSize { get; set; }
        public int ExcelRowHigh { get; set; }
        public bool ExcelCellsFontBold { get; set; }
        public int ExcelPrinterScale { get; set; }
        public int ExcelRowsPerPage { get; set; }
        public decimal ExcelHeaderMargin { get; set; }
        public decimal ExcelFooterMargin { get; set; }
        public decimal ExcelLeftMargin { get; set; }
        public decimal ExcelRightMargin { get; set; }
        public bool ExcelIsHeaderRotated { get; set; }
        public bool ExcelAutoFitColumns { get; set; }
        public bool ShowPageTotal { get; set; }
        public bool ShowPageFooter { get; set; }
        #endregion

        #region ExcellFooterValues

        public string? RightText1 { get; set; }
        public string? RightText2 { get; set; }
        public string? CenterText1 { get; set; }
        public string? CenterText2 { get; set; }
        public string? LeftText1 { get; set; }
        public string? LeftText2 { get; set; }

        #endregion

        #region pdf

        public string? BankStatsPdfHeaderText { get; set; }

        #endregion
    }
}