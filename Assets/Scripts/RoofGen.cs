using UnityEngine;

public class RoofGen : MonoBehaviour {
	
	public GameObject roofToBuild;
	public int labirintHeight = 39;
	public int labirintWidth = 39;
	public bool generateRoof = false;
	
    void /*On*/Validate() {
		
		if (generateRoof) {
			
			for (int i = 0; i < labirintHeight; i++) {
				for (int j = 0; j < labirintWidth; j++) { 
						
						Instantiate(roofToBuild, ClonePosition(i, j), Quaternion.Euler(-90f, 0f, 0f));
				}
			}
		}
    }
	
	Vector3 ClonePosition (int a, int b) {
		
		Vector3 clonePosition = new Vector3(6f * a + 6f, 7.3f, 6f * b + 6f);
		return clonePosition;
	}
}