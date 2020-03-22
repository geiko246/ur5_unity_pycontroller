import socket
import time
import json
import keyboard

# Container for offline model of arm
class RobotArm:
    
    def __init__(self):
        self.joint_angles = [0,0,0,0,0,0]
        self.speed = 1

    # joint_num - index of joint to be moved
    # angle - angle to move towards
    #
    # TODO accomodate for wrapping angles
    def move_joint_to_angle(self, joint_num, angle, cc=False):

        direction = 1
        if cc:
            direction = -1

        curr_angle = arm.joint_angles[joint_num]
        # for now this will always move clockwise
        
        while (int(curr_angle) != int(angle)):
            curr_angle = (curr_angle + .05*direction) % 360
            self.joint_angles[joint_num] = curr_angle
            print(bytes(json.dumps(self.__dict__),'utf-8'))
            s.sendall(bytes(json.dumps(self.__dict__),'utf-8'))
            time.sleep(1.0/(60 * arm.speed))

if __name__ == "__main__":

    # Begin script
    host_name = socket.gethostname() 
    HOST = socket.gethostbyname(host_name)
    PORT = 11111        # Port to listen on (non-privileged ports are > 1023)

    print(HOST)

    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
        s.connect((HOST, PORT))
        counter = 0
    
        arm = RobotArm()
        arm.speed = 3

        arm.move_joint_to_angle(1, 20)
        arm.move_joint_to_angle(2, 110)
        arm.move_joint_to_angle(3, 30)
        
        for i in range(10):
            arm.move_joint_to_angle(0, 10)
            time.sleep(1)
            arm.move_joint_to_angle(0, 350, True)
            time.sleep(1)
