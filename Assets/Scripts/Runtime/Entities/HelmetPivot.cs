using System;
using UnityEngine;

public class HelmetPivot : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] TransformState leftTransformState;
    [Space]
    [SerializeField] TransformState frontTransformState;
    [Space]
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
