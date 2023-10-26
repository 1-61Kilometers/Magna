using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;
public class toArduino : MonoBehaviour
{
    float y;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        UduinoManager.Instance.sendCommand("updateValues", this.transform.eulerAngles.x, this.transform.eulerAngles.y, this.transform.eulerAngles.z, (this.transform.position.y+.3)*100);

    }
}
