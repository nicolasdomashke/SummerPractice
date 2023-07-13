using UnityEngine;

public class IntuitionLook : MonoBehaviour {
    
	public GameManager gameManager;
	
    void Update() {
		
		transform.position = gameManager.mouseLook.transform.position + new Vector3(0f, 50f, 0f);
        transform.rotation = gameManager.mouseLook.transform.rotation;
    }
}
