/**
 * 
 */
package com.ot.soa;

/**
 * Accounting Application
 */
public class AccountingApplication extends JHttpService {

	// Service registry
	private final String DNSSD_CONFIG = "10.43.2.12:8761";
	private DnsSDRegistrator dnssd = DnsSDFactory.getInstance().createRegistrator(DNSSD_CONFIG);
	private DnsSDBrowser browser = DnsFactory.getInstance().createBrowser(DNSSD_CONFIG);
	
	// Accounting application configuration
	private final String SERVICE_NAME = "AccountingApplication";
	private final String SERVICE_TYPE = "http";
	private final String SERVICE_HOST = "10.43.4.23";
	private final int SERVICE_PORT = 8081;
	
	private final String AUTHENTICATION_TARGET = "Attestation-Service";
	
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
	
	/**
	 * Verify user identity via attestation service
	 * @param user Identity to verify
	 * @return true on success; false otherwise
	 */
	private boolean authentication(User user) {
		if (!browser.findService(AUTHENTICATION_TARGET))
			return false;
		
		ServiceData attestaionService = browser.getServiceData(AUTHENTICATION_TARGET);
		HttpRequest req = new HttpRequest(attestaionService.buildDomain());
		req.setMethod("POST");
		req.body("user", JsonEncode(user));
		Json json = req.send();
	
		return Boolean.parseBoolean(json.getObject("success"));
	}
	
	@Mapping("/sign-in")
	public Object login(Http http) {
		if (http.getMethod() == "GET")
			return new Html("/login.html");
		else if (http.getMethod() == "POST" && authentication(new User(http.getBody()))) {
			// Sign in successful
		} else {
			return new Html("/login.html");
		}
	}
	
	// Other functionalities
	@Mapping("...")
	public Object functionality(Http http) {}
}
