import http.client, urllib.parse
import json
##import RPi.GPIO as GPIO
import time

#set raspberry
##GPIO.setmode(GPIO.BOARD)
##GPIO.setup(3, GPIO.OUT)
##p = GPIO.PWM(3,50)
##p.start(7.5)
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
areaConn = http.client.HTTPConnection('ec2-54-191-225-241.us-west-2.compute.amazonaws.com')
print("start")

while(True):
    areaConn.request("GET", "/Areas/Details?idArea=0", {} ,headers)
    areaResponse = areaConn.getresponse()
    if(areaResponse.reason == "OK"):
        area = json.loads(areaResponse.read().decode())
        a = Area(**area)

        if (a.Status):
           print("abrete")
##           p.ChangeDutyCycle(4.5)
        else:
            print("cierrate")
##           p.ChangeDutyCycle(10.5)
    print(areaResponse.Header)
    time.sleep(10)
    
