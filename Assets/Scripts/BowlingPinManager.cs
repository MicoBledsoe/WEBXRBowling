using UnityEngine;
//My Directives

public class BowlingPinManager : MonoBehaviour
{
    private bool isKnockedOver = false;
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        if (!isKnockedOver && HasSignificantlyMovedOrRotated())
        {
            isKnockedOver = true;
            GameManager.Instance.PinKnockedOver();
        }
    }

    bool HasSignificantlyMovedOrRotated()
    {
        float positionChangeThreshold = 0.1f; // Minimum significant position change
        float rotationChangeThreshold = 5.0f; // Minimum significant rotation change (in degrees)

        bool positionChanged = Vector3.Distance(transform.position, startPosition) > positionChangeThreshold; //current obj
        bool rotationChanged = Quaternion.Angle(transform.rotation, startRotation) > rotationChangeThreshold; //curremt obj 

        return positionChanged || rotationChanged;
    }

}
