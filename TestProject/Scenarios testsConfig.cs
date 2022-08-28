using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestsProject
{
    public class Scenarios_testsConfig
    {
        public class Config
        {
            #region Default Data
            private const string LETQUERY = "let myCulmn = Connections";
            private const string Actuale_LETQUERY = " let CustomerData0 = Connections ";
            private const string LOOKUPQUERY = "T| lookup mykind = leftouter(DimensionTable) on CommonColumn, $left.Col1 == $right.Col2";
            private const string Actuale_LOOKUPQUERY = "T| lookup CustomerData0 = leftouter(DimensionTable) on CommonColumn, $left.Col1 == $right.Col2";
            #endregion
            public static string CONFIG_FNAME = "ScenariosConfig12.xml";
            public static ConfigData GetConfigData()
            {
                if (!File.Exists(CONFIG_FNAME)) // create config file with default values
                {
                    using (FileStream fs = new FileStream(CONFIG_FNAME, FileMode.Create))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(ConfigData));
                        ConfigData sxml = new ConfigData();
                        xs.Serialize(fs, sxml);
                        return sxml;
                    }
                }
                else // read configuration from file
                {
                    ConfigData sc = new ConfigData();
                    using (FileStream fs = new FileStream(CONFIG_FNAME, FileMode.Open))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(ConfigData));
                        try
                        {
                            sc = (ConfigData)xs.Deserialize(fs);
                        }
                        catch (Exception ex)
                        {
                            while (ex != null)
                            {
                                return null;
                            }
                        }
                        return sc;
                    }
                }
            }

            public static bool SaveConfigData(ConfigData config)
            {
                if (!File.Exists(CONFIG_FNAME)) return false; // don't do anything if file doesn't exist

                using (FileStream fs = new FileStream(CONFIG_FNAME, FileMode.Open))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(ConfigData));
                    xs.Serialize(fs, config);
                    return true;
                }
            }

            // this class holds configuration data
            public class ConfigData
            {
                public string letQuery;
                public string lookupQuery;
                public string actualeLetQuery;
                public string actualeLoockupQuery;

                public ConfigData()
                {
                    letQuery = LETQUERY;
                    lookupQuery = LOOKUPQUERY;
                    actualeLetQuery = Actuale_LETQUERY;
                    actualeLoockupQuery = Actuale_LOOKUPQUERY;
                }
            }
        }
    }
}

