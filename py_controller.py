import socket
import time
import keyboard

host_name = socket.gethostname() 
HOST = socket.gethostbyname(host_name)
PORT = 11111        # Port to listen on (non-privileged ports are > 1023)

print(HOST)

speed = 3

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.connect((HOST, PORT))
    counter = 0
    
    while (True):
        joint0 = counter
        joint1 = 0
        joint2 = counter
        joint3 = counter
        joint4 = 0
        joint5 = counter
        toSend = str(joint0) + ' ' + str(joint1) + ' ' + str(joint2) + ' ' + str(joint3) + ' ' + str(joint4) + ' ' + str(joint5)

        print(toSend)
        s.sendall(bytes(toSend,'utf-8'))
        counter = counter + .1
        time.sleep(1.0/(60 * speed))