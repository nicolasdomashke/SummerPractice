using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderEffectShaking : MonoBehaviour {

	float shift = 0f;
	private Texture texture;
	private Material material;
	float shiftRes = 0f;

	void Awake ()
	{
		material = new Material( Shader.Find("Hidden/Distortion") );
		texture = Resources.Load<Texture>("Checkerboard-big");
	}
		
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_ValueX", shift);
		material.SetTexture("_Texture", texture);
		Graphics.Blit (source, destination, material);
	}
	
	void Update() {
		
		if (shift == shiftRes) shiftRes = Random.Range(-.5f, .5f);
		shift = Mathf.MoveTowards(shift, shiftRes, 2f * Time.deltaTime);
	}
}
