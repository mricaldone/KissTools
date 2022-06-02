using KissTools.Toolbox.Tests.Mock;
using Xunit;

namespace KissTools.Toolbox.Tests
{
    public class AutoMapperShouldRiseError
    {
        [Fact]
        public void WithSameAttributeNameAndDifferentParseableType()
        {
            //Arrange
            TargetClass t = new TargetClass();
            SourceClass s = new SourceClass() { HasMoons = "true" };
            //Act & Assert
            Assert.Throws<MappingException>(() => AutoMapper.Map(s).InTo(t));
        }

        [Fact]
        public void WithSameAttributeNameAndDifferentNotParseableType()
        {
            //Arrange
            TargetClass t = new TargetClass();
            SourceClass s = new SourceClass() { Gravity = "Big" };
            //Act & Assert
            Assert.Throws<MappingException>(() => AutoMapper.Map(s).InTo(t, MapperOptions.FORCE_TYPE));
        }
        [Fact]
        public void WithDifferentAttributeNameAndDifferentParseableType()
        {
            //Arrange
            TargetClass t = new TargetClass();
            SourceClass s = new SourceClass() { HasMoons = "true" };
            //Act & Assert
            Assert.Throws<MappingException>(() => AutoMapper.Map(s).InTo(t).Link(o => o.HasMoons).InTo(o => o.WithMoons));
        }

        [Fact]
        public void WithDifferentAttributeNameAndDifferentNotParseableType()
        {
            //Arrange
            TargetClass t = new TargetClass();
            SourceClass s = new SourceClass() { HasMoons = "yes" };
            //Act & Assert
            Assert.Throws<MappingException>(() => AutoMapper.Map(s).InTo(t, MapperOptions.FORCE_TYPE).Link(o => o.HasMoons).InTo(o => o.WithMoons));
        }

        [Fact]
        public void WhithSameAttributeNameAndDifferentParseableType()
        {
            //Arrange
            TargetClass t = new TargetClass() { HasMoons = false };
            SourceClass s = new SourceClass() { HasMoons = "true" };
            //Act & Assert
            Assert.Throws<MappingException>(() => AutoMapper.Map(s).InTo(t));
        }
    }
}
