using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public GameManager gameManager;
	CharacterController controller;
	public float speed  = 4f;
	float velocity = 100f;
	float playerSpeed;
	public Vector3 vectorSpeed = new Vector3(0f, 0f, 0f);
	public bool crouchFlag = false;
	
	void Start () {
		
		controller = GetComponent<CharacterController>();
		playerSpeed = speed;
	}
    void Update() {
		
        if (gameManager.isControllerEnabled) {
			
			float horizontalMove = Input.GetAxis("Horizontal");
			float verticalMove = Input.GetAxis("Vertical");
			bool isShiftToggled = Input.GetKeyDown("c");
			if (gameManager.isCrouchEnabled && isShiftToggled) crouchFlag = !crouchFlag;
			//playerSpeed = Mathf.Clamp(playerSpeed, 1f, crouchFlag ? .5f * speed : speed);
			playerSpeed += (gameManager.mouseLook.isCameraAtExecutioners && !gameManager.executionerAI.isInvisible) ? -5f * Time.deltaTime : .5f * Time.deltaTime;
			playerSpeed = Mathf.Clamp(playerSpeed, .2f, speed);
			Vector3 playerMove = transform.right * horizontalMove + transform.forward * verticalMove;
			playerMove = Vector3.ClampMagnitude(playerMove, 1f);
			vectorSpeed = playerMove;
			
			controller.Move(playerMove * playerSpeed * Time.deltaTime);
			controller.Move(Vector3.down * velocity * Time.deltaTime);
			//Debug.Log(playerSpeed);
		}
    }
}
