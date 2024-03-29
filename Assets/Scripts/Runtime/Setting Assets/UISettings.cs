using UnityEngine;

// [CreateAssetMenu(fileName = "UI Settings", menuName = "Settings Assets/UI Settings")]
public class UISettings : ScriptableObject
{
    [field: Header("Colors")]
    [field: SerializeField]
    public Color bottomBarElementSelectedColor { get; private set; }
    [field: SerializeField]
    public Color bottomBarElementUnselectedColor { get; private set; }

    [field: Header("Timings")]
    [field: SerializeField]
    public float bottomBarElementTweenDuration { get; private set; }
    [field: SerializeField]
    public float loadingAndFailureTweenDuration { get; private set; }
}
