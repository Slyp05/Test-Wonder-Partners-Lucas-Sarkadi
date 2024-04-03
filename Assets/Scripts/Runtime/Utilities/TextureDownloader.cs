using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

/// <summary>
/// Lightweight utility to get <see cref="Texture"/> using <see cref="UnityWebRequest"/>.
/// </summary>
public static class TextureDownloader
{
    /// <summary>
    /// Try asynchronously to get a <see cref="Texture"/> at the given <paramref name="url"/>.
    /// </summary>
    public static async Task GetTextureAsync(string url, Action<Texture> onSuccess, Action<string> onError)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

        await request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            onError(request.error);
        }
        else
        {
            Texture texture = DownloadHandlerTexture.GetContent(request);

            onSuccess(texture);
        }
    }

    /// <summary>
    /// Try asynchronously to get multiple <see cref="Texture"/> at the given <paramref name="urls"/>.
    /// </summary>
    public static async Task GetTexturesAsync(string[] urls, Action<Texture[]> onSuccess, Action<string> onError)
    {
        UnityWebRequest[] requests = urls.Select(url => UnityWebRequestTexture.GetTexture(url))
                                         .ToArray();

        UnityWebRequestAsyncOperation[] asyncOperations = requests.Select(r => r.SendWebRequest())
                                                                  .ToArray();

        Stopwatch sw = new Stopwatch();
        int counter = 0;
        foreach (AsyncOperation operation in asyncOperations)
        {
            sw.Restart();
            await operation;
            sw.Stop();

            Debug.Log($"{++counter}: {sw.Elapsed.TotalSeconds}");
        }

        UnityWebRequest failedRequest = requests.Where(r => r.result != UnityWebRequest.Result.Success)
                                                .FirstOrDefault();
        if (failedRequest != null)
        {
            onError(failedRequest.error);
        }
        else
        {
            Texture[] textures = requests.Select(r => DownloadHandlerTexture.GetContent(r))
                                         .ToArray();

            onSuccess(textures);
        }
    }
}
