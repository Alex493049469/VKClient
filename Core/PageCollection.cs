using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public class PageCollection<T> : IList<T>, IList, INotifyCollectionChanged
	{
		private readonly IItemsProvider<T> _provider;
		private readonly int _pageSize;
		private readonly Dictionary<int, IList<T>> _pages = new Dictionary<int, IList<T>>();

		public PageCollection(IItemsProvider<T> provider, int pageSize)
		{
			_provider = provider;
			_pageSize = pageSize;
		}

		public IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < Count; i++)
			{
				yield return this[i];
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(T item)
		{
			//_list.Add(item);
			//this.OnNotifyCollectionChanged(
			// new NotifyCollectionChangedEventArgs(
			//NotifyCollectionChangedAction.Add, item));
		}

		public int Add(object value)
		{
			throw new NotImplementedException();
		}

		bool IList.Contains(object value)
		{
			return false;
		}

		public bool Contains(T item)
		{
			return false;
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, object value)
		{
			throw new NotImplementedException();
		}

		public void Remove(object value)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public bool Remove(T item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		public int Count
		{
			get { return _provider.FetchCount(); }
		}

		public object SyncRoot { get; }
		public bool IsSynchronized { get; }

		public bool IsReadOnly { get; }
		public bool IsFixedSize { get; }

		#region IndexOf

		int IList.IndexOf(object value)
		{
			return IndexOf((T)value);
		}

		/// <summary>
		/// Not supported
		/// </summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1"/>.</param>
		/// <returns>
		/// Always -1.
		/// </returns>
		public int IndexOf(T item)
		{
			return -1;
		}

		#endregion

		public void Insert(int index, T item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set { throw new NotImplementedException(); }
		}

		public T this[int index]
		{
			get
			{
				// determine which page and offset within page
				int pageIndex = index / _pageSize;
				int pageOffset = index % _pageSize;
				RequestPage(pageIndex);

				// defensive check in case of async load
				if (_pages[pageIndex] == null)
					return default(T);

				// return requested item
				return _pages[pageIndex][pageOffset];
			}
			set { throw new NotSupportedException(); }
		}

		protected virtual void RequestPage(int pageIndex)
		{
			if (!_pages.ContainsKey(pageIndex))
			{
				_pages.Add(pageIndex, null);
				Trace.WriteLine("Added page: " + pageIndex);
				LoadPage(pageIndex);
			}
		}

		protected virtual void LoadPage(int pageIndex)
		{
			PopulatePage(pageIndex, FetchPage(pageIndex));
		}

		protected async virtual void PopulatePage(int pageIndex, Task<List<T>> page)
		{
			Trace.WriteLine("Page populated: " + pageIndex);
			if (_pages.ContainsKey(pageIndex))
				_pages[pageIndex] = await page;
			FireCollectionReset();
		}

		protected async Task<List<T>> FetchPage(int pageIndex)
		{
			return await _provider.FetchRange(pageIndex * _pageSize, _pageSize);
		}

		//#region INotifyCollectionChanged
		//private void OnNotifyCollectionChanged(NotifyCollectionChangedEventArgs args)
		//{
		//	if (this.CollectionChanged != null)
		//	{
		//		this.CollectionChanged(this, args);
		//	}
		//}
		//public event NotifyCollectionChangedEventHandler CollectionChanged;
		//#endregion INotifyCollectionChanged

		#region INotifyCollectionChanged

		/// <summary>
		/// Occurs when the collection changes.
		/// </summary>
		public event NotifyCollectionChangedEventHandler CollectionChanged;

		/// <summary>
		/// Raises the <see cref="E:CollectionChanged"/> event.
		/// </summary>
		/// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			NotifyCollectionChangedEventHandler h = CollectionChanged;
			if (h != null)
				h(this, e);
		}

		/// <summary>
		/// Fires the collection reset event.
		/// </summary>
		private void FireCollectionReset()
		{
			NotifyCollectionChangedEventArgs e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
			OnCollectionChanged(e);
		}

		#endregion
	}
}
