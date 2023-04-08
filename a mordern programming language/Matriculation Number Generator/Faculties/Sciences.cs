using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatriculationNumberSimulator.Faculties
{
    public class Sciences
    {
        public enum Department
        {
            Biochemistry,
            Botany,
            Cell_Biology_and_Genetics,
            Chemistry,
            Computer_Sciences,
            GeoSciences,
            Mathematics,
            Marine_Science,
            Microbiology,
            Physics,
            Zoology
        }

        public static Dictionary<Department, int> studentCount = new Dictionary<Department, int>()
        {
            { Department.Biochemistry, 0 },
            { Department.Botany, 0 },
            { Department.Cell_Biology_and_Genetics, 0 },
            { Department.Chemistry, 0 },
            { Department.Computer_Sciences, 0 },
            { Department.GeoSciences, 0 },
            { Department.Mathematics, 0 },
            { Department.Marine_Science, 0 },
            { Department.Microbiology, 0 },
            { Department.Physics, 0 },
            { Department.Zoology, 0 },
        };

        public static void updateDepartmentStudentCount(Department department)
        {
            int currentCount = 0;
            studentCount.TryGetValue(department, out currentCount);
            studentCount[department] = currentCount + 1;
        }
    }
}
