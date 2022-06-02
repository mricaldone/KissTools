using System;
using Xunit;

namespace KissTools.Toolbox.Tests
{
    public class ConvertionHelperShould
    {
        [Fact]
        public void ConvertStringToBoolean()
        {
            //Arrange
            String vin = "true";
            //Act
            Boolean vout = ConvertionHelper.ConvertType<Boolean>(vin);
            //Assert
            Assert.Equal(Boolean.Parse(vin), vout);
        }

        [Fact]
        public void ConvertStringToInteger()
        {
            //Arrange
            String vin = "020";
            //Act
            Int32 vout = ConvertionHelper.ConvertType<Int32>(vin);
            //Assert
            Assert.Equal(Int32.Parse(vin), vout);
        }

        [Fact]
        public void ConvertStringToDouble()
        {
            //Arrange
            String vin = "19.30";
            //Act
            Double vout = ConvertionHelper.ConvertType<Double>(vin);
            //Assert
            Assert.Equal(Double.Parse(vin), vout);
        }

        [Fact]
        public void ConvertBooleanToString()
        {
            //Arrange
            Boolean vin = false;
            //Act
            String vout = ConvertionHelper.ConvertType<String>(vin);
            //Assert
            Assert.Equal(vin.ToString(), vout);
        }

        [Fact]
        public void ConvertBooleanToDouble()
        {
            //Arrange
            Boolean vin = false;
            //Act
            Double vout = ConvertionHelper.ConvertType<Double>(vin);
            //Assert
            Assert.Equal(0, vout);
        }

        [Fact]
        public void ConvertBooleanToInteger()
        {
            //Arrange
            Boolean vin = true;
            //Act
            Int32 vout = ConvertionHelper.ConvertType<Int32>(vin);
            //Assert
            Assert.Equal(1, vout);
        }

        [Fact]
        public void ConvertDoubleToBoolean()
        {
            //Arrange
            Double vin = 1;
            //Act
            Boolean vout = ConvertionHelper.ConvertType<Boolean>(vin);
            //Assert
            Assert.True(vout);
        }

        [Fact]
        public void ConvertDoubleToInteger()
        {
            //Arrange
            Double vin = 1.5;
            //Act
            Int32 vout = ConvertionHelper.ConvertType<Int32>(vin);
            //Assert
            Assert.Equal(Math.Round(vin), vout);
        }

        [Fact]
        public void ConvertDoubleToString()
        {
            //Arrange
            Double vin = 1.5;
            //Act
            String vout = ConvertionHelper.ConvertType<String>(vin);
            //Assert
            Assert.Equal(vin.ToString(), vout);
        }

        [Fact]
        public void ConvertIntegerToString()
        {
            //Arrange
            Int32 vin = 5;
            //Act
            String vout = ConvertionHelper.ConvertType<String>(vin);
            //Assert
            Assert.Equal(vin.ToString(), vout);
        }

        [Fact]
        public void ConvertIntegerToBoolean()
        {
            //Arrange
            Int32 vin = 0;
            //Act
            Boolean vout = ConvertionHelper.ConvertType<Boolean>(vin);
            //Assert
            Assert.False(vout);
        }

        [Fact]
        public void ConvertIntegerToDouble()
        {
            //Arrange
            Int32 vin = 9;
            //Act
            Double vout = ConvertionHelper.ConvertType<Double>(vin);
            //Assert
            Assert.Equal(vin, vout);
        }

    }
}
