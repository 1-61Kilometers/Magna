using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Events;
using Abb.Egm;
using Google.Protobuf;
using System.Threading;
using TMPro;
//include libraries necessary for Hololens UDP connection
//these libaries are utilized through the Universal Windows Platform
//and will not work within the Unity Editor

#if !UNITY_EDITOR
using Windows.Networking.Sockets;
using Windows.Networking.Connectivity;
using Windows.Networking;
#endif

namespace communication
{
    [System.Serializable]

    public class UDPCommunication : MonoBehaviour
    {
        public TextMeshPro text;
        // Initialize MemoryStream 
        //port number corresponds to hollolens UDP port and Robot Controller UDP port
        //externalIP corresponds to IP address of PC running robot controller
        MemoryStream memoryStream;
        private string port;
        private string externalIP;

        //Initiate variables utilized to build UDP data packets
        private uint seqNumber = 0;
        public string currState = "";
        private byte[] data;
        private byte[] mydata;

        // Initiate GameObject which acts a guide for virtual robot
        // Initiate EgmRobot which will be used to build EGMSensor datapacket
        // public GameObject cube;
        private EgmRobot robot;

        //Initiate variables used in GetInformation() function - denote the robot's current position
        public double jointdebug =0;
        public double xC;
        public double yC;
        public double zC;
        public double xR;
        public double yR;
        public double zR;
        private double xc;
        private double yc;
        private double zc;
        private bool firstime = true;



        //Initiate variables passed into SendInformation() Function - denote the robot's new current position 
        private double currentX;
        private double currentY;//maybe change these to futureX, futureY, and futureZ??
        private double currentZ;
        private double currentRX;
        private double currentRY;
        private double currentRZ;




        private readonly Queue<Action> ExecuteOnMainThread = new Queue<Action>();

        public double getX()
        {
            
            return xc;
        }
        public double getY()
        {
            return yc;
        }
        public double getZ()
        {
            return zc;
        }
#if !UNITY_EDITOR

        //Initialize UDP Datagram socket
        DatagramSocket socket;


    
    public UDPCommunication(string ControllerIP, string ControllerPort)
    {

        port = ControllerPort;
        externalIP = ControllerIP;
       SetupServer();

    }
    public async void SetupServer()//async void Start()
    /*
    Summary: Runs when app is first opened in Hololens. Opens new Datagram socket, finds IP address
     of hololens, and uses this ip adress to bind to the hololens port.        
    */
    {

        socket = new DatagramSocket();
        socket.MessageReceived += Socket_MessageReceived;

        HostName IP = null;
        try
        {
            var icp = NetworkInformation.GetInternetConnectionProfile();

            IP = Windows.Networking.Connectivity.NetworkInformation.GetHostNames()
            .SingleOrDefault(
                hn =>
                    hn.IPInformation?.NetworkAdapter != null && hn.IPInformation.NetworkAdapter.NetworkAdapterId
                    == icp.NetworkAdapter.NetworkAdapterId);

            await socket.BindEndpointAsync(IP, port);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            Debug.Log(SocketError.GetStatus(e.HResult).ToString());
            return;
        }

    }
   
    public async void SendUDPMessage(string ControllerIP, string ControllerPort)
    /*
    Summary: Initiates private function _SendUDPMessage
    Inputs:
        - ControllerIP: ipaddress of robot controller  (generally the ip of the PC which hosts controller)
        - ControllerPort: listening port of Robot Controller (this is equal to the "Local Port Number" of the robot studio "EGMDevice")
    */
    {
        await _SendUDPMessage(ControllerIP, ControllerPort);
    }



    private async System.Threading.Tasks.Task _SendUDPMessage(string ControllerIP, string ControllerPort)

    /*
    Summary: This function creates a data packet of type EgmSensor and writes it to a memory stream
    this memory stream is then converted to an array of bytes and sent through writer and StoreAsync()
    methods
    
    Inputs: 
        - ControllerIP: ipaddress of robot controller  (generally the ip of the PC which hosts controller)
        - ControllerPort: listening port of Robot Controller (this is equal to the "Local Port Number" of 
        the robot studio "EGMDevice")
    */

    {
        EgmSensor mymessage = new EgmSensor();
        CreateSensorMessage(mymessage); //CreateSensorMessage() is a function that builds the EGMSensor data packet

        using (var stream = await socket.GetOutputStreamAsync(new Windows.Networking.HostName(ControllerIP), ControllerPort))
        {
            using (var writer = new Windows.Storage.Streams.DataWriter(stream))
            {
               
                memoryStream = new MemoryStream();
                EgmSensor sensorMessage = mymessage;
                sensorMessage.WriteTo(memoryStream);
                writer.WriteBytes(memoryStream.ToArray()); //memoryStream.Toarray() converts the memory stream into a byte array
                await writer.StoreAsync();

            }
        }
    }



    private void Socket_MessageReceived(Windows.Networking.Sockets.DatagramSocket sender, Windows.Networking.Sockets.DatagramSocketMessageReceivedEventArgs args)

