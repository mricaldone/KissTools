using System;
using System.Linq.Expressions;

namespace KissTools.Toolbox
{
    public static class AutoMapper
    {
        public static Mapper<T1> Map<T1>(T1 obj)
        {
            return new Mapper<T1>(obj, MapperOptions.NONE);
        }

        public class Mapped<T1, T2>
        {
            private T1 _sourceObj;
            private T2 _targetObj;
            private MapperOptions _options;
            internal Mapped(T1 sourceObj, T2 targetObj, MapperOptions options)
            {
                _sourceObj = sourceObj;
                _targetObj = targetObj;
                _options = options;
            }

            public Linker<T1, T2> Link(Expression<Func<T1, object>> sourceProp)
            {
                return new Linker<T1, T2>(_sourceObj, _targetObj, sourceProp, _options);
            }
        }

        public class Mapper<T1>
        {
            private T1 _sourceObj;
            private MapperOptions _options;
            internal Mapper(T1 sourceObj, MapperOptions options)
            {
                _sourceObj = sourceObj;
                _options = options;
            }

            public Mapped<T1,T2> InTo<T2>(T2 targetObj, MapperOptions options = MapperOptions.NONE)
            {
                String targetPropName = "", sourcePropName = "";
                try
                {
                    _options = options;
                    foreach (String propName in ReflectionHelper.GetProperties(_sourceObj))
                    {
                        try
                        {
                            sourcePropName = propName;
                            targetPropName = ReflectionHelper.GetBestMatchProperty(targetObj, sourcePropName, options);
                            object sourcePropValue = ReflectionHelper.GetValue(_sourceObj, sourcePropName);
                            ReflectionHelper.SetValue(targetObj, targetPropName, sourcePropValue, (_options & MapperOptions.FORCE_TYPE) == MapperOptions.FORCE_TYPE);
                        }
                        catch (Exception ex)
                        {
                            if ((_options & MapperOptions.IGNORE_ERRORS) != MapperOptions.IGNORE_ERRORS) throw ex;
                        }
                        
                    }
                    return new Mapped<T1, T2>(_sourceObj, targetObj, _options);
                }
                catch (InvalidCastException ex)
                {
                    String action = $"When trying to map {typeof(T1).Name}.{sourcePropName} into {typeof(T2).Name}.{targetPropName}";
                    if ((MapperOptions.FORCE_TYPE & options) != MapperOptions.FORCE_TYPE)
                        throw new MappingException($"{ex.Message}. {action}. Use {nameof(MapperOptions)}.{nameof(MapperOptions.FORCE_TYPE)} to force conversion.");
                    else
                        throw new MappingException($"{ex.Message}. {action}.");
                }
            }
        }

        public class Linker<T1,T2>
        {
            private Expression<Func<T1, object>> _sourceProp;
            private T1 _sourceObj;
            private T2 _targetObj;
            private MapperOptions _options;

            internal Linker(T1 sourceObj, T2 targetObj, Expression<Func<T1, object>> sourceProp, MapperOptions options)
            {
                _sourceProp = sourceProp;
                _targetObj = targetObj;
                _sourceObj = sourceObj;
                _options = options;
            }

            public Mapped<T1, T2> InTo(Expression<Func<T2, object>> targetProp)
            {
                String targetPropName = "", sourcePropName = "";
                try
                {
                    try
                    {
                        targetPropName = ReflectionHelper.GetPropertyName(targetProp);
                        sourcePropName = ReflectionHelper.GetPropertyName(_sourceProp);
                        object sourcePropValue = ReflectionHelper.GetValue(_sourceObj, sourcePropName);
                        ReflectionHelper.SetValue(_targetObj, targetPropName, sourcePropValue, (_options & MapperOptions.FORCE_TYPE) == MapperOptions.FORCE_TYPE);
                    }
                    catch (Exception ex)
                    {
                        if ((_options & MapperOptions.IGNORE_ERRORS) != MapperOptions.IGNORE_ERRORS) throw ex;
                    }
                    return new Mapped<T1, T2>(_sourceObj, _targetObj, _options);
                }
                catch (InvalidCastException ex)
                {
                    String action = $"When trying to map {typeof(T1).Name}.{sourcePropName} into {typeof(T2).Name}.{targetPropName}";
                    if ((MapperOptions.FORCE_TYPE & _options) != MapperOptions.FORCE_TYPE)
                        throw new MappingException($"{ex.Message}. {action}. Use {nameof(MapperOptions)}.{nameof(MapperOptions.FORCE_TYPE)} to force conversion.");
                    else
                        throw new MappingException($"{ex.Message}. {action}.");
                }
            }
        }

    }
}
