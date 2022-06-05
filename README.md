# KissTools
Keep it simple, stupid! Some tools for .net

  - AutoMapper: Map one object to another.
  - Reflector: Some methods that helps with reflection.
  - Transmutator: Helps in type conversion.

## AutoMapper
https://www.nuget.org/packages/KissTools.AutoMapper/

Maps one object to another.
### Usage
General use:
```
AutoMapper.Map(object sObj).To(object tObj, MapperOption = MapperOption.NONE).Link(sObj => sObj.sP1).To(tObj => tObj.tP1).Link(sObj => sObj.sP2).To(tObj => tObj.tP2).Link(...).To(...).Link(sObj => sObj.sPn).To(tObj => tObj.tPn);
```
Link usage is optional as well as options.
#### Map
To map all properties from a source object into a target object use:
```
AutoMapper.Map(sourceObj).To(targetObj);
```
This maps all the public properties of the source object that have the same name in the target object. Lacking properties in the target object will be not mapped. In case of the property has different types a `MappingException` will be thrown. You can ignore all exceptions while setting `MapperOption.IGNORE_ERRORS`:
```
AutoMapper.Map(sourceObj).To(targetObj, MapperOption.IGNORE_ERRORS);
```
This means that incompatible types will be null on the target object. In some cases there are properties that has different types but value is convertible for example a string number into a float. For this cases you can set `MapperOption.FORCE_TYPE`:
```
AutoMapper.Map(sourceObj).To(targetObj, MapperOption.FORCE_TYPE);
```
Of course you can combine options:
```
AutoMapper.Map(sourceObj).To(targetObj, MapperOption.IGNORE_ERRORS | MapperOption.FORCE_TYPE);
```
Also you may need to map objects having properties with similar name, for example, different text case or containing underscores. In this case you can use:
```
AutoMapper.Map(sourceObj).To(targetObj, MapperOption.IGNORE_CASE | MapperOption.IGNORE_UNDERSCORE);
```
These are all the posible MapperOptions so if you want to use them together for the god sake use:
```
AutoMapper.Map(sourceObj).To(targetObj, MapperOption.ALL);
```
#### Link
If you still need to map properties with different name you can use:
```
AutoMapper.Map(sourceObj).To(targetObj).Link(sO => sO.aProp).To(tO => tO.difProp);
```
Link statement takes in care options that you set, so if you want to force type conversion or ignore errors you can use:
```
AutoMapper.Map(sourceObj).To(targetObj, MapperOption.IGNORE_ERRORS | MapperOption.FORCE_TYPE).Link(sO => sO.aProp).To(tO => tO.difProp);
```
Finally, if you need to link more than one extra property you can use link many times:
```
AutoMapper.Map(sourceObj).To(targetObj).Link(sO => sO.sP1).To(tO => tO.tP1).Link(sO => sO.sP2).To(tO => tO.tP2).Link(...).To(...).Link(sO => sO.sPn).To(tO => tO.tPn);
```
#### Options
Here you have a a summary of all posible options:
 - IGNORE_CASE
 - IGNORE_UNDERSCORE
 - FORCE_TYPE
 - IGNORE_ERRORS
 - ALL
