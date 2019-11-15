using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace coinBlock.ViewModels
{
    public class FloatConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string rVal = null;
            try
            {
                if (DependencyProperty.UnsetValue == values[0] || DependencyProperty.UnsetValue == values[1])
                {
                    return "0";
                }
                else
                {
                    //rVal = (decimal.Parse(values[0].ToString())).ToString(values[1].ToString());
                    decimal value = decimal.Parse(values[0].ToString());
                    decimal floatCount = decimal.Parse(Math.Pow(10.0, int.Parse(values[1].ToString().Replace("n", ""))).ToString());
                    string strFormat = values[1].ToString();
                    if (values.Count() < 3)
                    {
                        rVal = (Math.Truncate(value * floatCount) / floatCount).ToString(strFormat);
                    }
                    else
                    {
                        rVal = (Math.Truncate(value * floatCount) / floatCount).ToString(strFormat) + " " + values[2].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            return rVal;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
