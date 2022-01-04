using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExtensionMethodsWithLambdaExpression
{
    public class Program
    {
        public int Count { get; set; }
        static void Main(string[] args)
        {
            var obj = new Program();
            obj.PrintDummyText();
            obj.PrintDummyText("Hello Again");

            Console.ReadKey();

            var programList = new List<Program> {
               new Program{ Count = 2},
               new Program{ Count = 3},
               new Program{ Count = 4},
               new Program{ Count = 5}
            };

            int total = programList.AsQueryable().CountData(x => x.Count > 3);

            Console.WriteLine(total);
            Console.ReadKey();
        }
    }

    public static class ProgramExtensions
    {

        // exmaple of Extension MEthod
        public static void PrintDummyText(this Program program,string inputStr=null)
        {
            if (string.IsNullOrEmpty(inputStr))
                Console.WriteLine("Dummy");
            else
                Console.WriteLine(inputStr);
        }

        // example of Extension MEthods with Lambda Expression
        public static int CountData(this IQueryable<Program> programs , Expression<Func<Program,bool>> predicate)
        {
            int total = 0;
            var func = predicate.Compile();
            foreach (var item in programs)
            {
                if (func(item))
                    total += item.Count;
            }
            return total;
        }
    }
}
