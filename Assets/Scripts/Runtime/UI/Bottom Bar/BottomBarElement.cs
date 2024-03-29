using DG.Tweening;
using InspectorAttribute;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI component that operate a single bottom bar button.
/// </summary>
public class BottomBarElement : MonoBehaviour
{
    static readonly Vector3 ZeroXScale = new Vector3(0, 1, 1);

    [Header("Settings")]
    [SerializeField] UISettings settings;
    [Header("References")]
    [Tooltip("Child icon Image component.")]
    [ConditionalHide(HideCondition.IsPlaying, HideType.Readonly)]
    [SerializeField] Image icon;
    [Tooltip("Child text TextMeshProUGUI component.")]
    [ConditionalHide(HideCondition.IsPlaying, HideType.Readonly)]
    [SerializeField] TextMeshProUGUI text;

    Button button;

    float iconBaseX;

    public RectTransform RectTransform { get; private set; }

    /// <summary>
    /// Called when this button is clicked by the user.
    /// </summary>
    public event Action OnClick;

    void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(ButtonClicked);

        iconBaseX = icon.rectTransform.anchoredPosition.x;

        RectTransform = transform as RectTransform;
    }

    void ButtonClicked()
    {
        OnClick?.Invoke();
    }

    /// <summary>
    /// Set this button display to the selected state.
    /// </summary>
    public void Select(bool instant = false)
    {
        button.interactable = false;

        if (instant)
        {
            text.color = settings.bottomBarElementSelectedColor;
            icon.color = settings.bottomBarElementSelectedColor;

            text.rectTransform.localScale = Vector3.one;
            icon.rectTransform.anchoredPosition = new Vector2(iconBaseX, icon.rectTransform.anchoredPosition.y);
        }
        else
        {
            text.DOColor(settings.bottomBarElementSelectedColor, settings.bottomBarElementTweenDuration);
            icon.DOColor(settings.bottomBarElementSelectedColor, settings.bottomBarElementTweenDuration);

            text.rectTransform.DOScaleX(1, settings.bottomBarElementTweenDuration);
            icon.rectTransform.DOAnchorPosX(iconBaseX, settings.bottomBarElementTweenDuration);
        }
    }

    /// <summary>
    /// Set this button display to the NOT selected state.
    /// </summary>
    public void Unselect(bool instant = false)
    {
        button.interactable = true;

        if (instant)
        {
            text.color = settings.bottomBarElementUnselectedColor;
            icon.color = settings.bottomBarElementUnselectedColor;

            text.rectTransform.localScale = ZeroXScale;
            icon.rectTransform.anchoredPosition = new Vector2(0, icon.rectTransform.anchoredPosition.y);
        }
        else
        {
            text.DOColor(settings.bottomBarElementUnselectedColor, settings.bottomBarElementTweenDuration);
            icon.DOColor(settings.bottomBarElementUnselectedColor, settings.bottomBarElementTweenDuration);

            text.rectTransform.DOScaleX(0, settings.bottomBarElementTweenDuration);
            icon.rectTransform.DOAnchorPosX(0, settings.bottomBarElementTweenDuration);
        }
    }
}
