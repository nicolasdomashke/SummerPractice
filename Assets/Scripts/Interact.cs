using UnityEngine;

public class Interact : MonoBehaviour {
	
	Camera cameraMain;
	public LayerMask interactiveMask;
	public GameManager gameManager;
	public bool isAtInteractive = false;
	
    void Update() {
        
		if (!gameManager.intuitionManager.isIntuitionActive) {
			
			RaycastHit hit;
			isAtInteractive = Physics.Raycast(transform.position, transform.forward, out hit, 6, interactiveMask);
			bool isEPressed = Input.GetKeyDown("e");
			
			if (hit.collider != null && isEPressed) {
				if (hit.collider.tag == "Collectible") {
					
					hit.collider.gameObject.SetActive(false);
				}
				else {
					
					hit.collider.gameObject.SendMessage("InteractiveFunction");
				}
			}
			
			//Debug.DrawRay(transform.position, transform.forward * 5, Color.black, 0f);
		}

		gameManager.interactUI.SetActive(isAtInteractive && !gameManager.intuitionManager.isIntuitionActive);
    }
}
