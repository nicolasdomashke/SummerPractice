using UnityEngine;

public class OrbCollectible : MonoBehaviour {
	
	public GameManager gameManager;
	
    void OnDisable() {
        
		gameManager.orbsCollected += 1;
		Debug.Log("Orb event");
    }
}
