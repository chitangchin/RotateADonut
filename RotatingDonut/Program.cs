namespace RotatingDonut
{
    class Program
    {
        static void Main(string[] args)
        {
            var renderer = new DonutRenderer();

            Console.WriteLine("\x1b[2J"); // Clear the console

            while (true)
            {
                renderer.InitializeBuffers();
                renderer.RenderFrame();
                renderer.DisplayFrame();
                renderer.UpdateAngles();

                Thread.Sleep(30);
            }
        }
    }
}
