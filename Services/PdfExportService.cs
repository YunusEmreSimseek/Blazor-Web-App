using BlazorApp1.Models;
using Microsoft.Extensions.Localization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace BlazorApp1.Services
{
    public class PdfExportService
    {

        public byte[] GenerateCustomerPdf(List<Customer> customers, IStringLocalizer localizer)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A2.Landscape());
                    page.Margin(30);
                    var date = DateTime.Now.ToString("D", CultureInfo.CurrentCulture);
                    page.Header().Text($"{localizer["customerList"]} - {date}").FontSize(18).Bold();
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                        });

                        table.Cell().Element(CellStyle).Text(localizer["index"]).SemiBold();
                        table.Cell().Element(CellStyle).Text(localizer["id"]).SemiBold();
                        table.Cell().Element(CellStyle).Text(localizer["name"]).SemiBold();
                        table.Cell().Element(CellStyle).Text(localizer["surname"]).SemiBold();
                        table.Cell().Element(CellStyle).Text(localizer["company"]).SemiBold();
                        table.Cell().Element(CellStyle).Text(localizer["city"]).SemiBold();
                        table.Cell().Element(CellStyle).Text(localizer["country"]).SemiBold();
                        table.Cell().Element(CellStyle).Text(localizer["phone"]+1).SemiBold();
                        table.Cell().Element(CellStyle).Text(localizer["phone"]+2).SemiBold();
                        table.Cell().Element(CellStyle).Text(localizer["email"]).SemiBold();
                        table.Cell().Element(CellStyle).Text(localizer["subDate"]).SemiBold();
                        table.Cell().Element(CellStyle).Text(localizer["website"]).SemiBold();

                        foreach (var m in customers)
                        {
                            table.Cell().Element(CellStyle).Text(m.Index);
                            table.Cell().Element(CellStyle).Text(m.Customer_Id);
                            table.Cell().Element(CellStyle).Text(m.First_Name);
                            table.Cell().Element(CellStyle).Text(m.Last_Name);
                            table.Cell().Element(CellStyle).Text(m.Company);
                            table.Cell().Element(CellStyle).Text(m.City);
                            table.Cell().Element(CellStyle).Text(m.Country);
                            table.Cell().Element(CellStyle).Text(m.Phone_1);
                            table.Cell().Element(CellStyle).Text(m.Phone_2);
                            table.Cell().Element(CellStyle).Text(m.Email);
                            table.Cell().Element(CellStyle).Text(m.Subscription_Date);
                            table.Cell().Element(CellStyle).Text(m.Website);
                        }

                        IContainer CellStyle(IContainer container) => container.Padding(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                    });
                });
            });

            using var stream = new MemoryStream();
            document.GeneratePdf(stream);
            return stream.ToArray();
        }
    }
}
