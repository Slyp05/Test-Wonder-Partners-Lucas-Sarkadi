using UnityEngine;

/// <summary>
/// Manager component that loads some textures, apply them to a renderer, and notify <see cref="LoadAndFailController"/>.
/// </summary>
public class ObjectTexturesLoader : MonoBehaviour
{
    const int TextAreaMinLines = 1;
    const int TextAreaMaxLines = 10;

    [Header("Parameters")]
    [Tooltip("Where to fetch the main texture. This will be appended to the base Firebase URL.")]
    [TextArea(TextAreaMinLines, TextAreaMaxLines)]
    [SerializeField] string baseMapExtraUrl = "Material_MR (Base Color).png?alt=media&token=454bf4a2-cf19-4de2-bef6-47dce562a46a";
    [Tooltip("Where to fetch the emissive texture. This will be appended to the base Firebase URL.")]
    [TextArea(TextAreaMinLines, TextAreaMaxLines)]
    [SerializeField] string emissiveExtraUrl = "Material_MR (Emissive).png?alt=media&token=81bec0e1-3a05-4b59-a640-61b4bb528cc7";
    [Tooltip("Where to fetch the metallic and roughness texture. This will be appended to the base Firebase URL.")]
    [TextArea(TextAreaMinLines, TextAreaMaxLines)]
    [SerializeField] string metallicRougnessExtraUrl = "Material_MR (Metallic Roughness).png?alt=media&token=28146c1c-e58e-40ad-b21c-eed27de95ebd";
    [Tooltip("Where to fetch the normal texture. This will be appended to the base Firebase URL.")]
    [TextArea(TextAreaMinLines, TextAreaMaxLines)]
    [SerializeField] string normalExtraUrl = "Material_MR (Normal).png?alt=media&token=4450b7d1-9a16-4143-8007-2ea1ee6c351e";
    [Tooltip("Where to fetch the occlusion texture. This will be appended to the base Firebase URL.")]
    [TextArea(TextAreaMinLines, TextAreaMaxLines)]
    [SerializeField] string occlusionExtraUrl = "Material_MR (Occlusion).png?alt=media&token=91d2e1a6-50e0-4c0d-8bf0-fbf112a50ebc";
    [Space]
    [Tooltip("Should we convert the metallic/rougness texture format from (ARGB - R:Metallic, G:Roughness) to (RGBA - R:Metallic, A:Smoothness).")]
    [SerializeField] bool convertMetallicRougnessFromGLTFToUnityStandardFormat = true;
    [Space]
    [Tooltip("Renderer we should put the loaded textures on.")]
    [SerializeField] Renderer targetRenderer;
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
        Material newMaterial = new Material(targetRenderer.material);

        if (convertMetallicRougnessFromGLTFToUnityStandardFormat)
            textures[2] = TextureConverterUtility.ConvertMetallicRougnessFromGLTFToUnityStandard(textures[2] as Texture2D);

        StandardShaderUtility.AssignTexture(newMaterial, StandardShaderTextureType.BaseMap, textures[0]);
        StandardShaderUtility.AssignTexture(newMaterial, StandardShaderTextureType.Emissive, textures[1]);
        StandardShaderUtility.AssignTexture(newMaterial, StandardShaderTextureType.MetallicRoughness, textures[2]);
        StandardShaderUtility.AssignTexture(newMaterial, StandardShaderTextureType.Normal, textures[3]);
        StandardShaderUtility.AssignTexture(newMaterial, StandardShaderTextureType.Occlusion, textures[4]);

        targetRenderer.material = newMaterial;

        loadAndFail.Success();
    }

    void TextureDownloadFailure(string error)
    {
        Debug.LogError($"Could not load texture(s): {error}");

        loadAndFail.Failure();
    }
}
