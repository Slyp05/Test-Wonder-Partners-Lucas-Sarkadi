using System;
using UnityEngine;

public class HelmetPivot : MonoBehaviour
{
    [Header("Parameters")]
    [Tooltip("Values that will be applied to the Transform when selecting the left button.")]
    [SerializeField] TransformState leftTransformState;
    [Space]
    [Tooltip("Values that will be applied to the Transform when selecting the front button.")]
    [SerializeField] TransformState frontTransformState;
    [Space]
    [Tooltip("Values that will be applied to the Transform when selecting the right button.")]
    [SerializeField] TransformState rightTransformState;
    [Space]
    [SerializeField] float tweenDuration = 1f;

    new Transform transform;

    void Awake()
    {
        transform = base.transform;

        frontTransformState.ApplyToTransform(transform);
    }

    void Start()
    {
        FindObjectOfType<BottomBarController>().OnSelectionChange += SelectionChanged;
    }

    void SelectionChanged(SelectedElement selected)
    {
        TransformState targetState = selected switch
        {
            SelectedElement.Left    => leftTransformState,
            SelectedElement.Front   => frontTransformState,
            SelectedElement.Right   => rightTransformState,

            _ => throw new NotImplementedException(),
        };

        targetState.ApplyToTransform(transform, tweenDuration);
    }
}
