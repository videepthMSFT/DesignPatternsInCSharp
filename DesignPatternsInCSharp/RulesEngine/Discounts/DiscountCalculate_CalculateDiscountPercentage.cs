﻿using FluentAssertions;
using System;
using Xunit;

namespace DesignPatternsInCSharp.RulesEngine.Discounts
{
    public class DiscountCalculate_CalculateDiscountPercentage
    {
        private DiscountCalculator _calculator = new DiscountCalculator();
        const int DEFAULT_AGE = 30;

        [Fact]
        public void Returns0PctForBasicCustomer()
        {
            var customer = CreateCustomer(DEFAULT_AGE, DateTime.Today.AddDays(-1));

            var result = _calculator.CalculateDiscountPercentage(customer);

            result.Should().Be(0m);
        }

        [Fact]
        public void Returns5PctForCustomersOver65()
        {
            var customer = CreateCustomer(65, DateTime.Today.AddDays(-1));

            var result = _calculator.CalculateDiscountPercentage(customer);

            result.Should().Be(.05m);
        }

        [Theory]
        [InlineData(20)]
        [InlineData(70)]
        public void Returns15PctForCustomerFirstPurchase(int customerAge)
        {
            var customer = CreateCustomer(customerAge);

            var result = _calculator.CalculateDiscountPercentage(customer);

            result.Should().Be(.15m);
        }

        [Theory]
        [InlineData(20)]
        [InlineData(70)]
        public void Returns10PctForCustomersWhoAreVeterans(int customerAge)
        {
            var customer = CreateCustomer(customerAge, DateTime.Today.AddDays(-1));
            customer.IsVeteran = true;

            var result = _calculator.CalculateDiscountPercentage(customer);

            result.Should().Be(.10m);
        }

        private Customer CreateCustomer(int age = DEFAULT_AGE, DateTime? firstPurchaseDate = null)
        {
            return new Customer
            {
                DateOfBirth = DateTime.Today.AddYears(-age).AddDays(-1),
                DateOfFirstPurchase = firstPurchaseDate
            };
        }
    }
}
