using UnityEngine;
using UnityEngine.AI;

public class ExecutionerBehavior : MonoBehaviour {
	
	public GameManager gameManager;
	public LayerMask obstaclesMask;
	NavMeshAgent agent;
	MeshRenderer mesh;
	public int executionerState = 2;
	public float maximumSpeed = 3f;
	public float iterationDecreasement = 3f;
	public Vector3 playerRange;
	public Transform executionerFace;
	public bool hasHuntBegin;
	float visionTimer = 0f;
	int iterationAmount = 0;
	float roamingCD = 0f;
	
	//Executioner states:
	//-1 - System state.
	//0 - Roaming in search for a Jeremy.
	//1 - Engaged, smells Jeremy or hears footsteps, slightly accelerated chasing the source of the smell/sound.
	//2 - Noticed Jeremy, chasing down his victim at maximum speed.
	//3 - Physical contact with Jeremy, state during the screamer.
    void Start () {
		
		executionerFace = transform.GetChild(1);
		agent = GetComponent<NavMeshAgent>();
		mesh = GetComponentInChildren<MeshRenderer>();
	}
	
	void Update () {
		
		agent.speed = maximumSpeed + executionerState;
		playerRange = (transform.position - gameManager.playerMovement.transform.position) / 6;
		//Debug.Log(playerRange.magnitude);
		
		//Debug.DrawLine(executionerFace.position, gameManager.playerMovement.transform.position + new Vector3(0, gameManager.mouseLook.height, 0), Color.red);	
        if(!Physics.Linecast(executionerFace.position, gameManager.playerMovement.transform.position + new Vector3(0, gameManager.mouseLook.height, 0), obstaclesMask)) {
			visionTimer = 3f;
			//Debug.Log(true);
		}
		//else Debug.Log(false);
		
		if (executionerState != -1) {
			if (playerRange.magnitude < 8f) hasHuntBegin = true;
			else if (iterationAmount == 0) hasHuntBegin = false;
				
			if (playerRange.magnitude < .75f) executionerState = 3;
			else if (visionTimer > 0) {
				executionerState = 2;
				visionTimer -= Time.deltaTime;
			}
			else if (hasHuntBegin) executionerState = 1;
			else executionerState = 0;
			//Debug.Log(executionerState);
		}
		
		switch (executionerState) {
			case 0:
				if (agent.velocity.magnitude < 0.2) {
					
					roamingCD -= Time.deltaTime;;
					if (roamingCD <= 0) { 
						
						if (!gameManager.IsVisible(transform.position)) Hunt(20f - iterationDecreasement * iterationAmount);
						else agent.destination = gameManager.playerMovement.transform.position;
						roamingCD = 5f;
						iterationAmount++;
					}
				}
				break;
			case 1:
				if (playerRange.magnitude < 4f && !gameManager.playerMovement.crouchFlag) agent.destination = gameManager.playerMovement.transform.position;
				else if (agent.velocity.magnitude < 0.2) {
					
					roamingCD -= Time.deltaTime;
					if (roamingCD <= 0) { 
						
						Hunt(8f);
						roamingCD = 2f;
						iterationAmount--;
					}
				}
				break;
			case 2:
				agent.destination = gameManager.playerMovement.transform.position;
				break;
			case 3:
				mesh.enabled = false;
				gameManager.Screamer();
				break;
		}
	}
	
	void Hunt (float huntRange) {
		/*
		//Vector3 newPosition = gameManager.posCalculator(Mathf.Clamp(huntRange, 8f, 100f));
		if (!gameManager.IsVisible(newPosition)) {
			
			//Debug.DrawLine(gameManager.mouseLook.transform.position, newPosition, Color.red, 1f);	
			agent.enabled = false;
			//transform.position = newPosition;
			agent.enabled = true;
			agent.destination = gameManager.playerMovement.transform.position;
		}*/
	}
}
