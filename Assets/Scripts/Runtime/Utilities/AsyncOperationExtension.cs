using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public static class AsyncOperationExtension
{
	public static TaskAwaiter GetAwaiter(this AsyncOperation asyncOp)
	{
        TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

		asyncOp.completed += obj => tcs.SetResult(null);

		return ((Task)tcs.Task).GetAwaiter();
	}
}
