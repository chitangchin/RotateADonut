float A = 0, B = 0;
double i, j;
float[] z = new float[1760];
char[] b = new char[1760];

Console.WriteLine("\x1b[2J");

for (; ; )
{
    Array.Fill(b, ' ');
    Array.Fill(z, 0);

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

            if (22 > y && y > 0 && x > 0 && 80 > x && D > z[o])
            {
                z[o] = D;
                b[o] = ".,-~:;=!*#$@"[Math.Max(0, Math.Min(11, N))];
            }
        }
    }

    Console.SetCursorPosition(0, 0);

    for (int k = 0; k < 1760; k++)
    {
        Console.Write(k % 80 != 0 ? b[k] : '\n');
    }

    A += 0.04f;
    B += 0.02f;

    System.Threading.Thread.Sleep(30);
}