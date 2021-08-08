
public class Course {
	private String courseName;
	private String[] students = new String[10];
	private int numberOfStudents = 0;
	
	public Course(String courseName){
		this.courseName = courseName;
	}
	
	public void addStudent(String student) {
		if(numberOfStudents >= students.length) {
			String[] s = new String[numberOfStudents * 2];
			for(int i = 0; i <= (students.length - 1); i++) {
				s[i] = students[i];
			}
			students = s;
		}
		students[numberOfStudents] = student;
		numberOfStudents++;
	}
	
	public String getCourseName() {
		return courseName;
	}
	
	public String[] getStudents() {
		return students;
	}
	
	public int getNumberOfStudents() {
		return numberOfStudents;
	}
	
	public void clear() {
		numberOfStudents = 0;
		students = new String[10];
	}
	
	public void dropStudent(String student) {
		boolean doesStudentExist = false;
		for(int i = 0; i < (students.length - 1); i++) {
			if(student.equalsIgnoreCase(students[i])) {
				students[i] = null;
				numberOfStudents--;
				doesStudentExist = true;
				while(i < numberOfStudents) {
					students[i] = students[i + 1];
					i++;
				}
				break;
			}
		}
		if(!doesStudentExist) {
			System.out.println("Student does not exist");
		}
	}
}
