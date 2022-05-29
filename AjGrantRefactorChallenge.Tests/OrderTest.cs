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
        private readonly static IPolicy BMW = new Policy("Car", POLICY_HOLDER_JANE, DESC_BMW, CAR_PRICE);
        private readonly static IPolicy Harley = new Policy("MotorCycle", POLICY_HOLDER_JOHN, DESC_HARLEY, MOTORCYCLE_PRICE);
        private readonly static IPolicy SunnyCoast = new Policy("Home", POLICY_HOLDER_JOHN, DESC_SUNSHINE_COAST, HOME_PRICE);

        public void Order_Should_Be_Empty(int quantity)
        {
            // arrange
            IOrder order = new Order(COMPANY_AJGRANT);
            // act
            var result = order.GetLines();
            // assert
            result.ShouldBeEmpty();
        }
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(100)]
        public void Order_Should_Contain_BMW(int quantity)
        {
            // arrange
            IOrder order = new Order(COMPANY_AJGRANT);
            ILine line = new CarLine(BMW, quantity);
            // act
            order.AddLine(line);
            var result = order.GetLines();
            // assert
            result.ShouldContain(line);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(100)]
        public void Order_Should_Contain_Harley(int quantity)
        {
            // arrange
            IOrder order = new Order(COMPANY_AJGRANT);
            ILine line = new MotorCycleLine(Harley, quantity);
            // act
            order.AddLine(line);
            var result = order.GetLines();
            // assert
            result.ShouldContain(line);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(100)]
        public void Order_Should_Contain_SunnyCoast(int quantity)
        {
            // arrange
            IOrder order = new Order(COMPANY_AJGRANT);
            ILine line = new HomeLine(SunnyCoast, quantity);
            // act
            order.AddLine(line);
            var result = order.GetLines();
            // assert
            result.ShouldContain(line);
        }
 
        [TestCase(0, 0, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 3)]
        [TestCase(3, 2, 1)]
        [TestCase(4, 5, 6)]
        public void Order_Should_Contain_All(int carQuantity, int motorcycleQuantity, int homeQuantity)
        {
            // arrange
            IOrder order = new Order(COMPANY_AJGRANT);
            ILine carline = new CarLine(BMW, carQuantity);
            ILine motorcycleline = new MotorCycleLine(Harley, motorcycleQuantity);
            ILine homeline = new HomeLine(SunnyCoast, homeQuantity);
            // act
            order.AddLine(carline);
            order.AddLine(motorcycleline);
            order.AddLine(homeline);
            var result = order.GetLines();
            // assert
            result.ShouldContain(carline);
            result.ShouldContain(motorcycleline);
            result.ShouldContain(homeline);
        }

    }
}