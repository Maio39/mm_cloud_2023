namespace Day20Lab1
{
    public class Utils
    {
        public static bool isDD(int a)
        {
            return (a%2 == 0);
        }

        public static string Reverse(string text)
        {
            char[] chars = text.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        public static double FakeDiv(double a,double b)
        {
            /*BUG
            if (a == 1010)
                return 1010;
            */
            return a / b; //3.141517;
        }
    }
}