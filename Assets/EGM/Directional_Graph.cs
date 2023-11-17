using System.Collections.Generic;
using UnityEngine;

// Author: Miles Popiela
// Description: Represents the one-directional graph for robotic waypoint pathing.
public class Directional_Graph : MonoBehaviour
{
    public Transform wayPoints;
    public GameObject ToolCenterPoint;
    public GameObject MoveTarget;
    public List<GameObject> targetList;
    LineRenderer lineRenderer;
    private bool isLinked = false;
    private bool isFirst = true;
    public Color c1 = Color.cyan;
    public Color c2 = Color.magenta;
    bool isRecording = false;
    Vector3 pastPosition;

    //Toggles recording
    public void ToggleRecording(){
        isRecording = !isRecording;
    }

    // Clears all waypoints.
    public void ClearWaypoints()
    {
        foreach (var target in targetList)
        {
            Destroy(target);
        }

        isLinked = false;
        targetList.Clear();
        lineRenderer.positionCount = 0;
    }

    //Removes Last Waypoint
    public void RemoveLastWaypoint(){
        if(targetList.Count > 0){
            Destroy(targetList[targetList.Count-1]);
            targetList.RemoveAt(targetList.Count-1);
            lineRenderer.positionCount -= 1;
        }
    }

    //Adds Waypoint
    public void AddWaypoint()
    {
        if (targetList.Count == 0 && isFirst) {
            ConfigureLineRenderer();
            isFirst = false;
        }

        isLinked=true;
        GameObject newObject = Instantiate(MoveTarget, ToolCenterPoint.transform.position, ToolCenterPoint.transform.rotation);
        newObject.transform.SetParent(wayPoints);
        targetList.Add(newObject);
    }

    // Links new Target node with prior node and keeps paths updated.
    private void LinkTargets()
    {
        if (isLinked)
        {
            for (int i = 0; i < targetList.Count; i++)
            {
                lineRenderer.positionCount = i + 1;
                lineRenderer.SetPosition(i, targetList[i].transform.position);
            }
        }
    }

    void ConfigureLineRenderer(){
        
        lineRenderer = this.gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        targetList = new List<GameObject>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if((isRecording == true) && (ToolCenterPoint.transform.position != pastPosition)){
            AddWaypoint();
        }
        pastPosition = ToolCenterPoint.transform.position;
        LinkTargets();
    }


    public List<GameObject> GetList()
    {
        return targetList;
    }
}
