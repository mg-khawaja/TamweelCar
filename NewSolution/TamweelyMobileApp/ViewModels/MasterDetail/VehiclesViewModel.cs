using Syncfusion.SfNavigationDrawer.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TamweelyMobileApp.Models;
using TamweelyMobileApp.Resx;
using TamweelyMobileApp.Services.Interfaces;
using TamweelyMobileApp.Views.MasterDetail;
using Xamarin.Forms;

namespace TamweelyMobileApp.ViewModels
{
    public class VehiclesViewModel : BaseViewModel
    {
        #region Fields
        private IVehicles vehiclesService;
        SfNavigationDrawer drawer;
        private IUserAuthenticate userService;
        private INavigation _navigation;
        private List<Make> makeList;
        private Make selectedMake;
        private double balance;
        private bool noVehicles = false;
        private bool isHigh = false;
        private bool isLow = false;
        private bool next = true;
        private int page = 0;
        private int totalVehicles = 0;
        #endregion

        #region Constructor
        public VehiclesViewModel(IVehicles vehservice, IUserAuthenticate userservice, double balance, INavigation navigation, SfNavigationDrawer sfNavigationDrawer)
        {
            _navigation = navigation;
            vehiclesService = vehservice;
            userService = userservice;
            Vehicles = new ObservableCollection<VehicleModel>();
            this.Balance = balance;
            InitializeCommand.Execute(null);
            this.drawer = sfNavigationDrawer;
        }
        #endregion

        #region property
        public ObservableCollection<VehicleModel> Vehicles { get; set; }
        public List<Make> MakeList
        {
            get
            {
                return this.makeList;
            }

            set
            {
                if (this.makeList == value)
                {
                    return;
                }
                this.makeList = value;
                this.NotifyPropertyChanged();
            }
        }
        public double Balance
        {
            get
            {
                return this.balance;
            }

            set
            {
                if (this.balance == value)
                {
                    return;
                }

                this.balance = value;
                this.NotifyPropertyChanged();
            }
        }
        public Make SelectedMake
        {
            get
            {
                return this.selectedMake;
            }

            set
            {
                if (this.selectedMake == value)
                {
                    return;
                }

                this.selectedMake = value;
                this.NotifyPropertyChanged();
            }
        }
        public bool IsHigh
        {
            get
            {
                return this.isHigh;
            }

            set
            {
                if (this.isHigh == value)
                {
                    return;
                }

                this.isHigh = value;
                this.NotifyPropertyChanged();
            }
        }
        public bool IsLow
        {
            get
            {
                return this.isLow;
            }

            set
            {
                if (this.isLow == value)
                {
                    return;
                }

                this.isLow = value;
                this.NotifyPropertyChanged();
            }
        }
        public bool NoVehicles
        {
            get
            {
                return this.noVehicles;
            }

            set
            {
                if (this.noVehicles == value)
                {
                    return;
                }

                this.noVehicles = value;
                this.NotifyPropertyChanged();
            }
        }
        public int TotalVehicles
        {
            get
            {
                return this.totalVehicles;
            }

            set
            {
                if (this.totalVehicles == value)
                {
                    return;
                }

                this.totalVehicles = value;
                this.NotifyPropertyChanged();
            }
        }
        #endregion

        #region Command
        public Command InitializeCommand => new Command(Initialize);
        public Command LoadMoreItemsCommand => new Command(LoadMoreItems, CanLoadMoreItems);
        public Command VehicleSelectedCommand => new Command(VehicleSelected);
        public Command FilterCommand => new Command(Filter);
        public Command ResetCommand => new Command(Reset);
        #endregion

        #region methods
        private bool CanLoadMoreItems(object obj)
        {
            return next && !IsBusy;
        }
        private async void VehicleSelected(object obj)
        {
            var vehicle = obj as VehicleModel;
            if (vehicle != null)
            {
                //if(vehicle.TotalPrice > 50000)
                //{
                //    (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.car_value_error, TamweelyResources.ok);
                //    return;
                //}
                await _navigation.PushAsync(new SelectedVehiclePage(vehicle, Balance));
            }
        }
        private async void LoadMoreItems(object obj)
        {
            IsBusy = true;
            var listView = obj as Syncfusion.ListView.XForms.SfListView;
            listView.IsBusy = true;
            try
            {
                int make = 0;
                int highlow = 0;
                if (selectedMake != null)
                {
                    make = selectedMake.Id;
                }
                if (IsHigh)
                {
                    highlow = 1;
                }
                else if (IsLow)
                {
                    highlow = 2;
                }
                var result = await vehiclesService.GetVehicles(Balance.ToString(), page.ToString(), 3.ToString(), make, highlow);
                if (result != null && result.Status == "success")
                {
                    next = Convert.ToBoolean(result.Next);
                    TotalVehicles = result.TotalRecords;
                    page++;
                    var data = result.Data;
                    foreach (var item in data)
                    {
                        var image = item.VehicleImages.Where(i => i.IsMain).FirstOrDefault();
                        item.VehicleImages.Remove(image);
                        item.VehicleImages.Insert(0, image);
                        Vehicles.Add(item);
                    }
                    if (Vehicles == null || Vehicles.Count == 0)
                    {
                        NoVehicles = true;
                    }
                    else
                    {
                        NoVehicles = false;
                    }
                }
                else
                {
                    next = false;
                }
            }
            catch (Exception ex)
            {
                next = false;
            }
            listView.IsBusy = false;
            IsBusy = false;
        }
        private async void Initialize(object obj)
        {
            IsBusy = true;
            try
            {
                MakeList = await userService.GetMakeList();
            }
            catch (Exception ex)
            {
                await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error,
                    TamweelyResources.something, TamweelyResources.ok);
            }
            IsBusy = false;
        }
        private async void Filter(object obj)
        {
            IsBusy = true;
            try
            {
                var listView = obj as Syncfusion.ListView.XForms.SfListView;
                if (listView != null)
                {
                    if (SelectedMake == null && (!IsHigh && !IsLow))
                    {
                        IsBusy = false;
                        return;
                    }
                    next = true;
                    page = 0;
                    TotalVehicles = 0;
                    Vehicles.Clear();
                    LoadMoreItemsCommand.Execute(listView);
                    drawer.ToggleDrawer();
                }
                var h = IsHigh;
                var l = IsLow;
                var m = SelectedMake;


                //var list = await userService.GetMakeList();
                //if (list != null)
                //{
                //    MakeList = new ObservableCollection<Make>(list);
                //}
            }
            catch (Exception ex)
            {
                await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error,
                    TamweelyResources.something, TamweelyResources.ok);
            }
            IsBusy = false;
        }
        private async void Reset(object obj)
        {
            IsBusy = true;
            try
            {
                var listView = obj as Syncfusion.ListView.XForms.SfListView;
                if (listView != null)
                {
                    if (SelectedMake == null && (!IsHigh && !IsLow))
                    {
                        IsBusy = false;
                        return;
                    }
                    IsHigh = false;
                    IsLow = false;
                    SelectedMake = null;
                    next = true;
                    page = 0;
                    TotalVehicles = 0;
                    Vehicles.Clear();
                    LoadMoreItemsCommand.Execute(listView);
                    drawer.ToggleDrawer();
                }
                //var list = await userService.GetMakeList();
                //if (list != null)
                //{
                //    MakeList = new ObservableCollection<Make>(list);
                //}
            }
            catch (Exception ex)
            {
                await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error,
                    TamweelyResources.something, TamweelyResources.ok);
            }
            IsBusy = false;
        }
        #endregion


    }
}
