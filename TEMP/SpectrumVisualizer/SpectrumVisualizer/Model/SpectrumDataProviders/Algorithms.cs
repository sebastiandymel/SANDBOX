using System;

namespace SpectrumVisualizer.Model
{
    public class Algorithms
    {
        public static double Scale(double valueIn, double baseMin, double baseMax, double limitMin, double limitMax)
        {
            return ((limitMax - limitMin) * (valueIn - baseMin) / (baseMax - baseMin)) + limitMin;
        }

        public static void ToMagnitudeComplex(double[] sampleBuffer, bool decibel)
        {
            var half = sampleBuffer.Length/2;
            for (int i = 0; i < half ; i ++)
            {
                sampleBuffer[i] =
                    decibel
                    ? 10.0 * Math.Log10((float)(Math.Sqrt(sampleBuffer[i] * sampleBuffer[i] + sampleBuffer[i + half] * sampleBuffer[i + half])))
                    : (float)(Math.Sqrt(sampleBuffer[i] * sampleBuffer[i] + sampleBuffer[i + half] * sampleBuffer[i + half]));
            }
        }

        public static void ToMagnitudeReal(double[] sampleBuffer, bool decibel)
        {
            for (int i = 0; i < sampleBuffer.Length; i++)
            {
                sampleBuffer[i] =
                    decibel
                        ? 10.0 * Math.Log10((float)(Math.Sqrt(sampleBuffer[i] * sampleBuffer[i])))
                        : (float)(Math.Sqrt(sampleBuffer[i] * sampleBuffer[i]));
            }
        }
    }
}