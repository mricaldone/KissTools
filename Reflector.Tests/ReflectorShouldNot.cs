using Reflector.Tests.Mock;
using System;
using Xunit;

namespace KissTools.Tests
{
    public class ReflectorShouldNot
    {

        [Fact]
        public void GetBestMatchPropertyName()
        {
            //Arrange
            TestClass obj = new TestClass() { _LastName = "Hawking" };
            //Act
            String r = Reflector.GetBestMatchProperty(obj, "_Last_Name", ReflectorOption.NONE);
            //Assert
            Assert.Null(r);
        }

        [Fact]
        public void GetBestMatchPropertyNameIgnoringCase()
        {
            //Arrange
            TestClass obj = new TestClass() { _LastName = "Hawking" };
            //Act
            String r = Reflector.GetBestMatchProperty(obj, "_Last_name", ReflectorOption.IGNORE_CASE);
            //Assert
            Assert.Null(r);
        }

        [Fact]
        public void GetBestMatchPropertyNameIgnoringUnderscore()
        {
            //Arrange
            TestClass obj = new TestClass() { _LastName = "Hawking" };
            //Act
            String r = Reflector.GetBestMatchProperty(obj, "Last_name", ReflectorOption.IGNORE_UNDERSCORE);
            //Assert
            Assert.Null(r);
        }

        [Fact]
        public void GetPropertyValue()
        {
            //Arrange
            TestClass obj = new TestClass();
            //Act
            object r = Reflector.GetValue(obj, "None");
            //Assert
            Assert.Null(r);
        }

    }
}
