using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Utility
{
    public class LocalizationLib
    {
        public static string GetLocalizaionString(string key)
        {
            string uiString;
            try
            {   
                ResourceManager rm = new ResourceManager(Assembly.GetExecutingAssembly().GetName().Name + ".Localization.Resource", Assembly.GetExecutingAssembly());
                uiString = rm.GetString(key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return uiString;
        }
    }
}
