using AjGrantRefactorChallenge.Line;
using NUnit.Framework;
using Shouldly;


namespace AjGrantRefactorChallenge.Tests
{
    [TestFixture]
    public class LineTest
    {
        private const string POLICY_HOLDER_JANE = "Jane Doe";
        private const string POLICY_HOLDER_JOHN = "John Doe";

        #region Car
        private const string DESC_BMW = "BMW";
        private const decimal CAR_PRICE = 105m;
        private const decimal CAR_DISCOUNT = .9m;
        private readonly static IPolicy BMW = new Policy("Car", POLICY_HOLDER_JANE, DESC_BMW, CAR_PRICE);
        [TestCase(0)]
        [TestCase(1)]
        public void OneOrLess_Car_Amount_No_Discount(int quantity)
        {
            // arrange
            decimal expectedAmount = CAR_PRICE * quantity;
            ILine carLine = new CarLine(BMW, quantity);
            // act
            var result = carLine.Amount;
            // assert
            result.ShouldBe(expectedAmount);
        }

        [TestCase(2)]
        [TestCase(10)]
        [TestCase(100)]
        public void TwoOrMore_Car_Amount_Have_Discount(int quantity)
        {
            // arrange
            decimal expectedAmount = CAR_PRICE * quantity * CAR_DISCOUNT;
            ILine carLine = new CarLine(BMW, quantity);
            // act
            var result = carLine.Amount;
            // assert
            result.ShouldBe(expectedAmount);
        }
        #endregion

        #region MotorCycle
        private const string DESC_HARLEY = "Harley";
        private const decimal MOTORCYCLE_PRICE = 56m;
        private const decimal MOTORCYCLE_DISCOUNT = .8m;
        private readonly static IPolicy Harley = new Policy("MotorCycle", POLICY_HOLDER_JOHN, DESC_HARLEY, MOTORCYCLE_PRICE);
        [TestCase(0)]
        [TestCase(1)]
        public void OneOrLess_MotorCycle_Amount_No_Discount(int quantity)
        {
            // arrange
            decimal expectedAmount = MOTORCYCLE_PRICE * quantity;
            ILine motorCycleLine = new MotorCycleLine(Harley, quantity);
            // act
            var result = motorCycleLine.Amount;
            // assert
            result.ShouldBe(expectedAmount);
        }

        [TestCase(2)]
        [TestCase(10)]
        [TestCase(100)]
        public void TwoOrMore_MotorCycle_Amount_Have_Discount(int quantity)
        {
            // arrange
            decimal expectedAmount = MOTORCYCLE_PRICE * quantity * MOTORCYCLE_DISCOUNT;
            ILine motorCycleLine = new MotorCycleLine(Harley, quantity);
            // act
            var result = motorCycleLine.Amount;
            // assert
            result.ShouldBe(expectedAmount);
        }
        #endregion

        #region Home
        private const string DESC_SUNSHINE_COAST = "Sunshine Coast";
        private const decimal HOME_PRICE = 235m;
        private const decimal HOME_DISCOUNT = .8m;
        private readonly static IPolicy SunnyCoast = new Policy("Home", POLICY_HOLDER_JOHN, DESC_SUNSHINE_COAST, HOME_PRICE);
        [TestCase(0)]
        [TestCase(1)]
        public void OneOrLess_Home_Amount_No_Discount(int quantity)
        {
            // arrange
            decimal expectedAmount = HOME_PRICE * quantity;
            ILine homeLine = new HomeLine(SunnyCoast, quantity);
            // act
            var result = homeLine.Amount;
            // assert
            result.ShouldBe(expectedAmount);
        }

        [TestCase(2)]
        [TestCase(10)]
        [TestCase(100)]
        public void TwoOrMore_Home_Amount_Have_Discount(int quantity)
        {
            // arrange
            decimal expectedAmount = HOME_PRICE * quantity * HOME_DISCOUNT;
            ILine homeLine = new HomeLine(SunnyCoast, quantity);
            // act
            var result = homeLine.Amount;
            // assert
            result.ShouldBe(expectedAmount);
        }
        #endregion
    }
}