using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;

namespace Spectrogram
{
    public static class Tools
    {
        private static string FindFile(string filePath)
        {
            // look for it in this folder
            string pathFileHere = System.IO.Path.GetFullPath(filePath);
            if (System.IO.File.Exists(pathFileHere))
                return pathFileHere;
            else
                Console.WriteLine($"File not found in same folder: {pathFileHere}");

            // look for it in the package data folder
            string fileName = System.IO.Path.GetFileName(filePath);
            string pathDataFolder = System.IO.Path.GetFullPath("../../../../data/");
            string pathInDataFolder = System.IO.Path.Combine(pathDataFolder, fileName);
            if (System.IO.File.Exists(pathInDataFolder))
                return pathInDataFolder;
            else
                Console.WriteLine($"File not found in data folder: {pathInDataFolder}");

            throw new ArgumentException($"Could not locate {filePath}");
        }

        public static float[] FloatsFromBytesINT16(byte[] bytes, int skipFirstBytes = 0)
        {
            float[] pcm = new float[(bytes.Length - skipFirstBytes) / 2];
            for (int i = skipFirstBytes; i < bytes.Length - 2; i += 2)
            {
                if (i / 2 >= pcm.Length)
                    break;
                pcm[i / 2] = BitConverter.ToInt16(bytes, i);
            }
            return pcm;
        }

        public static float[] ReadWav(string wavFilePath, int? sampleLimit = null)
        {
            // quick and drity WAV file reader (only for 16-bit signed mono files)
            string actualPath = FindFile(wavFilePath);
            if (actualPath == null)
                throw new ArgumentException("file not found: " + actualPath);
            byte[] bytes = System.IO.File.ReadAllBytes(actualPath);
            int sampleCount = bytes.Length / 2;
            if (sampleLimit != null)
                sampleCount = Math.Min(sampleCount, (int)sampleLimit);
            return FloatsFromBytesINT16(bytes, skipFirstBytes: 44);
        }

        public static int[] LoadDetectedFrec(string binFilePath, double scaler)
        {
            string actualPath = FindFile(binFilePath);
            if (actualPath == null)
                throw new ArgumentException("file not found: " + actualPath);
            if (File.Exists(actualPath))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(actualPath, FileMode.Open)))
                {
                    long intsToRead = new System.IO.FileInfo(actualPath).Length / 4;
                    int[] detectedFrec = new int[intsToRead];
                    for(int i = 0; i < intsToRead; i++)
                    {
                        int freq = reader.ReadInt32();
                        detectedFrec[i] = (int)((double) freq / scaler);
                    }
                    return detectedFrec;
                }
            }
            return null;
        }

        public static float[] ReadMp3(string mp3FilePath, int? sampleLimit = null)
        {
            string actualPath = FindFile(mp3FilePath);
            var reader = new NAudio.Wave.Mp3FileReader(actualPath);
            int bytesToRead = (int)reader.Length;
            if (sampleLimit != null)
                bytesToRead = Math.Min(bytesToRead, (int)sampleLimit * 2);
            byte[] bytes = new byte[bytesToRead];
            reader.Read(bytes, 0, bytesToRead);
            float[] pcm = FloatsFromBytesINT16(bytes);
            return pcm;
        }

    }
}
