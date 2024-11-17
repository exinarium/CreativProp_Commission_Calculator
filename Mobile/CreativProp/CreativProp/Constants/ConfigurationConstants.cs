using System;
namespace CreativProp.Constants
{
    public class ConfigurationConstants
    {
        public readonly static string DB_PATH = "creativprop.db3";

#if DEBUG
        public readonly static string BASE_URL = "https://localhost";
#else
        public readonly static string BASE_URL = "https://localhost";
#endif
    }
}

