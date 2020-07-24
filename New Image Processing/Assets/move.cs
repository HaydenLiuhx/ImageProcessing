using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class move : MonoBehaviour
{
    public GameObject red;
    public List<GameObject> clonePoints;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Sk().Count<2)
            {
                GameObject g = Instantiate(red, Input.mousePosition, new Quaternion(0, 0, 0, 0));
                // 注意：Resources.Load()只能在Resources文件夹下读取，别的不行，所以该路径名默认在Asset/Resources下，且texture不要加文件后缀
                g.GetComponent<ButtonListener>().crosshair = (Texture2D)(Resources.Load("Nice-crosshair"));
                //g.GetComponent<UIDrag>().crosshair = (Texture2D)(Resources.Load("Nice-crosshair"));
                //g.transform.SetParent = GameObject.Find("Canvas").gameObject.transform;
                g.transform.SetParent(GameObject.Find("Canvas").gameObject.transform);
                g.layer = 0;
                
                clonePoints.Add(g);
                GameObject.Find("RawImage").GetComponent<setPixel>().objs = clonePoints;
            }

            //for (int i = 0; i < clonePoints.Count; i++)
            //{
            //    for (int j = -2; j < 3; j++)
            //    {
            //        Cursor.SetCursor(crosshair, new Vector2(clonePoints[i].transform.position.x + j, clonePoints[i].transform.position.y + j), CursorMode.Auto);
            //    }
            //}
           


        }
       
        if (Input.GetKeyDown(KeyCode.A))
        {
            for(int i = 0; i < clonePoints.Count; i++)
            {
                Debug.Log(clonePoints[i].transform.position);
            }
        }

    }
    private bool IsTouchedUI()
    {
        bool touchedUI = false || EventSystem.current.IsPointerOverGameObject();
        return touchedUI;
    }
    
    public List<GameObject> Sk()
    {
        //GameObject[] obj = null;
        List<GameObject> obj = new List<GameObject>();


        GraphicRaycaster[] graphicRaycasters = FindObjectsOfType<GraphicRaycaster>();

        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.pressPosition = Input.mousePosition;
        eventData.position = Input.mousePosition;
        List<RaycastResult> list = new List<RaycastResult>();

        foreach (var item in graphicRaycasters)
        {
            item.Raycast(eventData, list);
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    obj.Add(list[i].gameObject);
                }
            }
        }

        return obj;
    }
    
    //public GameObject Skode_GetCurrentSelect()
    //{
    //    GameObject obj = null;


    //    GraphicRaycaster[] graphicRaycasters = FindObjectsOfType<GraphicRaycaster>();

    //    PointerEventData eventData = new PointerEventData(EventSystem.current);
    //    eventData.pressPosition = Input.mousePosition;
    //    eventData.position = Input.mousePosition;
    //    List<RaycastResult> list = new List<RaycastResult>();

    //    foreach (var item in graphicRaycasters)
    //    {
    //        item.Raycast(eventData, list);
    //        if (list.Count > 0)
    //        {
    //            for (int i = 0; i < list.Count; i++)
    //            {
    //                obj = list[i].gameObject;
    //            }
    //        }
    //    }

    //    return obj;
    //}
}