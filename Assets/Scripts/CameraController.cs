using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private Vector2 offset = Vector2.zero;
    [SerializeField]
    public Vector2 minVal = Vector2.zero;
    [SerializeField]
    public Vector2 maxVal = Vector2.zero;
    [SerializeField]
    private Camera cam;

    public Transform destinationTransform;

    public void ClampPos() {
        Vector3 destinationPosition = destinationTransform.position + new Vector3(offset.x, offset.y, 0);
        destinationPosition.x = Mathf.Clamp(destinationPosition.x, minVal.x, maxVal.x);
        destinationPosition.y = Mathf.Clamp(destinationPosition.y, minVal.y, maxVal.y);
        destinationPosition.z = cam.transform.position.z;
        cam.transform.position = destinationPosition;
    }

    private void FixedUpdate() {
        if(destinationTransform == null)
            return;

        Vector3 destinationPosition = destinationTransform.position + new Vector3(offset.x, offset.y, 0);
        destinationPosition.x = Mathf.Clamp(destinationPosition.x, minVal.x, maxVal.x);
        destinationPosition.y = Mathf.Clamp(destinationPosition.y, minVal.y, maxVal.y);
        destinationPosition.z = cam.transform.position.z;
        cam.transform.position = Vector3.Lerp(cam.transform.position, destinationPosition, speed * Time.deltaTime);
    }
}
