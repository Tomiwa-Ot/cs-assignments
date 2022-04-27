import java.util.ArrayList;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.Semaphore;

public class Server {
	public static String ip;

	public static void main(String[] args) {
		ExecutorService connections = Executors.newFixedThreadPool(5);
		for (int i = 0; i < 5; i++) {
			ip = getIP();
			connections.execute(Logger.getInstance());
		}
		
		connections.shutdown();
	}
	
	public static String getIP() {
		int digits[]= new int[4];
		for(int i = 0; i < 4; i++) {
			digits[i] = (int) (Math.random() * 255);
		}
		return Integer.toString(digits[0]) + "." + Integer.toString(digits[1]) + "." + 
				Integer.toString(digits[2]) + "." + Integer.toString(digits[3]);
	}
}

class Logger implements Runnable{
	
	Semaphore sempahore = new Semaphore(1);
	ArrayList<String> logs = new ArrayList<String>();
	
	private static final Logger manager = new Logger();
	
	@Override
	public void run() {
		try {
			sempahore.acquire();
			writeLog(Server.ip);
		} catch (InterruptedException e) {
			e.printStackTrace();
		} finally {
			sempahore.release();
		}
	}
	
	public void writeLog(String ip) {
		logs.add(ip);
		System.out.println(logs.toString());
	}
	
	public static Logger getInstance() {
		return manager;
	}
}
