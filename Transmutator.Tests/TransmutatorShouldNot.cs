using System;
using Xunit;

namespace KissTools.Tests
{
    public class TransmutatorShouldNot
    {
        [Fact]
        public void ConvertStringToBoolean()
        {
            //Arrange
            String vin = "true_";
            //Act & Assert
            Assert.Throws<FormatException>(() => Transmutator.ConvertType<Boolean>(vin));
        }

        [Fact]
        public void ConvertStringToInteger()
        {
            //Arrange
            String vin = "10_";
            //Act & Assert
            Assert.Throws<FormatException>(() => Transmutator.ConvertType<Int32>(vin));
        }

        [Fact]
        public void ConvertStringToDouble()
        {
            //Arrange
            String vin = "19.30_";
            //Act & Assert
            Assert.Throws<FormatException>(() => Transmutator.ConvertType<Double>(vin));
        }

    }
}
