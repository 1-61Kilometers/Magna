using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHome : MonoBehaviour
{
    private TestingMove robotMovement;
    private Vector3 lastPos;
    private Quaternion lastAng;
    public float speedPos = .6f, speedAng = .6f;
    public float dist = 0.001f;
    // Start is called before the first frame update
    void Start()
    {
       robotMovement = gameObject.GetComponent<TestingMove>();
       //save last positions 
       lastPos = transform.position;
       lastAng = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        //start script when robot is going to home pos   
        if(robotMovement.isGoingHome){
            //if its not moving anymore start movement script
            if(!Moving()){
                //robotMovement.Pause();
                //StartCoroutine(waitABit());
                robotMovement.isGoingHome = false;
                
                
            }
            //otherwise move towards position
            else{
                //Debug.Log("test");
            var step1 = speedPos * Time.deltaTime;
            var step2 = speedAng * Time.deltaTime;
           
            //Vector3 _direction = (robotMovement.Anglestart - transform.position).normalized;
            //Quaternion _lookRotation = Quaternion.LookRotation(_direction);
            transform.position = Vector3.MoveTowards(transform.position, robotMovement.Transstart, step1);
            transform.rotation = Quaternion.Slerp(transform.rotation, robotMovement.Anglestart, step2);
            
            }
        }
        
    }

    //checks if robot is still moving towards home
    bool Moving(){
        var displacement1 = transform.position - robotMovement.Transstart;
        
        var displacement2 = transform.rotation.eulerAngles - robotMovement.Anglestart.eulerAngles;
        
        
        return displacement1.magnitude > dist && displacement2.magnitude > dist;
        
    }

    IEnumerator waitABit(){
        yield return new WaitForSeconds(2);
        robotMovement.Resume();
    }
}
