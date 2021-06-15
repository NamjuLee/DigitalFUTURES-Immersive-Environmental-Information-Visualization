using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMouseEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    // https://www.google.com/search?q=unity+mouse+down+event&oq=unity+mouse+down&aqs=chrome.1.69i57j0j0i22i30l3j0i10i22i30l2j0i22i30l3.10922j1j7&sourceid=chrome&ie=UTF-8 
    // OnMouseDown	OnMouseDown is called when the user has pressed the mouse button while over the Collider.
    // OnMouseDrag	OnMouseDrag is called when the user has clicked on a Collider and is still holding down the mouse.
    // OnMouseEnter	Called when the mouse enters the Collider.
    // OnMouseExit	Called when the mouse is not any longer over the Collider.
    // OnMouseOver	Called every frame while the mouse is over the Collider.
    // OnMouseUp	OnMouseUp is called when the user has released the mouse button.
    // OnMouseUpAsButton	OnMouseUpAsButton is only called when the mouse is released over the same Collider as it was pressed.

    void OnMouseDown() {
        Debug.Log(this.gameObject.name.ToString());
        Debug.Log(this.gameObject.name.ToString() + ", Down");
    }
    void OnMouseEnter() {
        Debug.Log(this.gameObject.name.ToString() + ", Entered");
    }
    void OnMouseUp() {
        Debug.Log(this.gameObject.name.ToString());
        Debug.Log(this.gameObject.name.ToString() + ", Up");

        this.transform.localScale = new Vector3(this.transform.localScale.x + 0.1f, this.transform.localScale.y + 0.1f, this.transform.localScale.z + 0.1f);
    }
}
