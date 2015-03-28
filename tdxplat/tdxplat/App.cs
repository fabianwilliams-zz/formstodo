using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace tdxplat
{
	public class App : Application
	{
		public static TodoService todoService = new TodoService();

		public App ()
		{
			//MainPage = new EntryPageAlpha ();
			MainPage = new EntryPageBravo ();
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
