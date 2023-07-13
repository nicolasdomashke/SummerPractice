using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ExecutionerAI : MonoBehaviour {

	public GameManager gameManager;
	public LayerMask obstaclesMask;
	public GameObject scaryImage;
	public GameObject executionerBody;
	public GameObject executionerBodyReal;
	public Material standartMat;
	public Material faceMat;
	public Material invisMat;
	public Animator animatorZombie;
	NavMeshAgent agent;
	MeshRenderer mesh;
	public float executionerSpeed = 2f;
	public Vector3 playerRange;
	public Transform executionerFace;
	Renderer bodyRend;
	Renderer headRend;
	float visionTimer = 0f;
	float timeSinceHunt = 0f;
	float timeDelay = 0f;
	int timeDelayPenalty = 0;
	int attempts = 0;
	public bool isInvisible = false;
	public enum State {Disabled, Roaming, Hunting, Chasing};
	public State state = State.Disabled;
	State prevState;
	
    void Start() {

		prevState = State.Disabled;
		executionerFace = transform.GetChild(1);
		agent = GetComponent<NavMeshAgent>();
		//mesh = GetComponentInChildren<MeshRenderer>();
		timeDelay = TimeDelayGen();
		bodyRend = executionerBody.GetComponent<Renderer>();
		//headRend = executionerFace.GetComponentInChildren<Renderer>();
    }

    void Update() {
		
		//Debug.DrawLine(executionerFace.position, gameManager.playerMovement.transform.position + new Vector3(0, gameManager.mouseLook.height, 0), Color.red);
		//Debug.Log(Time.timeSinceLevelLoad - timeSinceHunt - timeDelay);
		playerRange = (transform.position - gameManager.playerMovement.transform.position) / 6f;
		//Debug.Log(playerRange.magnitude);
		EventHandler();
		StateChanger();
		animatorZombie.SetFloat("agentSpeed", agent.speed);
		if (playerRange.magnitude < .5f && !isInvisible) {
		
			//mesh.enabled = false;
			gameManager.Screamer();
			//gameObject.SetActive(false);
			setState(State.Disabled);
			agent.enabled = false;
			animatorZombie.SetBool("isKilling", true);
		}
    }
	
	void EventHandler () {

		switch(state) {
			
			case State.Disabled:
			
				break;
			case State.Roaming:
				
				agent.speed = 1f;
				if (!agent.enabled) {

					transform.position = PosCalculator(gameManager.playerMovement.transform.position) + new Vector3 (12f * Mathf.Floor(Random.Range(2f, 6.99f)), 0f, 12f * Mathf.Floor(Random.Range(2f, 6.99f)));
					//Debug.Log(new bool [] {Physics.Linecast(executionerFace.position, gameManager.playerMovement.transform.position + new Vector3(0, gameManager.mouseLook.height, 0), obstaclesMask), !bodyRend.isVisible, !headRend.isVisible});
					if (Physics.Linecast(executionerFace.position, gameManager.playerMovement.transform.position + new Vector3(0, gameManager.mouseLook.height, 0), obstaclesMask) && !bodyRend.isVisible) {
						
						MakeInvisible(false);
						agent.destination = gameManager.playerMovement.transform.position;
					}
				}
				else if (agent.remainingDistance < 1f) MakeInvisible(true);
				break;
			case State.Hunting:
			
				agent.speed = 1f;
				if (!agent.enabled) {

					transform.position = PosCalculator(gameManager.playerMovement.transform.position) + new Vector3 (12f * Mathf.Floor(Random.Range(0f, 2.99f)), 0f, 12f * Mathf.Floor(Random.Range(0f, 2.99f)));
					//Debug.Log(new bool [] {Physics.Linecast(executionerFace.position, gameManager.playerMovement.transform.position + new Vector3(0, gameManager.mouseLook.height, 0), obstaclesMask), !bodyRend.isVisible, !headRend.isVisible});
					if (Physics.Linecast(executionerFace.position, gameManager.playerMovement.transform.position + new Vector3(0, gameManager.mouseLook.height, 0), obstaclesMask) && !bodyRend.isVisible) {
					
						//Debug.Log(attempts);
						attempts -= 1;
						MakeInvisible(false);
						agent.destination = gameManager.playerMovement.transform.position;
					}
				}
				else if (attempts > 0 && agent.remainingDistance < 1f) MakeInvisible(true);
				break;
			case State.Chasing:
	
				if(!Physics.Linecast(executionerFace.position, gameManager.playerMovement.transform.position + new Vector3(0, gameManager.mouseLook.height, 0), obstaclesMask)) visionTimer = 3f;
				if (visionTimer > .01f) agent.destination = gameManager.playerMovement.transform.position;
				visionTimer = Mathf.MoveTowards(visionTimer, 0f, Time.deltaTime);
				break;
		}
	}
	
	void StateChanger () {
		
		switch(state) {
			
			case State.Disabled:
			
				break;
			case State.Roaming:
				if(!Physics.Linecast(executionerFace.position, gameManager.playerMovement.transform.position + new Vector3(0, gameManager.mouseLook.height, 0), obstaclesMask)) {
					
					agent.speed = executionerSpeed * 2.1f;
					visionTimer = 3f;
					setState(State.Chasing);
				}
				else if (Time.timeSinceLevelLoad - timeSinceHunt > timeDelay) {
					
					if (gameManager.gameDifficulty > Random.value) {
						
						//agent.speed = executionerSpeed * 1.5f;
						timeDelayPenalty = 0;
						attempts = (int) Mathf.Round(gameManager.gameDifficulty * 13f);
						gameManager.lightManager.SwitchLightmode(true);
						setState(State.Hunting);
					}
					else {
						
						timeSinceHunt = Time.timeSinceLevelLoad;
						timeDelayPenalty -= 1;
						timeDelay = TimeDelayGen() * Mathf.Pow(2, timeDelayPenalty);
					}
				}
				
				break;
			case State.Hunting:
				
				if (!isInvisible) {
					
					if(!Physics.Linecast(executionerFace.position, gameManager.playerMovement.transform.position + new Vector3(0, gameManager.mouseLook.height, 0), obstaclesMask)) {
						
						agent.speed = executionerSpeed * 2.1f;
						visionTimer = 3f;
						setState(State.Chasing);
					}
					else if (attempts == 0 && agent.remainingDistance < 1f) {
						
						agent.speed = executionerSpeed;
						timeSinceHunt = Time.timeSinceLevelLoad;
						timeDelay = TimeDelayGen();
						gameManager.lightManager.SwitchLightmode(false);
						setState(State.Roaming);
					}
				}
				break;
			case State.Chasing:
				
				if (visionTimer <= .01f && agent.remainingDistance < 1f) {
					
					//if (prevState == State.Hunting) agent.speed = executionerSpeed * 1.5f;
					//else agent.speed = executionerSpeed;
					setState(prevState);
				}
				break;
		}
	}
	
	IEnumerator ScaryImageShower() {
		
		bool toggler = true;
		for (int i = 0; i < 10; i += 1) {
			
			scaryImage.SetActive(toggler);
			toggler = !toggler;
			yield return new WaitForSeconds(Random.Range(.09f, .15f));
		}
	}
	
	public Vector3 PosCalculator (Vector3 offSet) {
		
		float x = Mathf.Round((offSet.x - 6f) / 12f) * 12f + 6f;
		float z = Mathf.Round((offSet.z - 6f) / 12f) * 12f + 6f;
		x = Mathf.Clamp(x, 6f, 222f);
		z = Mathf.Clamp(z, 6f, 222f);
		return new Vector3(x, 3f, z);
	}
	
	void MakeInvisible(bool boolean) {
		
		//bodyRend.material = boolean ? invisMat : standartMat;
		//headRend.material = boolean ? invisMat : faceMat;
		agent.enabled = !boolean;
		isInvisible = boolean;
		executionerBodyReal.SetActive(!boolean);
	}
	
	
	void setState(State stateToSet) {
		
		prevState = state;
		state = stateToSet;
	}
	
	float TimeDelayGen() {
		return Random.Range(240f, 360f);
	}
}
