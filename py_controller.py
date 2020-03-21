import socket

# fe80::703c:1cf3:b8ab:4489

host_name = socket.gethostname() 
HOST = socket.gethostbyname(host_name)
PORT = 11111        # Port to listen on (non-privileged ports are > 1023)

print(HOST)

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.connect((HOST, PORT))
    while (True):
        s.sendall(b'Hello, world')