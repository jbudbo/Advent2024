using System.Diagnostics;

[DebuggerStepThrough]
internal static class Utils
{
	public static IEnumerable<T> WithoutIndex<T>(this IEnumerable<T> source, int index)
	{
        T[] dataset = source as T[] ?? source.ToArray();
        for (int i = 0, j = dataset.Length; i < j; i++)
        {
            if (i != index) yield return dataset[i];
        }
    }

	public static IEnumerable<ValueTuple<T,T>> Pairwise<T>(this IEnumerable<T> source)
		=> source.Windowed(2).Select(Pairwise);

	private static ValueTuple<T, T> Pairwise<T>(T[] source) => (source[0], source[1]);

	public static IEnumerable<T[]> Windowed<T>(this IEnumerable<T> source, int WindowSize)
	{
		T[] dataset = source as T[] ?? source.ToArray();

		if (WindowSize <= 1 || WindowSize >= dataset.Length)
		{
			yield return dataset;
			yield break;
		}

		for (int i = 0, j = dataset.Length - WindowSize + 1; i < j; i++)
		{
			yield return dataset[i..(i + WindowSize)];
		}
	}
}