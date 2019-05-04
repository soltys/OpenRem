namespace OpenRem.Common
{
    public static class MathLib
    {
        /// <summary>
        /// Moving Avarage algorythm
        /// </summary>
        /// <param name="data">input data</param>
        /// <param name="windowSize">window size</param>
        /// <returns>Data after applying avarage filter </returns>
        public static double[] MovingAvarage(double[] data, int windowSize )
        {
            var buffer = new double[windowSize];
            var output = new double[data.Length];
            var current_index = 0;
            for (int i = 0; i < data.Length; i++)
            {
                buffer[current_index] = data[i] / windowSize;
                double ma = 0.0;
                for (int j = 0; j < windowSize; j++)
                {
                    ma += buffer[j];
                }
                output[i] = ma;
                current_index = (current_index + 1) % windowSize;
            }
            return output;
        }
    }
}
