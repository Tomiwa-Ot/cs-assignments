using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatriculationNumberSimulator.Faculties
{
    public class BasicMedicalSciences
    {
        public enum Department
        {
            Anatomy,
            Anatomic_and_Molecular_Pathology,
            Biochemistry,
            Medical_Microbiology_and_Parasitology,
            Medical_Laboratory_Science,
            Physiology
        }

        public static Dictionary<Department, int> studentCount = new Dictionary<Department, int>()
        {
            { Department.Anatomy, 0 },
            { Department.Anatomic_and_Molecular_Pathology, 0 },
            { Department.Biochemistry, 0 },
            { Department.Medical_Microbiology_and_Parasitology, 0 },
            { Department.Physiology, 0 },
        };

        public static void updateDepartmentStudentCount(Department department)
        {
            int currentCount = 0;
            studentCount.TryGetValue(department, out currentCount);
            studentCount[department] = currentCount + 1;
        }
    }
}
