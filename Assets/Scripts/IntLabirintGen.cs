using UnityEngine;

public class IntLabirintGen : MonoBehaviour {
	
	public GameObject wallToBuild;
	public GameObject masterMaze;
	public bool generateLabirint = false;
	Component[] walls;
	
    void OnValidate() {
		
		if (generateLabirint) {
			
			walls = masterMaze.GetComponentsInChildren(typeof(Transform));
			
			foreach (Transform wall in walls) {
			
				Instantiate(wallToBuild, wall.position + new Vector3(0f, 50f, 0f), wall.rotation);
			}
		}
    }
}