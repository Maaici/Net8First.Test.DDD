namespace LinqDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] ints1 = { 1, 32, 23, 12, 33, 42, 53, 26, 43, 27, 93 };
            //ints1.GetNewInts(func => func.)
            List<int> newInts = ints1.GetNewInts(t => t >= 10 && t <= 30);
            foreach (var item in newInts)
            {
                Console.WriteLine(item);
            }
        }
    }

    public static class ExtMethods {
        public static List<int> GetNewInts(this int[] ints, Func<int, bool> func)
        {
            List<int> newInts = [];
            foreach (int i in ints)
            {
                if (func(i)) {
                    newInts.Add(i);
                }
            }
            return newInts;
        }
    }
}

