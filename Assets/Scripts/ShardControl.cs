using UnityEngine;
using System.Collections;

public class ShardControl : MonoBehaviour {

	public GameManager gameManager;
	public GameObject shard;
	public float shardSensivity = 130f;
	public bool isShardActive = false;
	float shardY;
	float limitY;
	float shardPosX;
	float limitPosX;
	float shardRotation;
	float shardShakingX = 0f;
	float shardShakingY = 0f;
	float shardRotating = 0f;
	int shakingDirection = 1;
	int rotatingDirection = 1;
	bool shardPosition = true;
	Vector3 offsetRotation;
	Vector3 offsetPosition;
	
	void Start() {
		
		offsetPosition = shard.transform.localPosition;
		offsetRotation = shard.transform.localRotation.eulerAngles;
		limitY = offsetRotation.y;
		shardY =  limitY;
		limitPosX = shard.transform.localPosition.x;
		shardPosX = limitPosX;
		shardRotation = shard.transform.localRotation.z;
	}

    void Update() {
		
        bool isShardToggled = Input.GetKeyDown("left ctrl");
			
		if (isShardToggled && gameManager.mainCamera.enabled && gameManager.isShardEnabled) {
			isShardActive = !isShardActive;
			shard.SetActive(isShardActive);
		}
		if (shard.activeSelf) {
			
			float shardZ = Input.GetAxis("Shard Axis") * shardSensivity * Time.deltaTime;
			bool isShardLeftside = Input.GetKeyDown("r");
			
			if (isShardLeftside) shardPosition = !shardPosition;
			if (shardPosition) {
				
				limitY = offsetRotation.y;
				limitPosX = -.42f;
			}
			else {
				
				limitY = 360f - offsetRotation.y;
				limitPosX = .42f;
			}
			shardY = Mathf.MoveTowards(shardY, limitY, 3f * (360f - 2f * offsetRotation.y) * Time.deltaTime);
			shardPosX = Mathf.MoveTowards(shardPosX, limitPosX, 3f * .84f * Time.deltaTime);
			int ratio = shardPosX <= 0f ? 1 : -1;
			shardRotation = Mathf.Clamp(shardRotation + ratio * shardZ, 0f, 83.75f);
			//shardRotation += ratio * shardZ;
			
			shard.transform.localPosition = new Vector3(shardPosX + ShardShaking(gameManager.playerMovement.vectorSpeed.magnitude), offsetPosition.y + Mathf.Abs(shardShakingX) + .005f * Mathf.Sin(shardShakingY), offsetPosition.z);
			shard.transform.localRotation = Quaternion.Euler(offsetRotation.x + .5f * Mathf.Sin(shardRotating), shardY, ratio * shardRotation * Mathf.Abs(shardPosX / limitPosX) + .5f * Mathf.Sin(shardRotating));
			//Debug.Log(shardRotating);
		}
    }
	
	float ShardShaking (float playerSpeed) {
		
		float rotatingLimit = 1.5707f;
		shardRotating = Mathf.MoveTowards(shardRotating, rotatingLimit * rotatingDirection, .75f * rotatingLimit * Time.deltaTime);
		if (Mathf.Abs(shardRotating) >= rotatingLimit) rotatingDirection *= -1;
		if (playerSpeed > .01f) {
			
			float shakingLimitX = .005f * playerSpeed;
			shardShakingX = Mathf.MoveTowards(shardShakingX, shakingLimitX * shakingDirection, 4f * shakingLimitX * Time.deltaTime);
			shardShakingX = Mathf.Clamp(shardShakingX, -shakingLimitX, shakingLimitX);
			shardShakingY = Mathf.MoveTowards(shardShakingY, 0f, .78535f * Time.deltaTime);
			if (Mathf.Abs(shardShakingX) >= shakingLimitX) shakingDirection *= -1;
			return shardShakingX;
		}
		else {
			
			float shakingLimitY = 1.5707f;
			shardShakingY = Mathf.MoveTowards(shardShakingY, shakingLimitY * shakingDirection, .75f * shakingLimitY * Time.deltaTime);
			if (Mathf.Abs(shardShakingY) >= shakingLimitY) shakingDirection *= -1;
			return 0f;
		}
	}
}