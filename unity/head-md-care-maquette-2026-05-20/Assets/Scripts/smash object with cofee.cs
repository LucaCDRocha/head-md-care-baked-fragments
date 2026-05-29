using UnityEngine;
using UnityEngine.EventSystems;

public class smashobjectwithcofee : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private Vector3 moveOffset = new Vector3(0f, 0.18f, -0.08f);
    [SerializeField] private float xRotation = 20f;
    [SerializeField] private float holdDuration = 1f;
    [SerializeField] private float returnHoldDuration = 1f;
    [SerializeField] private float moveDuration = 0.45f;

    private bool isMoving;
    private bool firstReturn = true;
    private bool cameraChanged;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isMoving)
        {
            return;
        }

        _ = eventData;
        isMoving = true;
        StartCoroutine(MoveCup());
    }

    private System.Collections.IEnumerator MoveCup()
    {
        var start = transform.localPosition;
        var end = start + moveOffset;
        var startRotation = transform.localRotation;
        var endRotation = startRotation * Quaternion.Euler(xRotation, 0f, 0f);

        yield return MoveTo(end, startRotation, endRotation);
        yield return new WaitForSeconds(holdDuration);

        yield return MoveTo(start, endRotation, startRotation);

        yield return new WaitForSeconds(returnHoldDuration);

        if (firstReturn)
        {
            firstReturn = false;
        }

        isMoving = false;
    }

    private System.Collections.IEnumerator MoveTo(Vector3 target, Quaternion startRotation, Quaternion endRotation)
    {
        var start = transform.localPosition;
        var elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            var progress = Mathf.Clamp01(elapsed / moveDuration);
            transform.localPosition = Vector3.Lerp(start, target, progress);
            transform.localRotation = Quaternion.Slerp(startRotation, endRotation, progress);
            yield return null;
        }

        transform.localPosition = target;
        transform.localRotation = endRotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!firstReturn || cameraChanged)
        {
            return;
        }

        var cameraObject = GameObject.Find("Main Camera (1)");
        var cameraToUse = cameraObject != null ? cameraObject.GetComponent<Camera>() : null;
        if (cameraToUse == null)
        {
            return;
        }

        cameraToUse.targetDisplay = 1;
        cameraToUse.enabled = true;
        cameraChanged = true;
    }
}
