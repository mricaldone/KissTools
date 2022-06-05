using KissTools.Tests.Mock;
using Xunit;

namespace KissTools.Tests
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
            Assert.Throws<MappingException>(() => AutoMapper.From(s).MapTo(t).Go());
        }

        [Fact]
        public void WithSameAttributeNameAndDifferentNotParseableType()
        {
            //Arrange
            TargetClass t = new TargetClass();
            SourceClass s = new SourceClass() { Gravity = "Big" };
            //Act & Assert
            Assert.Throws<MappingException>(() => AutoMapper.From(s).MapTo(t, MapperOption.FORCE_TYPE).Go());
        }
        [Fact]
        public void WithDifferentAttributeNameAndDifferentParseableType()
        {
            //Arrange
            TargetClass t = new TargetClass();
            SourceClass s = new SourceClass() { HasMoons = "true" };
            //Act & Assert
            Assert.Throws<MappingException>(() => AutoMapper.From(s).MapTo(t).Link(o => o.HasMoons).InTo(o => o.WithMoons).Go());
        }

        [Fact]
        public void WithDifferentAttributeNameAndDifferentNotParseableType()
        {
            //Arrange
            TargetClass t = new TargetClass();
            SourceClass s = new SourceClass() { HasMoons = "yes" };
            //Act & Assert
            Assert.Throws<MappingException>(() => AutoMapper.From(s).MapTo(t, MapperOption.FORCE_TYPE).Link(o => o.HasMoons).InTo(o => o.WithMoons).Go());
        }

        [Fact]
        public void WhithSameAttributeNameAndDifferentParseableType()
        {
            //Arrange
            TargetClass t = new TargetClass() { HasMoons = false };
            SourceClass s = new SourceClass() { HasMoons = "true" };
            //Act & Assert
            Assert.Throws<MappingException>(() => AutoMapper.From(s).MapTo(t).Go());
        }
    }
}
