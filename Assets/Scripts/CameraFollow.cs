using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform target;

    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 prepareOffset;

    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(target.position.x, 0, target.position.z);
        Vector3 desiredPosition = targetPos + (GameManager.instance.State == GameState.Playing ? offset : prepareOffset);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;


        transform.LookAt(targetPos);
    }

}