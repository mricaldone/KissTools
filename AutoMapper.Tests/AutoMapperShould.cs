using Xunit;
using KissTools.Tests.Mock;

namespace KissTools.Tests
{
    public class AutoMapperShould
    {
        [Fact]
        public void MapAttributeWithSameName()
        {
            //Arrange
            const string v = "Saturn";
            TargetClass t = new TargetClass();
            SourceClass s = new SourceClass() { Name = v };
            //Act
            AutoMapper.Map(s).InTo(t);
            //Assert
            Assert.Equal(v, t.Name);
        }

        [Fact]
        public void MapAttributeWithSameNameIgnoringCase()
        {
            //Arrange
            const double v = 58.232;
            TargetClass t = new TargetClass();
            SourceClass s = new SourceClass() { perimeter = v };
            //Act
            AutoMapper.Map(s).InTo(t, MapperOptions.IGNORE_CASE);
            //Assert
            Assert.Equal(v, t.Perimeter);
        }

        [Fact]
        public void MapAttributeWithSameNameIgnoringUnderscore()
        {
            //Arrange
            const double v = 9.53707032;
            TargetClass t = new TargetClass();
            SourceClass s = new SourceClass() { Distance_To_Sun = v };
            //Act
            AutoMapper.Map(s).InTo(t, MapperOptions.IGNORE_UNDERSCORE);
            //Assert
            Assert.Equal(v, t.DistanceToSun);
        }

        [Fact]
        public void MapAttributeWithSameNameIgnoringCaseAndUnderscore()
        {
            //Arrange
            const double v = 9672.4;
            TargetClass t = new TargetClass();
            SourceClass s = new SourceClass() { Orbital_speed = v };
            //Act
            AutoMapper.Map(s).InTo(t, MapperOptions.IGNORE_UNDERSCORE | MapperOptions.IGNORE_CASE);
            //Assert
            Assert.Equal(v, t.OrbitalSpeed);
        }

        [Fact]
        public void MapAttributeWithDifferentName()
        {
            //Arrange
            const double v = 1013.14;
            TargetClass t = new TargetClass();
            SourceClass s = new SourceClass() { atmosfericPressure = v };
            //Act
            AutoMapper.Map(s).InTo(t).Link(o => o.atmosfericPressure).InTo(o => o.Pressure);
            //Assert
            Assert.Equal(v, t.Pressure);
        }
        
        [Fact]
        public void MapAttributeWhithSameNameAndDifferentParseableType()
        {
            //Arrange
            TargetClass t = new TargetClass();
            SourceClass s = new SourceClass() { HasMoons = "true" };
            //Act
            AutoMapper.Map(s).InTo(t, MapperOptions.FORCE_TYPE);
            //Assert
            Assert.True(t.HasMoons);
        }

        [Fact]
        public void MapAttributeWhithSameNameAndNullValue()
        {
            //Arrange
            TargetClass t = new TargetClass() { Name = "Mars" };
            SourceClass s = new SourceClass() { Name = null };
            //Act
            AutoMapper.Map(s).InTo(t);
            //Assert
            Assert.Null(t.Name);
        }

        [Fact]
        public void MapAttributeWhithDifferentNameAndNullValue()
        {
            //Arrange
            TargetClass t = new TargetClass() { Name = "Mars" };
            SourceClass s = new SourceClass() { Alias = null };
            //Act
            AutoMapper.Map(s).InTo(t).Link(o => o.Alias).InTo(o => o.Name);
            //Assert
            Assert.Null(t.Name);
        }

    }
}
