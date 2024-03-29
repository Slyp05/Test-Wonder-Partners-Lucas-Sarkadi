using UnityEngine;

// [CreateAssetMenu(fileName = "Web Request Settings", menuName = "Settings Assets/Web Request Settings")]
public class WebRequestSettings : ScriptableObject
{
    [field: Header("URLs")]
    [field: SerializeField, TextArea(1, 10), Tooltip("Firebase URL common to all requests, should include a '/' at the end.")]
    public string FirebaseURL { get; private set; }
}