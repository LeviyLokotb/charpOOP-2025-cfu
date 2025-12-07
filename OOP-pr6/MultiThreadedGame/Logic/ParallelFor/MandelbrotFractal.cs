namespace Mandelbrot
{
    /// <summary>
    /// Генерация множества Мандельброта на плоскости
    /// </summary>
    public static class MandelbrotFractal
    {
        /// <summary>
        /// Следующая точка последоватьельности Мандельброта
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="max_iters"></param>
        /// <returns></returns>
        public static MandelbrotPoint NextPoint(double x, double y, int max_iters=500)
        {
            var (x0, y0) = (x, y);
            // Если модуль комплексного числа >2, следующие числа последовательности -> +inf
            int i = 0;
            while (x*x + y*y <= 4 && i < max_iters)
            {
                // Эти значения переформулируются из комплексной формулы последовательности
                // z_{n+1} = z_{n}^{2} + c
                double x_ = x*x - y*y + x0;
                double y_ = 2*x*y + y0;

                (x, y) = (x_, y_);

                i++;
            }

            return new MandelbrotPoint() {
                X=x, 
                Y=y, 
                color = (byte)(255* (i / max_iters)),
                iters = i,
                maxIters = max_iters
            };
        }
    }

    /// <summary>
    /// Точка множества Мандельброта
    /// </summary>
    public struct MandelbrotPoint
    {
        public double X;
        public double Y;
        public byte color;
        public int iters;
        public int maxIters;
    }
}