using UnityEngine;

public class PlateBehavior : MonoBehaviour {
    
	public GameManager gameManager;
	
    void OnBecameVisible() {
		
        GetComponent<Renderer>().enabled = false;
		gameManager.plateManager.inactivePlates--;
		Destroy(gameObject);
    }
}
