using System.Drawing;
//using System.Runtime.InteropServices;


namespace ConsoleDemo
{
    //[ComVisible(true)]
    //[ClassInterface(ClassInterfaceType.None)]
    public class Program  //: IWindow
    {
        static void Main(string[] args)
        {
            DemoKike();
            //DemoHal();
            //DemoMozart();
            //DemoQRSS();
        }

        /*static void DemoMozart()
        {
            var spec = new Spectrogram.Spectrogram(sampleRate: 8000, fftSize: 2048, step: 700);
            float[] values = Spectrogram.Tools.ReadWav("mozart.wav");
            spec.AddExtend(values);
            int [] detectedFrec = Spectrogram.Tools.LoadDetectedFrec("data.bin", spec.displaySettings.fftResolution);
            spec.AddDetectedFrec(detectedFrec);
            Bitmap bmp = spec.GetBitmap(intensity: 2, freqHigh: 2500, showTicks: true);
            spec.SaveBitmap(bmp, "mozart.jpg");
        }*/


        static void DemoKike()
        {
            string name = "440";
            int fs = 48000, fft_size = 2048, frec_high = 2000;
            int height = frec_high * fft_size / fs;
            double scaler = (double)frec_high / (double)height;



            float[] values_in = Spectrogram.Tools.ReadWav(name + ".wav");
            float[] values_out = Spectrogram.Tools.ReadWav(name + "_det.wav");

            int[] detectedFrec_v2 = Spectrogram.Tools.LoadDetectedFrec(name + "_freq_det.bin", scaler);
            int[] objectiveFrec_v2 = Spectrogram.Tools.LoadDetectedFrec(name + "_freq_obj.bin", scaler);

            var spec_in_v2 = new Spectrogram.Spectrogram(sampleRate: fs, fftSize: fft_size, step: 2048);
            var spec_out_v2 = new Spectrogram.Spectrogram(sampleRate: fs, fftSize: fft_size, step: 2048);

            spec_in_v2.AddExtend(values_in);
            spec_out_v2.AddExtend(values_out);


            spec_in_v2.AddDetectedFrec(detectedFrec_v2);
            spec_out_v2.AddDetectedFrec(detectedFrec_v2);

            spec_in_v2.AddObjectiveFrec(objectiveFrec_v2);
            spec_out_v2.AddObjectiveFrec(objectiveFrec_v2);


           
            Bitmap bmp_in_v2 = spec_in_v2.GetBitmap(intensity: 2, freqHigh: frec_high, showTicks: true, colormap: Spectrogram.Colormap.grayscale);
            Bitmap bmp_out_v2 = spec_out_v2.GetBitmap(intensity: 2, freqHigh: frec_high, showTicks: true, colormap: Spectrogram.Colormap.grayscale);
            

            spec_in_v2.SaveBitmap(bmp_in_v2, name + "_v2.jpg");
            spec_out_v2.SaveBitmap(bmp_out_v2, name + "_out_v2.jpg");
        }


        /* static void DemoQRSS()
         {
             var spec = new Spectrogram.Spectrogram(sampleRate: 8000, fftSize: 16384, step: 8000);
             float[] values = Spectrogram.Tools.ReadMp3("qrss-w4hbk.mp3");
             spec.AddExtend(values);
             Bitmap bmp = spec.GetBitmap(intensity: 1.5, freqLow: 1100, freqHigh: 1500,
                 showTicks: true, tickSpacingHz: 50, tickSpacingSec: 60);
             spec.SaveBitmap(bmp, "qrss.png");
         }

         static void DemoHal()
         {
             var spec = new Spectrogram.Spectrogram(sampleRate: 44100, fftSize: 4096, step: 400);
             float[] values = Spectrogram.Tools.ReadMp3("cant-do-that.mp3");
             spec.AddExtend(values);
             Bitmap bmp;

             bmp = spec.GetBitmap(intensity: .2, freqHigh: 1000);
             spec.SaveBitmap(bmp, "cant-do-that.jpg");

             bmp = spec.GetBitmap(intensity: .2, freqHigh: 1000,
                 colormap: Spectrogram.Colormap.grayscale);
             spec.SaveBitmap(bmp, "cant-do-that-grayscale.jpg");

             bmp = spec.GetBitmap(intensity: .2, freqHigh: 1000,
                 colormap: Spectrogram.Colormap.grayscaleInverted);
             spec.SaveBitmap(bmp, "cant-do-that-grayscale-inverted.jpg");

             bmp = spec.GetBitmap(intensity: .2, freqHigh: 1000,
                 colormap: Spectrogram.Colormap.vdGreen);
             spec.SaveBitmap(bmp, "cant-do-that-green.jpg");
         }*/
    }
}
