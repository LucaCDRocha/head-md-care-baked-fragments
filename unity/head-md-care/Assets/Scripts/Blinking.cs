using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Blinking : MonoBehaviour, IPointerClickHandler
{
    // make the object pulsating by changing its scale
    public float pulsateSpeed = 1f;
    public float pulsateAmount = 0.1f;
    private Vector3 originalScale;
    private bool keepPulsating = true;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (!keepPulsating) return;

        float scaleFactor = 1f + Mathf.Sin(Time.time * pulsateSpeed) * pulsateAmount;
        transform.localScale = originalScale * scaleFactor;
    }

    // stop pulsating when the object is clicked

    public void OnPointerClick(PointerEventData eventData)
    {
        keepPulsating = false;
        transform.localScale = originalScale; // reset to original scale
    }

}
