using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectLandscape : MonoBehaviour {
	public Button yourButton;
	private TestingMove traverse;
	private string file;

	private Quaternion oldLightRot;
	void Start () {
		traverse = GameObject.FindGameObjectWithTag("cube").GetComponent<TestingMove>();
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		file = gameObject.transform.GetChild(0).name.Trim().Replace(" ", "_") + ".txt";
		traverse.ReadFile(convertFileToIndex(file));
		
	}

	private int convertFileToIndex(string file){
		switch(file){
			case "Layered_Materials_on_Northeast_Hellas_Planitia_Rim.txt": return 2;
			case "Possible_Carbonate_in_Libya_Montes_Region.txt": return 3;
			case "Crater_with_Surrounding_Depression.txt": return 4;
			case "Athabasca_Valles_Flood_Erosion_of_Impact_Crater.txt": return 5;
			case "Evidence_of_Ice_and_Rock_in_Crater_Near_Side_Channel_of_Mamers_Valles.txt": return 6;
			//case "Hill_Southeast_of_Jezero_Crater.txt": return 0;
			case "Well_Preserved_Impact_Crater_with_Steep_Slopes.txt": return 7;
			case "Exit_Breach_in_Well-Preserved_Crater.txt": return 8;
			//case "Layered_Outcrop_in_Gale_Crater.txt": return 0;
			case "Cross_Section_of_Hummocks_in_Atlantis_Chaos.txt": return 9;
			case "Central_Peak_of_an_Impact_Crater.txt": return 10;
			case "Fan_at_Intersection_of_Valley_and_Crater_Wall_in_Xanthe_Terra.txt": return 11;
			//case "Gully_in_Crater_Wall.txt": return 0;
			case "Recent_Gullies_in_a_Crater_in_Noachis_Terra.txt": return 12;
			//case "Layered_Rocks_in_Orson_Welles_Crater.txt": return 0;
			default: return 0;
		}
	}

	void changeButtons(){
		
	}
}