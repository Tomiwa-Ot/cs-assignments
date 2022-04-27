import java.util.concurrent.BrokenBarrierException;
import java.util.concurrent.CyclicBarrier;

public class Game {
	
	public static void main(String[] args) {
		CyclicBarrier barrier = new CyclicBarrier(4);
		for(char c = 'A'; c < 'E'; c++) {
			new Thread(new Multiplayer(barrier, c)).start();
		}
	}

	public static class Multiplayer implements Runnable {

		private CyclicBarrier barrier;
		private char id;

		public Multiplayer(CyclicBarrier barrier, char id) {
			this.barrier = barrier;
			this.id = id;
		}
		
		@Override
		public void run() {
			waitingRoom();
		}
		
		public void waitingRoom() {
			try {
				System.out.println("Player " + id + " arrived");
				if(barrier.await() == 0) {
					executeGame();
				}
			} catch (InterruptedException | BrokenBarrierException e) {
				e.printStackTrace();
			}
		}
		
		public void executeGame() {
			System.out.println("Game begins");
		}
	}
	
}
