import requests
from dnssd import service_discovery
from mfrc522 import SimpleMFRC522

# Door ID
def get_hostname():
    try:
        hostname = socket.gethostname()
        return hostname
    except Exception as e:
        print("An error occurred while getting hostname:", e)

rfid = SimpleMFRC522()
# Get access control service url from registry
url = service_discovery(‘access-control’)

# Listen for data from RFID scanner
while True:
    try:
        id, text = rfid.read()
        payload = {'door': get_hostname(), 'user': text}
        response = requests.post(url, json=payload)
        if response.status_code == 200:
            # Open door
    except:
        continue
