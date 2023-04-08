using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatriculationNumberSimulator
{
    public class Student
    {
        public string matricNumber { get; }
        public string fullname { get;  }
        public Faculty faculty { get; }
        public string department { get; }

        public Student(string matricNumber, string fullname, Faculty faculty, string department)
        {
            this.matricNumber = matricNumber;
            this.fullname = fullname;
            this.faculty = faculty;
            this.department = department;
        }
    }
}
