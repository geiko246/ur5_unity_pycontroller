// Author: Long Qian
// Email: lqian8@jhu.edu

// Fork Author: Tyler Han
// Email: geiko246@gmail.com

using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text; 

public class UR5Controller : MonoBehaviour {

    public GameObject RobotBase;
    public float[] jointValues = new float[6];
    private GameObject[] jointList = new GameObject[6];
    private float[] upperLimit = { 180f, 180f, 180f, 180f, 180f, 180f };
    private float[] lowerLimit = { -180f, -180f, -180f, -180f, -180f, -180f };

    private Socket clientSocket;
    private byte[] inBuffer = new byte[1024];

    // Use this for initialization
    void Start () {
        initializeJoints();
	    InitSocket();
	}

    void Update() {

        try {
            string data = null;

            int numByte = clientSocket.Receive(inBuffer);

            data += Encoding.ASCII.GetString(inBuffer, 0, numByte);
            Debug.Log("Text received -> " + data); 
        }
        catch (Exception e) { 
            Debug.Log(e.ToString()); 
        } 


    }
	
	// Update is called once per frame
	void LateUpdate () {
        for ( int i = 0; i < 6; i ++) {
            Vector3 currentRotation = jointList[i].transform.localEulerAngles;
            //Debug.Log(currentRotation);
            currentRotation.z = jointValues[i];
            jointList[i].transform.localEulerAngles = currentRotation;
        }
	}

    void OnGUI() {
        int boundary = 20;

#if UNITY_EDITOR
        int labelHeight = 20;
        GUI.skin.label.fontSize = GUI.skin.box.fontSize = GUI.skin.button.fontSize = 20;
#else
        int labelHeight = 40;
        GUI.skin.label.fontSize = GUI.skin.box.fontSize = GUI.skin.button.fontSize = 40;
#endif
        GUI.skin.label.alignment = TextAnchor.MiddleLeft;
        for (int i = 0; i < 6; i++) {
            GUI.Label(new Rect(boundary, boundary + ( i * 2 + 1 ) * labelHeight, labelHeight * 4, labelHeight), "Joint " + i + ": ");
            jointValues[i] = GUI.HorizontalSlider(new Rect(boundary + labelHeight * 4, boundary + (i * 2 + 1) * labelHeight + labelHeight / 4, labelHeight * 5, labelHeight), jointValues[i], lowerLimit[i], upperLimit[i]);
        }
    }


    // Create the list of GameObjects that represent each joint of the robot
    void initializeJoints() {
        var RobotChildren = RobotBase.GetComponentsInChildren<Transform>();
        for (int i = 0; i < RobotChildren.Length; i++) {
            if (RobotChildren[i].name == "control0") {
                jointList[0] = RobotChildren[i].gameObject;
            }
            else if (RobotChildren[i].name == "control1") {
                jointList[1] = RobotChildren[i].gameObject;
            }
            else if (RobotChildren[i].name == "control2") {
                jointList[2] = RobotChildren[i].gameObject;
            }
            else if (RobotChildren[i].name == "control3") {
                jointList[3] = RobotChildren[i].gameObject;
            }
            else if (RobotChildren[i].name == "control4") {
                jointList[4] = RobotChildren[i].gameObject;
            }
            else if (RobotChildren[i].name == "control5") {
                jointList[5] = RobotChildren[i].gameObject;
            }
        }
    }

    void InitSocket()
    {
        // initialize server
        IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
        //IPAddress ipAddr = ipHost.AddressList[0];
        IPAddress ipAddr = IPAddress.Parse("172.16.70.177");
        int socket = 11111;
        IPEndPoint localEndPoint = new IPEndPoint(ipAddr, socket);

        Debug.Log(ipAddr.ToString());

        Socket listener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        try {
            listener.Bind(localEndPoint);
            listener.Listen(10);

            clientSocket = listener.Accept();
        }
        catch (Exception e) { 
            Console.WriteLine(e.ToString()); 
        }
    }

}
