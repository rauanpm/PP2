using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Student s1 = new Student("Rauan Faizullayev", "18bd11030");
            s1.NewYear();

        }
    }
    class Student
    {
        string Name { get; set; }
        string Id { get; set; }
        int Year { get; set; }
        public Student(string name, string id)
        {
            Name = name;
            Id = id;
            Year = 2019;
        }
        public void NewYear()
        {
            Console.WriteLine("Student: {0} / Id: {1}", Name, Id);
            Year++;
            Console.ReadKey();
        }
    }
}
        

