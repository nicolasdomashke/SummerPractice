using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour {

	public GameManager gameManager;
	public float ambientColorMax = 0.15f;
	public bool Shads = true;
	Component [] lamps;
	public enum LampState {Off, RapidlyShivering, Shivering, On};
	
	/*void OnValidate() {
        if (!Shads) {
			
			Component [] lamps1 = GetComponentsInChildren<Light>();
			foreach (Light lampBehav in lamps1) lampBehav.lightmapBakeType = LightmapBakeType.Realtime;
			foreach (Light lampBehav in lamps1) lampBehav.color = new Color(0f, 0f, 1f, 1);
		}
    }*/
	
    void Start() {
        
		lamps = GetComponentsInChildren<LampBehavior>();
		StartCoroutine("LampsIdle");
    }

    public void SwitchLightmode(bool huntTrigger) {
		
		if (huntTrigger) {
			
			foreach (LampBehavior lampBehav in lamps) lampBehav.SendMessage("SetState", LampState.Off);
			StopCoroutine("LampsIdle");
			StopCoroutine("FogOff");
			StartCoroutine("FogOn");
		}
		else {
			
			foreach (LampBehavior lampBehav in lamps) lampBehav.SendMessage("SetState", LampState.On);
			StartCoroutine("LampsIdle");
			StopCoroutine("FogOn");
			StartCoroutine("FogOff");
		}
    }
	
	IEnumerator LampsIdle () {
		
		for (;;) {
				
			yield return new WaitForSeconds(5f);
			for (int i = 0; i < 5; i++) {
				
				int lampTriggered = (int) Random.Range(0f, lamps.Length - .01f);
				lamps[lampTriggered].SendMessage("SetState", Random.value > .7f ? LampState.RapidlyShivering : LampState.Shivering);
			}
		}
	}
	
	IEnumerator FogOff () {
		
		/*StopCoroutine("FogON");
		float ambientColor = ambientColorMax;
		float fogDens = 0.1f;
		while (ambientColor > 0f) {
			
			ambientColor = Mathf.MoveTowards(ambientColor, 0, ambientColorMax / 3f * Time.deltaTime);
			fogDens = Mathf.MoveTowards(fogDens, 0, 0.1f / 3f * Time.deltaTime);
			RenderSettings.ambientLight = new Color(ambientColor, ambientColor, ambientColor, 1f);
			RenderSettings.fogDensity = fogDens;
			yield return new WaitForEndOfFrame();
		}
		RenderSettings.fog = false;*/
		
		float fogDens = 0.1f;
		//float fogDens = 0.4f;
		while (RenderSettings.fogDensity > 0.06f) {
			
			fogDens = Mathf.MoveTowards(fogDens, 0.06f, 0.0133f * Time.deltaTime);
			//fogDens = Mathf.MoveTowards(fogDens, 0.06f, 0.1133f * Time.deltaTime);
			RenderSettings.fogDensity = fogDens;
			yield return new WaitForEndOfFrame();
		}
	}
	
	IEnumerator FogOn () {
		
		/*StopCoroutine("FogOFF");
		yield return new WaitForSeconds(3f);
		RenderSettings.fogDensity = 0.1f;
		RenderSettings.fog = true;
		float ambientColor = 0f;
		while (ambientColor < ambientColorMax) {
			
			ambientColor = Mathf.MoveTowards(ambientColor, ambientColorMax, ambientColorMax / 10f * Time.deltaTime);
			RenderSettings.ambientLight = new Color(ambientColor, ambientColor, ambientColor, 1f);
			yield return new WaitForEndOfFrame();
		}*/
		
		float fogDens = 0.06f;
		while (RenderSettings.fogDensity < 0.1f) {
			
			fogDens = Mathf.MoveTowards(fogDens, 0.1f, 0.0133f * Time.deltaTime);
			RenderSettings.fogDensity = fogDens;
			yield return new WaitForEndOfFrame();
		}
	}
}
