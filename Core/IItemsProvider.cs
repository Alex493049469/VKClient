﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
	public interface IItemsProvider<T>
	{
		/// <summary>
		/// Fetches the total number of items available.
		/// </summary>
		/// <returns></returns>
		int FetchCount();

		/// <summary>
		/// Fetches a range of items.
		/// </summary>
		/// <param name="startIndex">The start index.</param>
		/// <param name="count">The number of items to fetch.</param>
		/// <returns></returns>
		Task<List<T>> FetchRange(int startIndex, int count);
	}
}