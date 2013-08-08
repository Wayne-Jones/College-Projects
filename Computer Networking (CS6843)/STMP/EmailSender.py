from socket import *
import ssl

msg="\r\n I love computer networks!"
endmsg="\r\n.\r\n"
# choose a mail server (e.g. a Google server) and call it mailserver
mailserver = 'smtp.gmail.com'
port = 465
# create socket called clientSocket and establish a TCP connection with mailserver
clientSocket = socket(AF_INET, SOCK_STREAM)
clientSocket = ssl.wrap_socket(clientSocket)
clientSocket.connect((mailserver, port))
recv=clientSocket.recv(1024)
print recv
if recv[:3]!='220':
    print '220 reply not received from server.'

#Send HELO command and print server response.
heloCommand='HELO Alice\r\n'
clientSocket.send(heloCommand)
recv1=clientSocket.recv(1024)
print recv1
if recv1[:3]!='250':
    print '250 reply not received from server.'

# Auth Command
authCommand = 'AUTH LOGIN d2VqOTE2QGdtYWlsLmNvbQ==\r\n'
clientSocket.send(authCommand)
recvAuth = clientSocket.recv(1024)
print recvAuth
authCommand2 = 'YmFkYm95NGxpZmU=\r\n'
clientSocket.send(authCommand2)
recvAuth2 = clientSocket.recv(1024)
print recvAuth2
if recvAuth2[:3]!='235':
    print '235 reply not received from server.'
 
#Send MAIL FROM command and print server response.
fromCommand = 'MAIL FROM: <wej916@gmail.com>\r\n'
clientSocket.send(fromCommand)
recv2 = clientSocket.recv(1024)
print recv2
if recv2[:3]!='250':
    print '250 reply not received from server.'
 
#Send RCPT TO command and print server response.
rcptCommand = 'RCPT TO: <wej916@gmail.com>\r\n'
clientSocket.send(rcptCommand)
recv3 = clientSocket.recv(1024)
print recv3
if recv3[:3]!='250':
    print '250 reply not received from server.'
 
#Send DATA command and print server response.
dataCommand = 'DATA\r\n'
clientSocket.send(dataCommand)
recv4 = clientSocket.recv(1024)
print recv4
if recv4[:3]!='354':
    print '354 reply not received from server.'
 
#send message data.
clientSocket.send(msg)
 
#message ends with a single period.
clientSocket.send(endmsg)
 
#send QUIT command and get server response.
quitCommand = 'QUIT\r\n'
clientSocket.send(quitCommand)
recv5 = clientSocket.recv(1024)
print recv5
