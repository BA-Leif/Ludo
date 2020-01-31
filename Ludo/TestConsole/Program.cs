using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string value = "255,0,15";
            string color_Hex = "#";
            string[] value_array = (value as string).Split(',');
            foreach (string item in value_array)
            {
                int subcolor = Int32.Parse(item);
                string substring = string.Format("{0:x}", subcolor);
                substring = substring.ToUpper();
                if (substring.Length == 1)
                {
                    substring = "0" + substring;
                }
                color_Hex += substring;
            }


            Console.WriteLine(color_Hex);
            Console.ReadLine();




            //string value = "#FF000F";
            //string color_hex = value as string;
            //color_hex = color_hex.Substring(1);
            //int[] color_array = new int[3];
            //for (int i = 0; i < 3; i++)
            //{
            //    string subcolor_hex = color_hex.Substring(i * 2, 2);
            //    color_array[i] = System.Convert.ToInt32(subcolor_hex, 16);
            //}
            //foreach (var item in color_array)
            //{
            //    Console.Write(item + " ");
            //}
            //Console.ReadLine();
        }
    }
}
