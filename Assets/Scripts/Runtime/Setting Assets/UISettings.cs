using UnityEngine;

/// <summary>
/// Asset (<see cref="ScriptableObject"/>) that contains common UI settings.
/// </summary>
// [CreateAssetMenu(fileName = "UI Settings", menuName = "Settings Assets/UI Settings")]
public class UISettings : ScriptableObject
{
    [field: Header("Colors")]
    [field: SerializeField, Tooltip("Buttons icon and text color when selected.")]
    public Color bottomBarElementSelectedColor { get; private set; }
    [field: SerializeField, Tooltip("Buttons icon and text color when NOT selected.")]
    public Color bottomBarElementUnselectedColor { get; private set; }

    [field: Header("Timings")]
    [field: SerializeField, Tooltip("Time to animate from one button selection state to another.")]
    public float bottomBarElementTweenDuration { get; private set; }
    [field: SerializeField, Tooltip("Time to animate screen transitions on load success or failure.")]
    public float loadingAndFailureTweenDuration { get; private set; }
}
