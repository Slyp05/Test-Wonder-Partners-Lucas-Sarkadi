using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class TextureDownloader
{
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

    public static async Task GetTexturesAsync(string[] urls, Action<Texture[]> onSuccess, Action<string> onError)
    {
        UnityWebRequest[] requests = urls.Select(url => UnityWebRequestTexture.GetTexture(url))
                                         .ToArray();

        UnityWebRequestAsyncOperation[] asyncOperations = requests.Select(r => r.SendWebRequest())
                                                                  .ToArray();

        foreach (AsyncOperation operation in asyncOperations)
            await operation;

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
