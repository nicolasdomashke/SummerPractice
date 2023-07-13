using UnityEngine;

public class ItemsGen : MonoBehaviour {

	public GameManager gameManager;
	public GameObject plateToBuild;
	public int labirintHeight = 19;
	public int labirintWidth = 19;
	public bool GenerateItemGen = false;
	public int inactivePlates;
	int inactivePlatesMax;
	int objectsAmount = 0;
	float [] percent = new float [] {.6f, .95f, 2f};
	
	void /*On*/Validate() {
		
		if (GenerateItemGen) {
			
			for (int i = 0; i < labirintHeight; i++) {
				for (int j = 0; j < labirintWidth; j++) {
					
					Instantiate(plateToBuild, new Vector3(12f * i + 6f, .7f, 12f * j + 6f), Quaternion.Euler(0f, 0f, 0f));
				}
			}
		}
	}
	
    void Start() {
		
        inactivePlatesMax = labirintHeight * labirintWidth;
        inactivePlates = inactivePlatesMax;
    }

    void Update() {
		
		gameManager.stageCompletion = 1f - (float)inactivePlates / inactivePlatesMax;
		//Debug.Log(inactivePlates);
        if (gameManager.stageCompletion > percent[objectsAmount]) {
			
			Transform [] plates = GetComponentsInChildren<Transform>();
			int iMin = 0;
			float lMin = (plates[0].position - gameManager.playerMovement.transform.position).magnitude;
			for (int i = 1; i < plates.Length; i++) {
				
				if ((plates[i].position - gameManager.playerMovement.transform.position).magnitude < lMin) {
					
					iMin = i;
					lMin = (plates[i].position - gameManager.playerMovement.transform.position).magnitude;
				}
			}
			//Debug.Log(plates[iMin].position + new Vector3(0f, 2.3f, 0f));
			gameManager.hiddenObjects.transform.GetChild(objectsAmount).gameObject.SetActive(true);
			gameManager.hiddenObjects.transform.GetChild(objectsAmount).position = plates[iMin].position + new Vector3(0f, 2.3f, 0f);
			plates[iMin].GetComponent<Renderer>().enabled = false;
			inactivePlates--;
			Destroy(plates[iMin].gameObject);
			objectsAmount++;
		}
    }
}
