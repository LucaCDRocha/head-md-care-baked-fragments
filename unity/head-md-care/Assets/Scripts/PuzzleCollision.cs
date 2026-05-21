using UnityEngine;
// use new input system
using UnityEngine.EventSystems;

public class PuzzleCollision : MonoBehaviour
{
    //if the object collides with another object, make one random object out of 4 visible

    // take objects to show as an array in a parameter
    public GameObject[] objectsToShow;

    public void Start()
    {
        // disable all objects in the array at the start
        if (objectsToShow != null)
        {
            foreach (var obj in objectsToShow)
            {
                if (obj != null)
                {
                    MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
                    if (renderer != null) renderer.enabled = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // make one object in the array visible when colliding and if there is another collide it reavels the next one
        if (objectsToShow != null)
        {
            foreach (var obj in objectsToShow)
            {
                if (obj != null)
                {
                    MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
                    if (renderer != null && !renderer.enabled)
                    {
                        renderer.enabled = true;
                        break; // exit the loop after enabling one object
                    }
                }
            }
        }

    }

}
