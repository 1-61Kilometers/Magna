using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnIdel : MonoBehaviour
{
    public bool idel;
    public float rotationsPerMinute = 3f;
    public bool prevIdel = false;
    // Start is called before the first frame update
    void Start()
    {
        idel = false;
    }

    void FixedUpdate()
    {
        if (idel)
            transform.Rotate(0,6.0f*rotationsPerMinute*Time.deltaTime,0);
    }

    void OnMouseOver(){
      if(idel){
        prevIdel = true;
        idel = false;
      }
    }

    void OnMouseExit(){
      if(!idel && prevIdel){
        prevIdel = false;
        idel = true;
      }
    }

    public void StopRotating(){
        idel = false;
    }

    public void StartRotating(){
        idel = true;
    }
}
