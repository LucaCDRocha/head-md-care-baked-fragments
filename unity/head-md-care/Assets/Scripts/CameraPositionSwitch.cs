using UnityEngine;
// use new input system
using UnityEngine.EventSystems;

public class CameraPositionSwitch : MonoBehaviour, IPointerClickHandler
{
    //take in 1 camera and its old and new position x,y,z that are changed on clicking the object

    public Camera cameraToMove;
    public Vector3 newPosition;

    //detect a mouse click on object
    public void OnPointerClick(PointerEventData eventData)
    {
        // play audio source if it exists
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null) audioSource.Play();

       
        if (cameraToMove == null) return;
        // swap positions using tuple deconstruction
        (cameraToMove.transform.position, newPosition) = (newPosition, cameraToMove.transform.position);
    }


}
