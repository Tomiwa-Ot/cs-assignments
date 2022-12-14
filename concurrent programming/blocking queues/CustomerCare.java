import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.BlockingQueue;

public class CustomerCare {

	public static void main(String[] args) {
		BlockingQueue<Character> clientQueue = new ArrayBlockingQueue<Character>(5);
		new Thread(new Client(clientQueue)).start();
		for(int i = 0; i < 5; i++) {
			new Thread(new HelpDesk(clientQueue, i)).start();
		}
	}
	
	// Consumer Class
	public static class HelpDesk implements Runnable {
		
		private BlockingQueue<Character> clientQueue;
		private int helpdeskID;
		
		public HelpDesk(BlockingQueue<Character> clientQueue, int helpdeskID) {
			this.clientQueue = clientQueue;
			this.helpdeskID = helpdeskID;
		}
		
		@Override
		public void run() {
			while(!clientQueue.isEmpty()) {
				try {
					System.out.println("Agent " + Integer.toString(helpdeskID) + " is chatting with Client " + clientQueue.take());
					// Represents chat session
					Thread.sleep((int) (Math.random() * 5000));
				} catch (InterruptedException e) {
					e.printStackTrace();
				}
			}
		}
	}
	
	// Producer Class
	public static class Client implements Runnable {
		
		private BlockingQueue<Character> clientQueue;
		
		public Client(BlockingQueue<Character> clientQueue) {
			this.clientQueue = clientQueue;
		}
		
		@Override
		public void run() {
			for(char c = 'A'; c <= 'J'; c++) {
				try {
					clientQueue.put(c);
					System.out.println("New Client: " + c);
				} catch (InterruptedException e) {
					e.printStackTrace();
				}
			}
		}
	}

}
