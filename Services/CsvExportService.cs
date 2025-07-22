using BlazorApp1.Models;
using Microsoft.Extensions.Localization;
using System.Text;

namespace BlazorApp1.Services
{
    public class CsvExportService
    {
        public byte[] GenerateCustomerCsv(List<Customer> customers, IStringLocalizer localizer)
        {
            var sb = new StringBuilder();

            sb.AppendLine(string.Join(",",
                localizer["index"],
                localizer["id"],
                localizer["name"],
                localizer["surname"],
                localizer["company"],
                localizer["city"],
                localizer["country"],
                localizer["phone"] + "1",
                localizer["phone"] + "2",
                localizer["email"],
                localizer["subDate"],
                localizer["website"]
            ));

            foreach (var m in customers)
            {
                sb.AppendLine(string.Join(",", new string[]
                {
                    m.Index.ToString(),
                    m.Customer_Id,
                    m.First_Name,
                    m.Last_Name,
                    m.Company,
                    m.City,
                    m.Country,
                    m.Phone_1,
                    m.Phone_2,
                    m.Email,
                    m.Subscription_Date.ToString(),
                    m.Website
                }.Select(EscapeCsvValue)));
            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        private string EscapeCsvValue(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";

            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
            {
                value = value.Replace("\"", "\"\"");
                return $"\"{value}\"";
            }

            return value;
        }
    }
}

