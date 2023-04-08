using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatriculationNumberSimulator.Faculties
{
    public class ClinicalServices
    {
        public enum Department
        {
            Anaesthesia,
            Clinical_Pathology,
            Community_Health_and_Primary_Care,
            Haematology_and_Blood_Transfusion,
            Medicine,
            Nursing,
            Obstetrics_and_Gynaecology,
            Ophthalmology,
            Paediatrics,
            Psychiatry,
            Physiotherapy,
            Radiology,
            Surgery
        }

        public static Dictionary<Department, int> studentCount = new Dictionary<Department, int>()
        {
            { Department.Anaesthesia, 0 },
            { Department.Clinical_Pathology, 0 },
            { Department.Community_Health_and_Primary_Care, 0 },
            { Department.Haematology_and_Blood_Transfusion, 0 },
            { Department.Medicine, 0 },
            { Department.Nursing, 0 },
            { Department.Obstetrics_and_Gynaecology, 0 },
            { Department.Ophthalmology, 0 },
            { Department.Paediatrics, 0 },
            { Department.Psychiatry, 0 },
            { Department.Physiotherapy, 0 },
            { Department.Radiology, 0 },
            { Department.Surgery, 0 },
        };

        public static void updateDepartmentStudentCount(Department department)
        {
            int currentCount = 0;
            studentCount.TryGetValue(department, out currentCount);
            studentCount[department] = currentCount + 1;
        }
    }
}
