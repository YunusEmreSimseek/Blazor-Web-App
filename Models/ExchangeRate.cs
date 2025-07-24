using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Models
{

    public class ExchangeRate
    {
        public int RBr { get; set; }                      // Number of exchange rate list
        public DateTime Datum { get; set; }               // Exchange rate list validity date
        public string Valuta { get; set; }                // Currency code (as string!)
        public string Oznaka { get; set; }                // Type of currency
        public string Drzava { get; set; }                // Country name - Macedonian
        public int Nomin { get; set; }                    // Currency units
        public double Sreden { get; set; }                // Exchange rate
        public string DrzavaAng { get; set; }             // Country name - English
        public string DrzavaAl { get; set; }              // Country name - Albanian
        public string NazivMak { get; set; }              // Currency name - Macedonian
        public string NazivAng { get; set; }              // Currency name - English
        public string NazivAl { get; set; }               // Currency name - Albanian
        public DateTime Datum_f { get; set; }             // Creation date of rate
    
        public double? Kupoven { get; set; }              // Buying rate
        public double? Prodazen { get; set; }             // Selling rate
}

    // public class ExchangeRate
    // {
    //     public string Id { get; set; } = Guid.NewGuid().ToString();
    //     public int RBr { get; set; }
    //     public DateTime Datum { get; set; }
    //     public double? Valuta { get; set; }
    //     public double? Nomin { get; set; }
    //     public string? Oznaka { get; set; }
    //     public string? Drzava { get; set; }
    //     public string? DrzavaAng { get; set; }
    //     public string? DrzavaAlt { get; set; }
    //     public string? NazivMak { get; set; }
    //     public string? NazivAng { get; set; }
    //     public string? NazivAl { get; set; }
    //     public double? Sreden { get; set; }
    //     public double? Datum_f { get; set; }

    // }
}