using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatriculationNumberSimulator.Faculties
{
    public class Engineering
    {
        public enum Department
        {
            Biomedical_Engineering,
            Chemical_Engineering,
            Civil_and_Environmental_Engineering,
            Electrical_and_Electronics,
            Mechanical_Engineering,
            Metallurgical_and_Materials,
            Surveying_and_Geoinformatics,
            Systems_Engineering
        }

        public static Dictionary<Department, int> studentCount = new Dictionary<Department, int>()
        {
            { Department.Biomedical_Engineering, 0 },
            { Department.Chemical_Engineering, 0 },
            { Department.Civil_and_Environmental_Engineering, 0 },
            { Department.Electrical_and_Electronics, 0 },
            { Department.Mechanical_Engineering, 0 },
            { Department.Metallurgical_and_Materials, 0 },
            { Department.Surveying_and_Geoinformatics, 0 },
            { Department.Systems_Engineering, 0 },
        };

        public static void updateDepartmentStudentCount(Department department)
        {
            int currentCount = 0;
            studentCount.TryGetValue(department, out currentCount);
            studentCount[department] = currentCount + 1;
        }
    }
}
