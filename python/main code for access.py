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
headers = {"Content-type": "application/json"}
conn = http.client.HTTPConnection('ec2-54-202-173-41.us-west-2.compute.amazonaws.com:80')


##functions
#check area status
def CheckAreaStatus():
    global areaDetails
    conn.request("GET", "/Areas/Details?idArea=" + idArea, {} ,headers)
    areaResponse = conn.getresponse()
    if(areaResponse.reason == "OK"):
        area = json.loads(areaResponse.read().decode())
        areaDetails = Area(**area)
                
#open close method
def OpenClose(user):
    
    jsonArea = MyEncoder().encode(areaDetails)
    
    conn.request("POST", "/Areas/OpenClose", jsonArea ,headers)

    response = conn.getresponse()

    if(response.reason == "OK"):
        data = json.loads(response.read().decode())
        if ( areaDetails.Status == False and "Success" in data):
            accessDetails = Access(user, areaDetails)
            Register(accessDetails)

    print(response.reason, "openclose")
    
def Register(access):
    jsonAccess = MyEncoder().encode(access)
    
    conn.request("POST", "/Accesses/Create", jsonAccess ,headers)

    response = conn.getresponse()
    response.read()
    
    print(response.reason, "access")


#user ReadInput
def ReadInput():
    global areaDetails
    while(True):
        noControl = input()
        conn.request("GET", "/Users/GetUserByNoControl?noControl=" + noControl, {} ,headers)
        userResponse = conn.getresponse()
        if(userResponse.reason == "OK"):
            user = json.loads(userResponse.read().decode())
            userDetails = User(**user)
            userDetails.UserType = UserType(**userDetails.UserType)

            CheckAreaStatus()
            
            hasAccessDetails = [HasAccess(**x) for x in areaDetails.HasAccess]
            
            if(userDetails.UserType.IdUserType in [x.IdUserType for x in hasAccessDetails]):
                OpenClose(userDetails)  

ReadInput()


