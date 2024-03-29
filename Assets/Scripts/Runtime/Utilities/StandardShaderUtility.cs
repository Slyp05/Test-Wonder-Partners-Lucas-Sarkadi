using System.Collections.Generic;
using UnityEngine;

public static class StandardShaderUtility
{
    static readonly IReadOnlyDictionary<StandardShaderTextureType, string> TextureNames = new Dictionary<StandardShaderTextureType, string>()
    {
        { StandardShaderTextureType.BaseMap,            "_MainTex" },
        { StandardShaderTextureType.Emissive,           "_EmissionMap" },
        { StandardShaderTextureType.MetallicRoughness,  "_MetallicGlossMap" },
        { StandardShaderTextureType.Normal,             "_BumpMap" },
        { StandardShaderTextureType.Occlusion,          "_OcclusionMap" },
    };

    public static void AssignTexture(Material material, StandardShaderTextureType textureType, Texture texture)
    {
        material.SetTexture(TextureNames[textureType], texture);
    }
}
