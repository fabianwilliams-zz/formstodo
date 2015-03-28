using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace tdxplatV3
{
	public class EntryPageAlpha : ContentPage
	{
		private ListView listView;
		private SearchBar searchBar;
		private TodoService todoService;
		public List<ToDoItem> Items { get; private set;}


		private Array fabianlist = new[] {
			"alpha",
			"bravo",
			"charlie",
			"delta",
			"echo",
			"foxtrot",
			"golf",
			"hotel",
			"india",
			"juliet"

		};

		public EntryPageAlpha ()
		{

			todoService = new TodoService ();

			listView = new ListView
			{
				HasUnevenRows = true,
				//IsGroupingEnabled = true,

			};

			searchBar = new SearchBar { Placeholder = "Enter search" };
			searchBar.SearchButtonPressed += async (object sender, EventArgs e) => 
			{
				await RefreshAsync();
			};

			var syncButton = new Button {
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Font = Font.SystemFontOfSize(NamedSize.Medium),
				Text = "sync"

			};
			syncButton.Clicked += async (object sender, EventArgs e) => {
				try 
				{
					syncButton.Text = "Syncing...";	
					await todoService.SyncAsync();
					await this.RefreshAsync();
				} 
				finally 
				{
					syncButton.Text = "Sync";
				}


			};
				

			this.Title = "All Books";
			this.Content = new StackLayout {
				Padding = new Thickness(0, Device.OnPlatform(20,0,0),0,0),
				Spacing = 10,
				Orientation = StackOrientation.Vertical,
				Children = {
					searchBar,
					new StackLayout {
						Orientation = StackOrientation.Horizontal,
						Children = {
							syncButton
						}
					},
					listView
				}

			};
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			await this.RefreshAsync();
		}

		private async Task RefreshAsync()
		{        

			Items = await todoService.SearchTodo ();
			listView.ItemsSource = Items;
			//listView.ItemsSource = fabianlist;
		}

	}
}

