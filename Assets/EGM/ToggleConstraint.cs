using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MixedReality.Toolkit.SpatialManipulation;
public class ToggleConstraint : MonoBehaviour
{
    
    public MoveAxisConstraint xConstraint;
    public MoveAxisConstraint yConstraint;
    public MoveAxisConstraint zConstraint;
    public RotationAxisConstraint RxConstraint;
    public RotationAxisConstraint RyConstraint;
    public RotationAxisConstraint RzConstraint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Toggelonx(){
        xConstraint.enabled = true;
    }
    public void Toggeloffx(){
        xConstraint.enabled = false;
    }

    public void Toggelony(){
        yConstraint.enabled = true;
    }
    public void Toggeloffy(){
        yConstraint.enabled = false;
    }

    public void Toggelonz(){
        zConstraint.enabled = true;
    }
    public void Toggeloffz(){
        zConstraint.enabled = false;
    }
    
    public void ToggelonRx(){
        RxConstraint.enabled = true;
    }
    public void ToggeloffRx(){
        RxConstraint.enabled = false;
    }

    public void ToggelonRy(){
        RyConstraint.enabled = true;
    }
    public void ToggeloffRy(){
        RyConstraint.enabled = false;
    }

    public void ToggelonRz(){
        RzConstraint.enabled = true;
    }
    public void ToggeloffRz(){
        RzConstraint.enabled = false;
    }
}
