using UnityEngine;
using UnityEngine.WSA;

public class DynamicCameraSwitch : MonoBehaviour
{
    public GameObject targetObject;  // The object that the camera will follow
    public Collider activationZone;  // Collider that triggers the camera switch
    public GameObject primaryCamera;  // The regular gameplay camera
    public GameObject trackingCamera;  // The camera that follows the target

    private bool isTracking = false;  // Flag to check if the camera is currently tracking the target

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BowlingBall"))  // Make sure the collider tag is set correctly
        {
            SwitchToTrackingCamera();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BowlingBall"))
        {
            ReturnToPrimaryCamera();
        }
    }

    private void LateUpdate()
    {
        if (isTracking && targetObject != null)
        {
            TrackTarget();
        }
    }

    private void SwitchToTrackingCamera()
    {
        Debug.Log("Switching to tracking camera.");
        primaryCamera.SetActive(false);
        trackingCamera.SetActive(true);
        isTracking = true;
    }

    private void ReturnToPrimaryCamera()
    {
        primaryCamera.SetActive(true);
        trackingCamera.SetActive(false);
        isTracking = false;
    }

    private void TrackTarget()
    {
        Vector3 cameraOffset = new Vector3(0f, 2f, -5f);  // Set the desired offset
        trackingCamera.transform.position = targetObject.transform.position + cameraOffset;
        trackingCamera.transform.LookAt(targetObject.transform.position);
    }
}
