using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp_Files_and_Strings
{

    public delegate int ComparePeopleDelegate(Person p1, Person p2);

    public class Person {
        public Person(string name, int age, int weight) {
            Name = name;
            Age = age;
            Weight = weight;
        }
        public string Name { get; set; }
        public int    Age { get; set; }
        public double Weight { get; set; }
        public override string ToString() {
            return $"{Name};{Age};{Weight}";
        }
    }
    public class ComparePeople : IComparer<Person> {

        private ComparePeopleDelegate comparisonMethod;

        public ComparePeople(ComparePeopleDelegate comparer)
        {
            comparisonMethod = comparer;
        }

        public int Compare(Person x, Person y) {
            return comparisonMethod(x,y);
        }
    }

    class Program {

        public static List<string> ReadFileLines(string filename) {
            List<string> result = new List<string>();
            using (var file = new StreamReader(filename)) {
                string line;
                while ((line = file.ReadLine()) != null) {
                    result.Add(line);
                }
            }
            return result;
        }
        public static List<Person> ReadCSVFile(string filename)
        {
            List<Person> personList = new List<Person>();
            List<string> fileLines = ReadFileLines(filename);
            foreach (string line in fileLines)
            {
                string[] fieldValues = line.Split(';');
                string name = fieldValues[0];
                int age = int.Parse(fieldValues[1]);
                int weight = int.Parse(fieldValues[2]);
                personList.Add(new Person(name, age, weight));
            }

            return personList;
        }

        static void Main(string[] args) {
            // Read all people from file
            List<Person> people = ReadCSVFile("../../data.csv");

            // Sort using lambda expression
            ComparePeople comparerObj = new ComparePeople((Person a, Person b) => a.Name.CompareTo(b.Name));
            people.Sort(comparerObj);

            // Output result
            foreach (var p in people)
            {
                Console.WriteLine(p);
            }
            Console.WriteLine("Number of people in data file : " + people.Count);
            Console.ReadKey();
        }
    }
}
