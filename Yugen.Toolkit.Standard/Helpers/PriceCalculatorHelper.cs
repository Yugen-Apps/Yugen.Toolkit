using System;

namespace Yugen.Toolkit.Standard.Helpers
{
    //http://www.thecodingguys.net/tutorials/csharp/csharp-creating-and-managing-classes-vat-calculator
    public static class PriceCalculatorHelper
    {
        public static decimal PriceFromPercentage(decimal price, decimal percentage) => price * percentage / 100;
        public static decimal VatPercentageMultiplier(decimal vatRate) => (vatRate + 100) / 100;

        public static decimal PriceWithVat(decimal price, decimal vat) => (Math.Round(price * VatPercentageMultiplier(vat), 2));
        public static decimal OriginalPrice(decimal price, decimal vatRate) => (Math.Round(price / VatPercentageMultiplier(vatRate), 2));
        public static decimal VatAmountFromPrice(decimal price, decimal vatRate) => PriceWithVat(price, vatRate) - price;

        public static decimal PriceWithDiscountPercentage(decimal price, decimal discountPercentage) => price - PriceFromPercentage(price, discountPercentage);
        public static decimal PriceWithDiscount(decimal price, decimal discount) => price - discount;
    }
}