using UnityEngine;

public class IntuitionManager : MonoBehaviour {
	
	public GameManager gameManager;
	public GameObject menuUi;
	//public Material standartMat;
	//public Material intuitionMat;
	public bool isIntuitionActive = false;
	public bool isIntuitionToggled = false;
	public float intuitionCD = 1f;
	/*Component[] walls;
	Component[] hiddenObjects;
	MeshRenderer executionerMesh;
	
	void Start () {
		
		walls = GetComponentsInChildren<MeshRenderer>();
		hiddenObjects = gameManager.hiddenObjectsManager.GetComponentsInChildren<MeshRenderer>();
		executionerMesh = gameManager.executionerBehavior.GetComponentInChildren<MeshRenderer>();
	*/
	
    void Update() {
        
		if (gameManager.isIntuitionEnabled) {
			
			isIntuitionToggled = Input.GetKeyDown("q");
			
			if(intuitionCD > 0) {
				intuitionCD -= Time.deltaTime;
			}
			else if (isIntuitionToggled && gameManager.executionerAI.state != ExecutionerAI.State.Chasing) {
				
				isIntuitionActive = !isIntuitionActive;
				menuUi.SetActive(false);
			}
		}
		//Debug.Log(isIntuitionActive);
    }
	public void WallsToggle () {
		
		/*foreach (MeshRenderer wall in walls) {
		
			wall.enabled = !isIntuitionActive;
		}
		foreach (MeshRenderer hidee in hiddenObjects) {
		
			hidee.enabled = !isIntuitionActive;
		}
		executionerMesh.enabled = !isIntuitionActive;*/
		intuitionCD += 1f;
		RenderSettings.fog = !isIntuitionActive;
		gameManager.mainCamera.enabled = !isIntuitionActive;
		gameManager.intuitionCamera.enabled = isIntuitionActive;
	}
}
