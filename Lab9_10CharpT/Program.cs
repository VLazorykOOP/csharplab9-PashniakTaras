using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Lab8CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lab#8 ");
            Console.WriteLine("What task do you want?");
            Console.WriteLine("1. Task 1");
            Console.WriteLine("2. Task 2");
            Console.WriteLine("3. Task 3");
            Console.WriteLine("4. Task 4");
            Console.WriteLine("5. Exit");

            int choice;
            bool isValidChoice = false;

            do
            {
                Console.Write("Enter number of task: ");
                isValidChoice = int.TryParse(Console.ReadLine(), out choice);

                if (!isValidChoice || choice < 1 || choice > 5)
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                    isValidChoice = false;
                }
            } while (!isValidChoice);

            switch (choice)
            {
                case 1:
                    Task1();
                    break;
                case 2:
                    Task2();
                    break;
                case 3:
                    Task3();
                    break;
                case 4:
                    Task4();
                    break;
                case 5:
                    break;
            }
        }

        static void Task1()
        {
            Console.WriteLine("Task 1");
            string filePath = "formula.txt";
            string formula;

            try
            {
                formula = File.ReadAllText(filePath);
            }
            catch (IOException e)
            {
                Console.WriteLine("Error: " + e.Message);
                return;
            }

            int result = EvaluateFormula(formula);
            Console.WriteLine("Result of formula " + formula + " is " + result);
        }

        static void Task2()
        {
            Console.Write("Task 2\n");

            // Читання з файлу та створення списку студентів
            List<Student> students = new List<Student>();
            using (StreamReader sr = new StreamReader("students.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] data = line.Split(' ');
                    string lastName = data[0];
                    string firstName = data[1];
                    string middleName = data[2];
                    int groupNumber = int.Parse(data[3]);
                    int grade1 = int.Parse(data[4]);
                    int grade2 = int.Parse(data[5]);
                    int grade3 = int.Parse(data[6]);
                    Student student = new Student(lastName, firstName, middleName, groupNumber, grade1, grade2, grade3);
                    students.Add(student);
                }
            }

            // Розділення студентів на дві черги: успішно здана сесія та інші
            Queue<Student> successfulStudents = new Queue<Student>();
            Queue<Student> otherStudents = new Queue<Student>();

            foreach (var student in students)
            {
                if (IsSuccessful(student))
                    successfulStudents.Enqueue(student);
                else
                    otherStudents.Enqueue(student);
            }

            // Виведення студентів в потрібному порядку
            Console.WriteLine("Студенти, які успішно здали сесію:");
            while (successfulStudents.Count > 0)
            {
                Console.WriteLine(successfulStudents.Dequeue());
            }

            Console.WriteLine("Інші студенти:");
            while (otherStudents.Count > 0)
            {
                Console.WriteLine(otherStudents.Dequeue());
            }
        }

        static void Task3()
        {
            Console.Write("Task 2\n");

            // Читання з файлу та створення списку студентів
            ArrayList students = new ArrayList();
            using (StreamReader sr = new StreamReader("students.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] data = line.Split(' ');
                    string lastName = data[0];
                    string firstName = data[1];
                    string middleName = data[2];
                    int groupNumber = int.Parse(data[3]);
                    int grade1 = int.Parse(data[4]);
                    int grade2 = int.Parse(data[5]);
                    int grade3 = int.Parse(data[6]);
                    Student2 student = new Student2(lastName, firstName, middleName, groupNumber, grade1, grade2, grade3);
                    students.Add(student);
                }
            }

            ArrayList successfulStudents = new ArrayList();
            ArrayList otherStudents = new ArrayList();

            foreach (Student2 student in students)
            {
                if (IsSuccessful2(student))
                    successfulStudents.Add(student);
                else
                    otherStudents.Add(student);
            }

            // Виведення студентів в потрібному порядку
            Console.WriteLine("Студенти, які успішно здали сесію:");
            foreach (Student2 student in successfulStudents)
            {
                Console.WriteLine(student);
            }

            Console.WriteLine("Інші студенти:");
            foreach (Student2 student in otherStudents)
            {
                Console.WriteLine(student);
            }
        }

        static void Task4()
        {
            Console.WriteLine("Task 4\n");

            Hashtable catalog = new Hashtable();

            catalog.Add("CD1", new ArrayList() { "Shape of You - Ed Sheeran", "Dance Monkey - Tones and I", "Bohemian Rhapsody - Queen" });
            catalog.Add("CD2", new ArrayList() { "Billie Jean - Michael Jackson", "Sweet Child O' Mine - Guns N' Roses", "Hotel California - Eagles" });

            catalog.Remove("CD2");

            ((ArrayList)catalog["CD1"]).Add("Yesterday - The Beatles");

            foreach (DictionaryEntry entry in catalog)
            {
                Console.WriteLine("CD: " + entry.Key);
                ArrayList songs = (ArrayList)entry.Value;
                foreach (string song in songs)
                {
                    Console.WriteLine("- " + song);
                }
            }

            string[] artistsToSearch = { "Ed Sheeran", "Michael Jackson", "Queen" };

            foreach (DictionaryEntry entry in catalog)
            {
                ArrayList songs = (ArrayList)entry.Value;
                foreach (string song in songs)
                {
                    foreach (string artistToSearch in artistsToSearch)
                    {
                        if (song.Contains(artistToSearch))
                        {
                            Console.WriteLine("Song: " + song + " is performed by " + artistToSearch);
                        }
                    }
                }
            }
        }

        static int EvaluateFormula(string formula)
        {
            Stack<int> operands = new Stack<int>();
            Stack<char> operators = new Stack<char>();

            foreach (char ch in formula)
            {
                if (ch == '(')
                {
                    operators.Push(ch);
                }
                else if (char.IsDigit(ch))
                {
                    operands.Push(int.Parse(ch.ToString()));
                }
                else if (ch == 'm' || ch == 'p')
                {
                    operators.Push(ch);
                }
                else if (ch == ',' || ch == ')')
                {
                    while (operators.Count > 0 && operators.Peek() != '(' && operands.Count >= 2)
                    {
                        char op = operators.Pop();
                        int operand2 = operands.Pop();
                        int operand1 = operands.Pop();

                        if (op == 'm')
                            operands.Push((operand1 - operand2) % 10);
                        else if (op == 'p')
                            operands.Push((operand1 + operand2) % 10);
                    }

                    if (ch == ')')
                    {
                        operators.Pop(); // Pop the '('

                        if (operators.Count > 0 && (operators.Peek() == 'm' || operators.Peek() == 'p'))
                        {
                            char op = operators.Pop();
                            int operand2 = operands.Pop();
                            int operand1 = operands.Pop();

                            if (op == 'm')
                                operands.Push((operand1 - operand2) % 10);
                            else if (op == 'p')
                                operands.Push((operand1 + operand2) % 10);
                        }
                    }
                }
            }

            return operands.Pop();
        }

        static bool IsSuccessful(Student student)
        {
            // Перевірка, чи студент успішно здав сесію (в цьому випадку, якщо середня оцінка більше 3)
            double averageGrade = (student.Grade1 + student.Grade2 + student.Grade3) / 3.0;
            return averageGrade > 3.0;
        }

        static bool IsSuccessful2(Student2 student)
        {
            // Перевірка, чи студент успішно здав сесію (в цьому випадку, якщо середня оцінка більше 3)
            double averageGrade = (student.Grade1 + student.Grade2 + student.Grade3) / 3.0;
            return averageGrade > 3.0;
        }

        

        
    }

    class Student
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int GroupNumber { get; set; }
        public int Grade1 { get; set; }
        public int Grade2 { get; set; }
        public int Grade3 { get; set; }

        public Student(string lastName, string firstName, string middleName, int groupNumber, int grade1, int grade2, int grade3)
        {
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            GroupNumber = groupNumber;
            Grade1 = grade1;
            Grade2 = grade2;
            Grade3 = grade3;
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {MiddleName} {GroupNumber} {Grade1} {Grade2} {Grade3}";
        }
    }

    class Student2 : IComparable, ICloneable
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int GroupNumber { get; set; }
        public int Grade1 { get; set; }
        public int Grade2 { get; set; }
        public int Grade3 { get; set; }

        public Student2(string lastName, string firstName, string middleName, int groupNumber, int grade1, int grade2, int grade3)
        {
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            GroupNumber = groupNumber;
            Grade1 = grade1;
            Grade2 = grade2;
            Grade3 = grade3;
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {MiddleName} {GroupNumber} {Grade1} {Grade2} {Grade3}";
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Student2 otherStudent = obj as Student2;
            if (otherStudent != null)
            {
                double averageGradeThis = (Grade1 + Grade2 + Grade3) / 3.0;
                double averageGradeOther = (otherStudent.Grade1 + otherStudent.Grade2 + otherStudent.Grade3) / 3.0;
                return averageGradeThis.CompareTo(averageGradeOther);
            }
            else
            {
                throw new ArgumentException("Object is not a Student2");
            }
        }

        public object Clone()
        {
            return new Student2(this.LastName, this.FirstName, this.MiddleName, this.GroupNumber, this.Grade1, this.Grade2, this.Grade3);
        }
    }

}
