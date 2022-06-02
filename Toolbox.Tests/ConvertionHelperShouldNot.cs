using System;
using Xunit;

namespace Toolbox.Tests
{
    public class ConvertionHelperShouldNot
    {
        [Fact]
        public void ConvertStringToBoolean()
        {
            //Arrange
            String vin = "true_";
            //Act & Assert
            Assert.Throws<FormatException>(() => ConvertionHelper.ConvertType<Boolean>(vin));
        }

        [Fact]
        public void ConvertStringToInteger()
        {
            //Arrange
            String vin = "10_";
            //Act & Assert
            Assert.Throws<FormatException>(() => ConvertionHelper.ConvertType<Int32>(vin));
        }

        [Fact]
        public void ConvertStringToDouble()
        {
            //Arrange
            String vin = "19.30_";
            //Act & Assert
            Assert.Throws<FormatException>(() => ConvertionHelper.ConvertType<Double>(vin));
        }

    }
}
