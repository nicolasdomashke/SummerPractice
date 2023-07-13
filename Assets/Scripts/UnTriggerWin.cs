using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnTriggerWin : MonoBehaviour {
    
	public GameObject orbsWarning;
	public GameObject keyWarning;
	
    void OnTriggerEnter(Collider other) {
		
		if (other.tag == "Player") {
			
			orbsWarning.SetActive(false);
			keyWarning.SetActive(false);
		}
	}
}
