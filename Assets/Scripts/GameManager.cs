using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
	public Camera mainCamera;
	public Camera intuitionCamera;
	public Camera menuCamera;
	public PlayerMovement playerMovement;
	public MouseLook mouseLook;
	public Interact interact;
	public IntuitionManager intuitionManager;
	public ExecutionerAI executionerAI;
	public LightManager lightManager;
	public ItemsGen plateManager;
	public ShardControl shardControl;
	public GameObject hiddenObjects;
	public GameObject interactUI;
	public GameObject quitUI;
	public GameObject blinkingUI;
	public GameObject screamerUI;
	public GameObject gameOverUI;
	public GameObject victoryUI;
	public LayerMask ignoranceMask;
	public Text victoryTextUI;
	public bool isControllerEnabled = true;
	public bool isIntuitionEnabled = true;
	public bool isCrouchEnabled = false;
	public bool isShardEnabled = true;
	public int stage = 0;
	public int orbsCollected = 2;
	public int itemsCollected = 1;
	public int purpleOrbsCollected = 0;
	public float gameDifficulty = 0f;
	public float stageCompletion = 0f;
	float [] gameBasicDifficulty = new float [] {.3f};
	float [] stageCompletionEstimated = new float [] {25f};
		
	void Start () {
		
		Debug.Log("Game launched!");
		gameDifficulty = gameBasicDifficulty[stage];
	}
	
	void Update () {
		
		gameDifficulty = gameBasicDifficulty[stage] + .3f * stageCompletion * Mathf.Pow(2, -Time.time / (30f * stageCompletionEstimated[stage]));
		//Debug.Log(gameDifficulty);
		//Debug.Log(stageCompletion);
		//Debug.Log(PosCalculator(2f));
	}
	
	public void GameQuit() {
		
		Debug.Log("Game done!");
		Application.Quit();
	}
	
	public void Screamer() {
		
		isControllerEnabled = false;
		isIntuitionEnabled = false;
		isShardEnabled = false;
		//screamerUI.SetActive(true);
		Debug.Log("Game over!");
		StartCoroutine("GameOverEvent");
	}
	
	public void Victory() {
		
		victoryTextUI.text = "Secret orbs collected: "+ purpleOrbsCollected.ToString() + "/2";
		isControllerEnabled = false;
		isIntuitionEnabled = false;
		isShardEnabled = false;
		victoryUI.SetActive(true);
		Debug.Log("Victory!");
	}
	
	public void FromMenuTransition() {
		
		isControllerEnabled = true;
		isIntuitionEnabled = true;
		isCrouchEnabled = false;
		isShardEnabled = true;
		menuCamera.enabled = false;
		StartCoroutine("GameBeginningEvent");
	}
	
	IEnumerator GameOverEvent () {
		
		yield return new WaitForSeconds(1f);
		screamerUI.SetActive(false);
		gameOverUI.SetActive(true);
	}
	
	IEnumerator GameBeginningEvent () {
		
		yield return new WaitForSeconds(10f);
		executionerAI.state = ExecutionerAI.State.Roaming;
	}
	
	/*public Vector3 PosCalculator (float offsetRange) {
		
		//Vector2 randomCircle = Random.insideUnitCircle.normalized * offsetRange * 6f;
		float x = Mathf.Round((playerMovement.transform.position.x - 6f) / 12f) * 12f + 6f;
		float z = Mathf.Round((playerMovement.transform.position.z - 6f) / 12f) * 12f + 6f;
		//if (x < 6f || x > 222f) x = Mathf.Round((-randomCircle.x + playerMovement.transform.position.x - 6f) / 12f) * 12f + 6f;
		//if (z < 6f || z > 222f) z = Mathf.Round((-randomCircle.y + playerMovement.transform.position.z - 6f) / 12f) * 12f + 6f;
		x = Mathf.Clamp(x, 6f, 222f);
		z = Mathf.Clamp(z, 6f, 222f);
		return new Vector3(x, 3f, z);
	}*/
	
	public bool IsVisible (Vector3 executionerPosition) {
		
		return !(Physics.Linecast(mouseLook.transform.position, executionerPosition + new Vector3(2.97f, 0f, 2.97f), ignoranceMask)
			&& Physics.Linecast(mouseLook.transform.position, executionerPosition + new Vector3(2.97f, 0f, -2.97f), ignoranceMask)
			&& Physics.Linecast(mouseLook.transform.position, executionerPosition + new Vector3(-2.97f, 0f, 2.97f), ignoranceMask)
			&& Physics.Linecast(mouseLook.transform.position, executionerPosition + new Vector3(-2.97f, 0f, -2.97f), ignoranceMask));
	}
}