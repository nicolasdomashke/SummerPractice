using UnityEngine;

public class PhotoCollectible : MonoBehaviour {
	
	public GameManager gameManager;
	
    void OnDisable() {
        
		gameManager.purpleOrbsCollected += 1;
		Debug.Log("Photo event");
    }
}
