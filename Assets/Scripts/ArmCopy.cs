using UnityEngine;

public class ArmCopy : MonoBehaviour {
    
	public bool armsClone = false;
	Transform [] parts;
	
	void OnValidate () {
		
		if (armsClone) {
			
			parts = GetComponentsInChildren<Transform>();
			foreach (Transform part in parts) {
				
				part.position -= new Vector3(0f, 0f, part.position.z * 2f);
				part.rotation = Quaternion.Euler(part.rotation.eulerAngles.x, 180f - part.rotation.eulerAngles.y, part.rotation.eulerAngles.z);
			}
		}
	}
}
