using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ExtentionCz
{
    public static class Extensions
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int PostMessage(IntPtr hwnd, Int32 wMsg, Int32 wParam, Int32 lParam);

        /// <summary>
        /// Adds an open option to <c>DateTimePicker</c>
        /// </summary>
        /// <param name="obj"></param>
        public static void Open(this DateTimePicker obj)
        {
            const int WM_LBUTTONDOWN = 0x0201;
            const int WM_LBUTTONUP = 0x0202;
            int width = obj.Width - 10;
            int height = obj.Height / 2;
            int lParam = width + height * 0x00010000; // VooDoo to shift height
            PostMessage(obj.Handle, WM_LBUTTONDOWN, 1, lParam);
            PostMessage(obj.Handle, WM_LBUTTONUP, 1, lParam);
        }

        /// <summary>
        /// Makes string Title case or First char case
        /// </summary>
        /// <param name="str">Input string</param>
        /// <param name="titleCase">If true - title case, else First char case</param>
        /// <returns></returns>
        public static string ToTitleCase(this string str, bool titleCase = true) //Exenstion methood
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            if (string.IsNullOrEmpty(str)) return str;
            str = str.ToLower();
            if (titleCase)
            {
                return ci.TextInfo.ToTitleCase(str);
            }
            else
            {
                var strArray = str.Split(' ');
                if (strArray.Length > 1)
                {
                    strArray[0] = ci.TextInfo.ToTitleCase(strArray[0]);
                    return string.Join(" ", strArray);
                }
            }
            return ci.TextInfo.ToTitleCase(str);
        }
    }
}
