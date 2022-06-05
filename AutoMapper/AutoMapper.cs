using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace KissTools
{
    public static class AutoMapper
    {
        public static Mapper<T1> From<T1>(T1 obj, MapperOption options = MapperOption.NONE)
        {
            return new Mapper<T1>(obj, options);
        }

        public class MapBuild<T1, T2>
        {
            private T1 _sourceObj;
            private T2 _targetObj;
            private Dictionary<string, string> _mappedProperties;
            private MapperOption _options;
            
            public MapBuild(T1 sourceObj, T2 targetObj, Dictionary<string, string> mappedProperties, MapperOption options)
            {
                _sourceObj = sourceObj;
                _targetObj = targetObj;
                _mappedProperties = mappedProperties;
                _options = options;
            }

            public Linker<T1, T2> Link(Expression<Func<T1, object>> sourceProp)
            {
                string sourcePropName = Reflector.GetPropertyName(sourceProp);
                return new Linker<T1, T2>(this, sourcePropName);
            }

            internal void Link(string sourcePropName, string targetPropName)
            {
                if (targetPropName == null) return;
                if (_mappedProperties.ContainsKey(sourcePropName))
                    _mappedProperties[sourcePropName] = targetPropName;
                else
                    _mappedProperties.Add(sourcePropName, targetPropName);
            }

            public MapBuild<T1, T2> Ignoring(Expression<Func<T2, object>> sourceProp)
            {
                string sourcePropName = Reflector.GetPropertyName(sourceProp);
                _mappedProperties.Remove(sourcePropName);
                return this;
            }

            public Mapper<T1> Go()
            {
                string sourcePropName = "", targetPropName = "";
                try { 
                    foreach (string propName in _mappedProperties.Keys)
                    {
                        try
                        {
                            sourcePropName = propName;
                            targetPropName = _mappedProperties[sourcePropName];
                            object sourcePropValue = Reflector.GetValue(_sourceObj, sourcePropName);
                            Reflector.SetValue(_targetObj, targetPropName, sourcePropValue, (_options & MapperOption.FORCE_TYPE) == MapperOption.FORCE_TYPE);
                        }
                        catch (Exception ex)
                        {
                            if ((_options & MapperOption.IGNORE_ERRORS) != MapperOption.IGNORE_ERRORS) throw ex;
                        }
                    }
                    return new Mapper<T1>(_sourceObj, _options);
                }
                catch (InvalidCastException ex)
                {
                    String action = $"When trying to map {typeof(T1).Name}.{sourcePropName} into {typeof(T2).Name}.{targetPropName}";
                    if ((MapperOption.FORCE_TYPE & _options) != MapperOption.FORCE_TYPE)
                        throw new MappingException($"{ex.Message}. {action}. Use {nameof(MapperOption)}.{nameof(MapperOption.FORCE_TYPE)} to force conversion.");
                    else
                        throw new MappingException($"{ex.Message}. {action}.");
                }
            }
        }

        public class Mapper<T1>
        {
            private T1 _sourceObj;
            private MapperOption _options;
            internal Mapper(T1 sourceObj, MapperOption options)
            {
                _sourceObj = sourceObj;
                _options = options;
            }

            public MapBuild<T1, T2> MapTo<T2>(T2 targetObj)
            {
                return MapTo(targetObj, _options);
            }

            public MapBuild<T1, T2> MapTo<T2>(T2 targetObj, MapperOption options)
            {
                Dictionary<string, string> mappedProperties = new Dictionary<string, string>();
                string[] sourcePropertiesNames = Reflector.GetProperties(_sourceObj);
                foreach (string sourcePropName in sourcePropertiesNames)
                {
                    string targetPropName = Reflector.GetBestMatchProperty(targetObj, sourcePropName, (ReflectorOption) options);
                    if (targetPropName != null) mappedProperties.Add(sourcePropName, targetPropName);
                }
                return new MapBuild<T1, T2>(_sourceObj, targetObj, mappedProperties, options);
            }
        }

        public class Linker<T1, T2>
        {
            private MapBuild<T1, T2> _mapBuild;
            private string _sourcePropName;

            internal Linker(MapBuild<T1, T2> mapBuild, string sourcePropName)
            {
                _mapBuild = mapBuild;
                _sourcePropName = sourcePropName;
            }

            public MapBuild<T1, T2> InTo(Expression<Func<T2, object>> targetProp)
            {
                string targetPropName = Reflector.GetPropertyName(targetProp);
                if (targetPropName != null) _mapBuild.Link(_sourcePropName, targetPropName);
                return _mapBuild;
            }
        }

    }
}
