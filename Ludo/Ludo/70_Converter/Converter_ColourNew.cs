﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Ludo._70_Converter
{
    public class Converter_ColorNew : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            {
                // VM -> View
                string color_Hex = "#";
                int[] values = value as int[];
                foreach (int item in values)
                {
                    int subcolor = item;
                    string substring = string.Format("{0:x}", subcolor);
                    substring = substring.ToUpper();
                    if (substring.Length == 1)
                    {
                        substring = "0" + substring;
                    }
                    color_Hex += substring;
                }
                return color_Hex;
            }
    }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // View -> VM
            string color_hex = value as string;
            color_hex = color_hex.Substring(1);
            int[] color_array = new int[3];
            if (true)
            {
                for (int i = 0; i < 3; i++)
                {
                    string subcolor_hex = color_hex.Substring(i * 2, 2);
                    color_array[i] = System.Convert.ToInt32(subcolor_hex, 16);
                }
            }
            return color_array;
        }
    }
}
