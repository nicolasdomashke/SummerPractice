using UnityEngine;

public class IntOrbShine : MonoBehaviour {
	
	public GameManager gameManager;
	Light shine;
    void Start() {
        
		shine = GetComponent<Light>();
    }

    void Update() {
		
        float intens = Mathf.Clamp((gameManager.mouseLook.transform.position - transform.position + new Vector3(0f, 50f, 0f)).magnitude / 10f, 5f, 100f);
		shine.intensity = intens * intens / 5;
    }
}
