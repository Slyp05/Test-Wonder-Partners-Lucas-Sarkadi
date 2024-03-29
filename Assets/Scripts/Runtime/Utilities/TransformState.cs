using DG.Tweening;
using System;
using UnityEngine;

/// <summary>
/// Define a <see cref="Transform"/> local position/euler angles/scale snapshot that we can go back to.
/// </summary>
[Serializable]
public class TransformState
{
    [SerializeField, Tooltip("Local position.")] Vector3 position;
    [SerializeField, Tooltip("Local euler angles (rotation).")] Vector3 eulerAngles;
    [SerializeField, Tooltip("Local scale.")] Vector3 scale;

    public Vector3 Position => position;
    public Vector3 EulerAngles => eulerAngles;
    public Vector3 Scale => scale;

    /// <summary>
    /// Tween the <paramref name="target"/> local position/euler angles/scale to the serialized values in <paramref name="duration"/> seconds.
    /// </summary>
    public void ApplyToTransform(Transform target, float duration)
    {
        target.DOLocalMove(position, duration);
        target.DOLocalRotate(eulerAngles, duration, RotateMode.FastBeyond360);
        target.DOScale(scale, duration);
    }

    /// <summary>
    /// Set the <paramref name="target"/> local position/euler angles/scale to the serialized values.
    /// </summary>
    public void ApplyToTransform(Transform target)
    {
        target.localPosition = position;
        target.localEulerAngles = eulerAngles;
        target.localScale = scale;
    }
}
