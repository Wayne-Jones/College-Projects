import socket

import os
import sys
import struct
import time
import select
import binascii  
 
ICMP_ECHO_REQUEST = 8
 
def checksum(str):

    csum = 0
    countTo = (len(str) / 2) * 2

    count = 0
    while count < countTo:
        thisVal = ord(str[count+1]) * 256 + ord(str[count])

        csum = csum + thisVal

        csum = csum & 0xffffffff #

        count = count + 2
 
    if countTo < len(str):
        csum = csum + ord(str[len(str) - 1])
        csum = csum & 0xffffffff

    csum = (csum >> 16) + (csum & 0xffff)
    csum = csum + (csum >> 16)
    answer = ~csum

    answer = answer & 0xffff


    answer = answer >> 8 | (answer << 8 & 0xff00)

    return answer
 
 
def receiveOnePing(mySocket, ID, timeout, destAddr):
    timeLeft = timeout

    while 1:

        startedSelect = time.time()
        whatReady = select.select([mySocket], [], [], timeLeft)
        howLongInSelect = (time.time() - startedSelect)
        if whatReady[0] == []: # Timeout
            return "Request timed out."

        timeReceived = time.time()

        recPacket, addr = mySocket.recvfrom(1024)
        imcpHeader = recPacket[20:28]
        type, code, checksum, packetID, sequence = struct.unpack( "bbHHh", imcpHeader)

        if packetID == ID:
            bytesInDouble = struct.calcsize("d")
            timeSent = struct.unpack("d", recPacket[28:28 + bytesInDouble])[0]
            return timeReceived - timeSent

        timeLeft = timeLeft - howLongInSelect
        if timeLeft <= 0:
            return "Request timed out."

 
def sendOnePing(mySocket, destAddr, ID):
    # Header is type (8), code (8), checksum (16), id (16), sequence (16)
    dest_addr = socket.gethostbyname(dest_addr)
    myChecksum = 0
    # Make a dummy header with a 0 checksum.
    # struct -- Interpret strings as packed binary data
    header = struct.pack("bbHHh", ICMP_ECHO_REQUEST, 0, myChecksum, ID, 1)
    bytesInDouble = struct.calcsize("d")
    data = (192 - bytesInDouble) * "Q"
    data = struct.pack("d", time.time())

    # Calculate the checksum on the data and the dummy header.
    myChecksum = checksum(header + data)
 
    # Get the right checksum, and put in the header
    if sys.platform == 'darwin':
        myChecksum = socket.htons(myChecksum) & 0xffff
    #Convert 16-bit integers from host to network  byte order.
    else:
        myChecksum = socket.htons(myChecksum)


    header = struct.pack("bbHHh", ICMP_ECHO_REQUEST, 0, myChecksum, ID, 1)

    packet = header + data

    mySocket.sendto(packet, (dest_addr, 1)) # AF_INET address must be tuple, not str
    #Both LISTS and TUPLES consist of a number of objects
    #which can be referenced by their position number within the object.
 
def doOnePing(dest_addr, timeout):
 
    icmp = socket.getprotobyname("icmp")
    #SOCK_RAW is a powerful socket type. For more details:   http://sock-raw.org/papers/sock_raw

    mySocket = socket.socket(socket.AF_INET, socket.SOCK_RAW, icmp)

    myID = os.getpid() & 0xFFFF  #Return the current process i
    sendOnePing(mySocket, dest_addr, myID)
    delay = receiveOnePing(mySocket, myID, timeout, dest_addr)

    mySocket.close()

    return delay
 
 
def ping(host, timeout=1):
    #timeout=1 means: If one second goes by without a reply from the server,
    #the client assumes that either the client's ping or the server's pong is lost
    dest = socket.gethostbyname(host)
    print "Pinging " + dest + " using Python:"
    print ""
    #Send ping requests to a server separated by approximately one second
    while 1 :
        delay = doOnePing(dest, timeout)
        print delay
        time.sleep(1)# one second
    return delay

if __name__ == '__main__':
    ping("www.google.com")
    ping("au.yahoo.com")
    ping("bbc.co.uk")
    ping("gov.za")
