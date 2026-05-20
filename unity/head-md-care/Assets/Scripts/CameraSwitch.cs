using System.Linq;
using UnityEngine;
// use new input system
using UnityEngine.EventSystems;


public class CameraSwitch : MonoBehaviour, IPointerClickHandler
{
    // take in parameter x cameras to switch between, if empty, switch between all cameras in the scene
    public Camera[] cameras;

    //detect a mouse click
    public void OnPointerClick(PointerEventData eventData)
    {
        // ensure we have a valid camera array; if inspector array is empty or contains only nulls, find scene cameras
        if (cameras == null || cameras.Length == 0 || cameras.All(c => c == null))
        {
            cameras = FindObjectsOfType<Camera>();
        }

        // filter out any null entries
        cameras = cameras.Where(c => c != null).ToArray();

        if (cameras.Length == 0)
        {
            return;
        }

        int currentIndex = -1;
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i].enabled)
            {
                currentIndex = i;
                break;
            }
        }

        // if no camera is enabled, enable the first one
        if (currentIndex == -1)
        {
            cameras[0].enabled = true;
            return;
        }

        int nextIndex = (currentIndex + 1) % cameras.Length;

        // if there's only one camera, keep it enabled
        if (nextIndex == currentIndex)
        {
            return;
        }

        cameras[currentIndex].enabled = false;
        cameras[nextIndex].enabled = true;
    }
}
