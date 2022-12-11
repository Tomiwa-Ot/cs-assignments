import java.util.ArrayList;
import java.util.Collections;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class VotingSystem {

	public static int candidateA = 0, candidateB = 0, candidateC = 0;

	public static void main(String[] args) {
		CountDownLatch voters = new CountDownLatch(50);
		ExecutorService service = Executors.newCachedThreadPool();
		for(int i = 0; i < 50; i++) {
			service.execute(new Runnable() {
				@Override
				public void run() {
					vote(voters);
				}
			});
		}
		
		service.shutdown();
		try {
			voters.await();
			calculateResults();
		} catch (InterruptedException e) {
			e.printStackTrace();
		}
	}
	
	public static synchronized void vote(CountDownLatch voters) {
		int choice = (int) (Math.random() * 3);
		switch (choice) {
			case 0:
				candidateA++;
				break;
			case 1:
				candidateB++;
				break;
			case 2:
				candidateC++;
				break;
			default:
				break;
		}
		voters.countDown();
	}
	
	public static void calculateResults() {
		ArrayList<Integer> results = new ArrayList<Integer>();
		Collections.addAll(results, candidateA, candidateB, candidateC);
		System.out.println("A: " + results.get(0) + ", B: " + results.get(1) + ", C: " + results.get(2));
		int highestVotes = Collections.max(results);
		switch (results.indexOf(highestVotes)) {
			case 0:
				System.out.println("Candidate A won");
				break;
			case 1:
				System.out.println("Candidate B won");
				break;
			case 2:
				System.out.println("Candidate C won");
				break;
			default:
				break;
		}
	}
}
