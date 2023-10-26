using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Diagnostics;
using System.IO;

public class CSVMove : MonoBehaviour
{
    //positional values
    Vector3 set;
    //rotational values
    Vector3 angles;
    //reference cube
    public GameObject cube;

    /*public TextAsset animations;*/
    /*private string line;*/

    //count of line
    private int count;
    //list of each line in file
    public List<string> lines;
    //starting rotation of cube
    Vector3 Anglestart;
    //starting position of cube
    Vector3 Transstart;
    float setx, sety, setz;
    //float setx,sety,setz;
    //positional values
    //float setx,sety,setz;
    /*bool first = true;*/
    bool pause = false;

    // Read animation data from TextAsset object
   // public void ReadString(TextAsset animationData)
    // Start is called before the first frame update
    //public void ReadString();
    
    //on start get currint positional and rotational values of cube and read file
    /*void Start()
    {
        Transstart = cube.transform.position; 
        Anglestart = cube.transform.eulerAngles;
        List<string> lines = new List<string>();
        ReadFile();
    }

    public void ReadFile()
    {
        List<string> liness = new List<string>();
        string[] fileLines = animationData.text.Split('\n');

        // Extract first line separately (assuming it contains header information)
        string firstLine = fileLines[0];

        // Extract animation data from subsequent lines
        for (int i = 1; i < fileLines.Length; i++)
        {
            if (fileLines[i] != "")
            {
                liness.Add(fileLines[i]);
            }
        }
        lines = liness;
    }

    public void disconnected()
    {
        pause = true;
    }

    public void reconnect()
    {
        pause = false;
    }

    void Start()
    {
        Transstart = cube.transform.position;
        Anglestart = cube.transform.eulerAngles;
        ReadString(animations);
    }
    void Start()
    {
        Transstart = cube.transform.position; 
        Anglestart = cube.transform.eulerAngles;
        List<string> lines = new List<string>();
        ReadString();
    }
    

    void FixedUpdate()
    {
        if (pause == false)
        {
            if (count < lines.Count)
            {
                GameObject fakeCube = cube;
                string[] positions = lines[count].Split(',');

                if (first == true)
                {
                    setx = float.Parse(positions[0]);
                    sety = float.Parse(positions[1]);
                    setz = float.Parse(positions[2]);
                    first = false;
                }
                string[] positions = lines[count].Split(","[0]);
                if(first == true){
                    setx = float.Parse(positions[0]);
                    sety = float.Parse(positions[1]);
                    setz = float.Parse(positions[2]);
                    first = false;
                }
                string[] positions = lines[count].Split(","[0]);

                set = fakeCube.transform.position;
                set.x = float.Parse(positions[0]) - setx;
                set.y = float.Parse(positions[1]) - sety;
                set.z = float.Parse(positions[2]) - setz;

                angles = cube.transform.eulerAngles;
                angles.x = float.Parse(positions[3]);
                angles.y = float.Parse(positions[4]);
                angles.z = float.Parse(positions[5]);

                fakeCube.transform.position = set;
                fakeCube.transform.eulerAngles = angles;

                cube = fakeCube;
                count++;
            }
            else
            {
                var step = 0.1f * Time.deltaTime;
                cube.transform.position = Vector3.MoveTowards(cube.transform.position, Transstart, step);
                cube.transform.rotation = Quaternion.Lerp(cube.transform.rotation, Quaternion.LookRotation(new Vector3(0, 0, 0)), 10f * Time.deltaTime);

                if (cube.transform.position == Transstart && cube.transform.eulerAngles == Anglestart)
                {
                    count = 0;
                }
            }
        }
    }

    void Update()
    {

    }*/
}
