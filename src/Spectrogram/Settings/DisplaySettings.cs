﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Spectrogram.Settings
{
    public class DisplaySettings
    {
        // The Spectrograph library does two things:
        //   1) convert a signal to a FFT List
        //   2) convert a FFT list to a Bitmap

        // This class stores settings that control how the Bitmap looks (#2)

        public double fftResolution;
        public double freqLow;
        public double freqHigh;

        public int pixelLower { get { return (int)(freqLow / fftResolution); } }
        public int pixelUpper { get { return (int)(freqHigh / fftResolution); } }
        public int height { get { return pixelUpper - pixelLower; } }
        public int width;

        public float brightness = 1;
        public bool decibels;
        public Colormap colormap = Colormap.viridis;
        public int? highlightColumn = null;
        public bool showTicks = false;
        public double tickSpacingSec = 1;
        public double tickSpacingHz = 250;

        public bool renderNeeded;

        public double lastRenderMsec;

        public int tickSize = 4;
        public Font tickFont = new Font(FontFamily.GenericSansSerif, (float)6);
        public StringFormat sfTicksRight = new StringFormat()
        {
            LineAlignment = StringAlignment.Center,
            Alignment = StringAlignment.Far
        };
        public StringFormat sfTicksLower = new StringFormat()
        {
            LineAlignment = StringAlignment.Far,
            Alignment = StringAlignment.Center
        };

    }
}
