using UnityEngine;

public class MouseLook : MonoBehaviour {
	
	public GameManager gameManager;
	public ShaderEffectShaking shader;
	public float mouseSensitivity = 100f;
	public float height = 1.3f;	
	public bool isCameraAtExecutioners;
	float xRotation = 0f;
	float xRotationLimit = 90f;
	float peekRange = .7f;
	float peekRotate = 15f;
	float peekSpeed = 10f;
	float xPosition;

    void Start() {
		
		Cursor.lockState = CursorLockMode.Locked;
		isCameraAtExecutioners = false;
		//shader = GetComponent<ShaderEffect_CorruptedVram>();
	}

    void Update() {
		
		if (gameManager.isControllerEnabled) {

			Vector3 vectorDiffrence = gameManager.executionerAI.executionerFace.position - transform.position;
			float lookAngle = Vector3.Angle(gameManager.mouseLook.transform.forward, vectorDiffrence);
			//Debug.Log(lookAngle);
			//SHOULD BE UNCOMENTED
			//isCameraAtExecutioners = 
			//	lookAngle < 60f
			//	&& !Physics.Linecast(transform.position, gameManager.executionerAI.executionerFace.position, gameManager.ignoranceMask)
			//	&& gameManager.executionerAI.state == ExecutionerAI.State.Chasing;

			float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
			float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
			//bool isLMBPressed = Input.GetMouseButton(0);
			//bool isRMBPressed = Input.GetMouseButton(1);
			//SHOULD BE UNCOMENTED
			/*if (isCameraAtExecutioners && !gameManager.executionerAI.isInvisible) {

				if (lookAngle < 10f) {
					
					mouseX = Mathf.Clamp(mouseX, -1f + lookAngle / 10f, 1f - lookAngle / 10f);
					mouseY = Mathf.Clamp(mouseY, -1f + lookAngle / 10f, 1f - lookAngle / 10f);
				}
				else {
					
					mouseX = 0f;
					mouseY = 0f;
					
				}
			}*/
			
			height -= gameManager.playerMovement.crouchFlag && gameManager.isCrouchEnabled ? 3f * Time.deltaTime : -3f * Time.deltaTime;
			height = Mathf.Clamp(height, .3f, 1.3f);
			/*if (isLMBPressed && gameManager.executionerBehavior.executionerState < 2 && !gameManager.intuitionManager.intuitionFlag) {
				
				xPosition -= peekSpeed * Time.deltaTime;
				xPosition = Mathf.Clamp(xPosition, -peekRange, peekRange);
				
				transform.localRotation = Quaternion.Euler(xRotation, 0f, -xPosition * peekRotate / peekRange);
				transform.localPosition = new Vector3(xPosition, height, .25f);
				
			}
			else if (isRMBPressed && gameManager.executionerBehavior.executionerState < 2 && !gameManager.intuitionManager.intuitionFlag) {
				
				xPosition += peekSpeed * Time.deltaTime;
				xPosition = Mathf.Clamp(xPosition, -peekRange, peekRange);
				
				transform.localRotation = Quaternion.Euler(xRotation, 0f, -xPosition * peekRotate / peekRange);
				transform.localPosition = new Vector3(xPosition, height, .25f);
			}
			if (xPosition  > 0) {
				
				xPosition -= peekSpeed * Time.deltaTime;
				xPosition = Mathf.Clamp(xPosition, 0, peekRange);
			}
			else if (xPosition < 0) {
				
				xPosition += peekSpeed * Time.deltaTime;
				xPosition = Mathf.Clamp(xPosition, -peekRange, 0);
			}*/
			xRotation -= mouseY;
			xRotation = Mathf.Clamp(xRotation, -xRotationLimit, xRotationLimit);
			
			
			transform.localPosition = new Vector3(xPosition, height, .25f);
			if (gameManager.executionerAI.state == ExecutionerAI.State.Chasing) {
				
				Vector2 shakeDistance = Random.insideUnitCircle * .01f;
				transform.localPosition += new Vector3(shakeDistance.x, shakeDistance.y, 0f);
				//shader.enabled = true;
			}
			//else shader.enabled = false;
			
			gameManager.playerMovement.transform.Rotate(Vector3.up * mouseX);
			if (isCameraAtExecutioners && !gameManager.executionerAI.isInvisible) CameraShake(vectorDiffrence); 
			
			transform.localRotation = Quaternion.Euler(xRotation, 0f, -xPosition * peekRotate / peekRange);
		}
    }
	
	void CameraShake(Vector3 vectorDiffrence) {
				
		float x = Quaternion.LookRotation(vectorDiffrence).eulerAngles.x;
		//Debug.Log(x);
		xRotation = Mathf.MoveTowards(xRotation, x < 180f ? x : x - 360, 100f * Time.deltaTime);
		
		gameManager.playerMovement.transform.rotation = Quaternion.RotateTowards(gameManager.playerMovement.transform.rotation, Quaternion.LookRotation(Vector3.ProjectOnPlane(gameManager.executionerAI.executionerFace.position - gameManager.playerMovement.transform.position, Vector3.up )), 100f * Time.deltaTime);
	}
}
