using DG.Tweening;
using InspectorAttribute;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI controller for the loading and failure screens.
/// </summary>
public class LoadAndFailController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] UISettings settings;
    [Header("References")]
    [Tooltip("UI graphical elements always displayed when loading or on failure.")]
    [ConditionalHide(HideCondition.IsPlaying, HideType.Readonly)]
    [SerializeField] Graphic[] generalGraphics;
    [Tooltip("UI graphical elements displayed only when loading.")]
    [ConditionalHide(HideCondition.IsPlaying, HideType.Readonly)]
    [SerializeField] Graphic[] loadingGraphics;
    [Tooltip("UI graphical elements displayed only on failure.")]
    [ConditionalHide(HideCondition.IsPlaying, HideType.Readonly)]
    [SerializeField] Graphic[] failureGraphics;
    [Space]
    [Tooltip("GameObject enabled when loading or on failure.")]
    [ConditionalHide(HideCondition.IsPlaying, HideType.Readonly)]
    [SerializeField] GameObject generalObj;
    [Tooltip("GameObject enabled only when loading.")]
    [ConditionalHide(HideCondition.IsPlaying, HideType.Readonly)]
    [SerializeField] GameObject loadingObj;
    [Tooltip("GameObject enabled only on failure.")]
    [ConditionalHide(HideCondition.IsPlaying, HideType.Readonly)]
    [SerializeField] GameObject failureObj;

    void Awake()
    {
        Show(generalGraphics, generalObj, true);
        Show(loadingGraphics, loadingObj, true);
        Hide(failureGraphics, failureObj, true);
    }

    void Hide(Graphic[] graphics, GameObject gameObject, bool instant = false)
    {
        if (instant)
        {
            foreach (Graphic g in graphics)
                g.color = new Color(g.color.r, g.color.g, g.color.b, 0);
            gameObject.SetActive(false);
        }
        else
        {
            foreach (Graphic g in graphics)
                g.DOFade(0, settings.loadingAndFailureTweenDuration)
                 .OnComplete(() => gameObject.SetActive(false));
        }
    }

    void Show(Graphic[] graphics, GameObject gameObject, bool instant = false)
    {
        gameObject.SetActive(true);

        if (instant)
        {
            foreach (Graphic g in graphics)
                g.color = new Color(g.color.r, g.color.g, g.color.b, 1);
        }
        else
        {
            foreach (Graphic g in graphics)
                g.DOFade(1, settings.loadingAndFailureTweenDuration);
        }
    }

    /// <summary>
    /// Call this method to hide the loading screen.
    /// </summary>
    public void Success()
    {
        Hide(generalGraphics, generalObj);
        Hide(loadingGraphics, loadingObj);
    }

    /// <summary>
    /// Call this method to display the failure screen.
    /// </summary>
    public void Failure()
    {
        Hide(loadingGraphics, loadingObj);
        Show(failureGraphics, failureObj);
    }
}
