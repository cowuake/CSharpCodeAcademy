using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RgbToHexDesktopConverter.Models
{
    public class ColorModel
    {
        public byte R { get; set; }

        public byte G { get; set; }

        public byte B { get; set; }

        public string Hex { get; set; }

        public ColorModel(byte red, byte green, byte blue)
        {
            R = red;
            G = green;
            B = blue;
            FromRgbToHex();
        }

        public ColorModel(string hex)
        {
            Hex = hex;
            FromHexToRgb();
        }

        public void FromRgbToHex()
        {
            Hex = $"{R:X2}{G:X2}{B:X2}";
        }

        public void FromHexToRgb()
        {
            if (string.IsNullOrEmpty(Hex))
                return;

            R = byte.Parse(Hex.Substring(0,2), NumberStyles.AllowHexSpecifier);
            G = byte.Parse(Hex.Substring(2,2), NumberStyles.AllowHexSpecifier);
            B = byte.Parse(Hex.Substring(4,2), NumberStyles.AllowHexSpecifier);
        }
    }
}