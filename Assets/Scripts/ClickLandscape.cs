using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickLandscape : MonoBehaviour
{
   public Material unselected, selected, hover_selected, hover_unselected;
   public GameObject model;
   //private OnMapSwipe marsMovement;
   private Transform panel;
   public GameObject mars;
   RotateOnIdel rotateOnIdel;
   Text data;

   void Start(){
      panel = GameObject.FindGameObjectWithTag("MainCanvas").transform.GetChild(0);
      //marsMovement = mars.GetComponent<OnMapSwipe>();
      rotateOnIdel = mars.GetComponent<RotateOnIdel>();
      data = GameObject.FindGameObjectWithTag("MainCamera").transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Text>();
   }  

   void OnMouseOver(){
      if(rotateOnIdel.idel){
        rotateOnIdel.prevIdel = true;
        rotateOnIdel.idel = false;
      }

      if (gameObject.tag == "Clicked")
         gameObject.GetComponent<MeshRenderer>().material = hover_selected;
      else
         gameObject.GetComponent<MeshRenderer>().material = hover_unselected;
   }

   void OnMouseExit(){
      if(!rotateOnIdel.idel && rotateOnIdel.prevIdel){
        rotateOnIdel.prevIdel = false;
        rotateOnIdel.idel = true;
      }

      if (gameObject.tag == "Clicked")
         gameObject.GetComponent<MeshRenderer>().material = selected;
      else
         gameObject.GetComponent<MeshRenderer>().material = unselected;
   }

     void OnMouseDown(){
      //previously clicked button
      var prev = GameObject.FindGameObjectWithTag("Clicked");
      //current shown box
      //var curr_box = GameObject.FindGameObjectWithTag("Box");
      //if no previously clicked button
      if (!prev){
         gameObject.tag = "Clicked";
         gameObject.GetComponent<MeshRenderer>().material = selected;
         //marsMovement.movingLeft = true;
         //StartCoroutine(WaitForMarsMovement());
         display();
      }
      //if previously clicked button is current button
      else if (prev == gameObject){
         prev.tag = gameObject.tag = "Untagged";
         gameObject.GetComponent<MeshRenderer>().material = unselected;
         //marsMovement.movingCenter = true;
         panel.gameObject.SetActive(false);
         data.gameObject.SetActive(false);
      }
      //if previously clicked button is another button
      else{
         prev.tag = "Untagged";
         prev.GetComponent<MeshRenderer>().material = unselected;
         //curr_box.SetActive(false);
         gameObject.tag = "Clicked";
         gameObject.GetComponent<MeshRenderer>().material = selected;
         display();
      }
      
     }
     void display(){
      /*panel.GetChild(0).name = gameObject.name;
      panel.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = gameObject.name;
      panel.GetChild(1).GetComponent<Image>().sprite = img;*/
      GameObject oldMod = panel.GetChild(1).GetChild(0).gameObject;
      //model.transform.SetPositionAndRotation(oldMod.transform.localPosition, oldMod.transform.localRotation);
      //model.transform.localScale = oldMod.transform.localScale;
      SetComponentValues(model.name);
      Instantiate(model, panel.transform.GetChild(1));
      Destroy(oldMod);
      panel.gameObject.SetActive(true);
      data.gameObject.SetActive(true);
      }

   void SetComponentValues(string name){
      Vector3 position, scale;
      Quaternion rotation;
      if (name == "Athabasca Valles Flood Erosion of Impact Crater"){
         position = new Vector3(-53.4000015f,18.2999878f,5.04998779f);
         rotation = Quaternion.Euler(-44.158f,-63.461f,45.23f);
         scale = new Vector3(6.86388397f,25.5622272f,6.86388397f);
         data.text = "9.331N 155.852E | 1X | SCALE: 1KM";
      }
      else if(name == "Central Peak of an Impact Crater"){
         position = new Vector3(-50.9000015f,18.2999878f,5.04998779f);
         rotation = Quaternion.Euler(-37.917f,-57.533f,35.984f);
         scale = new Vector3(7.5f,20,7.5f);
         data.text = "-28.370N 83.425E | 1X | SCALE: 1KM";
      }
      else if (name == "Crater with Surrounding Depression"){
         position = new Vector3(-51.4000015f,18.2999878f,5.04998779f);
         rotation = Quaternion.Euler(-41.328f,-67.509f,51.311f);
         scale = new Vector3(7f,50f,7f);
         data.text = "40.660N 16.456E | 1X | SCALE: 1KM";
      }
      else if (name == "Fan at Intersection of Valley and Crater Wall in Xanthe Terra"){
         position = new Vector3(-48.2999992f,16f,5.04998779f);
         rotation = Quaternion.Euler(-44.158f,-63.461f,45.23f);
         scale = new Vector3(4.63000011f,10.9300003f,2.83999991f);
         data.text = "7.596N -38.979E | 1X | SCALE: 1KM";
      }
      else if (name == "Evidence of Ice and Rock in Crater Near Side Channel of Mamers Valles"){
         position = new Vector3(-53.4000015f,18.2999878f,5.04998779f);
         rotation = Quaternion.Euler(-44.158f,-63.461f,45.23f);
         scale = new Vector3(7f,20f,7f);
         data.text = "37.138N 15.14E | 1X | SCALE: 1KM";
      }
      else if (name == "Exit Breach in Well-Preserved Crater"){
         position = new Vector3(-52.5999985f,18.2999878f,5.04998779f);
         rotation = Quaternion.Euler(-44.158f,-63.461f,45.23f);
         scale = new Vector3(8.5f,50f,8.5f);
         data.text = "36.706N 20.660E | 1X | SCALE: 1KM";
      }
      else if (name == "Possible Carbonate in Libya Montes Region"){
         position = new Vector3(-53.4000015f,18.2999878f,5.04998779f);
         rotation = Quaternion.Euler(-41.183f,-58.518f,37.944f);
         scale = new Vector3(7f,25.5622272f,6f);
         data.text = "3.292N 84.620E | 1X | SCALE: 1KM";
      }
      else if (name == "Cross Section of Hummocks in Atlantis Chaos"){
         position = new Vector3(-51.5f,18.2999878f,5.04998779f);
         rotation = Quaternion.Euler(8.755f,-238.348f,-39.751f);
         scale = new Vector3(9.11610031f,9.11610031f,4.55805016f);
         data.text = "-4.924N 138.350E | 1X | SCALE: 1KM";
      }
      else if (name == "Layered Materials on Northeast Hellas Planitia Rim"){
         position = new Vector3(-50.2000008f,18.2999878f,5.04998779f);
         rotation = Quaternion.Euler(-44.158f,-63.461f,45.23f);
         scale = new Vector3(3f,5f,1.29999995f);
         data.text = "-27.284N 78.304E | 1X | SCALE: 1KM";
      }
      else if (name == "Well-Preserved-Impact-Crater-With-Steep-Slopes"){
        position = new Vector3(-53.4000015f,18.2999878f,5.04998779f);
        rotation = Quaternion.Euler(-44.158f,-63.461f,45.23f);
        scale = new Vector3(6.86388397f,25.5622272f,6.86388397f);
        data.text = "-38.209N -176.072E | 1X | SCALE: 1KM";
      }
      else{
         Debug.Log("name does not match");
         position = new Vector3(-55.7000008f,18.2999878f,5.04998779f);
         rotation = Quaternion.Euler(-46.261f,-68.177f,51.869f);
         scale = new Vector3(3.46964002f,8.67409992f,2.34200716f);
         data.text = "-38.812N -177.920E | 1X | SCALE: 1KM";
      }

      model.transform.SetPositionAndRotation(position, rotation);
      model.transform.localScale = scale;
   }

   /*IEnumerator WaitForMarsMovement(){
      while(marsMovement.movingLeft){
         yield return null;
      }
      display();
   }*/
}
