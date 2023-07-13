using UnityEngine;
using static System.Convert;

public class LabirintGen : MonoBehaviour {
	
	public GameObject wallToBuild;
	public int labirintHeight = 40;
	public int labirintWidth = 40;
	public bool generateLabirint = false;
	Component[] walls;
	
    void /*On*/Validate() {
		
		if (generateLabirint) {
			
			bool[,] maze = new bool[labirintHeight, labirintWidth];
			walls = GetComponentsInChildren(typeof(Transform));
			
			foreach (Transform wall in walls) {
			
				int x = System.Convert.ToInt32((wall.position.x + 68.3586f) / 4);
				int z = System.Convert.ToInt32((wall.position.z + 59.28529f) / 4);
				maze[x, z] = true;
			}
			
			for (int i = 0; i < labirintHeight; i++) {
				for (int j = 0; j < labirintWidth; j++) { 
					if (maze[i, j]){
					
						if (i == 0 || i == labirintHeight - 1) {
							
							Instantiate(wallToBuild, ClonePosition(i, j) + Vector3.left * 3f, Quaternion.Euler(-90f, 90f, 0f));
							Instantiate(wallToBuild, ClonePosition(i, j) + Vector3.right * 3f, Quaternion.Euler(-90f, 270f, 0f));
						}
						else if (j == 0 || j == labirintWidth - 1) {
							
							Instantiate(wallToBuild, ClonePosition(i, j) + Vector3.back * 3f, Quaternion.Euler(-90f, 0f, 0f));
							Instantiate(wallToBuild, ClonePosition(i, j) + Vector3.forward * 3f, Quaternion.Euler(-90f, 180f, 0f));
						}
						else {
							
							if (!maze[i, j - 1]) Instantiate(wallToBuild, ClonePosition(i, j) + Vector3.back * 3f, Quaternion.Euler(-90f, 0f, 0f));
							if (!maze[i, j + 1]) Instantiate(wallToBuild, ClonePosition(i, j) + Vector3.forward * 3f, Quaternion.Euler(-90f, 180f, 0f));
							if (!maze[i - 1, j]) Instantiate(wallToBuild, ClonePosition(i, j) + Vector3.left * 3f, Quaternion.Euler(-90f, 90f, 0f));
							if (!maze[i + 1, j]) Instantiate(wallToBuild, ClonePosition(i, j) + Vector3.right * 3f, Quaternion.Euler(-90f, 270f, 0f));
						}
					}
				}
			}
		}
    }
	
	Vector3 ClonePosition (int a, int b) {
		
		Vector3 clonePosition = new Vector3(6f * a, .5f, 6f * b);
		return clonePosition;
	}
}
