namespace Toolbox
{
    public enum MapperOptions
    {
        NONE                = 0,
        IGNORE_CASE         = 1,
        IGNORE_UNDERSCORE   = 2,
        FORCE_TYPE          = 4,
        IGNORE_ERRORS       = 8,
        ALL = IGNORE_CASE | IGNORE_UNDERSCORE | FORCE_TYPE | IGNORE_ERRORS
    }
}
