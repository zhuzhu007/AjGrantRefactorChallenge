using AjGrantRefactorChallenge.Constants;
using AjGrantRefactorChallenge.Line;
using NUnit.Framework;
using Shouldly;
using System;

namespace AjGrantRefactorChallenge.Tests
{
    [TestFixture]
    public class OrderTest
    {
        private const string COMPANY_AJGRANT = "AjGrant";
        private const string POLICY_HOLDER_JANE = "Jane Doe";
        private const string POLICY_HOLDER_JOHN = "John Doe";
        private const string DESC_BMW = "BMW";
        private const string DESC_HARLEY = "Harley";
        private const string DESC_SUNSHINE_COAST = "Sunshine Coast";
        private const decimal CAR_PRICE = 105m;
        private const decimal MOTORCYCLE_PRICE = 56m;
        private const decimal HOME_PRICE = 235m;
        private const decimal TAX = .1m;
        private readonly static IPolicy BMW = new Policy("Car", POLICY_HOLDER_JANE, DESC_BMW, CAR_PRICE);
        private readonly static IPolicy Harley = new Policy("MotorCycle", POLICY_HOLDER_JOHN, DESC_HARLEY, MOTORCYCLE_PRICE);
        private readonly static IPolicy SunnyCoast = new Policy("Home", POLICY_HOLDER_JOHN, DESC_SUNSHINE_COAST, HOME_PRICE);
        private static DateTime OrderTime;
        private static string OrderTimeString = null!;

        [OneTimeSetUp]
        public void OneTimeSetUp(){
            OrderTime = DateTime.Now;
            OrderTimeString = string.Format("{0}", OrderTime.ToString("F"));
        }

        private string GenerateExpectedReceipt(string template, string lines, decimal amount, decimal tax, decimal total) =>
            template
            .Replace("{COMPANY_NAME}", COMPANY_AJGRANT)
            .Replace("{LINES}", lines)
            .Replace("{SUBTOTAL}", amount.ToString("C"))
            .Replace("{TAX}", tax.ToString("C"))
            .Replace("{TOTAL}", total.ToString("C"))
            .Replace("{DATETIME}", OrderTimeString);

        #region Receipts
        [Test]
        public void ReceiptOneBMW()
        {
            // arrange
            var order = new Order(COMPANY_AJGRANT);
            const int quantity = 1;
            const decimal amount = CAR_PRICE;
            const decimal tax = amount * TAX;
            const decimal total = amount + tax;
            string lines = string.Format(ReceiptTemplateConstants.CONSOLE_LINE_TEMPLATE, quantity, POLICY_HOLDER_JANE, DESC_BMW, amount.ToString("C"));
            // Order Receipt for AjGrant{Environment.NewLine}\t1 x Jane Doe BMW = $105.00{Environment.NewLine}Sub-Total: $105.00{Environment.NewLine}Tax: $10.50{Environment.NewLine}Total: $115.50";
            var expectedResultStatementOneBMW = GenerateExpectedReceipt(ReceiptTemplateConstants.CONSOLE_RECEIPT_TEMPLATE, lines, amount, tax, total);
            // act
            order.AddLine(new CarLine(BMW, quantity));
            var result = order.Receipt(OrderTime);
            // assert
            result.ShouldBe(expectedResultStatementOneBMW);
        }

        [Test]
        public void ReceiptOneHarley()
        {
            // arrange
            var order = new Order(COMPANY_AJGRANT);
            const int quantity = 1;
            const decimal amount = MOTORCYCLE_PRICE;
            const decimal tax = amount * TAX;
            const decimal total = amount + tax;
            string lines = string.Format(ReceiptTemplateConstants.CONSOLE_LINE_TEMPLATE, quantity, POLICY_HOLDER_JOHN, DESC_HARLEY, amount.ToString("C"));
            // Order Receipt for AjGrant{Environment.NewLine}\t1 x John Doe Harley = $56.00{Environment.NewLine}Sub-Total: $56.00{Environment.NewLine}Tax: $5.60{Environment.NewLine}Total: $61.60
            var expectedResultStatementOneHarley = GenerateExpectedReceipt(ReceiptTemplateConstants.CONSOLE_RECEIPT_TEMPLATE, lines, amount, tax, total);
            // act
            order.AddLine(new MotorCycleLine(Harley, quantity));
            var result = order.Receipt(OrderTime);
            // assert
            result.ShouldBe(expectedResultStatementOneHarley);
        }

