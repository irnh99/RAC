import http.client, urllib.parse
import json
from json import JSONEncoder
##import RPi.GPIO as GPIO
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
roomStatus = 10.5
idArea = "1"
areaDetails = None
#set raspberry
##GPIO.setmode(GPIO.BOARD)
##GPIO.setup(3, GPIO.OUT)
##p = GPIO.PWM(3,50)
##p.start(7.5)
#set connection
headers = {"Content-type": "application/json"}
conn = http.client.HTTPConnection('localhost:50509')


##functions
#check area status
def CheckAreaStatus():
    global areaDetails
    while(True):
        conn.request("GET", "/Areas/Details?idArea=" + idArea, {} ,headers)
        areaResponse = conn.getresponse()
        if(areaResponse.reason == "OK"):
            area = json.loads(areaResponse.read().decode())
            areaDetails = Area(**area)
            if (areaDetails.Status):
               print("abrete")
               roomStatus = 4.5
            else:
                print("cierrate")
                roomStatus = 10.5
##            p.ChangeDutyCycle(roomStatus)
        time.sleep(5)       
                
#open close method
def OpenClose(user):

    time.sleep(1)
    
    jsonArea = MyEncoder().encode(areaDetails)
    
    conn.request("POST", "/Areas/OpenClose", jsonArea ,headers)

    response = conn.getresponse()

    if(response.reason == "OK"):
        data = response.read()
        print(data)
    
##    accessDetails = Access(user, areaDetails)

#user ReadInput
def ReadInput():
    global areaDetails
    while(True):
        if(areaDetails is not None):
            noControl = input()
            conn.request("GET", "/Users/GetUserByNoControl?noControl=" + noControl, {} ,headers)
            userResponse = conn.getresponse()
            if(userResponse.reason == "OK"):
                user = json.loads(userResponse.read().decode())
                userDetails = User(**user)
                userDetails.UserType = UserType(**userDetails.UserType)
                
                hasAccessDetails = [HasAccess(**x) for x in areaDetails.HasAccess]
                
                if(userDetails.UserType.IdUserType in [x.IdUserType for x in hasAccessDetails]):
                    OpenClose(userDetails)  

##Check if the user requested local access

##trheads
#start checkroom thread
statusThread = threading.Thread(target=CheckAreaStatus)
statusThread.daemon = True
statusThread.start()
#local check thread
inputThread = threading.Thread(target=ReadInput)
inputThread.daemon = True
inputThread.start()

