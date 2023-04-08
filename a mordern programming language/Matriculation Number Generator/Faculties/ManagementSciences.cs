using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatriculationNumberSimulator.Faculties
{
    public class ManagementSciences
    {
        public enum Department
        {
            Accounting,
            Actuarial_Science_and_Insurance,
            Banking_and_Finance,
            Business_Administration,
            Industrial_Relations_and_Personnel_Management
        }

        public static Dictionary<Department, int> studentCount = new Dictionary<Department, int>()
        {
            { Department.Accounting, 0 },
            { Department.Actuarial_Science_and_Insurance, 0 },
            { Department.Banking_and_Finance, 0 },
            { Department.Business_Administration, 0 },
            { Department.Industrial_Relations_and_Personnel_Management, 0 },
        };

        public static void updateDepartmentStudentCount(Department department)
        {
            int currentCount = 0;
            studentCount.TryGetValue(department, out currentCount);
            studentCount[department] = currentCount + 1;
        }
    }
}
