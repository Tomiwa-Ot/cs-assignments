using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatriculationNumberSimulator.Faculties
{
    public class SocialSciences
    {
        public enum Department
        {
            Economics,
            Geogrpahy,
            Mass_Communication,
            Psychology,
            Political_Science,
            Sociology
        }

        public static Dictionary<Department, int> studentCount = new Dictionary<Department, int>()
        {
            { Department.Economics, 0 },
            { Department.Geogrpahy, 0 },
            { Department.Mass_Communication, 0 },
            { Department.Psychology, 0 },
            { Department.Political_Science, 0 },
            { Department.Sociology, 0 }
        };

        public static void updateDepartmentStudentCount(Department department)
        {
            int currentCount = 0;
            studentCount.TryGetValue(department, out currentCount);
            studentCount[department] = currentCount + 1;
        }
    }
}
