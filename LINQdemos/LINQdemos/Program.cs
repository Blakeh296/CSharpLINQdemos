using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace LINQdemos
{
    class Program
    {
        static void Main(string[] args)
        {
            QueryStringArray();

            QueryIntArray();

            QueryArrayList();

            Console.ReadLine();
        }

        static void QueryStringArray()
        {
            string[] dogs = {"K 9", "Brian Griffin", "Scooby Doo", "Old Yeller", "Rin Tin Tin", "Benji"
            , "Charlie B. Barkin", "Lassie", "Snoopy"};

            var dogSpaces = from dog in dogs
                            where dog.Contains(" ")
                            orderby dog descending
                            select dog;

            foreach(var i in dogSpaces)
            { Console.WriteLine(i); }

            Console.WriteLine();
        }

        static int[] QueryIntArray()
        {
            int[] nums = { 5, 10, 15, 20, 25, 30, 35 };

            var get20 = from num in nums
                        where num > 20
                        orderby num
                        select num;

            foreach(var i in get20)
            { Console.WriteLine(i); }
            Console.WriteLine();
            Console.WriteLine($"Get Type : {get20.GetType()}");

            var listGT20 = get20.ToList<int>();
            var arrayGT20 = get20.ToArray();

            nums[0] = 40;

            foreach(var i in get20)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine();
            return arrayGT20;
        }

        static void QueryArrayList()
        {
            ArrayList famAnimals = new ArrayList()
            {
                new Animal{Name = "Heidi", Height=.8, Weight=18}
                ,new Animal{Name = "Shrek", Height=4, Weight=130}
                ,new Animal{Name = "Congo", Height=3.8,Weight=90}

            };

            var famAnimalEnum = famAnimals.OfType<Animal>();

            var smallAnimals = from animal in famAnimalEnum
                               where animal.Weight <= 90
                               orderby animal.Name
                               select animal;

            foreach(var animal in smallAnimals)
            { Console.WriteLine("{0} weighs {1} lbs", animal.Name, animal.Weight); }

        }
    }
}
