/**
 * 
 */
package com.ot.soa;

/**
 * Authentication Service
 */
public class AttestationServer extends JHttpService {
	
	// Service registry
	private final String DNSSD_CONFIG = "10.43.2.12:8761";
	private DnsSDRegistrator dnssd = DnsSDFactory.getInstance().createRegistrator(DNSSD_CONFIG);
	
	// Attestation service configurations
	private final String SERVICE_NAME = "Attestation-Service";
	private final String SERVICE_TYPE = "http";
	private final String SERVICE_HOST = "10.43.4.12";
	private final int SERVICE_PORT = 8080;
	
	@Override
	protected void onStartup() {
		super.onStartup();
		
		// Register or update service registry with hosting configurations
		try {
			ServiceData data = new ServiceData(SERVICE_NAME, SERVICE_TYPE, SERVICE_HOST, SERVICE_PORT);
			reg.registerService(data);
			
		} catch (DnsSDException e) {
			e.printStackTrace();
		}
	}
	
	@Mapping("/")
	public Object userAuthentication(Http http) {
		// Verify users
	}
}
