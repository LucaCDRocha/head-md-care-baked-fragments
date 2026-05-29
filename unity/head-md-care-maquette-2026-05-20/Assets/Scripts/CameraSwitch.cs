using UnityEngine;
// use new input system
using UnityEngine.EventSystems;


public class CameraSwitch : MonoBehaviour, IPointerClickHandler
{
    // take in parameter x cameras to switch between, if empty, switch between all cameras in the scene
    public Camera[] cameras;

    public void Start()
    {
        // if inspector array is empty or contains only nulls, find scene cameras
        bool allNull = true;
        if (cameras == null || cameras.Length == 0)
        {
            allNull = true;
        }
        else
        {
            allNull = true;
            for (int i = 0; i < cameras.Length; i++)
            {
                if (cameras[i] != null)
                {
                    allNull = false;
                    break;
                }
            }
        }

        if (allNull)
        {
            cameras = FindObjectsByType<Camera>(FindObjectsSortMode.InstanceID);
        }

        // filter out any null entries
        if (cameras != null)
        {
            var list = new System.Collections.Generic.List<Camera>();
            foreach (var c in cameras)
            {
                if (c != null) list.Add(c);
            }
            cameras = list.ToArray();
        }

        // disable all but the first camera
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].enabled = false;
        }
        // after cameras = FindObjectsByType<Camera>(FindObjectsSortMode.InstanceID);
        foreach (var c in cameras) c.targetDisplay = 0; // Display 1 is index 0
    }

    //detect a mouse click
    public void OnPointerClick(PointerEventData eventData)
    {
        // mark parameter as used to avoid unused-parameter warnings
        _ = eventData;
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
