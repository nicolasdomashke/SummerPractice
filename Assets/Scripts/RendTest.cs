using UnityEngine;

public class RendTest : MonoBehaviour {
	
	Renderer rend;
    void Start() {
        
		rend = GetComponent<Renderer>();
    }

    void OnBecameVisible() {
        
		Debug.Log("visible");
    }
	
	void OnBecameInvisible() {
        
		Debug.Log("invisible");
    }
}
