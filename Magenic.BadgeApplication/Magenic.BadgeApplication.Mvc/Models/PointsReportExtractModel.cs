using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.Resources;
using SpreadsheetLight;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Magenic.BadgeApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class PointsReportExportModel
        : IDisposable
    {
        private const string pointsReportName = "Points Report";
        private const string pointsReportTableName = "PointsReportTable";

        private const string badgeAwardName = "Badge Awards";
        private const string badgeAwardTableName = "BadgeAwardTable";

        private const string combinedValuesName = "Combined Values";
        private const string combinedTableName = "CombinedTable";
        private const string defaultSheetName = "Sheet1";

        private const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private const string fileName = "PointsReportExport.xlsx";

        private SLDocument spreadsheet { get; set; }
        private IPointsReportCollection pointsReportCollection { get; set; }
        private IBadgeAwardEditCollection badgeAwardEditCollection { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointsReportExportModel" /> class.
        /// </summary>
        /// <param name="pointsReportCollection">The points report collection.</param>
        /// <param name="badgeAwardEditCollection">The badge award edit collection.</param>
        public PointsReportExportModel(IPointsReportCollection pointsReportCollection, IBadgeAwardEditCollection badgeAwardEditCollection)
        {
            this.spreadsheet = new SLDocument();
            this.pointsReportCollection = pointsReportCollection;
            this.badgeAwardEditCollection = badgeAwardEditCollection;
        }

        private void SetDefaultWorksheets()
        {
            spreadsheet.AddWorksheet(pointsReportName);
            spreadsheet.AddWorksheet(badgeAwardName);
            spreadsheet.AddWorksheet(combinedValuesName);
            spreadsheet.DeleteWorksheet(defaultSheetName);
        }

        private void CreatePointsReportWorksheet()
        {
            spreadsheet.SelectWorksheet(pointsReportName);

            // Header
            spreadsheet.SetCellValue(1, 1, ApplicationResources.PointsReportExportHeaderUserName);
            spreadsheet.SetCellValue(1, 2, ApplicationResources.PointsReportExportHeaderFirstName);
            spreadsheet.SetCellValue(1, 3, ApplicationResources.PointsReportExportHeaderLastName);
            spreadsheet.SetCellValue(1, 4, ApplicationResources.PointsReportExportHeaderLocation);
            spreadsheet.SetCellValue(1, 5, ApplicationResources.PointsReportExportHeaderTotalPoints);
            spreadsheet.SetColumnStyle(5, new SLStyle() { FormatCode = "#,##0" });

            // Data
            var row = 1;
            var column = 1;
            foreach (var pointsReportItem in pointsReportCollection)
            {
                row++;
                spreadsheet.SetCellValue(row, column++, pointsReportItem.EmployeeADName);
                spreadsheet.SetCellValue(row, column++, pointsReportItem.EmployeeFirstName);
                spreadsheet.SetCellValue(row, column++, pointsReportItem.EmployeeLastName);
                spreadsheet.SetCellValue(row, column++, pointsReportItem.EmployeeLocation);
                spreadsheet.SetCellValue(row, column++, pointsReportItem.TotalPoints);
                column = 1;
            }

            for (var index = 1; index <= 5; index++)
            {
                spreadsheet.AutoFitColumn(index);
            }

            var pointsReportTable = spreadsheet.CreateTable("A1", String.Format(CultureInfo.CurrentCulture, "E{0}", row));
            pointsReportTable.SetTableStyle(SLTableStyleTypeValues.Medium9);
            pointsReportTable.DisplayName = pointsReportTableName;
            pointsReportTable.Sort(1, true);
            spreadsheet.InsertTable(pointsReportTable);
        }

        private void CreateBadgeAwardWorksheet()
        {
            spreadsheet.SelectWorksheet(badgeAwardName);

            // Header
            spreadsheet.SetCellValue(1, 1, ApplicationResources.PointsReportExportHeaderUserName);
            spreadsheet.SetCellValue(1, 2, ApplicationResources.PointsReportExportHeaderBadgeName);
            spreadsheet.SetCellValue(1, 3, ApplicationResources.PointsReportExportHeaderAwardDate);
            spreadsheet.SetCellValue(1, 4, ApplicationResources.PointsReportExportHeaderAwardAmount);
            spreadsheet.SetColumnStyle(3, new SLStyle() { FormatCode = "MM/dd/yyyy" });
            spreadsheet.SetColumnStyle(4, new SLStyle() { FormatCode = "#,##0" });

            // Data
            var row = 1;
            var column = 1;
            var badgeAwardItems = badgeAwardEditCollection.Where(bae => bae.AwardAmount > 0);
            foreach (var badgeAwardItem in badgeAwardItems)
            {
                row++;
                spreadsheet.SetCellValue(row, column++, badgeAwardItem.EmployeeADName);
                spreadsheet.SetCellValue(row, column++, badgeAwardItem.BadgeName);
                spreadsheet.SetCellValue(row, column++, badgeAwardItem.AwardDate);
                spreadsheet.SetCellValue(row, column++, badgeAwardItem.AwardAmount);
                column = 1;
            }

            for (var index = 1; index <= 4; index++)
            {
                spreadsheet.AutoFitColumn(index);
            }

            var badgeAwardTable = spreadsheet.CreateTable("A1", String.Format(CultureInfo.CurrentCulture, "D{0}", row));
            badgeAwardTable.SetTableStyle(SLTableStyleTypeValues.Medium11);
            badgeAwardTable.DisplayName = badgeAwardTableName;
            badgeAwardTable.Sort(1, true);
            spreadsheet.InsertTable(badgeAwardTable);
        }

        private void CreateCombinedWorksheet()
        {
            spreadsheet.SelectWorksheet(combinedValuesName);

            // Header
            spreadsheet.SetCellValue(1, 1, ApplicationResources.PointsReportExportHeaderUserName);
            spreadsheet.SetCellValue(1, 2, ApplicationResources.PointsReportExportHeaderFirstName);
            spreadsheet.SetCellValue(1, 3, ApplicationResources.PointsReportExportHeaderLastName);
            spreadsheet.SetCellValue(1, 4, ApplicationResources.PointsReportExportHeaderLocation);
            spreadsheet.SetCellValue(1, 5, ApplicationResources.PointsReportExportHeaderBadgeName);
            spreadsheet.SetCellValue(1, 6, ApplicationResources.PointsReportExportHeaderAwardDate);
            spreadsheet.SetCellValue(1, 7, ApplicationResources.PointsReportExportHeaderAwardAmount);
            spreadsheet.SetColumnStyle(6, new SLStyle() { FormatCode = "MM/dd/yyyy" });
            spreadsheet.SetColumnStyle(7, new SLStyle() { FormatCode = "#,##0" });

            // Data
            var row = 1;
            var column = 1;
            var badgeAwardItems = badgeAwardEditCollection.Where(bae => bae.AwardAmount > 0);
            foreach (var badgeAwardItem in badgeAwardItems)
            {
                var pointsReportItem = pointsReportCollection.Where(pri => pri.EmployeeADName == badgeAwardItem.EmployeeADName).SingleOrDefault();

                if (pointsReportItem != null)
                {
                    row++;
                    spreadsheet.SetCellValue(row, column++, pointsReportItem.EmployeeADName);
                    spreadsheet.SetCellValue(row, column++, pointsReportItem.EmployeeFirstName);
                    spreadsheet.SetCellValue(row, column++, pointsReportItem.EmployeeLastName);
                    spreadsheet.SetCellValue(row, column++, pointsReportItem.EmployeeLocation);
                    spreadsheet.SetCellValue(row, column++, badgeAwardItem.BadgeName);
                    spreadsheet.SetCellValue(row, column++, badgeAwardItem.AwardDate);
                    spreadsheet.SetCellValue(row, column++, badgeAwardItem.AwardAmount);
                    column = 1;
                }
            }

            for (var index = 1; index <= 7; index++)
            {
                spreadsheet.AutoFitColumn(index);
            }

            var combinedValuesTable = spreadsheet.CreateTable("A1", String.Format(CultureInfo.CurrentCulture, "G{0}", row));
            combinedValuesTable.SetTableStyle(SLTableStyleTypeValues.Medium4);
            combinedValuesTable.DisplayName = combinedTableName;
            combinedValuesTable.Sort(1, true);
            spreadsheet.InsertTable(combinedValuesTable);
        }

        /// <summary>
        /// Creates the spreadsheet.
        /// </summary>
        public byte[] CreateSpreadsheet()
        {
            this.SetDefaultWorksheets();
            this.CreatePointsReportWorksheet();
            this.CreateBadgeAwardWorksheet();
            this.CreateCombinedWorksheet();

            using (var memoryStream = new MemoryStream())
            {
                spreadsheet.SaveAs(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (spreadsheet != null)
                {
                    spreadsheet.Dispose();
                    spreadsheet = null;
                }
            }
        }
    }
}