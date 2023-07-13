using UnityEngine;

public class Quit : MonoBehaviour {
	
	public GameManager gameManager;
	float counter = 0f;
    void Update() {
		
        bool ESCPressed = Input.GetKey("escape"); //Will be changed on escape;
		
		gameManager.quitUI.SetActive(ESCPressed);
		if (ESCPressed) {
			
			counter += 1f * Time.deltaTime;
			if (counter >= 1.5f) {
				
				gameManager.GameQuit();
			}
		}
		else counter = 0;
    }
}
