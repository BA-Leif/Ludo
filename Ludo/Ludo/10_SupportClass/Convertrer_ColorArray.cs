﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Ludo._10_SupportClass
{
    class Convertrer_ColorArray : IMultiValueConverter
    {
        //public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        //{
        //    {
        //        // VM -> View
        //        string color_Hex = "#";
        //        int[] value_array = values as int[];
        //        for (int i = 0; i < 3; i++)
        //        {
        //            string substring = string.Format("{0:x}", value_array[i]);
        //            substring = substring.ToUpper();
        //            if (substring.Length == 1)
        //            {
        //                substring = "0" + substring;
        //            }
        //            color_Hex += substring;
        //        }
        //        return color_Hex;
        //    }
        //}

        //public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        //{
        //    // View -> VM
        //    string color_hex = value as string;
        //    color_hex = color_hex.Substring(1);
        //    int[] color_array = new int[3];
        //    for (int i = 0; i < 3; i++)
        //    {
        //        string subcolor_hex = color_hex.Substring(i * 2, 2);
        //        color_array[i] = System.Convert.ToInt32(subcolor_hex, 16);
        //    }
        //    return color_array;
        //}
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
