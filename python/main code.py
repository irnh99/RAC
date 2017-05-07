import http.client, urllib.parse
import json
import time

#create object classes
class Area(object):
    def __init__(self, IdArea, Status, *args, **kwargs):
        self.IdArea = IdArea
        self.Status = Status

#general
headers = {"Content-type": "application/json"}
#area connection
#areaParams = {'@idArea': 0}
#areaJsonParams = json.dumps(areaParams)
areaConn = http.client.HTTPConnection('localhost:50509')
print("start")

while(True):
    areaConn.request("GET", "/Areas/Details?idArea=0", {} ,headers)
    areaResponse = areaConn.getresponse()
    if(areaResponse.reason == "OK"):
            area = json.loads(areaResponse.read().decode())
            a = Area(**area)

            if (a.Status):
                print("abrete")
            else:
                print("cierrate")
    time.sleep(10)
    
