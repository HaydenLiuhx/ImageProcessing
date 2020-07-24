using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDrag : MonoBehaviour
{
    float offsetX;
    float offsetY;
    public bool isClick;
    //public Texture2D crosshair;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BeginDrag()
    {
        offsetX = transform.position.x - Input.mousePosition.x;
        offsetY = transform.position.y - Input.mousePosition.y;

    }

    public void OnDrag()
    {
        transform.position = new Vector3(offsetX + Input.mousePosition.x, offsetY + Input.mousePosition.y);
        //Vector2 cursorOffset = new Vector2(crosshair.width / 2, crosshair.height / 2);
        //Cursor.SetCursor(crosshair, cursorOffset, CursorMode.Auto);
    }
    
}
