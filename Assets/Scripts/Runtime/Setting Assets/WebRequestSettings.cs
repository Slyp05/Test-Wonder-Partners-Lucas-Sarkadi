using UnityEngine;

// [CreateAssetMenu(fileName = "Web Request Settings", menuName = "Settings Assets/Web Request Settings")]
public class WebRequestSettings : ScriptableObject
{
    [field: Header("URLs")]
    [field: SerializeField, TextArea(1, 10)]
    public string FirebaseURL { get; private set; }
}