using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLandscape : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationsPerMinute = 3f;
    public GameObject model;
    // Update is called once per frame
    void FixedUpdate()
    {
        float angle = Mathf.Sin(Time.time) * rotationsPerMinute; //tweak this to change frequency
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}
