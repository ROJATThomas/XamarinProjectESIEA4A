using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListOfCustomers : ContentPage
	{
		public ListOfCustomers ()
		{
			InitializeComponent ();
		}
	}
}