using System;

namespace lsc.Spider
{
    class Program
    {
        static void Main(string[] args)
        {
            Spiderruyile spiderruyile = new Spiderruyile();
            spiderruyile.SpiderTest();
            Console.WriteLine("抓取完毕");
            Console.ReadKey();
        }
    }
}
