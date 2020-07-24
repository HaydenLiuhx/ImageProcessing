using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventTriggerHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isClick;
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        EventTrigger trigger = btn.gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        // 鼠标点击事件
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(OnClick);
        trigger.triggers.Add(entry);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnClick(BaseEventData baseEventData)
    {
        isClick = true;
        Debug.Log("isClick: " + isClick);
    }
}
