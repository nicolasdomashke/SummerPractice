using UnityEngine;

public class Intuition : MonoBehaviour {
	
	public GameManager gameManager;
	Animator animator;
	bool isInMenu = true;
	
	void Start () {
		animator = this.GetComponent<Animator>();
	}
	void Update () {
		
		animator.SetBool("isIntuitionOn", gameManager.intuitionManager.isIntuitionActive);
		animator.SetBool("isInterapted", gameManager.executionerAI.state == ExecutionerAI.State.Hunting || gameManager.executionerAI.state == ExecutionerAI.State.Chasing);
	}

    public void WallsToggleAnim() {
		if (isInMenu) {
			
			isInMenu = false;
			gameManager.FromMenuTransition();
		}
		gameManager.intuitionManager.WallsToggle();
	}
	
	public void IntuitionInterapted() {
		gameManager.intuitionManager.intuitionCD = 3f;
		gameManager.intuitionManager.isIntuitionActive = false;
	}
}
