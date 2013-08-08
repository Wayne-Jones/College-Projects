#We will need the following module to generate random lost packets import random
from socket import *
import random
 
#create a UDP socket
serverSocket = socket(AF_INET, SOCK_DGRAM) #assign IP address and port number to socket
serverSocket.bind(('127.0.0.1', 11000))
 
while True:
    #generate random number in the range of 10
    rand = random.randint(0, 10)
    message, address = serverSocket.recvfrom(1024)
    message = message.upper()
    #if "rand" is less is than 4, we consider the packet lost and do not respond
    if rand < 4:
        continue
    #otherwise, the server responds
    serverSocket.sendto(message, address)
