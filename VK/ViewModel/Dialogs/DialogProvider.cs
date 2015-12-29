using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Virtualizing;

namespace VK.ViewModel.Dialogs
{
	//public class DialogProvider : IItemsProvider<DialogItemViewModel>
	//{
		//	 private readonly int _count;
		//private readonly int _fetchDelay;

		///// <summary>
		///// Initializes a new instance of the <see cref="DemoCustomerProvider"/> class.
		///// </summary>
		///// <param name="count">The count.</param>
		///// <param name="fetchDelay">The fetch delay.</param>
		//public DialogProvider(int count, int fetchDelay)
		//{
		//	_count = count;
		//	_fetchDelay = fetchDelay;
		//}

		///// <summary>
		///// Fetches the total number of items available.
		///// </summary>
		///// <returns></returns>
		//public int FetchCount()
		//{
		//   // Trace.WriteLine("FetchCount");
		//   // Thread.Sleep(_fetchDelay);
		//	return _count; 
		//}

		///// <summary>
		///// Fetches a range of items.
		///// </summary>
		///// <param name="startIndex">The start index.</param>
		///// <param name="count">The number of items to fetch.</param>
		///// <returns></returns>
		//public IList<DialogItemViewModel> FetchRange(int startIndex, int count)
		//{
		//	Trace.WriteLine("FetchRange: "+startIndex+","+count);
		//	Thread.Sleep(_fetchDelay);

		//	List<DialogItemViewModel> list = new List<DialogItemViewModel>();
		//	for( int i=startIndex; i<startIndex+count; i++ )
		//	{
		//		DialogItemViewModel customer = new DialogItemViewModel { Id = i + 1, Name = "Customer " + (i + 1) };
		//		list.Add(customer);
		//	}
		//	return list;
		//}
	//}
}
