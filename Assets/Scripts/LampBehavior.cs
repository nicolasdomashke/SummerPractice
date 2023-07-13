using UnityEngine;
using System.Collections;

public class LampBehavior : MonoBehaviour {

	public Material lampON;
	public Material lampOFF;
	Renderer lampRenderer;
	Light lampLight;
	Component [] lights;

    void Start() {
		
		lampLight = GetComponentInChildren<Light>();
		lampRenderer = GetComponent<Renderer>();
    }
	
    public void SetState(LightManager.LampState lampState) {
        
		switch (lampState) {
			case LightManager.LampState.Off:
				StartCoroutine(LampShivering(Random.Range(.5f, 3f), Random.value > .3f ? true : false, true));
				break;
			case LightManager.LampState.RapidlyShivering:
				StartCoroutine(LampShivering(Random.Range(2f, 5f), true, false));
				break;
			case LightManager.LampState.Shivering:
				StartCoroutine(LampShivering(Random.Range(4f, 10f), false, false));
				break;
			case LightManager.LampState.On:
				StartCoroutine(LampShivering(Random.Range(.5f, 3f), Random.value > .3f ? true : false, false));
				break;
		}
    }
	
	IEnumerator LampShivering (float length, bool isShiverDelayed, bool isLampOff) {
		
		float timer = 0f;
		while(timer < length) {
			
			float shiverDelay;
			if (isShiverDelayed) shiverDelay = Random.value;
			else shiverDelay = .01f;
			LampOff();
			yield return new WaitForSeconds(shiverDelay);
			LampOn();
			yield return new WaitForSeconds(shiverDelay);
			timer += 2 * shiverDelay;
		}
		if (isLampOff) LampOff();
	}
	
	void LampOn () {
		
		lampRenderer.material = lampON;
		lampLight.enabled = true;
	}
	
	void LampOff () {
		
		lampRenderer.material = lampOFF;
		lampLight.enabled = false;
	}
}
