import java.util.ArrayList;
import java.util.Arrays;
import java.util.concurrent.Semaphore;

public class DiningPhilosophersProblem {

	public static int[] eatingPhilosophers = {0, 0, 0, 0, 0};
	
	public static void main(String[] args) {
		
		ArrayList<Semaphore> forks = new ArrayList<Semaphore>(
				Arrays.asList(
						new Semaphore(1),
						new Semaphore(1),
						new Semaphore(1),
						new Semaphore(1),
						new Semaphore(1)
				)
		);
		
		for(int i = 0; i < 5; i++) {
			new Thread(new Philosophers(i, forks.get(i % 5), forks.get((i + 1) % 5))).start();
		}

	}
	
	private static class Philosophers implements Runnable {
		
		private int id;
		private Semaphore leftFork;
		private Semaphore rightFork;
		
		public Philosophers(int id, Semaphore leftFork, Semaphore rightFork) {
			this.id = id;
			this.leftFork = leftFork;
			this.rightFork = rightFork;
		}

		@Override
		public void run() {
			try {
				while(true) {
					if(this.leftFork.availablePermits() == 1 && this.rightFork.availablePermits() == 1) {
						eat();
						releaseFork();
					}
					think();
				}
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
		
		public void eat() throws InterruptedException {
			this.leftFork.acquire();
			this.rightFork.acquire();
			System.out.println(String.format("Philosopher %d started eating", this.id + 1));
			DiningPhilosophersProblem.eatingPhilosophers[this.id % 5] = this.id + 1;
			DiningPhilosophersProblem.eatingPhilosophers[(this.id + 1) % 5] = this.id + 1;
			System.out.println(Arrays.toString(DiningPhilosophersProblem.eatingPhilosophers));
			Thread.sleep((long) (Math.random() * 10000));
		}
		
		public void releaseFork() {
			this.leftFork.release();
			this.rightFork.release();
			DiningPhilosophersProblem.eatingPhilosophers[this.id % 5] = 0;
			DiningPhilosophersProblem.eatingPhilosophers[(this.id + 1) % 5] = 0;
		}
		
		public void think() throws InterruptedException {
			System.out.println(String.format("Philosopher %d started thinking", this.id + 1));
			Thread.sleep((long) (Math.random() * 10000));
			System.out.println(String.format("Philosopher %d is hungry", this.id + 1));
		}
		
	}

}
