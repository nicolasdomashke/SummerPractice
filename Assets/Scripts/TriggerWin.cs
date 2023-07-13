using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWin : MonoBehaviour {
    
	public GameManager gameManager;
	public GameObject orbsWarning;
	public GameObject keyWarning;
	
    void OnTriggerEnter(Collider other) {
		
		if (other.tag == "Player") {
			
			if (gameManager.orbsCollected < 2) orbsWarning.SetActive(true);
			else if (gameManager.itemsCollected < 1) keyWarning.SetActive(true);
			else gameManager.Victory();
		}
	}
}
