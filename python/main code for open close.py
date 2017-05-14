import http.client, urllib.parse
import json
from json import JSONEncoder
import RPi.GPIO as GPIO
import time, threading


##create object classes
class Access(object):
    def __init__(self, User, Area, *args, **kwargs):
        self.User = User
        self.Area = Area

class Area(object):
    def __init__(self, IdArea, Name, Status, HasAccess, *args, **kwargs):
        self.IdArea = IdArea
        self.Name =  Name
        self.Status = Status
        self.HasAccess = HasAccess

class HasAccess(object):
    def __init__(self, IdHasAccess, IdUserType, IdArea, *args, **kwargs):
        self.IdHasAccess = IdHasAccess
        self.IdUserType = IdUserType
        self.IdArea = IdArea

class UserType(object):
    def __init__(self, IdUserType, Description, Users, *args, **kwargs):
        self.IdUserType = IdUserType
        self.Description = Description
        self.Users = Users

class User(object):
    def __init__(self, IdUser, Name, Pass, UserName, NoControl, UserType, *args, **kwargs):
        self.IdUser = IdUser
        self.Name = Name
        self.Pass = Pass
        self.UserName = UserName
        self.NoControl = NoControl
        self.UserType = UserType
        
class MyEncoder(JSONEncoder):
    def default(self, o):
        return o.__dict__   

##general vars
GPIO.setwarnings(False) 
roomPreStatus = 0
idArea = "1"
areaDetails = None
##set raspberry
GPIO.setmode(GPIO.BOARD)
GPIO.setup(3, GPIO.OUT)
p = GPIO.PWM(3,50)
p.start(7.5)
##set connection
headers = {"Content-type": "application/json"}
conn = http.client.HTTPConnection('ec2-54-202-173-41.us-west-2.compute.amazonaws.com:80')


##functions
#check area status
def CheckAreaStatus():
    global areaDetails
    global roomPreStatus
    while(True):
        conn.request("GET", "/Areas/Details?idArea=" + idArea, {} ,headers)
        areaResponse = conn.getresponse()
        if(areaResponse.reason == "OK"):
            area = json.loads(areaResponse.read().decode())
            areaDetails = Area(**area)
            if (areaDetails.Status):
               print("abrete")
               roomStatus = 3.1
            else:
                print("cierrate")
                roomStatus = 7
            if (roomPreStatus != roomStatus):
                roomPreStatus = roomStatus
                p.ChangeDutyCycle(roomStatus)
        time.sleep(5)

##trheads
#start checkroom thread
statusThread = threading.Thread(target=CheckAreaStatus)
statusThread.daemon = True
statusThread.start()