    /*
    
    Summary: Retreives UDP message from hololens port. Utilizes buffer to read in byte array of message and 
    calls ExtractData() in order to transcribe byte array into variables that can be utilized within the program.
    Inputs: 
        - args: Provides data for a UDP message received on a Datagram Socket
    
    */

    {

        byte[] message = null;
        using (var reader = args.GetDataReader())
        {
            var buf = new byte[reader.UnconsumedBufferLength];
            reader.ReadBytes(buf);
            message = buf;
        }
        

        if (ExecuteOnMainThread.Count == 0)
        {
            ExecuteOnMainThread.Enqueue(() =>
            {  
                ExtractData(message);
            });
        }
    }



    void ExtractData(byte[] mydata)

    /*
    Summary: Receives a byte array of data and transcribes this data to variables associated with the robot's coordinates
    Input:
        - mydata: byte array of retrieved UDP message 
    */

    {
        
         if (mydata != null)
            {
                // de-serialize inbound message from robot
                robot = new EgmRobot();
                robot.MergeFrom(mydata); //needs Google.Protobuf
            
            }

        if (robot.Header.HasSeqno && robot.Header.HasTm && robot.Header.HasMtype)
            {
                
                xC = robot.FeedBack.Cartesian.Pos.X;
                yC = robot.FeedBack.Cartesian.Pos.Y;
                zC = robot.FeedBack.Cartesian.Pos.Z;
                xR = robot.FeedBack.Cartesian.Euler.X;
                yR = robot.FeedBack.Cartesian.Euler.Y;
                zR = robot.FeedBack.Cartesian.Euler.Z;
                if (firstime ==true){
                    yc = yC;
                    xc = xC;
                    zc = zC;
                    firstime = false;
                }
                currState = robot.MciState.State.ToString();
                //jointdebug = robot.FeedBack.Joints.Joints[0];
                //text.text = robot.FeedBack.Joints.Joints.ToString();
                Debug.Log("xyz, Cxyz: " + xC + " " + yC + " " + zC + " " + xR + " " + yR  + " " + zR);
            }
        else
            {
                Debug.Log("No header in robot message");
            }
    }
    
  

    public void cubeMove(double x, double y, double z, double xx, double yy, double zz)

    /*
    
    Summary: Retrieves x,y, and z data of cube location, and transcribes this information into coordinates to send
    to robot controller. Once the coordinates are set, SendUDPMessage() is utilized to build and send a UDP packet 
    that contains these coordinates to the robot controller.
    Inputs: 
        - x: X position of cube
        - y: Y position of cube
        - z: Z position of cube
        - xx: X rotational position of cube
        - yy: Y rotational position of cube
        - zz: Z rotational position of cube
    
    */

    {
         while (ExecuteOnMainThread.Count > 0) //???
        {
            ExecuteOnMainThread.Dequeue().Invoke();

        }
        
        currentY = (x*1000) + yc;//yC + deviation;
        currentX = (z*1000) + xc;//xC;
        currentZ = (y*1000) + zc;//zC;
        currentRX = xx;
        currentRY = yy;
        currentRZ = zz;
        SendUDPMessage(externalIP,port);

    }



    void CreateSensorMessage(EgmSensor sensor)

    /*
    Summary: 
        Function utilized to create a UDP data packet that includes: 
            1. Header -- Sequence Number (Seqno),TimeStamp (Tm), MessageType (Mtype)
            2. Body -- Data
        This data contains a Translational Cartesian movement and a Euler Cartesian movement.
        - tcp_p.X : Translational x 
        - tcp_p.Y : Translational y 
        - tcp_p.Z : Translational z 
        - ea_p.X : Rotational x
        - ea_p.Y : Rotational y
        - ea_p.Z : Rotational z
         
    Inputs: Empty UDP packet (of type EgmSensor)
    Outputs: Returns filled version of UDP packet (sensor)
    */

    {
        EgmHeader hdr = new EgmHeader();
        hdr.Seqno = seqNumber++;
        hdr.Tm = (uint)DateTime.Now.Ticks;
        hdr.Mtype = EgmHeader.Types.MessageType.MsgtypeCorrection;

        sensor.Header = hdr;
        EgmPlanned planned_trajectory = new EgmPlanned();
        EgmPose cartesian_pos = new EgmPose();
        EgmCartesian tcp_p = new EgmCartesian();
        EgmEuler ea_p = new EgmEuler();

        tcp_p.X = currentX;
        tcp_p.Y = currentY;
        tcp_p.Z = currentZ;

        ea_p.X = currentRX;
        ea_p.Y = currentRY;
        ea_p.Z = currentRZ;

        cartesian_pos.Pos = tcp_p;
        cartesian_pos.Euler = ea_p;

        planned_trajectory.Cartesian = cartesian_pos;
        sensor.Planned = planned_trajectory;
        return;
    }
#else
        void Start()
        {

        }

        void Update()
        {

        }


#endif
    }
}
