using Syncfusion.SfNavigationDrawer.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TamweelyMobileApp.Helpers;
using TamweelyMobileApp.Models;
using TamweelyMobileApp.Resx;
using TamweelyMobileApp.Services.Interfaces;
using TamweelyMobileApp.Views.MasterDetail;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TamweelyMobileApp.ViewModels
{
    public class MasterDetailViewModel : BaseViewModel
    {
        #region Fields
        private IVehicles vehiclesService;
        private AdModel advertisement;
        private List<CountryCode> countryCodes;
        private List<InstallmentSetting> installmentSettings;
        private string name;
        private string salary;
        private string al;
        private string pl;
        private string ml;
        private string cc;
        private bool onAL;
        private bool onPL;
        private bool onML;
        private bool onCC;
        private bool isClaimVisible;
        private bool vehiclesVisible;
        private double balance;
        private double drawerHeight;
        #endregion

        #region Constructor
        public MasterDetailViewModel(INavigation navigationService, IVehicles service)
        {
            _navigationService = navigationService;
            DrawerHeight = 300;
            vehiclesService = service;
            this.InitializeCommand = new Command(this.InitializeData);
            this.GoToVehiclesCommand = new Command(this.GoToVehicles);
            this.CalcBalanceCommand = new Command(this.CalcBalance);
            this.AdClickCommand = new Command(this.AdClicked);
            InitializeCommand.Execute(null);
        }
        #endregion

        #region property
        private INavigation _navigationService { get; }
        public AdModel Advertisement
        {
            get
            {
                return this.advertisement;
            }

            set
            {
                if (this.advertisement == value)
                {
                    return;
                }

                this.advertisement = value;
                this.NotifyPropertyChanged();
            }
        }
        public List<CountryCode> CountryCodes
        {
            get
            {
                return this.countryCodes;
            }

            set
            {
                if (this.countryCodes == value)
                {
                    return;
                }

                this.countryCodes = value;
                this.NotifyPropertyChanged();
            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name == value)
                {
                    return;
                }

                this.name = value;
                this.NotifyPropertyChanged();
            }
        }
        public string Salary
        {
            get
            {
                return this.salary;
            }

            set
            {
                if (value.ToString().Contains('.'))
                {
                    if (value.ToString().Length - 1 - value.ToString().IndexOf(".") > 2)
                    {
                        var index = value.ToString().IndexOf(".");
                        Salary = value.ToString().Substring(0, index + 3);
                        return;
                    }
                }
                if (value.Length > 1 && value[0] == '0')
                {
                    value = value.Remove(0, 1);
                    Salary = value;
                    return;
                }
                this.salary = value;
                this.NotifyPropertyChanged();
            }
        }
        public string AL
        {
            get
            {
                return this.al;
            }

            set
            {
                if (value.ToString().Contains('.'))
                {
                    if (value.ToString().Length - 1 - value.ToString().IndexOf(".") > 2)
                    {
                        var index = value.ToString().IndexOf(".");
                        AL = value.ToString().Substring(0, index + 3);
                        return;
                    }
                }
                if (value.Length > 1 && value[0] == '0')
                {
                    value = value.Remove(0, 1);
                    AL = value;
                    return;
                }
                this.al = value;
                this.NotifyPropertyChanged();
            }
        }
        public string PL
        {
            get
            {
                return this.pl;
            }

            set
            {
                if (value.ToString().Contains('.'))
                {
                    if (value.ToString().Length - 1 - value.ToString().IndexOf(".") > 2)
                    {
                        var index = value.ToString().IndexOf(".");
                        PL = value.ToString().Substring(0, index + 3);
                        return;
                    }
                }
                if (value.Length > 1 && value[0] == '0')
                {
                    value = value.Remove(0, 1);
                    PL = value;
                    return;
                }
                this.pl = value;
                this.NotifyPropertyChanged();
            }
        }
        public string ML
        {
            get
            {
                return this.ml;
            }

            set
            {
                if (value.ToString().Contains('.'))
                {
                    if (value.ToString().Length - 1 - value.ToString().IndexOf(".") > 2)
                    {
                        var index = value.ToString().IndexOf(".");
                        ML = value.ToString().Substring(0, index + 3);
                        return;
                    }
                }
                if (value.Length > 1 && value[0] == '0')
                {
                    value = value.Remove(0, 1);
                    ML = value;
                    return;
                }

                this.ml = value;
                this.NotifyPropertyChanged();
            }
        }
        public string CC
        {
            get
            {
                return this.cc;
            }

            set
            {
                if (value.ToString().Contains('.'))
                {
                    if (value.ToString().Length - 1 - value.ToString().IndexOf(".") > 2)
                    {
                        var index = value.ToString().IndexOf(".");
                        CC = value.ToString().Substring(0, index + 3);
                        //CC = Convert.ToDouble(value.ToString().Substring(0, index + 3));
                        return;
                    }
                }
                if (value.Length > 1 && value[0] == '0')
                {
                    value = value.Remove(0, 1);
                    CC = value;
                    return;
                }
                
                this.cc = value;
                this.NotifyPropertyChanged();
            }
        }
        public bool OnAL
        {
            get
            {
                return this.onAL;
            }

            set
            {
                if (this.onAL == value)
                {
                    return;
                }

                this.onAL = value;
                this.NotifyPropertyChanged();
            }
        }
        public bool OnPL
        {
            get
            {
                return this.onPL;
            }

            set
            {
                if (this.onPL == value)
                {
                    return;
                }

                this.onPL = value;
                this.NotifyPropertyChanged();
            }
        }
        public bool OnML
        {
            get
            {
                return this.onML;
            }

            set
            {
                if (this.onML == value)
                {
                    return;
                }

                this.onML = value;
                this.NotifyPropertyChanged();
            }
        }
        public bool OnCC
        {
            get
            {
                return this.onCC;
            }

            set
            {
                if (this.onCC == value)
                {
                    return;
                }

                this.onCC = value;
                this.NotifyPropertyChanged();
            }
        }
        public bool IsClaimVisible
        {
            get
            {
                return this.isClaimVisible;
            }

            set
            {
                if (this.isClaimVisible == value)
                {
                    return;
                }

                this.isClaimVisible = value;
                this.NotifyPropertyChanged();
            }
        }
        public bool VehiclesVisible
        {
            get
            {
                return this.vehiclesVisible;
            }

            set
            {
                if (this.vehiclesVisible == value)
                {
                    return;
                }

                this.vehiclesVisible = value;
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
        public double DrawerHeight
        {
            get
            {
                return this.drawerHeight;
            }

            set
            {
                if (this.drawerHeight == value)
                {
                    return;
                }

                this.drawerHeight = value;
                this.NotifyPropertyChanged();
            }
        }
        #endregion

        #region Command
        public Command InitializeCommand { get; set; }
        public Command GoToVehiclesCommand { get; set; }
        public Command CalcBalanceCommand { get; set; }
        public Command AdClickCommand { get; set; }
        #endregion

        #region methods
        private async void InitializeData(object obj)
        {
            IsBusy = true;
            try
            {
                var result = await vehiclesService.GetInstallmentSettings();
                if (result != null && result.Status == "success")
                {
                    installmentSettings = result.Data;
                }
                var ads = await vehiclesService.GetAds();
                if (ads != null && ads.Status == "success")
                {
                    Advertisement = ads.Data.Where(ad => ad.ShowOnHome).FirstOrDefault();
                }
                var links = await vehiclesService.GetLinks();
                if (links != null)
                {
                    GlobalSetting.Instance.Links = links;
                }
            }
            catch (Exception ex)
            {

            }

            IsBusy = false;
        }
        private async void GoToVehicles(object obj)
        {
            IsBusy = true;
            await Task.Delay(200);
            try
            {
                await _navigationService.PushAsync(new VehiclesPage(Balance));
            }
            catch (Exception ex)
            {

            }
            IsBusy = false;
        }
        private async void CalcBalance(object obj)
        {
            IsBusy = true;
            await Task.Delay(200);
            var drawer = obj as SfNavigationDrawer;
            try
            {
                double dblSalary = 0;
                double dblAL = 0;
                double dblPL = 0;
                double dblML = 0;
                double dblCC = 0;
                if (!string.IsNullOrEmpty(Salary))
                {
                    dblSalary = Convert.ToDouble(Salary);
                }
                if (dblSalary <= 0)
                {
                    await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.salary_error, TamweelyResources.ok);
                    IsBusy = false;
                    return;
                }
                if (!string.IsNullOrEmpty(AL))
                {
                    dblAL = Convert.ToDouble(AL);
                }
                if (OnAL && dblAL <= 0)
                {
                    await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.al_error, TamweelyResources.ok);
                    IsBusy = false;
                    return;
                }
                if (!string.IsNullOrEmpty(PL))
                {
                    dblPL = Convert.ToDouble(PL);
                }
                if (OnPL && dblPL <= 0)
                {
                    await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.pl_error, TamweelyResources.ok);
                    IsBusy = false;
                    return;
                }
                if (!string.IsNullOrEmpty(ML))
                {
                    dblML = Convert.ToDouble(ML);
                }
                if (OnML && dblML <= 0)
                {
                    await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.ml_error, TamweelyResources.ok);
                    IsBusy = false;
                    return;
                }
                if (!string.IsNullOrEmpty(CC))
                {
                    dblCC = Convert.ToDouble(CC);
                }
                if (OnCC && dblCC < 0)
                {
                    await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.cc_error, TamweelyResources.ok);
                    IsBusy = false;
                    return;
                }
                InstallmentSetting installment = installmentSettings.FirstOrDefault();
                foreach (var item in installmentSettings)
                {
                    double? from = Convert.ToDouble(item.SalaryFrom);
                    double? to = null;
                    double num;
                    var isDouble = Double.TryParse(item.SalaryTo, out num);
                    if (isDouble)
                        to = Convert.ToDouble(item.SalaryTo);
                    if (to == null && dblSalary > from)
                    {
                        installment = item;
                        break;
                    }
                    else if (dblSalary >= from && dblSalary <= to)
                    {
                        installment = item;
                        break;
                    }
                }

                double salaryPerc;
                if (OnML)
                {
                    salaryPerc = installment.WithMirgage * dblSalary / 100; 
                }
                else
                {
                    salaryPerc = installment.NoMorgage * dblSalary / 100;
                }
                double sum = 0;
                if (OnAL)
                {
                    sum += dblAL;
                }
                if (OnPL)
                {
                    sum += dblPL;
                }
                if (OnML)
                {
                    sum += dblML;
                }
                var ccPerc = 5 * dblCC / 100;
                sum += ccPerc;
                Balance = salaryPerc - sum;
                if(Balance != 0)
                    Balance = Convert.ToDouble(Balance.ToString("#.##"));
                if(Balance <= 0)
                {
                    VehiclesVisible = false;
                    IsClaimVisible = false;
                    DrawerHeight = 150;
                }
                else
                {
                    VehiclesVisible = true;
                    IsClaimVisible = true;
                    DrawerHeight = 330;
                }
                if (drawer != null)
                    drawer.ToggleDrawer();
            }
            catch (Exception ex)
            {

            }
            IsBusy = false;
        }
        private async void AdClicked(object obj)
        {
            IsBusy = true;
            await Task.Delay(200);
            try
            {
                await Browser.OpenAsync(Advertisement.URL, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {

            }
            IsBusy = false;
        }
        #endregion


    }
}
