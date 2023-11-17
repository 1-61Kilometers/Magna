using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using communication;
using TMPro;
using locations;
using System;
using System.IO;
using MixedReality.Toolkit.UX;

public class moveCube : MonoBehaviour
{
    public Transform origin;
    float radius = .950f;
    Vector3 Positions;
    int count = 0;
    public bool Pathmode;
    bool playmode = false;
    public Directional_Graph graph;
    public AnimationRecord record;
    public UDPCommunication myserver;
    public GameObject TCP;
    public GameObject button;
    Vector3 Pos;
    Vector3 Set;
    Quaternion quatern;
    Vector3 angles;
    public Material mesh;
    string port = "6511";
    public string controllerip;
    bool server = false;
    bool second = false;
    double cz; //initialize variables above
    double cx;
    double cy;
    double rx;
    double ry;
    double rz;
    private double Cx;
    private double Cy;
    private double Cz;
    private double Rx;
    private double Ry;
    private double Rz;
    public float moveSpeed = 10.0f;
    private int i;
    private float j1;
    public Transform[] Joints;
    List<GameObject> Paths;
    public void pathtrue()
    {
        Pathmode = true;
        Debug.Log("true");
    }
    public void pathfalse()
    {
        Pathmode = false;
        Debug.Log("false");
    }
    //Sets the offset of  the bounding box ,,, could replace with QR
    public void Setpos(){
        
        Set = TCP.transform.position;
        Pos = Set - new Vector3(0,.58798f,-.23605f);
        angles = TCP.transform.eulerAngles;
        quatern = TCP.transform.rotation;
        server = true;
        origin.SetParent(null);
    }

    public void Home(){
        StartCoroutine(MoveOverTime());
    }

    private IEnumerator MoveOverTime() {
        bool home = false;
        //Configure to move to home over time
        while(home == false){
            var step = 0.5f * Time.deltaTime;
            TCP.transform.rotation = Quaternion.Lerp(TCP.transform.rotation, Quaternion.LookRotation(new Vector3(0,0,0)), 4f* Time.deltaTime);
            TCP.transform.position = Vector3.MoveTowards(TCP.transform.position, Set, step);
            if(TCP.transform.position == Set && TCP.transform.rotation == quatern){
                home = true;
            }
            yield return null;
        }
        
    }

    public void GetPath()
    {
        Paths = graph.GetList();
    }
    //This Moves the pointer to next item in object
    public void MoveNext()
    {
        GetPath();
        if (i+1 > Paths.Count-1) {
            i = 0; 
        } else
        {
            i++;
        }
        TCP.transform.position = Paths[i].transform.position;
        TCP.transform.eulerAngles = Paths[i].transform.eulerAngles;
        Debug.Log(i);
    }




//Idea is to change the max on slider based on how many targets exist
//
//Create Get/Set functions inside pinchslider.cs
//
//

    
    public void PathPlayback(SliderEventData eventData)
    {
        GetPath();
        if(Paths.Count > 0 || Paths == null) {
            double slide = Math.Round((Paths.Count - 1) * eventData.NewValue);
            i = (int)slide;
            Debug.Log(i);
            TCP.transform.position = Paths[i].transform.position;
            TCP.transform.eulerAngles = Paths[i].transform.eulerAngles;
        }
    }
      

    public void MovePrev()
    {
        GetPath();
        if (i - 1 == -1)
        {
            i = Paths.Count-1; 
        }
        else
        {
            i--;
        }
        TCP.transform.position = Paths[i].transform.position;
        TCP.transform.eulerAngles = Paths[i].transform.eulerAngles;
        Debug.Log(i);
    }

    public void records(){
        record.sendto(cx,cy,cz,rz,rx,ry);
        mesh.color = new Color(255,0,0,1);
    }

    public void playToggle(){
        GetPath();
        if (playmode == false){
            playmode = true;
        } else {
            playmode = false;
        }
    }

    static void toFile()
    {

        // Create a string array with the lines of text
        string[] lines = { " " };

        // Set a variable to the Documents path.
        string docPath = Application.persistentDataPath;

        // Write the string array to a new file named "WriteLines.txt".
        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "WriteFile.txt")))
        {
            foreach (string line in lines)
                outputFile.WriteLine(line);
        }
    }

    static void appendtoFile(string[] lines)
    {
        string docPath = Application.persistentDataPath;
        // Append new lines of text to the file

        //Commentent out magna_mode
        //ObjectTraversal objectTraversal = GameObject.Find("LandScapes").GetComponent<ObjectTraversal>();
        //string landscapeName = objectTraversal.GetLandscapename() + ".txt";
        string newString = "Path";//landscapeName.Replace(" ", "_");
        File.AppendAllLines(Path.Combine(docPath, newString), lines);
        Debug.Log("Saved to: " + newString);
    }

    public void sendto(double cx, double cy, double cz, Vector3 ang){
        string line = (cx.ToString() + "," + cy.ToString() + "," + 
                    cz.ToString() + "," + ang.ToString().Substring(1, ang.ToString().Length - 2));
        string[] pos = {line};
        Debug.Log(pos[0]);
        appendtoFile(pos);
        
    }
    
    public void ToCSV(){
        GetPath();
        for (int x = 0; x<=Paths.Count-1; x++){
                sendto(Paths[x].transform.position.x - Set.x, Paths[x].transform.position.y - Set.y,
                 Paths[x].transform.position.z - Set.z, Paths[x].transform.eulerAngles);
            }
    }
