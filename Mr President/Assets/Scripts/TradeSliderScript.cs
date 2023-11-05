using UnityEngine;
using UnityEngine.EventSystems;

public class TradeSliderScript : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = transform.localPosition;
        transform.localPosition = new Vector3(Mathf.Clamp(pos.x + eventData.delta.x, -225, 225), pos.y, pos.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 pos = transform.localPosition;
        if (pos.x > -225 && pos.x < 225)
        {
            transform.localPosition = new Vector3(-225, pos.y, pos.z);
        }
        if (pos.x == 225)
        {
            GameObject invokerObject = GameObject.Find("GameManager");
            TradeScript invoker = invokerObject.GetComponent<TradeScript>();
            invoker.OnBoughtSoldEvent();
        }
    }
}
