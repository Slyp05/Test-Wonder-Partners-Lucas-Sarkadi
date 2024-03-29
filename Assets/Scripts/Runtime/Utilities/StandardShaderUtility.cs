using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Utility to edit a <see cref="Material"/> using the Standard shader at runtime.
/// </summary>
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

    /// <summary>
    /// Assign the given <paramref name="texture"/> to <paramref name="material"/> in the <paramref name="textureType"/> slot.
    /// </summary>
    public static void AssignTexture(Material material, StandardShaderTextureType textureType, Texture texture)
    {
        material.SetTexture(TextureNames[textureType], texture);
    }
}
