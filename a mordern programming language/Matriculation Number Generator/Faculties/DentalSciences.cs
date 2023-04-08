using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatriculationNumberSimulator.Faculties
{
    public class DentalSciences
    {
        public enum Department
        {
            Child_Dental_Health,
            Oral_Pathology,
            Oral_and_Maxillofacial_Surgery,
            Preventive_Dentistry,
            Restorative_Dentistry
        }

        public static Dictionary<Department, int> studentCount = new Dictionary<Department, int>()
        {
            { Department.Child_Dental_Health, 0 },
            { Department.Oral_Pathology, 0 },
            { Department.Oral_and_Maxillofacial_Surgery, 0 },
            { Department.Preventive_Dentistry, 0 },
            { Department.Restorative_Dentistry, 0 },
        };

        public static void updateDepartmentStudentCount(Department department)
        {
            int currentCount = 0;
            studentCount.TryGetValue(department, out currentCount);
            studentCount[department] = currentCount + 1;
        }
    }
}
