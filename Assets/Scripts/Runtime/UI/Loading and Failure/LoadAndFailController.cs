using DG.Tweening;
using InspectorAttribute;
using UnityEngine;
using UnityEngine.UI;

public class LoadAndFailController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] UISettings settings;
    [Header("References")]
    [ConditionalHide(HideCondition.IsPlaying, HideType.Readonly)]
    [SerializeField] Graphic[] generalGraphics;
    [ConditionalHide(HideCondition.IsPlaying, HideType.Readonly)]
    [SerializeField] Graphic[] loadingGraphics;
    [ConditionalHide(HideCondition.IsPlaying, HideType.Readonly)]
    [SerializeField] Graphic[] failureGraphics;
    [Space]
    [ConditionalHide(HideCondition.IsPlaying, HideType.Readonly)]
    [SerializeField] GameObject generalObj;
    [ConditionalHide(HideCondition.IsPlaying, HideType.Readonly)]
    [SerializeField] GameObject loadingObj;
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

    public void Success()
    {
        Hide(generalGraphics, generalObj);
        Hide(loadingGraphics, loadingObj);
    }

    public void Failure()
    {
        Hide(loadingGraphics, loadingObj);
        Show(failureGraphics, failureObj);
    }
}
