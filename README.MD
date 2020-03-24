# Python Controller for Unity

All credit for the original UR5 Unity controller and prefabs goes to Long Qian and Shuyang Chen.

The original repo can be found [here](https://github.com/qian256/ur5_unity).

This project uses a socket connection between the Unity environment and the python script to control the robotic arm. The existing python script in this repository is simply a demo. To run the demo, load the project into Unity and press play. Then, launch the Python script.

**NOTE:** As of 3/23/2020, the Unity environment will freeze on "play". This is due to the blocking connection listener in the UR5 C# controller. It will unfreeze once a connection is made.

**NOTE:** As of 3/23/2020, json messages sent over the socket can be superfluous in rate. Meaning, while the python script can be set to speeds greater than 60 Hz (the update rate of Unity), Unity will not be able to keep up with this and the received messages will stuff the message queue, losing some messages as only one json is used to update the online robot.

### Dependencies

#### Python
socket:
``` pip install socket ```
