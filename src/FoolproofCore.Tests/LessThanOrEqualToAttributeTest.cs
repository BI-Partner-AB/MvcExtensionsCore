﻿namespace FoolproofCore.Tests
{
    using System;

    using FoolproofCore;

    using Xunit;

    public class LessThanOrEqualToAttributeTest
    {
        private class DateModel : ModelBase<LessThanOrEqualToAttribute>
        {
            public DateTime? Value1 { get; set; }

            [LessThanOrEqualTo("Value1")]
            public DateTime? Value2 { get; set; }
        }

        private class Int16Model : ModelBase<LessThanOrEqualToAttribute>
        {
            public Int16 Value1 { get; set; }

            [LessThanOrEqualTo("Value1")]
            public Int16 Value2 { get; set; }
        }

        [Fact]
        public void DateIsValid()
        {
            var model = new DateModel { Value1 = DateTime.Now, Value2 = DateTime.Now.AddDays(-1) };
            Assert.True(model.IsValid("Value2"));
        }

        [Fact]
        public void DateEqualIsValid()
        {
            var date = DateTime.Now;
            var model = new DateModel { Value1 = date, Value2 = date };
            Assert.True(model.IsValid("Value2"));
        }

        [Fact]
        public void DateNullValuesIsValid()
        {
            var date = DateTime.Now;
            var model = new DateModel();
            Assert.True(model.IsValid("Value2"));
        }

        [Fact]
        public void DateIsNotValid()
        {
            var model = new DateModel { Value1 = DateTime.Now, Value2 = DateTime.Now.AddDays(1) };
            Assert.False(model.IsValid("Value2"));
        }

        [Fact]
        public void DateWithNullsIsValid()
        {
            var model = new DateModel();
            Assert.True(model.IsValid("Value2"));
        }

        [Fact]
        public void DateWithValue1NullIsNotValid()
        {
            var model = new DateModel { Value2 = DateTime.Now };
            Assert.False(model.IsValid("Value2"));
        }

        [Fact]
        public void DateWithValue2NullIsNotValid()
        {
            var model = new DateModel { Value1 = DateTime.Now };
            Assert.False(model.IsValid("Value2"));
        }

        [Fact]
        public void Int16IsValid()
        {
            var model = new Int16Model { Value1 = 120, Value2 = 12 };
            Assert.True(model.IsValid("Value2"));
        }

        [Fact]
        public void Int16EqualIsValid()
        {
            var model = new Int16Model { Value1 = 12, Value2 = 12 };
            Assert.True(model.IsValid("Value2"));
        }

        [Fact]
        public void Int16IsNotValid()
        {
            var model = new Int16Model { Value1 = 12, Value2 = 120 };
            Assert.False(model.IsValid("Value2"));
        }     
    }
}
