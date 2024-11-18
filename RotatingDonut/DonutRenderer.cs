// DonutRenderer.cs
using System;

namespace RotatingDonut
{
    public class DonutRenderer
    {
        public float A { get; set; } = 0;
        public float B { get; set; } = 0;
        public float[] ZBuffer { get; set; } = new float[1760];
        public char[] OutputBuffer { get; set; } = new char[1760];

        public void InitializeBuffers()
        {
            Array.Fill(ZBuffer, 0);
            Array.Fill(OutputBuffer, ' ');
        }

        public void RenderFrame()
        {
            double i, j;

            for (j = 0; j < 6.28; j += 0.07)
            {
                for (i = 0; i < 6.28; i += 0.02)
                {
                    float c = (float)Math.Sin(i);
                    float d = (float)Math.Cos(j);
                    float e = (float)Math.Sin(A);
                    float f = (float)Math.Sin(j);
                    float g = (float)Math.Cos(A);
                    float h = d + 2;
                    float D = 1f / (c * h * e + f * g + 5);
                    float l = (float)Math.Cos(i);
                    float m = (float)Math.Cos(B);
                    float n = (float)Math.Sin(B);
                    float t = c * h * g - f * e;

                    int x = (int)(40 + 30 * D * (l * h * m - t * n));
                    int y = (int)(12 + 15 * D * (l * h * n + t * m));
                    int o = x + 80 * y;

                    int N = (int)(8 * ((f * e - c * d * g) * m - c * d * e - f * g - l * d * n));

                    if (22 > y && y > 0 && x > 0 && 80 > x && D > ZBuffer[o])
                    {
                        ZBuffer[o] = D;
                        OutputBuffer[o] = ".,-~:;=!*#$@"[Math.Max(0, Math.Min(11, N))];
                    }
                }
            }
        }

        public void UpdateAngles()
        {
            A += 0.04f;
            B += 0.02f;
        }

        public void DisplayFrame()
        {
            Console.SetCursorPosition(0, 0);

            for (int k = 0; k < OutputBuffer.Length; k++)
            {
                Console.Write(OutputBuffer[k]);
                if ((k + 1) % 80 == 0)
                {
                    Console.WriteLine();
                }
            }
        }
    }
}
