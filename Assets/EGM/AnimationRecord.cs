using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
namespace locations{
public class AnimationRecord : MonoBehaviour
{
    
    public GameObject cube;
    //private string[] poslist = new string;
    // Start is called before the first frame update
    void Start()
    {
        
        toFile();
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
        File.AppendAllLines(Path.Combine(docPath, "WriteFile.txt"), lines);
    }
    public void sendto(double cx, double cy, double cz, double rz, double rx, double ry){
        string line = (cx.ToString() + "," + cy.ToString() + "," + 
                    cz.ToString() + "," + rx.ToString() + "," + ry.ToString()+ "," +   cz.ToString() + ",500,0");
        string[] pos = {line};
        //Debug.Log(pos);
        appendtoFile(pos);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
}