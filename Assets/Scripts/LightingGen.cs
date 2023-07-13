using UnityEngine;

public class LightingGen : MonoBehaviour {
	
	public GameObject lampToBuild;
	public int labirintHeight = 39;
	public int labirintWidth = 39;
	public bool generateLightning = false;
	
    void /*On*/Validate() {
		
		if (generateLightning) {
			
			for (int i = 0; i < labirintHeight; i++) {
				for (int j = 0; j < labirintWidth; j++) { 
					
					if (i % 2 == 0 && j % 2 == 0) {
						
						Instantiate(lampToBuild, ClonePosition(i, j), Quaternion.Euler(90f, 0f, 90f));
					}
				}
			}
		}
    }
	
	Vector3 ClonePosition (int a, int b) {
		
		Vector3 clonePosition = new Vector3(6f * a + 6f, 7.15f, 6f * b + 6f);
		return clonePosition;
	}
}