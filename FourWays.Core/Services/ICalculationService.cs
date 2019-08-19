using System;
using System.Collections.Generic;
using System.Text;

namespace FourWays.Core.Services
{
    public interface ICalculationService
    {
        decimal TipAmount(decimal subTotal, double generosity);
    }
}
