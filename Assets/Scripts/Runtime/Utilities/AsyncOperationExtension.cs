using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Extension class that makes it possible to work with <see cref="UnityEngine.Networking.UnityWebRequest"/> in an asynchronous context.
/// </summary>
public static class AsyncOperationExtension
{
	/// <summary>
	/// Duck typed method that let any <see cref="AsyncOperation"/> be awaited.
	/// </summary>
	public static TaskAwaiter GetAwaiter(this AsyncOperation asyncOp)
	{
        TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

		asyncOp.completed += obj => tcs.SetResult(null);

		return ((Task)tcs.Task).GetAwaiter();
	}
}
