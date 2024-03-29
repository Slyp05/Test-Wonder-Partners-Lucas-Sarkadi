using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HelmetTexturesLoader : MonoBehaviour
{
    const int TextAreaMinLines = 1;
    const int TextAreaMaxLines = 10;

    [Header("Parameters")]
    [TextArea(TextAreaMinLines, TextAreaMaxLines)]
    [SerializeField] string baseMapExtraUrl = "Material_MR (Base Color).png?alt=media&token=454bf4a2-cf19-4de2-bef6-47dce562a46a";
    [TextArea(TextAreaMinLines, TextAreaMaxLines)]
    [SerializeField] string emissiveExtraUrl = "Material_MR (Emissive).png?alt=media&token=81bec0e1-3a05-4b59-a640-61b4bb528cc7";
    [TextArea(TextAreaMinLines, TextAreaMaxLines)]
    [SerializeField] string metallicRougnessExtraUrl = "Material_MR (Metallic Roughness).png?alt=media&token=28146c1c-e58e-40ad-b21c-eed27de95ebd";
    [TextArea(TextAreaMinLines, TextAreaMaxLines)]
    [SerializeField] string normalExtraUrl = "Material_MR (Normal).png?alt=media&token=4450b7d1-9a16-4143-8007-2ea1ee6c351e";
    [TextArea(TextAreaMinLines, TextAreaMaxLines)]
    [SerializeField] string occlusionExtraUrl = "Material_MR (Occlusion).png?alt=media&token=91d2e1a6-50e0-4c0d-8bf0-fbf112a50ebc";
    [Header("Settings")]
    [SerializeField] WebRequestSettings settings;

    LoadAndFailController loadAndFail;

    void Awake()
    {
        loadAndFail = FindObjectOfType<LoadAndFailController>();
    }

    void Start()
    {
        string[] urls = new string[]
        {
            settings.FirebaseURL + baseMapExtraUrl,
            settings.FirebaseURL + emissiveExtraUrl,
            settings.FirebaseURL + metallicRougnessExtraUrl,
            settings.FirebaseURL + normalExtraUrl,
            settings.FirebaseURL + occlusionExtraUrl,
        };

        _ = TextureDownloader.GetTexturesAsync(urls, TextureDownloadSuccess, TextureDownloadFailure);
    }

    void TextureDownloadSuccess(Texture[] textures)
    {
        //// apply to helmet

        loadAndFail.Success();
    }

    void TextureDownloadFailure(string error)
    {
        Debug.LogError($"Could not load texture(s): {error}");

        loadAndFail.Failure();
    }
}
