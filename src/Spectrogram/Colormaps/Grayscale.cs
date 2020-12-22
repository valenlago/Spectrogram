using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Spectrogram.Colormaps
{
    class Grayscale : Colormap
    {
        public override string GetName()
        {
            return "Grayscale";
        }

        public override void Apply(Bitmap bmp)
        {
            ColorPalette pal = bmp.Palette;

            pal.Entries[0] = Color.FromArgb(255, 255, 0, 0);
            pal.Entries[1] = Color.FromArgb(255, 0, 0, 255);
            for (int i=2; i<256 ; i++)
                pal.Entries[i] = Color.FromArgb(255, i, i, i);

            bmp.Palette = pal;
        }



    }
}
