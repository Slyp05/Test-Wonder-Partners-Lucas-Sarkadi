using UnityEngine;

/// <summary>
/// Utility dedicated to converting a <see cref="Texture2D"/> from a format to another.
/// </summary>
public static class TextureConverterUtility
{
    /// <summary>
    /// Convert the given <paramref name="metallicRougnessTexture"/> format from (ARGB - R:Metallic, G:Roughness) to (RGBA - R:Metallic, A:Smoothness).
    /// </summary>
    public static Texture2D ConvertMetallicRougnessFromGLTFToUnityStandard(Texture2D metallicRougnessTexture)
    {
        Texture2D convertedTexture = new Texture2D(metallicRougnessTexture.width, metallicRougnessTexture.height, TextureFormat.RGBA32, false);

        Color32[] originalPixels = metallicRougnessTexture.GetPixels32();
        Color32[] convertedPixels = new Color32[originalPixels.Length];

        for (int i = 0; i < originalPixels.Length; i++)
        {
            byte metallic = originalPixels[i].g;
            byte roughness = originalPixels[i].b;

            // byte smoothness = (byte)(byte.MaxValue - roughness);

            convertedPixels[i] = new Color32(metallic, 0, 0, roughness);
        }

        convertedTexture.SetPixels32(convertedPixels);
        convertedTexture.Apply();

        return convertedTexture;
    }
}
