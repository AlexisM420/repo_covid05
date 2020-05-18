using BillingManagement.Business;
using BillingManagement.Models;
using BillingManagement.UI.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace BillingManagement.UI.ViewModels
{
    class MainViewModel : BaseViewModel
    {
		private BaseViewModel _vm;

		public BaseViewModel VM
		{
			get { return _vm; }
			set {
				_vm = value;
				OnPropertyChanged();
			}
		}

		private string searchCriteria;

		public string SearchCriteria
		{
			get { return searchCriteria; }
			set { 
				searchCriteria = value;
				OnPropertyChanged();
			}
		}

		 BillingManagementContext database;/*
		public BillingManagementContext Database
		{
			get => database;
			set
			{
				database = value;
				OnPropertyChanged();
			}
		}*/

		private ObservableCollection<Customer> customers;
		public ObservableCollection<Customer> Customers
		{
			get => customers;
			set
			{
				customers = value;
				OnPropertyChanged();
			}
		}


		CustomerViewModel customerViewModel;
		InvoiceViewModel invoiceViewModel;

		public ChangeViewCommand ChangeViewCommand { get; set; }

		public DelegateCommand<object> AddNewItemCommand { get; private set; }

		public DelegateCommand<Invoice> DisplayInvoiceCommand { get; private set; }
		public DelegateCommand<Customer> DisplayCustomerCommand { get; private set; }

		public DelegateCommand<Customer> AddInvoiceToCustomerCommand { get; private set; }
		public RelayCommand<Customer> SearchCommand { get; private set; }


		public MainViewModel()
		{
			customers = new ObservableCollection<Customer>();
			//initDatabase();

			ChangeViewCommand = new ChangeViewCommand(ChangeView);
			DisplayInvoiceCommand = new DelegateCommand<Invoice>(DisplayInvoice);
			DisplayCustomerCommand = new DelegateCommand<Customer>(DisplayCustomer);

			AddNewItemCommand = new DelegateCommand<object>(AddNewItem, CanAddNewItem);
			AddInvoiceToCustomerCommand = new DelegateCommand<Customer>(AddInvoiceToCustomer);

			SearchCommand = new RelayCommand<Customer>(SearchByCustomer, CanSearch);

			customerViewModel = new CustomerViewModel();
			invoiceViewModel = new InvoiceViewModel(customerViewModel.Customers);

			VM = customerViewModel;
		}
		private bool CanSearch(object c)
		{
			if (VM == customerViewModel) return true;
			return false;
		}

		private void initDatabase()
		{
			database = new BillingManagementContext();
			List<Customer> Customers = new CustomersDataService().GetAll().ToList();

					foreach (Customer cust in Customers)
					{
						database.Customers.Add(cust);
				//database.Add(cust);
						database.SaveChanges();
					}


		}

		private void SearchByCustomer(object parameter)
		{
			string input = searchCriteria as string;
			int output;

			List<Customer> SearchResultCustomers = new List<Customer>();


			if (!Int32.TryParse(input, out output))
			{
				//SearchResultCustomers = Database.Customers.Where(cust => cust.Name.ToLower().StartsWith(input.ToLower())).ToList();
				if (SearchResultCustomers.Count < 1)
				{
					MessageBox.Show("No customer found with : " + input);
				}
				else
				{
					Customers.Clear();
					foreach (Customer c in SearchResultCustomers)
					{
						Customers.Add(c);
					}
					Customers = new ObservableCollection<Customer>(Customers.OrderBy(cust => cust.LastName));
					customerViewModel.Customers = Customers;
					customerViewModel.SelectedCustomer = Customers.First();
				}
			}
			else MessageBox.Show("Please enter a character, not numbers");
		}

		private void ChangeView(string vm)
		{
			switch (vm)
			{
				case "customers":
					VM = customerViewModel;
					break;
				case "invoices":
					VM = invoiceViewModel;
					break;
			}
		}

		private void DisplayInvoice(Invoice invoice)
		{
			invoiceViewModel.SelectedInvoice = invoice;
			VM = invoiceViewModel;
		}

		private void DisplayCustomer(Customer customer)
		{
			customerViewModel.SelectedCustomer = customer;
			VM = customerViewModel;
		}

		private void AddInvoiceToCustomer(Customer c)
		{
			var invoice = new Invoice(c);
			c.Invoices.Add(invoice);
			DisplayInvoice(invoice);
		}

		private void AddNewItem (object item)
		{
			if (VM == customerViewModel)
			{
				var c = new Customer();
				customerViewModel.Customers.Add(c);
				customerViewModel.SelectedCustomer = c;
			}
		}

		private bool CanAddNewItem(object o)
		{
			bool result = false;

			result = VM == customerViewModel;
			return result;
		}

	}
}
