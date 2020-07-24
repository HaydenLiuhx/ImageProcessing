using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler,IPointerEnterHandler, IPointerExitHandler
{
    public bool buttonPress;
    public Texture2D crosshair;

    public void OnPointerDown(PointerEventData eventData)
    {
        print("按下！！！！");
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        print("抬起！！！！");
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        print("dianji");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Vector2 cursorOffset = new Vector2(crosshair.width / 2, crosshair.height / 2);
        Cursor.SetCursor(null, cursorOffset, CursorMode.Auto);
        //throw new System.NotImplementedException();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector2 cursorOffset = new Vector2(crosshair.width / 2, crosshair.height / 2);
        Cursor.SetCursor(crosshair, cursorOffset, CursorMode.Auto);
        //throw new System.NotImplementedException();
    }
}
