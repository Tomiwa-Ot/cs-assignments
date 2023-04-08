using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatriculationNumberSimulator.Faculties
{
    public class Pharmacy
    {
        public enum Department
        {
            Pharmacognosy,
            Pharmaceutics_and_Pharmaceutical_Technology,
            Pharmaceutical_Chemistry,
            Clinical_Pharmacy_and_Biopharmacy
        }

        public static Dictionary<Department, int> studentCount = new Dictionary<Department, int>()
        {
            { Department.Pharmacognosy, 0 },
            { Department.Pharmaceutics_and_Pharmaceutical_Technology, 0 },
            { Department.Pharmaceutical_Chemistry, 0 },
            { Department.Clinical_Pharmacy_and_Biopharmacy, 0 },
        };

        public static void updateDepartmentStudentCount(Department department)
        {
            int currentCount = 0;
            studentCount.TryGetValue(department, out currentCount);
            studentCount[department] = currentCount + 1;
        }
    }
}
