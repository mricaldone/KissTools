using KissTools.Toolbox.Tests.Mock;
using Xunit;

namespace KissTools.Toolbox.Tests
{
    public class AutoMapperShouldNot
    {
        [Fact]
        public void MapAttributesWithDifferentName()
        {
            //Arrange
            const double v = 58.232;
            TargetClass t = new TargetClass();
            SourceClass s = new SourceClass() { perimeter = v };
            //Act
            AutoMapper.Map(s).InTo(t);
            //Assert
            Assert.NotEqual(v, t.Perimeter);
        }

        [Fact]
        public void MapAttributesWithSameNameAndDifferentTypesWhenSourceTypeIsNull()
        {
            //Arrange
            TargetClass t = new TargetClass() { HasMoons = true };
            SourceClass s = new SourceClass() { HasMoons = null };
            //Act
            AutoMapper.Map(s).InTo(t);
            //Assert
            Assert.False(t.HasMoons);
        }

        [Fact]
        public void MapAttributeWithDifferentNameAndDifferentNotParseableType()
        {
            //Arrange
            TargetClass t = new TargetClass() { WithMoons = false };
            SourceClass s = new SourceClass() { HasMoons = "yes" };
            //Act
            AutoMapper.Map(s).InTo(t, MapperOptions.FORCE_TYPE | MapperOptions.IGNORE_ERRORS).Link(o => o.HasMoons).InTo(o => o.WithMoons);
            //Assert
            Assert.False(t.WithMoons);
        }

        [Fact]
        public void MapAttributeWhithSameNameAndDifferentParseableType()
        {
            //Arrange
            TargetClass t = new TargetClass() { HasMoons = false };
            SourceClass s = new SourceClass() { HasMoons = "true" };
            //Act
            AutoMapper.Map(s).InTo(t, MapperOptions.IGNORE_ERRORS);
            //Assert
            Assert.False(t.HasMoons);
        }

    }
}
