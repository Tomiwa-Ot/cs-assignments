using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatriculationNumberSimulator.Faculties
{
    public class Arts
    {
        public enum Department
        {
            Creative_Arts,
            English,
            European_Languages_and_Integration_Studies,
            History_and_Strategic_Studies,
            Linguistics_African_Asian_Studies,
            Philosphy
        }

        public static Dictionary<Department, int> studentCount = new Dictionary<Department, int>()
        {
            { Department.Creative_Arts, 0 },
            { Department.English, 0 },
            { Department.European_Languages_and_Integration_Studies, 0 },
            { Department.History_and_Strategic_Studies, 0 },
            { Department.Linguistics_African_Asian_Studies, 0 },
            { Department.Philosphy, 0 },
        };

        public static void updateDepartmentStudentCount(Department department)
        {
            int currentCount = 0;
            studentCount.TryGetValue(department, out currentCount);
            studentCount[department] = currentCount + 1;
        }
    }
}
