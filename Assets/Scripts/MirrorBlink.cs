using UnityEngine;
using UnityEngine.UI;

public class MirrorBlink : MonoBehaviour {
	
	public GameManager gameManager;
	public RectTransform localRectTransform;
	public Image localImage;
	Image image;
	RectTransform rectTransform;
	
	void Start() {
		
		rectTransform = gameManager.blinkingUI.GetComponent(typeof(RectTransform)) as RectTransform;
		image = gameManager.blinkingUI.GetComponent(typeof(Image)) as Image;
	}
	
	void Update() {
		
		localRectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -rectTransform.anchoredPosition.y);
		localImage.color = image.color;
	}
}
