using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextOnHover : MonoBehaviour
{
   private string text;
   private string currentToolTipText = "";
   private GUIStyle guiStyleFore;
   private GUIStyle guiStyleBack;
    // Start is called before the first frame update
    void Start()
    {
      text = gameObject.name;
      guiStyleFore = new GUIStyle();
      guiStyleFore.normal.textColor = Color.green;
      guiStyleFore.alignment = TextAnchor.UpperCenter;
      guiStyleFore.wordWrap = true;
      guiStyleBack = new GUIStyle();
      guiStyleBack.normal.textColor = Color.black;
      guiStyleBack.alignment = TextAnchor.UpperCenter;
      guiStyleBack.wordWrap = true;
    }

    void OnMouseOver(){
      currentToolTipText = text;
   }

   void OnMouseExit(){
      currentToolTipText = "";
   }

   void OnGUI()
   {
      if (currentToolTipText != "")
      {
      float x = Event.current.mousePosition.x;
      float y = Event.current.mousePosition.y;
      GUI.Label(new Rect(x - 149, y + 20, 300, 60), currentToolTipText, guiStyleBack);
      GUI.Label(new Rect(x - 150, y + 20, 300, 60), currentToolTipText, guiStyleFore);
      }
   }
}
