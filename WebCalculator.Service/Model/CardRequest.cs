using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalculator.Service.Model
{
    public record CardRequest(string CardNumber, int ExpireMonth, int ExpireYear, string CVC, int CountryId);
}
