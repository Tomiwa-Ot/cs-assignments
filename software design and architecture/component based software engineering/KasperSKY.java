/**
 * 
 */
package com.ot.cbse;

import java.io.File;

/**
 * KasperSKY Antimalware
 */
public class KasperSKY implements AntimalwareProvider {

	private final String ENGINE_NAME = "KasperSKY P45-MKii";
	private final String VERSION = "v1.3.2";
	
	private KasperSKYEngine kasperskyEngine = new KasperSKYEngine();
	
	@Override
	public boolean initialize() {
		return kasperskyEngine.start();
	}

	@Override
	public ScanResult scanString(String stream) {
		return kasperskyEngine.scan(stream);
	}

	@Override
	public ScanResult scanFile(File file) {
		return kasperskyEngine.scan(file);
	}

	@Override
	public ScanResult scanDirectory(File directory) {
		return kasperskyEngine.scan(directory);
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
		return kasperskyEngine.stop();
	}

}
