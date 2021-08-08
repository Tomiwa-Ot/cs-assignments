
public class Exercise {

	public static void main(String[] args) {
		Course course = new Course("English");
		course.addStudent("Dennis");
		course.addStudent("Precious");
		course.addStudent("Nneka");
		course.dropStudent("Precious");
		String[] students = course.getStudents();
		for(int i = 0; i <= (course.getNumberOfStudents() - 1); i++) {
			System.out.println(students[i]);
		}
	}

}
