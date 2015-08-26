using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Button : MonoBehaviour {
	
	[SerializeField] private Texture image;
	private bool pressed = false;
	private bool downPressed = false;
	private Vector2 pointPressed;
	
	void Start () {
		downPressed = false;
		pressed = false;
	}
	// Update is called once per frame
	void Update () {
		downPressed = false;
		pressed = false;
		
		Rect rect = GetPixelInset(image);
		
		foreach(Touch touch in Input.touches) {
			Vector2 touchPosition = touch.position;
			touchPosition.y = Screen.height - touchPosition.y;
			if( rect.Contains(touchPosition) ) {
				if( touch.phase == TouchPhase.Began ) downPressed = true;
				pressed = true;
				pointPressed = touch.position;
				break;
			}
		}
		#if UNITY_EDITOR
		if(Input.GetMouseButton(0)) {
			Vector2 mouse = Input.mousePosition;
			mouse.y = Screen.height - mouse.y;
			if( rect.Contains(mouse) ) {
				if( Input.GetMouseButtonDown(0) ) downPressed = true;
				pressed = true;
				pointPressed = mouse;
			}
		}
		#endif
	}
	
	void OnGUI() {
		Color color = Color.white;
		if( !pressed ) color.a = 0.5f;
		GUI.color = color;
		if(image) GUI.DrawTexture( GetPixelInset(image), image );
	}
	
	private Rect GetPixelInset(Texture texture) {
		Vector2 position = GetCenter();
		Vector2 size = GetSize(texture);
		return new Rect( position.x-size.x/2.0f, position.y-size.y/2.0f, size.x, size.y );
	}
	
	private Vector2 GetCenter() {
		Vector3 position = transform.position;
		return new Vector2( position.x*Screen.width, position.y*Screen.height );
	}
	
	private Vector2 GetSize(Texture texture) {
		Vector3 scale = transform.localScale;
		float displaySize = (Screen.width + Screen.height)/2.0f;
		return new Vector2(displaySize*scale.x, displaySize*scale.y);
	}
	
	public bool IsDownPressed() {
		return downPressed;
	}
	
	public bool IsPressed() {
		return pressed;
	}
	
	public Vector2 GetPointPressed() {
		return pointPressed;
	}
	
}
