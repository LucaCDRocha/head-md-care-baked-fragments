using UnityEngine;
// use event system
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IDragHandler
{
    // on drag, move the object to the position of the mouse
    public void OnDrag(PointerEventData eventData)
    {
        var cam = Camera.main;
        if (cam == null) return;
        // move object to mouse position while preserving its screen-space Z
        transform.position = cam.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y,
            cam.WorldToScreenPoint(transform.position).z));
    }

}