        [Test]
        public void ReceiptOneSunnyCoast()
        {
            // arrange
            var order = new Order(COMPANY_AJGRANT);
            const int quantity = 1;
            const decimal amount = HOME_PRICE;
            const decimal tax = amount * TAX;
            const decimal total = amount + tax;
            string lines = string.Format(ReceiptTemplateConstants.CONSOLE_LINE_TEMPLATE, quantity, POLICY_HOLDER_JOHN, DESC_SUNSHINE_COAST, amount.ToString("C"));
            // Order Receipt for AjGrant{Environment.NewLine}\t1 x John Doe Sunshine Coast = $235.00{Environment.NewLine}Sub-Total: $235.00{Environment.NewLine}Tax: $23.50{Environment.NewLine}Total: $258.50
            var expectedResultStatementOneSunnyCoast = GenerateExpectedReceipt(ReceiptTemplateConstants.CONSOLE_RECEIPT_TEMPLATE, lines, amount, tax, total);
            // act
            order.AddLine(new HomeLine(SunnyCoast, quantity));
            var result = order.Receipt(OrderTime);
            // assert
            result.ShouldBe(expectedResultStatementOneSunnyCoast);
        }

        #endregion

        #region HTML Receipts
        [Test]
        public void HtmlReceiptOneBMW()
        {
            // arrange
            var order = new Order(COMPANY_AJGRANT);
            const int quantity = 1;
            const decimal amount = CAR_PRICE;
            const decimal tax = amount * TAX;
            const decimal total = amount + tax;
            string lines = string.Format(ReceiptTemplateConstants.HTML_LINE_TEMPLATE, quantity, POLICY_HOLDER_JANE, DESC_BMW, amount.ToString("C"));
            var expectedHtmlResultStatementOneBMW = GenerateExpectedReceipt(ReceiptTemplateConstants.HTML_RECEIPT_TEMPLATE, lines, amount, tax, total);
            // act
            order.AddLine(new CarLine(BMW, quantity));
            var result = order.HtmlReceipt(OrderTime);
            // assert
            result.ShouldBe(expectedHtmlResultStatementOneBMW);
        }

        [Test]
        public void HtmlReceiptOneHarley()
        {
            // arrange
            var order = new Order(COMPANY_AJGRANT);
            const int quantity = 1;
            const decimal amount = MOTORCYCLE_PRICE;
            const decimal tax = amount * TAX;
            const decimal total = amount + tax;
            string lines = string.Format(ReceiptTemplateConstants.HTML_LINE_TEMPLATE, quantity, POLICY_HOLDER_JOHN, DESC_HARLEY, amount.ToString("C"));
            var expectedHtmlResultStatementOneHarley = GenerateExpectedReceipt(ReceiptTemplateConstants.HTML_RECEIPT_TEMPLATE, lines, amount, tax, total);
            // act
            order.AddLine(new MotorCycleLine(Harley, quantity));
            var result = order.HtmlReceipt(OrderTime);
            // assert
            result.ShouldBe(expectedHtmlResultStatementOneHarley);
        }

        [Test]
        public void HtmlReceiptOneSunnyCoast()
        {
            // arrange
            var order = new Order(COMPANY_AJGRANT);
            const int quantity = 1;
            const decimal amount = HOME_PRICE;
            const decimal tax = amount * TAX;
            const decimal total = amount + tax;
            string lines = string.Format(ReceiptTemplateConstants.HTML_LINE_TEMPLATE, quantity, POLICY_HOLDER_JOHN, DESC_SUNSHINE_COAST, amount.ToString("C"));
            var expectedHtmlResultStatementOneSunnyCoast = GenerateExpectedReceipt(ReceiptTemplateConstants.HTML_RECEIPT_TEMPLATE, lines, amount, tax, total);
            // act
            order.AddLine(new HomeLine(SunnyCoast, quantity));
            var result = order.HtmlReceipt(OrderTime);
            // assert
            result.ShouldBe(expectedHtmlResultStatementOneSunnyCoast);
        }

        #endregion
    }
}