using AjGrantRefactorChallenge.Line;
using AjGrantRefactorChallenge.Receipts;
using NUnit.Framework;
using Shouldly;
using System;

namespace AjGrantRefactorChallenge.Tests
{
    public class ReceiptTest
    {
        private const string COMPANY_AJGRANT = "AjGrant";
        private const decimal TAX = .1m;
        private const string POLICY_HOLDER_JANE = "Jane Doe";
        private const string POLICY_HOLDER_JOHN = "John Doe";
        private const string DESC_BMW = "BMW";
        private const string DESC_HARLEY = "Harley";
        private const string DESC_SUNSHINE_COAST = "Sunshine Coast";
        private const decimal CAR_PRICE = 105m;
        private const decimal MOTORCYCLE_PRICE = 56m;
        private const decimal HOME_PRICE = 235m;
        private const string POLICY_CAR = "Car";
        private readonly static IPolicy BMW = new Policy(POLICY_CAR, POLICY_HOLDER_JANE, DESC_BMW, CAR_PRICE);
        private readonly static IPolicy Harley = new Policy("MotorCycle", POLICY_HOLDER_JOHN, DESC_HARLEY, MOTORCYCLE_PRICE);
        private readonly static IPolicy SunnyCoast = new Policy("Home", POLICY_HOLDER_JOHN, DESC_SUNSHINE_COAST, HOME_PRICE);
        private static DateTime GenerateTime;
        private static string GenerateTimeString = null!;

        [OneTimeSetUp]
        public void OneTimeSetUp(){
            GenerateTime = DateTime.Now;
            GenerateTimeString = string.Format("{0}", GenerateTime.ToString("F"));
        }

        private decimal GetLineAmount(string type, decimal price, int quantity) {
            if (quantity <= 1)
            {
                return quantity * price;
            }
            var discount = type.Equals(POLICY_CAR) ? .9m :.8m;
            return quantity * price * discount;
        }

        private string GenerateExpectedReceipt(string template, string lines, decimal amount, decimal tax, decimal total) =>
            template
            .Replace("{COMPANY_NAME}", COMPANY_AJGRANT)
            .Replace("{LINES}", lines)
            .Replace("{SUBTOTAL}", amount.ToString("C"))
            .Replace("{TAX}", tax.ToString("C"))
            .Replace("{TOTAL}", total.ToString("C"))
            .Replace("{DATETIME}", GenerateTimeString);

        #region Receipts
        IReceiptTemplate TextTemplate = new TextReceiptTemplate();
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(100)]
        public void ReceiptBMW(int quantity)
        {
            // arrange
            IOrder order = new Order(COMPANY_AJGRANT);
            decimal amount = GetLineAmount(BMW.PolicyType, CAR_PRICE, quantity);
            decimal tax = amount * TAX;
            decimal total = amount + tax;
            string lines = string.Format(TextTemplate.LineTemplate, quantity, POLICY_HOLDER_JANE, DESC_BMW, amount.ToString("C"));
            // Order Receipt for AjGrant{Environment.NewLine}\t1 x Jane Doe BMW = $105.00{Environment.NewLine}Sub-Total: $105.00{Environment.NewLine}Tax: $10.50{Environment.NewLine}Total: $115.50";
            var expectedResultStatementOneBMW = GenerateExpectedReceipt(TextTemplate.ReceiptTemplate, lines, amount, tax, total);
            // act
            order.AddLine(new CarLine(BMW, quantity));
            IReceipt receipt = new Receipt(order, TextTemplate);
            var result = receipt.GenerateReceipt(GenerateTime);
            // assert
            result.ShouldBe(expectedResultStatementOneBMW);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(100)]
        public void ReceiptHarley(int quantity)
        {
            // arrange
            IOrder order = new Order(COMPANY_AJGRANT);
            decimal amount = GetLineAmount(Harley.PolicyType, MOTORCYCLE_PRICE, quantity);
            decimal tax = amount * TAX;
            decimal total = amount + tax;
            string lines = string.Format(TextTemplate.LineTemplate, quantity, POLICY_HOLDER_JOHN, DESC_HARLEY, amount.ToString("C"));
            // Order Receipt for AjGrant{Environment.NewLine}\t1 x John Doe Harley = $56.00{Environment.NewLine}Sub-Total: $56.00{Environment.NewLine}Tax: $5.60{Environment.NewLine}Total: $61.60
            var expectedResultStatementOneHarley = GenerateExpectedReceipt(TextTemplate.ReceiptTemplate, lines, amount, tax, total);
            // act
            order.AddLine(new MotorCycleLine(Harley, quantity));
            IReceipt receipt = new Receipt(order, TextTemplate);
            var result = receipt.GenerateReceipt(GenerateTime);
            // assert
            result.ShouldBe(expectedResultStatementOneHarley);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(100)]
        public void ReceiptSunnyCoast(int quantity)
        {
            // arrange
            IOrder order = new Order(COMPANY_AJGRANT);
            decimal amount = GetLineAmount(SunnyCoast.PolicyType, HOME_PRICE, quantity);
            decimal tax = amount * TAX;
            decimal total = amount + tax;
            string lines = string.Format(TextTemplate.LineTemplate, quantity, POLICY_HOLDER_JOHN, DESC_SUNSHINE_COAST, amount.ToString("C"));
            // Order Receipt for AjGrant{Environment.NewLine}\t1 x John Doe Sunshine Coast = $235.00{Environment.NewLine}Sub-Total: $235.00{Environment.NewLine}Tax: $23.50{Environment.NewLine}Total: $258.50
            var expectedResultStatementOneSunnyCoast = GenerateExpectedReceipt(TextTemplate.ReceiptTemplate, lines, amount, tax, total);
            // act
            order.AddLine(new HomeLine(SunnyCoast, quantity));
            IReceipt receipt = new Receipt(order, TextTemplate);
            var result = receipt.GenerateReceipt(GenerateTime);
            // assert
            result.ShouldBe(expectedResultStatementOneSunnyCoast);
        }

