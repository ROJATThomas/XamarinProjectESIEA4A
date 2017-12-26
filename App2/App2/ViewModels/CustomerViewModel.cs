using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using System.Linq;
using Realms;
using Xamarin.Forms;
using App2.Models;
using App2.Views;
using System.Runtime.CompilerServices;


using System.Threading.Tasks;


namespace App2.ViewModels
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private CustomerDetails _customerDetails = new CustomerDetails();
        private List<CustomerDetails> _listOfCustomerDetails;
        Realm _getRealmInstance = Realm.GetInstance();
       

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<CustomerDetails> ListOfCustomerDetails
        {
            get { return _listOfCustomerDetails; }
            set
            {
                _listOfCustomerDetails = value;
                OnPropertyChanged(); // Added the OnPropertyChanged Method
            }
        }

        public CustomerViewModel()
        {
            // supply the public ListOfCustomerDetails with the retrieved list of details
            ListOfCustomerDetails = _getRealmInstance.All<CustomerDetails>().ToList();
        }

        public CustomerDetails CustomerDetails
        {
            get { return _customerDetails; }
            set
            {
                _customerDetails = value;
                OnPropertyChanged(); // Add the OnPropertyChanged();
            }
        }

        // Add this inside your view model class
        public Command CreateCommand // for ADD
        {
            get
            {
                return new Command(() => {
                    // for auto increment the id upon adding
                    _customerDetails.CustomerId = _getRealmInstance.All<CustomerDetails>().Count() + 1;
                    _getRealmInstance.Write(() =>
                    {
                        _getRealmInstance.Add(_customerDetails); // Add the whole set of details
                    });
                });
            }
        }

        public Command UpdateCommand // For UPDATE
        {
            get
            {
                return new Command(() =>
                {
                    // instantiate to supply the new set of details
                    var customerDetailsUpdate = new CustomerDetails
                    {
                        CustomerId = _customerDetails.CustomerId,
                        CustomerName = _customerDetails.CustomerName,
                        CustomerAge = _customerDetails.CustomerAge
                    };

                    _getRealmInstance.Write(() =>
                    {
                        // when there's id match, the details will be replaced except by primary key
                        _getRealmInstance.Add(customerDetailsUpdate, update: true);
                    });
                });
            }
        }

        public Command RemoveCommand
        {
            get
            {
                return new Command(() =>
                {
                    // get the details with specific id
                    var getAllCustomerDetailsById = _getRealmInstance.All<CustomerDetails>().First(x => x.CustomerId == _customerDetails.CustomerId);

                    using (var transaction = _getRealmInstance.BeginWrite())
                    {
                        // remove all details
                        _getRealmInstance.Remove(getAllCustomerDetailsById);
                        transaction.Commit();
                    };
                });
            }
        }

        // For Navigation Page
        public Command NavToListCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await App.Current.MainPage.Navigation.PushAsync(new ListOfCustomers());
                });
            }
        }

        public Command NavToCreateCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Console.WriteLine("haha");
                    var navPage = new NavigationPage(new MainPage());
                    Application.Current.MainPage = navPage;
                    
                    await navPage.PushAsync(new CreateCustomer());
                    await  navPage.PopAsync();
                    navPage.Popped += (s, e) => { };

                });
            }
        }

        public Command NavToUpdateDeleteCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await App.Current.MainPage.Navigation.PushAsync(new UpdateOrDeleteCustomer());
                });
            }
        }
    }
}
