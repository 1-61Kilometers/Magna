using System.Collections.Generic;
using UnityEngine;
using System;

public class TestingMove : MonoBehaviour
{
    public float slowDown = 0.5f;
    Vector3 set;
    Vector3 angles;
    public GameObject cube;
    public TextAsset[] animations;
    private List<string> lines;
    private int count;
    private bool pause = false;
    public Quaternion Anglestart;
    public Vector3 Transstart;
    private int /*currentAnimationIndex = 0, */idelAnimationIndex = 0;
    public bool isGoingHome;
    public GameObject globe;
    private long current;
    float avg = 0, x = 0;


    void Start()
    {
        //move idel on start 
        //get starting positions
        Transstart = cube.transform.position;
        Anglestart = cube.transform.rotation;
        //robot should be starting at home pos
        isGoingHome = false;
        //ChangeTimeScale(idelAnimationIndex);
        ReadFile(idelAnimationIndex);
    }

    public int NonIdelAnimation(int index){
        if(index == 16){
            Pause();
            Debug.Log("AVG TIME = " + avg / x);
            Debug.Log("DONE");
        }
        while(animations[index].name == "Idel" || animations[index].name == "Idel2"){
            index = index + 1;
        }
        if(index > 16){
            Pause();
            Debug.Log("DONE");
        }
        ChangeTimeScale(index);
        x = x + 1;
        return index;
    }

    private void ChangeTimeScale(int index){
        if (index == 2)
            Time.timeScale = slowDown;
        else if (index == 4)
            Time.timeScale = slowDown;
        else if (index == 6)
            Time.timeScale = slowDown;
        else if (index == 8)
            Time.timeScale = slowDown;
        else if (index == 9)
            Time.timeScale = slowDown;
        else if (index == 13)
            Time.timeScale = slowDown;
        else if (index == 15)
            Time.timeScale = slowDown;
        else
            Time.timeScale = 1f;
    }

    void FixedUpdate()
    {
        
        //this script should stop when the robot is going to home pos or paused
        if (isGoingHome)
        {
            return;
        }

        if (pause)
        {
            return;
        }

        //read coordinates from animation file
        if (count < lines.Count)
        {
            //Debug.Log("Index " + count + " out of " + lines.Count);
            GameObject fakeCube = cube;
            string[] positions = lines[count].Split(',');
            
            set = fakeCube.transform.position;
            set.x = float.Parse(positions[0]);
            set.y = float.Parse(positions[1]);
            set.z = float.Parse(positions[2]);

            angles = cube.transform.eulerAngles;
            angles.x = float.Parse(positions[3]);
            angles.y = float.Parse(positions[4]);
            angles.z = float.Parse(positions[5]);
            cube.transform.position = set;
            cube.transform.eulerAngles = angles;
            cube = fakeCube;
            count++;
        }
        else
        {
            //long elapsed = DateTimeOffset.Now.ToUnixTimeSeconds() - current;
            //avg = avg + elapsed;
            //Debug.Log(animations[index].name + " Elapsed time: " + elapsed);
            //read next idel animation
            int nextIndex = (idelAnimationIndex + 1) % 2;
            ChangeTimeScale(nextIndex);
            ReadFile(nextIndex);
        }
    }

    //select line of animation file with closes coordinates to current position
    private int findNearestCoord(){
        float minDist = Mathf.Infinity;
        Vector3 currLinePos = transform.position, currLineAng = transform.eulerAngles;
        int minIndex = 0;
        for (int i = 0; i < lines.Count; i++){
            string line = lines[i];
            currLinePos.x = float.Parse(line.Split(",")[0]);
            currLinePos.y = float.Parse(line.Split(",")[1]);
            currLinePos.z = float.Parse(line.Split(",")[2]);

            currLineAng.x = float.Parse(line.Split(",")[3]);
            currLineAng.y = float.Parse(line.Split(",")[4]);
            currLineAng.z = float.Parse(line.Split(",")[5]);
            float newDist = Vector3.Distance(currLinePos, transform.position) + Vector3.Distance(currLineAng, transform.eulerAngles);
            if (minDist > newDist){
                minDist = newDist;
                minIndex = i;
            }
        }
        return minIndex;
    }

    //pause and resume methods
    public void Pause()
    {
        pause = true;
    }

    public void Resume()
    {
        pause = false;
    }

    //read a given animation file
     public void ReadFile(int index)
    {   
        //only bounce between idel animations
        if (index == 0 || index == 1){
            //Debug.Log("IM IDEL");
            GameObject.FindGameObjectWithTag("Mars").GetComponent<RotateOnIdel>().StartRotating();
        }
        else{
            GameObject.FindGameObjectWithTag("Mars").GetComponent<RotateOnIdel>().StopRotating();
        }
        //currentAnimationIndex = index;
        //ChangeTimeScale(index);
        lines = new List<string>();
        TextAsset currentAnimation = animations[index];
        string[] fileLines = currentAnimation.text.Split('\n');
        bool firstline = true;
        foreach (string line in fileLines)
        {
            if (line.Length > 0)
            {
                if (firstline)
                {
                    firstline = false;// Skip empty lines
                } else {
                    lines.Add(line);
                    
                }

            }
        }
        //change starting positions to first line of file
        if (index == 0 || index == 1)
            count = findNearestCoord();
        else
            count = 0;
        var x = float.Parse(lines[count].Split(",")[0]);
        var y = float.Parse(lines[count].Split(",")[1]);
        var z = float.Parse(lines[count].Split(",")[2]);

        var a = float.Parse(lines[count].Split(",")[3]);
        var b = float.Parse(lines[count].Split(",")[4]);
        var c = float.Parse(lines[count].Split(",")[5]);

        Transstart = new Vector3(x,y,z); 
        Anglestart = Quaternion.Euler(a,b,c);
        current = DateTimeOffset.Now.ToUnixTimeSeconds();
        isGoingHome = true;
    }

    
}
