using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Diagnostics;
using System.IO;

public class UpdatedMove : MonoBehaviour
{
    Vector3 set;
    Vector3 angles;
    public GameObject cube;
    // animation csv files
    public TextAsset[] animations;
    private List<string> lines;
    private int count;
    private bool first = true;
    private bool pause = false;
    private Vector3 Anglestart;
    private Vector3 Transstart;
    private float setx;
    private float sety;
    private float setz;
    private int currentAnimationIndex = 0;
    float time = 0;

    void Start()
    {
        //move idel on start 
        //ReadString();
        Transstart = cube.transform.position;
        Anglestart = cube.transform.eulerAngles;
    }

    void FixedUpdate()
    {
        if (pause)
        {
            return;
        }
        
            if (count < lines.Count)
            {
                GameObject fakeCube = cube;
                string[] positions = lines[count].Split(',');
                if (first)
                {
                    setx = float.Parse(positions[0]);
                    sety = float.Parse(positions[1]);
                    setz = float.Parse(positions[2]);
                    first = false;
                }

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
                var step = .01f * Time.deltaTime;
                cube.transform.position = Vector3.MoveTowards(cube.transform.position, Transstart, step);
                cube.transform.rotation = Quaternion.Lerp(cube.transform.rotation, Quaternion.LookRotation(new Vector3(0, 0, 0)), 10f * Time.deltaTime);

                if (cube.transform.position == Transstart && cube.transform.eulerAngles == Anglestart)
                {
                    count = 0;
                    SetNextAnimation();
                }
            }
    }


    public void SetNextAnimation()
    {
        //currentAnimationIndex = (currentAnimationIndex + 1) % animations.Length;
        
        //move between idle animations
        currentAnimationIndex = (currentAnimationIndex + 1) % 2;
        //ReadString();
        ResetAnimation();
    }

    private void findNearestCoord(){
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
        count = minIndex;
    }

    /* public void SetPreviousAnimation()
    {
        currentAnimationIndex = currentAnimationIndex - 1;
        if (currentAnimationIndex < 0)
        {
            currentAnimationIndex = animations.Length - 1;
        }
        ReadString();
        ResetAnimation();
    } */

    public void Pause()
    {
        pause = true;
    }

    public void Resume()
    {
        pause = false;
    }

    /* private void ReadString()
    {
        lines = new List<string>();
        TextAsset currentAnimation = animations[currentAnimationIndex];
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
        UnityEngine.Debug.Log(lines[1]);
    } */

    private void ResetAnimation()
    {
        first = true;
        count = 0;
        Transstart = cube.transform.position;
        Anglestart = cube.transform.eulerAngles;
        setx = 0;
        sety = 0;
        setz = 0;
    }
}
