/// Uncomment the line below to check the script in build.
// #undef UNITY_EDITOR

using System;
using UnityEngine;

/// <summary>
/// Simple component that moves the main <see cref="Camera"/> to ensure that a target is always fully in view.
/// </summary>
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class CameraFocus : MonoBehaviour
{
    [Flags]
    enum RefreshMode
    {
        None            = 0,

        OnStart         = 1 << 0,
        OnEditorUpdate  = 1 << 1,
        OnRuntimeUpdate = 1 << 2,

        All             = ~0,
    }

    [Tooltip("Target renderer to focus.")]
    [SerializeField] Renderer target;
    [Tooltip("Extra space ratio to have around the focused object.")]
    [SerializeField] float marginRatio = 1.1f;
    [Tooltip("When should we refresh the camera position to account for screen format or object position changes.")]
    [SerializeField] RefreshMode refreshMode = RefreshMode.OnStart;

    new Camera camera;

    void Awake()
    {
        camera = Camera.main;
    }

    void Start()
    {
        if (refreshMode.HasFlag(RefreshMode.OnStart))
            FocusTarget();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Application.isPlaying == false)
        {
            if (refreshMode.HasFlag(RefreshMode.OnEditorUpdate))
                FocusTarget();
        }
        else
#endif
        {
            if (refreshMode.HasFlag(RefreshMode.OnRuntimeUpdate))
                FocusTarget();
        }
    }

    void FocusTarget()
    {
        Bounds bounds = target.bounds;

        Vector3 centerAtFront = new(bounds.center.x, bounds.max.y, bounds.center.z);
        Vector3 centerAtFrontTop = new(bounds.center.x, bounds.max.y, bounds.max.z);
        float centerToTopDist = (centerAtFrontTop - centerAtFront).magnitude;
        float minDistanceY = centerToTopDist * marginRatio / Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);

        Vector3 centerAtFrontRight = new(bounds.max.x, bounds.center.y, bounds.max.z);
        float centerToRightDist = (centerAtFrontRight - centerAtFront).magnitude;
        float minDistanceX = centerToRightDist * marginRatio / Mathf.Tan(camera.fieldOfView * camera.aspect * Mathf.Deg2Rad);

        float minDistance = Mathf.Max(minDistanceX, minDistanceY);

        camera.transform.position = new Vector3(0, 0, bounds.center.z - minDistance);
    }
}
