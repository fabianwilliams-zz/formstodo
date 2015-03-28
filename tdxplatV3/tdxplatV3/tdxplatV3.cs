using System;

using Xamarin.Forms;

namespace tdxplatV3
{
	public class App : Application
	{
		//public static TodoService todoService = new TodoService(); //this gives a AppDelegate Error if i use it here

		public App ()
		{
			MainPage =  new EntryPageAlpha();

			/*
			// The root page of your application
			MainPage = new ContentPage {
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					Children = {
						new Label {
							XAlign = TextAlignment.Center,
							Text = "Welcome to Xamarin Forms!"
						}
					}
				}
			};
			*/
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

