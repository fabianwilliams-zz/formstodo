using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using UIKit;
using System.Threading;

namespace tdxplat
{
	public class TodoService
	{
		private MobileServiceClient MobileService = new MobileServiceClient(
			"https://tdxplat.azure-mobile.net/",
			"hmHSPmwycAHNqXXtojyGIXLTXyFdRI32"

		);

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

			await this.MobileService.SyncContext.PushAsync();

			var query = todoTable.CreateQuery()
				.Where(td => td.Complete == false)
				;

			await todoTable.PullAsync("myjobs", query);
		}

		public async Task<IEnumerable<ToDoItem>> SearchTodoItems(string searchInput)
		{
			var query = todoTable.CreateQuery ();

			if (!string.IsNullOrWhiteSpace(searchInput)) {
				query = query.Where (td => 
					td.Id == searchInput
				);
			}

			return await query.ToEnumerableAsync();
		}

	}
}

