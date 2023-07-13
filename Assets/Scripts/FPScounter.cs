using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FPScounter : MonoBehaviour {

	Text fps;
	float fpsRes = 0f;
	int fpsCount = 0;
	
	void Start () {
		
		fps = GetComponent<Text>();
		StartCoroutine("ShowFPS");
	}
	
	void Update () {
		
		fpsRes += 1f/Time.deltaTime;
		fpsCount++;
	}
	
	IEnumerator ShowFPS () {
		
		for (;;) {
			
			yield return new WaitForSeconds(.25f);
			fps.text = (fpsRes / fpsCount).ToString("0");
			fpsRes = 0f;
			fpsCount = 0;
		}
	}
}
