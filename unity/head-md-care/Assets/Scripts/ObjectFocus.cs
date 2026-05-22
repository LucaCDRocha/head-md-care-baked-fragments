using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectFocus : MonoBehaviour, IPointerClickHandler
{
    // take a camera in parameter
    public Camera cam;
    private Vector3 startPosition;
    private Quaternion startRotation;

    // on click, move the camera to the position of the object
    public void OnPointerClick(PointerEventData eventData)
    {
        cam ??= Camera.main;
        if (cam == null) return;

        if (startPosition == Vector3.zero)
        {
            startPosition = cam.transform.position;
            startRotation = cam.transform.rotation;
        }

        if (cam.transform.position == startPosition)
        {
            FocusOnObject();
        }
        else
        {
            if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
            transitionCoroutine = StartCoroutine(SmoothMove(cam.transform.position, cam.transform.rotation, startPosition, startRotation, transitionDuration));
        }
    }

    public float transitionDuration = 1f;
    private Coroutine transitionCoroutine;

    private void FocusOnObject()
    {
        Vector3 objectPosition = transform.position;
        Vector3 offset = transform.forward * -1f; // offset away from the object
        Vector3 targetPos = objectPosition + offset;
        Quaternion targetRot = Quaternion.LookRotation(objectPosition - targetPos);

        if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
        transitionCoroutine = StartCoroutine(SmoothMove(cam.transform.position, cam.transform.rotation, targetPos, targetRot, transitionDuration));
    }

    private System.Collections.IEnumerator SmoothMove(Vector3 fromPos, Quaternion fromRot, Vector3 toPos, Quaternion toRot, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(elapsed / duration));
            cam.transform.position = Vector3.Lerp(fromPos, toPos, t);
            cam.transform.rotation = Quaternion.Slerp(fromRot, toRot, t);
            yield return null;
        }
        cam.transform.position = toPos;
        cam.transform.rotation = toRot;
        transitionCoroutine = null;
    }
}
