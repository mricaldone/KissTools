# KissTools
Keep it simple, stupid! Some tools for .net

  - AutoMapper: Map one object to another.
  - Reflector: Some methods that helps with reflection.
  - Transmutator: Helps in type conversion.

## AutoMapper
https://www.nuget.org/packages/KissTools.AutoMapper/

Maps one object to another.
### Usage
General use to map 2 objects linking "n" properties and ignoring "m" properties:
```
AutoMapper.From(object sObj).MapTo(object tObj, MapperOption = MapperOption.NONE).Link(sObj => sObj.sP1).To(tObj => tObj.tP1).Link(sObj => sObj.sP2).To(tObj => tObj.tP2).Link(...).To(...).Link(sObj => sObj.sPn).To(tObj => tObj.tPn).Ignore(sObj => sObj.p1).Ignore(sObj => sObj.p2).Ignore(...).Ignore(sObj => sObj.pm).Go();
```
`Link(...)` usage is optional as well as `MapperOption` and `Ignore()`. But `Go()` is mandatory cause it starts mapping.
#### Map
To map all properties from a source object into a target object use:
```
AutoMapper.From(sourceObj).MapTo(targetObj).Go();
```
This maps all the public properties of the source object that have the same name in the target object. Lacking properties in the target object will be not mapped. In case of the property has different types a `MappingException` will be thrown. You can ignore all exceptions while setting `MapperOption.IGNORE_ERRORS`:
```
AutoMapper.From(sourceObj).MapTo(targetObj, MapperOption.IGNORE_ERRORS).Go();
```
This means that incompatible types will be null on the target object. In some cases there are properties that has different types but value is convertible for example a string number into a float. For this cases you can set `MapperOption.FORCE_TYPE`:
```
AutoMapper.From(sourceObj).MapTo(targetObj, MapperOption.FORCE_TYPE).Go();
```
Of course you can combine options:
```
AutoMapper.From(sourceObj).MapTo(targetObj, MapperOption.IGNORE_ERRORS | MapperOption.FORCE_TYPE).Go();
```
Also you may need to map objects having properties with similar name, for example, different text case or containing underscores. In this case you can use:
```
AutoMapper.From(sourceObj).MapTo(targetObj, MapperOption.IGNORE_CASE | MapperOption.IGNORE_UNDERSCORE).Go();
```
These are all the posible MapperOptions so if you want to use them together for the god sake use:
```
AutoMapper.From(sourceObj).MapTo(targetObj, MapperOption.ALL).Go();
```
#### Link
If you still need to map properties with different name you can use:
```
AutoMapper.From(sourceObj).MapTo(targetObj).Link(sO => sO.aProp).To(tO => tO.difProp).Go();
```
Link statement takes in care options that you set, so if you want to force type conversion or ignore errors you can use:
```
AutoMapper.From(sourceObj).MapTo(targetObj, MapperOption.IGNORE_ERRORS | MapperOption.FORCE_TYPE).Link(sO => sO.aProp).To(tO => tO.difProp).Go();
```
Finally, if you need to link more than one extra property you can use link many times:
```
AutoMapper.From(sourceObj).MapTo(targetObj).Link(sO => sO.sP1).To(tO => tO.tP1).Link(sO => sO.sP2).To(tO => tO.tP2).Link(...).To(...).Link(sO => sO.sPn).To(tO => tO.tPn).Go();
```
#### Ignore
Maybe you need to ignore certain property, for this case you can use:
```
AutoMapper.From(sourceObj).MapTo(targetObj).Ignore(sO => sO.aProp).Go();
```
As well as link, you can use it many times as your need:
```
AutoMapper.From(sourceObj).MapTo(targetObj).Ignore(sO => sO.sP1).Ignore(sO => sO.sP2).Ignore(...).To(...).Ignore(sO => sO.sPn).Go();
```
#### Go
The `Go()` statement starts the mapping process. After `Go()` you can add another target object using `MapTo()` again:
```
AutoMapper.From(sourceObj).MapTo(targetObj_1).Go().MapTo(targetObj_2).Go().[...].MapTo(targetObj_n).Go();
```
#### Options
Here you have a a summary of all posible options:
 - IGNORE_CASE
 - IGNORE_UNDERSCORE
 - FORCE_TYPE
 - IGNORE_ERRORS
 - ALL
