using Reflector.Tests.Mock;
using System;
using Xunit;

namespace KissTools.Tests
{
    public class ReflectorShouldRiseError
    {
        [Fact]
        public void WhenSetPropertyWithDifferentTypeValue()
        {
            //Arrange
            TestClass obj = new TestClass();
            //Act & Assert
            Assert.Throws<InvalidCastException>(() => { Reflector.SetValue(obj, "Age", "20", false); });
        }

        [Fact]
        public void WhenSetPropertyWithNoParseableValueWithConversion()
        {
            //Arrange
            TestClass obj = new TestClass();
            //Act & Assert
            Assert.Throws<InvalidCastException>(() => { Reflector.SetValue(obj, "Age", "xx", true); });
        }
    }
}
