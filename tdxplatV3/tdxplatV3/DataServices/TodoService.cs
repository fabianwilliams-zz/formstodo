using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
#if __IOS__
using UIKit;
#elif __ANDROID__
using Mono;
#endif
using System.Threading;

namespace tdxplatV3
{
	public class TodoService
	{
		private MobileServiceClient MobileService = new MobileServiceClient(
			"https://tdxplat.azure-mobile.net/",
			"hmHSPmwycAHNqXXtojyGIXLTXyFdRI32"

		);



		public List<ToDoItem> Items { get; private set;}

		private IMobileServiceSyncTable<ToDoItem> todoTable;

		public async Task InitializeAsync()
		{
			var store = new MobileServiceSQLiteStore("localdata.db");
			store.DefineTable<ToDoItem> ();
			await this.MobileService.SyncContext.InitializeAsync(store);

			todoTable = this.MobileService.GetSyncTable<ToDoItem>();
		}

		public async Task SyncAsync()
		{
			// Comment this back in if you want auth
			//if (!await IsAuthenticated())
			//    return;
			await InitializeAsync();
			await this.MobileService.SyncContext.PushAsync();

			var query = todoTable.CreateQuery()
				.Where(td => td.Complete == false)
				;

			await todoTable.PullAsync("myjobs", query);
		}

		public async Task<IEnumerable<ToDoItem>> SearchTodoItems(string searchInput)
		{
			await SyncAsync (); //ADDED THIS AFTER GETTING NULL EXCEPTION 
			var query = todoTable.CreateQuery ();

			if (!string.IsNullOrWhiteSpace(searchInput)) {
				query = query.Where (td => 
					td.Id == searchInput
					|| searchInput.ToUpper().Contains(td.Text.ToUpper()) // workaround bug: these are backwards
					|| searchInput.ToUpper().Contains(td.Id.ToUpper())
				);
			}

			return await query.ToEnumerableAsync();
		}

		public async Task<List<ToDoItem>> SearchTodo()
		{
			try {
				// update the local store
				// all operations on todoTable use the local database, call SyncAsync to send changes
				await SyncAsync(); 							

				// This code refreshes the entries in the list view by querying the local TodoItems table.
				// The query excludes completed TodoItems -- not anymore
				//I took out todoItems.Complete and replaced it with todoItem.Remove -- Fabian Williams
				Items = await todoTable
					.Where (todoItem => todoItem.Complete == false).ToListAsync ();

			} catch (MobileServiceInvalidOperationException e) {
				Console.Error.WriteLine (@"ERROR {0}", e.Message);
				return null;
			}

			return Items;
		}

	}
}

