using UnityEngine;
// use event system
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IDragHandler
{
    private const string CylinderName = "Cylinder (4)";
    private const string CafeName = "Cafe";
    private const string SphereName = "Sphere (1)";

    // take a camera in parameter
    public Camera cam;
    // on drag, move the object to the position of the mouse
    public void OnDrag(PointerEventData eventData)
    {
        var cam = this.cam;
        if (cam == null) return;
        // move object to mouse position while preserving its screen-space Z
        transform.position = cam.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y,
            cam.WorldToScreenPoint(transform.position).z));
    }

    private void OnCollisionEnter(Collision collision)
    {
        TryTurnGreen(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        TryTurnGreen(other.gameObject);
    }

    private void TryTurnGreen(GameObject otherObject)
    {
        if (gameObject.name != CylinderName)
        {
            return;
        }

        if (otherObject.name != CafeName && otherObject.name != SphereName)
        {
            return;
        }

        var targetObject = otherObject.name == CafeName
            ? otherObject.transform.Find(SphereName)?.gameObject
            : otherObject;

        var renderer = targetObject != null ? targetObject.GetComponent<Renderer>() : null;
        if (renderer != null)
        {
            renderer.material.color = Color.green;
        }
    }

}