#if !UNITY_EDITOR
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        Cx = 0;
        Cy = 0;
        Cz = 0;
        Rx = 0;
        Ry = 0;
        Rz = 0;
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
        GetPath();
        //Waits for button to be pressed
        if (server == true){
            //Connects to server
            Debug.Log("Server Set Up");
            myserver = new UDPCommunication(controllerip,port);
            server = false;
        }

        if(playmode == true){
            MoveNext();
        }
        float distance = Vector3.Distance(TCP.transform.position, origin.position);

        // If the object is outside the sphere, move it back to the closest point on the sphere
        if (distance > radius)
        {
            TCP.transform.position = origin.position + (TCP.transform.position - origin.position).normalized * radius;
        }
        //Offset positions of TCP
        cz = -TCP.transform.position.z + Set.z ; //initialize variables above
        cx = TCP.transform.position.x - Set.x;
        cy = TCP.transform.position.y - Set.y;
        

        rx = TCP.transform.eulerAngles.x;
        ry = TCP.transform.eulerAngles.y;
        rz = TCP.transform.eulerAngles.z;
        
        /*if (Pathmode == false)
        {
            myserver.TCPMove(cx, cy, cz, (-TCP.transform.eulerAngles.z - 180) + angles.z, TCP.transform.eulerAngles.x - angles.x, (-TCP.transform.eulerAngles.y - 180) - angles.y);
        } else
        {
            myserver.TCPMove(TargetList[i].transform.position.x, TargetList[i].transform.position.y, TargetList[i].transform.localRotation.eulerAngles.x,
            TargetList[i].transform.localRotation.eulerAngles.y, TargetList[i].transform.localRotation.eulerAngles.z);
        }
        */


        //myserver.TCPMove(cx, cy, cz, (-TCP.transform.eulerAngles.z - 180) + angles.z, TCP.transform.eulerAngles.x - angles.x, (-TCP.transform.eulerAngles.y - 180) - angles.y);
        
        //Safty Zone
        //Offset Pos
        
        myserver.cubeMove(cx, cy, cz, (-TCP.transform.eulerAngles.z - 180) + angles.z, TCP.transform.eulerAngles.x - angles.x, (-TCP.transform.eulerAngles.y - 180) - angles.y);
        /*
        if(Vector3.Distance(Pos, TCP.transform.position) > .95 || cy <= -.55f){ 
            //Only Update Rotation
            //Would like to have position on cusp of sphere radius
            myserver.cubeMove(cx, cy, cz, (-TCP.transform.eulerAngles.z - 180) + angles.z, TCP.transform.eulerAngles.x - angles.x, (-TCP.transform.eulerAngles.y - 180) - angles.y);         
            mesh.color = new Color(255,0,0,1);
        } else {
            myserver.cubeMove(cx, cy, cz, (-TCP.transform.eulerAngles.z - 180) + angles.z, TCP.transform.eulerAngles.x - angles.x, (-TCP.transform.eulerAngles.y - 180) - angles.y);
            mesh.color = new Color(255,255,255,0);
        }
        */
        
        Cx = cx;
        Cy = cx;
        Cz = cx;
        Rx = rx;
        Ry = ry;
        Rz = rz;

    }

#else

    void Start()
    {
        
    }
    void Update()
    {
        if (playmode == true)
        {
            MoveNext();
        }
        //Offset Pos
        float distance = Vector3.Distance(TCP.transform.position, origin.position);

        // If the object is outside the sphere, move it back to the closest point on the sphere
        if (distance > radius)
        {
            TCP.transform.position = origin.position + (TCP.transform.position - origin.position).normalized * radius;
        }
        cz = -TCP.transform.position.z + Set.z ; //initialize variables above
        cx = TCP.transform.position.x - Set.x;
        cy = TCP.transform.position.y - Set.y;
        rx = TCP.transform.eulerAngles.x;
        ry = TCP.transform.eulerAngles.y;
        rz = TCP.transform.eulerAngles.z;
        
        
    }

#endif
    //this is miles making a comment
    //this is gabriella making a comment
}
