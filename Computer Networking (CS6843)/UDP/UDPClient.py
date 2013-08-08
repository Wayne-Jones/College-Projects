# -*- coding: utf-8 -*-
#We will need the following module to generate random lost packets import random
from socket import *
import time
from datetime import datetime

#create a UDP socket
serverName = '127.0.0.1'
serverPort = 11000
clientSocket = socket(AF_INET, SOCK_DGRAM) #assign IP address and port number to socket


pingsLeft=10
i=0
msg="Ping"
while i<pingsLeft:
    i+=1
    start = time.time()
    clientSocket.sendto(msg,(serverName,serverPort))
    clientSocket.settimeout(0.01)
    try:
        modifiedMessage, serverAddress=clientSocket.recvfrom(1024)
        elapsedTime = time.time() - start
        print modifiedMessage
        print "Round Trip Time: ", elapsedTime
    except:
        print "Lost Packet"
if i==10:
    clientSocket.close()
