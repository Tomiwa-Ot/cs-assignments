using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatriculationNumberSimulator.Faculties
{
    public class EnvironmentalSciences
    {
        public enum Department
        {
            Architecture,
            Building,
            Estate_Management,
            Quantity_Surveying,
            Urban_and_Regional_Planning
        }

        public static Dictionary<Department, int> studentCount = new Dictionary<Department, int>()
        {
            { Department.Architecture, 0 },
            { Department.Building, 0 },
            { Department.Estate_Management, 0 },
            { Department.Quantity_Surveying, 0 },
            { Department.Urban_and_Regional_Planning, 0 },
        };

        public static void updateDepartmentStudentCount(Department department)
        {
            int currentCount = 0;
            studentCount.TryGetValue(department, out currentCount);
            studentCount[department] = currentCount + 1;
        }
    }
}
