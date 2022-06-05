using KissTools.Tests.Mock;
using Xunit;

namespace KissTools.Tests
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
            AutoMapper.From(s).MapTo(t);
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
            AutoMapper.From(s).MapTo(t).Go();
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
            AutoMapper.From(s).MapTo(t, MapperOption.FORCE_TYPE | MapperOption.IGNORE_ERRORS).Link(o => o.HasMoons).InTo(o => o.WithMoons).Go();
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
            AutoMapper.From(s).MapTo(t, MapperOption.IGNORE_ERRORS).Go();
            //Assert
            Assert.False(t.HasMoons);
        }

        [Fact]
        public void MapAttributeIfGoNotUsed()
        {
            //Arrange
            TargetClass t = new TargetClass();
            SourceClass s = new SourceClass() { Name = "Neptune" };
            //Act
            AutoMapper.From(s).MapTo(t);
            //Assert
            Assert.Null(t.Name);
        }

        [Fact]
        public void MapAttributeIfGoNotUsedAfterLink()
        {
            //Arrange
            TargetClass t = new TargetClass();
            SourceClass s = new SourceClass() { Name = "Neptune" };
            //Act
            AutoMapper.From(s).MapTo(t).Link(o => o.Name).InTo(o => o.Name);
            //Assert
            Assert.Null(t.Name);
        }

        [Fact]
        public void MapAnIgnoredAttribute()
        {
            //Arrange
            TargetClass t = new TargetClass();
            SourceClass s = new SourceClass() { Name = "Mercury" };
            //Act
            AutoMapper.From(s).MapTo(t).Ignoring(o => o.Name).Go();
            //Assert
            Assert.Null(t.Name);
        }

        [Fact]
        public void MapAnAlreadyIgnoredAttribute()
        {
            //Arrange
            TargetClass t = new TargetClass();
            SourceClass s = new SourceClass() { Name = "Mercury" };
            //Act
            AutoMapper.From(s).MapTo(t).Ignoring(o => o.Name).Ignoring(o => o.Name).Go();
            //Assert
            Assert.Null(t.Name);
        }

    }
}
