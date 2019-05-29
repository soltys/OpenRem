namespace OpenRem.Gaas.Client.Interface
{
    public class DataPoint
    {
        public DataPoint()
        {
            
        }

        public DataPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }
    }
}
