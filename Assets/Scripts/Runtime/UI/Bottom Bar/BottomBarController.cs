using DG.Tweening;
using InspectorAttribute;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI component that manages the three <see cref="BottomBarElement"/>.
/// </summary>
public class BottomBarController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] UISettings settings;
    [Header("References")]
    [Tooltip("Child BottomBarElement for the left button.")]
    [ConditionalHide(HideCondition.IsPlaying, HideType.Readonly)]
    [SerializeField] BottomBarElement left;
    [Tooltip("Child BottomBarElement for the front button.")]
    [ConditionalHide(HideCondition.IsPlaying, HideType.Readonly)]
    [SerializeField] BottomBarElement front;
    [Tooltip("Child BottomBarElement for the right button.")]
    [ConditionalHide(HideCondition.IsPlaying, HideType.Readonly)]
    [SerializeField] BottomBarElement right;
    [Space]
    [Tooltip("Background RectTransform that we will move behind the selected button.")]
    [ConditionalHide(HideCondition.IsPlaying, HideType.Readonly)]
    [SerializeField] RectTransform selectedBackground;

    IReadOnlyDictionary<SelectedElement, BottomBarElement> elements;

    /// <summary>
    /// Called when the button selection has changed.
    /// </summary>
    public Action<SelectedElement> OnSelectionChange;

    void Awake()
    {
        elements = new Dictionary<SelectedElement, BottomBarElement>()
        {
            {SelectedElement.Left,  left },
            {SelectedElement.Front, front },
            {SelectedElement.Right, right },
        };
    }

    void Start()
    {
        MoveSelectedBackground(SelectedElement.Front, true);

        left.OnClick += () => ElementClicked(SelectedElement.Left);
        front.OnClick += () => ElementClicked(SelectedElement.Front);
        right.OnClick += () => ElementClicked(SelectedElement.Right);

        left.Unselect(true);
        front.Select(true);
        right.Unselect(true);
    }

    void ElementClicked(SelectedElement selected)
    {
        foreach (KeyValuePair<SelectedElement, BottomBarElement> kvPair in elements)
        {
            if (kvPair.Key == selected)
                kvPair.Value.Select();
            else
                kvPair.Value.Unselect();
        }

        MoveSelectedBackground(selected);

        OnSelectionChange?.Invoke(selected);
    }

    void MoveSelectedBackground(SelectedElement selected, bool instant = false)
    {
        Vector2 targetPos = elements[selected].RectTransform.anchoredPosition;
        Vector2 targetSize = elements[selected].RectTransform.sizeDelta;

        if (instant)
        {
            selectedBackground.anchoredPosition = targetPos;
            selectedBackground.sizeDelta = targetSize;
        }
        else
        {
            selectedBackground.DOAnchorPos(targetPos, settings.bottomBarElementTweenDuration);
            selectedBackground.DOSizeDelta(targetSize, settings.bottomBarElementTweenDuration);
        }
    }
}
