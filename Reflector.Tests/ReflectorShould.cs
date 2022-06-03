using Reflector.Tests.Mock;
using System;
using System.Collections.Generic;
using Xunit;

namespace KissTools.Tests
{
    public class ReflectorShould
    {
        [Fact]
        public void GetPropertyName()
        {
            //Arrange
            TestClass obj = new TestClass();
            //Act
            String r = Reflector.GetPropertyName<TestClass>(o => o.Name);
            //Assert
            Assert.Equal("Name", r);
        }

        [Fact]
        public void GetBestMatchPropertyName()
        {
            //Arrange
            TestClass obj = new TestClass() { _LastName = "Hawking" };
            //Act
            String r = Reflector.GetBestMatchProperty(obj, "_LastName", ReflectorOptions.NONE);
            //Assert
            Assert.Equal("_LastName", r);
        }

        [Fact]
        public void GetBestMatchPropertyNameIgnoringCase()
        {
            //Arrange
            TestClass obj = new TestClass() { _LastName = "Hawking" };
            //Act
            String r = Reflector.GetBestMatchProperty(obj, "_Lastname", ReflectorOptions.IGNORE_CASE);
            //Assert
            Assert.Equal("_LastName", r);
        }

        [Fact]
        public void GetBestMatchPropertyNameIgnoringUnderscore()
        {
            //Arrange
            TestClass obj = new TestClass() { _LastName = "Hawking" };
            //Act
            String r = Reflector.GetBestMatchProperty(obj, "Last_Name", ReflectorOptions.IGNORE_UNDERSCORE);
            //Assert
            Assert.Equal("_LastName", r);
        }

        [Fact]
        public void GetBestMatchPropertyNameIgnoringCaseAndUnderscore()
        {
            //Arrange
            TestClass obj = new TestClass() { _LastName = "Hawking" };
            //Act
            String r = Reflector.GetBestMatchProperty(obj, "last_name", ReflectorOptions.IGNORE_CASE | ReflectorOptions.IGNORE_UNDERSCORE);
            //Assert
            Assert.Equal("_LastName", r);
        }

        [Fact]
        public void GetPropertiesNames()
        {
            //Arrange
            TestClass obj = new TestClass();
            //Act
            List<String> r = new List<String>(Reflector.GetProperties(obj));
            r.Sort();
            //Assert
            List<String> e = new List<String>() { "Name", "Age", "_LastName", "Address_Number", "addressnumber" };
            e.Sort();
            Assert.Equal(e, r);
        }

        [Fact]
        public void SetPropertyValue()
        {
            //Arrange
            TestClass obj = new TestClass();
            //Act
            Reflector.SetValue(obj, "Name", "Venus", false);
            //Assert
            Assert.Equal("Venus", obj.Name);
        }

        [Fact]
        public void SetPropertyValueWithConversion()
        {
            //Arrange
            TestClass obj = new TestClass();
            //Act
            Reflector.SetValue(obj, "Age", "20", true);
            //Assert
            Assert.Equal(20, obj.Age);
        }

        [Fact]
        public void GetPropertyValue()
        {
            //Arrange
            TestClass obj = new TestClass() { Age = 24 };
            //Act
            object r = Reflector.GetValue(obj, "Age");
            //Assert
            Assert.Equal(24, r);
        }


        [Fact]
        public void GetBestMatchAmbiguousPropertyName()
        {
            //Arrange
            TestClass obj = new TestClass() { addressnumber = 1000, Address_Number = 1001 };
            //Act
            String r = Reflector.GetBestMatchProperty(obj, "addressnumber", ReflectorOptions.NONE);
            //Assert
            Assert.Equal("addressnumber", r);
        }

        [Fact]
        public void GetBestMatchAmbiguousPropertyNameIgnoringCase()
        {
            //Arrange
            TestClass obj = new TestClass() { addressnumber = 1000, Address_Number = 1001 };
            //Act
            String r = Reflector.GetBestMatchProperty(obj, "AddressNumber", ReflectorOptions.IGNORE_CASE);
            //Assert
            Assert.Equal("addressnumber", r);
        }

        [Fact]
        public void GetBestMatchAmbiguousPropertyNameIgnoringUnderscore()
        {
            //Arrange
            TestClass obj = new TestClass() { addressnumber = 1000, Address_Number = 1001 };
            //Act
            String r = Reflector.GetBestMatchProperty(obj, "address_number", ReflectorOptions.IGNORE_UNDERSCORE);
            //Assert
            Assert.Equal("addressnumber", r);
        }

        [Fact]
        public void GetBestMatchAmbiguousPropertyNameIgnoringCaseAndUnderscore()
        {
            //Arrange
            TestClass obj = new TestClass() { addressnumber = 1000, Address_Number = 1001 };
            //Act
            String r = Reflector.GetBestMatchProperty(obj, "AddressNumber", ReflectorOptions.IGNORE_CASE | ReflectorOptions.IGNORE_UNDERSCORE);
            //Assert
            Assert.Equal("Address_Number", r);
        }

    }
}
