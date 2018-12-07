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

            QueryCollection();

            QueryAnimalData();

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

            Console.WriteLine();//This is a line break
        }

        static int[] QueryIntArray()
        {
            int[] nums = { 5, 10, 15, 20, 25, 30, 35 };

            var get20 = from num in nums
                        where num > 20
                        orderby num
                        select num;

            foreach(var i in get20) { Console.WriteLine(i); }

            Console.WriteLine();
            Console.WriteLine($"Get Type : {get20.GetType()}");
            Console.WriteLine();

            var listGT20 = get20.ToList<int>();
            var arrayGT20 = get20.ToArray();

            nums[0] = 40;

            foreach(var i in get20) { Console.WriteLine(i); }

            Console.WriteLine();
            return arrayGT20;
        }

        static void QueryArrayList()
        {
            ArrayList famousAnimals = new ArrayList()
            {
                new Animal{Name = "Heidi", Height=.8, Weight=18}
                , new Animal{Name = "Shrek", Height=4, Weight=130}
                , new Animal{Name = "Congo", Height=3.8,Weight=90}

            };

            var famAnimalEnum = famousAnimals.OfType<Animal>();

            var smallAnimals = from animal in famAnimalEnum
                               where animal.Weight <= 90
                               orderby animal.Name
                               select animal;

            foreach(var animal in smallAnimals)
            { Console.WriteLine("{0} weighs {1} lbs", animal.Name, animal.Weight); }

            Console.WriteLine();//This is a line break
        }

        static void QueryCollection()
        {
            //Create a list of Animals fore the Query to work with
            var animalList = new List<Animal>()
            {
                new Animal{Name = "German Sheperd", Height = 25, Weight = 77}
                , new Animal{Name = "Chihuahua", Height = 7, Weight = 4.4}
                , new Animal{Name = "Saint Bernard", Height = 30, Weight = 200}
            };

            //This is the Query, It retrieves the dogs from our animal list 
            //That meet credentials in the Where statement. Dogs heavier than 70lbs & taller than 25
            var bigDogs = from dog in animalList
                          where (dog.Weight > 70) && (dog.Height > 25)
                          orderby dog.Name
                          select dog;

            //Write the all of the dogs to the console that meed query credentials
            foreach(var dog in bigDogs)
            { Console.WriteLine("A {0} weighs {1} lbs", dog.Name, dog.Weight); }

            Console.WriteLine(); //This is a line break
        }

        //This method utilized INNER JOIN
        static void QueryAnimalData()
        {
            //Create a list of animals
            Animal[] animals = new[]
            {
                 new Animal{Name = "German Sheperd", Height = 25, Weight = 77, AnimalID = 1}
                , new Animal{Name = "Chihuahua", Height = 7, Weight = 4.4, AnimalID = 2}
                , new Animal{Name = "Saint Bernard", Height = 30, Weight = 200, AnimalID = 3}
                , new Animal{Name = "Pug", Height = 12, Weight = 16, AnimalID = 1}
                , new Animal{Name = "Beagle", Height = 15, Weight = 23, AnimalID = 2}
            };
            //Create a list of owners
            Owner[] owners = new[]
            {
                new Owner{Name = "Doug Parks", OwnerID = 1}
                , new Owner{Name = "Sally Smith", OwnerID = 2}
                , new Owner {Name = "Paul Brooks", OwnerID = 3}
            };

            // Pulling these two things out and making a brand new collection WITHOUT the Weight
            var nameHeight = from a in animals
                             select new
                             {
                                 a.Name,
                                 a.Height
                             };

            //Convert the query above into an Array
            Array arrNameHeight = nameHeight.ToArray();

            foreach (var i in arrNameHeight)
            { Console.WriteLine(i.ToString()); }

            Console.WriteLine();

            var innerJoin =
                from animal in animals //Start with the Animals List
                join owner in owners on animal.AnimalID //Join the owners List to the Animals List by the AnimalID
                equals owner.OwnerID
                select new
                {   //Variables created we can access
                    OwnerName = owner.Name
                    , AnimalName = animal.Name
                };

            foreach (var i in innerJoin)
            {
                Console.WriteLine("{0} owns {1}",
                    i.OwnerName, i.AnimalName);
            }
            Console.WriteLine();

            //One query
            var groupJoin = from owner in owners
                            orderby owner.OwnerID
                            join animal in animals
                            on owner.OwnerID
                            equals animal.AnimalID
                            into ownerGroup
                            select new
                            {
                                Owner = owner.Name, //Another query
                                Animals = from owner2
                                          in ownerGroup
                                          orderby owner2.Name
                                          select owner2
                            };

            foreach (var ownerGroup in groupJoin)
            {
                Console.WriteLine(ownerGroup.Owner);
                foreach(var animal in ownerGroup.Animals)
                {
                    Console.WriteLine("* {0}", animal.Name);
                }
            }
        }
    }
}
