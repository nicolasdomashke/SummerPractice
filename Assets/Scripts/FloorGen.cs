using UnityEngine;

public class FloorGen : MonoBehaviour {
	
	public GameObject floorToBuild;
	public int labirintHeight = 39;
	public int labirintWidth = 39;
	public bool generateFloor = false;
	
    void /*On*/Validate() {
		
		if (generateFloor) {
			
			for (int i = 0; i < labirintHeight; i++) {
				for (int j = 0; j < labirintWidth; j++) { 
						
						Instantiate(floorToBuild, ClonePosition(i, j), Quaternion.Euler(-90f, 0f, 0f));
				}
			}
		}
    }
	
	Vector3 ClonePosition (int a, int b) {
		
		Vector3 clonePosition = new Vector3(6f * a + 6f, .5f, 6f * b + 6f);
		return clonePosition;
	}
}