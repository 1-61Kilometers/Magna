using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMapSwipe : MonoBehaviour
{
    private float _sensitivity;
     private Vector3 _mouseReference;
     private Vector3 _mouseOffset;
     private Vector3 _rotationY;
     private bool _isRotating;
     //public float x_rot;
    // public bool movingLeft, movingCenter;
     public float speed = 350f;
     
     void Start ()
     {
         _sensitivity = 0.4f;
         _rotationY = Vector3.zero;
         //movingLeft = movingCenter = false;
     }
     
     void Update()
     {
         if(_isRotating)
         {
             // offset
             _mouseOffset = (Input.mousePosition - _mouseReference);
             
             // apply rotation
             _rotationY.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
             
            _rotationY.y = -(_mouseOffset.x) * _sensitivity;
             // rotate
             transform.Rotate(_rotationY);

             // store mouse
             _mouseReference = Input.mousePosition;
         }

         /*if (movingLeft){
            MoveLeft();
         }
         if (movingCenter){
            MoveCenter();
         }*/
         
     }
     
     void OnMouseDown()
     {
         // rotating flag
         _isRotating = true;
         
         // store mouse
         _mouseReference = Input.mousePosition;
     }
     
     void OnMouseUp()
     {
         // rotating flag
         _isRotating = false;
     }

     /*private void MoveLeft(){
        if (Vector3.Distance(transform.position, new Vector3(2120, 0, -74)) < .001)
            movingLeft = false;
        var step =  speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(2120, 0, -74), step);

     }
     private void MoveCenter(){
        if (Vector3.Distance(transform.position, new Vector3(2332, 0, -74)) < .001)
            movingCenter = false;
        var step =  speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(2332, 0, -74), step);

     }*/

     
 }
