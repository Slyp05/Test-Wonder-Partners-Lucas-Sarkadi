using UnityEngine;

public class ConstantRotate2D : MonoBehaviour
{
    [SerializeField, Tooltip("Rotation speed in degrees per seconds.")] float rotationSpeed;

    new Transform transform;

    void Awake()
    {
        transform = base.transform;
    }

    void LateUpdate()
    {
        float angle = transform.eulerAngles.z;

        angle += rotationSpeed * Time.deltaTime;

        transform.localEulerAngles = new Vector3(transform.eulerAngles.x,
                                                 transform.eulerAngles.y,
                                                 angle);
    }
}
