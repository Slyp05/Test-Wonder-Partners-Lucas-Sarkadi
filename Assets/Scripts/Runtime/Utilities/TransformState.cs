using DG.Tweening;
using System;
using UnityEngine;

[Serializable]
public class TransformState
{
    [SerializeField, Tooltip("Local position.")] Vector3 position;
    [SerializeField, Tooltip("Local euler angles (rotation).")] Vector3 eulerAngles;
    [SerializeField, Tooltip("Local scale.")] Vector3 scale;

    public Vector3 Position => position;
    public Vector3 EulerAngles => eulerAngles;
    public Vector3 Scale => scale;

    public void ApplyToTransform(Transform target, float duration)
    {
        target.DOLocalMove(position, duration);
        target.DOLocalRotate(eulerAngles, duration, RotateMode.Fast);
        target.DOScale(scale, duration);
    }

    public void ApplyToTransform(Transform target)
    {
        target.localPosition = position;
        target.localEulerAngles = eulerAngles;
        target.localScale = scale;
    }
}
