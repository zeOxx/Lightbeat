using UnityEngine;
using System.Collections;

public class MouseCursor : MonoBehaviour {

    public Texture cursorImage;
     
    void Start() {
    	Screen.showCursor = false;
    }
     
    void OnGUI() {		
		GUI.DrawTexture(new Rect(Input.mousePosition.x - cursorImage.width/2, Screen.height - Input.mousePosition.y - cursorImage.height/2, cursorImage.width, cursorImage.height), cursorImage);
    }
}
