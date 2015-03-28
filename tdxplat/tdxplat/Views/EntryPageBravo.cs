using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace tdxplat
{
	public class EntryPageBravo : ContentPage
	{
		public EntryPageBravo ()
		{
			var fabianlist = new[] {
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

			var listview1 = new ListView ();
			//using the below line it will set it to everything in the array

			listview1.ItemsSource = fabianlist;


			listview1.ItemSelected += (sender, e) => {
				Debug.WriteLine ("You clicked: " + e.SelectedItem);
			};


			var label1 = new Label {
				Text = "Fabian Learning Xamarin.Forms",
				Font = Font.SystemFontOfSize(NamedSize.Medium)
			};

			var entry1 = new Entry {
				Placeholder = "You can type something here"
			};

			var button1 = new Button {
				Text = "Click Me"
			};



			button1.Clicked += (sender, e) => {
				label1.Text = "User Typed: " + entry1.Text;
			};

			Content = new StackLayout{
				//by setting padding in this way allows for using the device class which
				//permits me to only set padding for ios since it is needed at the top
				Padding = new Thickness(0,Device.OnPlatform(20,0,0),0,0),
				Spacing = 10,
				Children = {label1,entry1,button1,listview1}
			};
		}

	}
}