        [TestCase(0, 0, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(2, 2, 2)]
        [TestCase(1, 10, 100)]
        [TestCase(100, 1, 10)]
        [TestCase(10, 100, 1)]
        public void ReceiptAll(int carQuantity, int motorcycleQuantity, int homeQuantity)
        {
            // arrange
            IOrder order = new Order(COMPANY_AJGRANT);
            decimal carAmount = GetLineAmount(BMW.PolicyType, CAR_PRICE, carQuantity);
            decimal motorcycleAmount = GetLineAmount(Harley.PolicyType, MOTORCYCLE_PRICE, motorcycleQuantity);
            decimal homeAmount = GetLineAmount(SunnyCoast.PolicyType, HOME_PRICE, homeQuantity);
            decimal totalAmount = carAmount + motorcycleAmount + homeAmount;
            decimal tax = totalAmount * TAX;
            decimal total = totalAmount + tax;
            string carLine = string.Format(TextTemplate.LineTemplate, carQuantity, POLICY_HOLDER_JANE, DESC_BMW, carAmount.ToString("C"));
            string motorcycleLine = string.Format(TextTemplate.LineTemplate, motorcycleQuantity, POLICY_HOLDER_JOHN, DESC_HARLEY, motorcycleAmount.ToString("C"));
            string homeLine = string.Format(TextTemplate.LineTemplate, homeQuantity, POLICY_HOLDER_JOHN, DESC_SUNSHINE_COAST, homeAmount.ToString("C"));
            var expectedResultStatementOneSunnyCoast = GenerateExpectedReceipt(TextTemplate.ReceiptTemplate, (carLine + motorcycleLine + homeLine), totalAmount, tax, total);
            // act
            order.AddLine(new CarLine(BMW, carQuantity));
            order.AddLine(new MotorCycleLine(Harley, motorcycleQuantity));
            order.AddLine(new HomeLine(SunnyCoast, homeQuantity));
            IReceipt receipt = new Receipt(order, TextTemplate);
            var result = receipt.GenerateReceipt(GenerateTime);
            // assert
            result.ShouldBe(expectedResultStatementOneSunnyCoast);
        }

        #endregion

        #region HTML Receipts
        IReceiptTemplate HtmlTemplate = new HtmlReceiptTemplate();
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(100)]
        public void HtmlReceiptBMW(int quantity)
        {
            // arrange
            IOrder order = new Order(COMPANY_AJGRANT);
            decimal amount = GetLineAmount(BMW.PolicyType, CAR_PRICE, quantity);
            decimal tax = amount * TAX;
            decimal total = amount + tax;
            string lines = string.Format(HtmlTemplate.LineTemplate, quantity, POLICY_HOLDER_JANE, DESC_BMW, amount.ToString("C"));
            var expectedHtmlResultStatementOneBMW = GenerateExpectedReceipt(HtmlTemplate.ReceiptTemplate, lines, amount, tax, total);
            // act
            order.AddLine(new CarLine(BMW, quantity));
            IReceipt receipt = new Receipt(order, HtmlTemplate);
            var result = receipt.GenerateReceipt(GenerateTime);
            // assert
            result.ShouldBe(expectedHtmlResultStatementOneBMW);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(100)]
        public void HtmlReceiptHarley(int quantity)
        {
            // arrange
            IOrder order = new Order(COMPANY_AJGRANT);
            decimal amount = GetLineAmount(Harley.PolicyType, MOTORCYCLE_PRICE, quantity);
            decimal tax = amount * TAX;
            decimal total = amount + tax;
            string lines = string.Format(HtmlTemplate.LineTemplate, quantity, POLICY_HOLDER_JOHN, DESC_HARLEY, amount.ToString("C"));
            var expectedHtmlResultStatementOneHarley = GenerateExpectedReceipt(HtmlTemplate.ReceiptTemplate, lines, amount, tax, total);
            // act
            order.AddLine(new MotorCycleLine(Harley, quantity));
            IReceipt receipt = new Receipt(order, HtmlTemplate);
            var result = receipt.GenerateReceipt(GenerateTime);
            // assert
            result.ShouldBe(expectedHtmlResultStatementOneHarley);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(100)]
        public void HtmlReceiptSunnyCoast(int quantity)
        {
            // arrange
            IOrder order = new Order(COMPANY_AJGRANT);
            decimal amount = GetLineAmount(SunnyCoast.PolicyType, HOME_PRICE, quantity);
            decimal tax = amount * TAX;
            decimal total = amount + tax;
            string lines = string.Format(HtmlTemplate.LineTemplate, quantity, POLICY_HOLDER_JOHN, DESC_SUNSHINE_COAST, amount.ToString("C"));
            var expectedHtmlResultStatementOneSunnyCoast = GenerateExpectedReceipt(HtmlTemplate.ReceiptTemplate, lines, amount, tax, total);
            // act
            order.AddLine(new HomeLine(SunnyCoast, quantity));
            IReceipt receipt = new Receipt(order, HtmlTemplate);
            var result = receipt.GenerateReceipt(GenerateTime);
            // assert
            result.ShouldBe(expectedHtmlResultStatementOneSunnyCoast);
        }

        #endregion
    }
}