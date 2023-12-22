/**
 * 
 */
package com.ot.cbse;

import java.io.File;

/**
 * Avast Antimalware
 */
public class Avast implements AntimalwareProvider {
	
	private final String ENGINE_NAME = "Avast engine";
	private final String VERSION = "v33.1.76";
	
	private AvastEngine avastEngine = new AvastEngine();
	
	@Override
	public boolean initialize() {
		return avastEngine.start();
	}

	@Override
	public ScanResult scanString(String stream) {
		return avastEngine.scan(stream);
	}

	@Override
	public ScanResult scanFile(File file) {
		return avastEngine.scan(file);
	}

	@Override
	public ScanResult scanDirectory(File directory) {
		return avastEngine.scan(directory);
	}

	@Override
	public void sendNotification(String title, String body, File iconPath) {
		// TODO Auto-generated method stub
	}

	@Override
	public String engineName() {
		return ENGINE_NAME;
	}

	@Override
	public String version() {
		return VERSION;
	}

	@Override
	public boolean uninitialize() {
		return avastEngine.stop();
	}

}
