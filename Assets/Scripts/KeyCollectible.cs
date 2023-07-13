using UnityEngine;

public class KeyCollectible : MonoBehaviour{
	
	public GameManager gameManager;
	
    void OnDisable() {
        
		gameManager.itemsCollected += 1;
		Debug.Log("Key obtained");
    }
}
