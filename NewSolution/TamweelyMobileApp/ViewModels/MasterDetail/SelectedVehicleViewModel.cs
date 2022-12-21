using Syncfusion.SfRangeSlider.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TamweelyMobileApp.Models;
using TamweelyMobileApp.Resx;
using TamweelyMobileApp.Services.Interfaces;
using TamweelyMobileApp.Views.MasterDetail;
using Xamarin.Forms;

namespace TamweelyMobileApp.ViewModels
{
    public class SelectedVehicleViewModel : BaseViewModel
    {
        #region Fields

        #region backup fields
        private string carValue_backup;
        private double totalPrice_backup;
        private double leaseAmount_backup;
        private int monthsSelected_backup;
        private double interest_backup;
        private double interestPercSelected_backup;
        private double insurance_backup;
        private double insurancePercSelected_backup;
        private double installmentSelected_backup;
        private double downPay_backup;
        private double downPayPercSelected_backup;
        private double lastPay_backup;
        private double lastPayPercSelected_backup;
        #endregion




        int installCount;
        private IVehicles vehiclesService;
        private bool isInterestPercVisible;
        private bool isInsurancePercVisible;
        private bool isDownPayPercVisible;
        private bool isLastPayPercVisible;
        private string carValue;
        private double totalPrice;
        private double leaseAmount;
        private double carValueMin;
        private double carValueMax;
        private int monthsSelected = 2;
        private int monthsMin = 0;
        private int monthsMax = 4;
        private double interest;
        private double interestPercSelected = 2;
        private double interestMin = 0;
        private double interestMax = 4;
        private double insurance;
        private double insurancePercSelected = 2;
        private double insuranceMin = 0;
        private double insuranceMax = 4;
        private string installment;
        private double installmentSelected = 2;
        private double installmentMin = 0;
        private double installmentMax = 4;
        private double downPay;
        private double downPayPercSelected = 2;
        private double downPayMin = 0;
        private double downPayMax = 4;
        private double lastPay;
        private double lastPayPercSelected = 2;
        private double lastPayMin = 0;
        private double lastPayMax = 4;
        private INavigation _navigation;
        private VehicleModel _vehicle;
        private double balance;
        #endregion

        #region Constructor
        public SelectedVehicleViewModel(IVehicles service, VehicleModel vehicle,
            INavigation navigation, double balance,
            Slider monthsSlider, Slider interestSlider, Slider insuranceSlider,
            Slider installmentSlider, Slider downPaySlider, Slider lastPaySlider)
        {
            this.MonthsSlider = monthsSlider;
            this.LastPaySlider = lastPaySlider;
            this.InterestSlider = interestSlider;
            this.InsuranceSlider = insuranceSlider;
            this.InstallmentSlider = installmentSlider;
            this.DownPaySlider = downPaySlider;

            isInterestPercVisible = true;
            isInsurancePercVisible = true;
            isDownPayPercVisible = true;
            isLastPayPercVisible = true;
            this.balance = balance;
            _navigation = navigation;
            vehiclesService = service;
            Vehicle = vehicle;
            InitializeCommand.Execute(null);
        }
        #endregion

        #region property
        public Slider MonthsSlider;
        public Slider InterestSlider;
        public Slider InsuranceSlider;
        public Slider InstallmentSlider;
        public Slider DownPaySlider;
        public Slider LastPaySlider;
        public VehicleModel Vehicle
        {
            get
            {
                return this._vehicle;
            }

            set
            {
                if (this._vehicle == value)
                {
                    return;
                }

                this._vehicle = value;
                this.NotifyPropertyChanged();
            }
        }
        public double TotalPrice
        {
            get
            {
                return this.totalPrice;
            }

            set
            {
                //if (this.totalPrice == value)
                //{
                //    return;
                //}

                this.totalPrice = value;
                this.NotifyPropertyChanged();
            }
        }
        public bool IsInterestPercVisible
        {
            get
            {
                return this.isInterestPercVisible;
            }

            set
            {
                if (isInterestPercVisible == value)
                {
                    return;
                }
                isInterestPercVisible = value;
                //leaseAmount = Convert.ToDouble(carValue) - DownPay;
                this.NotifyPropertyChanged();
            }
        }
        public bool IsInsurancePercVisible
        {
            get
            {
                return this.isInsurancePercVisible;
            }

            set
            {
                if (isInsurancePercVisible == value)
                {
                    return;
                }
                isInsurancePercVisible = value;
                //leaseAmount = Convert.ToDouble(carValue) - DownPay;
                this.NotifyPropertyChanged();
            }
        }
        public bool IsDownPayPercVisible
        {
            get
            {
                return this.isDownPayPercVisible;
            }

            set
            {
                if (isDownPayPercVisible == value)
                {
                    return;
                }
                isDownPayPercVisible = value;
                //leaseAmount = Convert.ToDouble(carValue) - DownPay;
                this.NotifyPropertyChanged();
            }
        }
        public bool IsLastPayPercVisible
        {
            get
            {
                return this.isLastPayPercVisible;
            }

            set
            {
                if (isLastPayPercVisible == value)
                {
                    return;
                }
                isLastPayPercVisible = value;
                //leaseAmount = Convert.ToDouble(carValue) - DownPay;
                this.NotifyPropertyChanged();
            }
        }
        public string CarValue
        {
            get
            {
                return this.carValue;
            }

            set
            {
                var oldValue = carValue;
                if (value.ToString().Contains('.'))
                {
                    if (value.ToString().Length - 1 - value.ToString().IndexOf(".") > 2)
                    {
                        var index = value.ToString().IndexOf(".");
                        CarValue = value.ToString().Substring(0, index + 3);
                        return;
                    }
                }
                if (value.Length > 1 && value[0] == '0')
                {
                    value = value.Remove(0, 1);
                    CarValue = value;
                    return;
                }
                //if (Convert.ToDouble(value) > 50000)
                //{
                //    (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.car_value_error, TamweelyResources.ok);
                //    CarValue = oldValue;
                //    return;
                //}
                this.carValue = value;
                if (!IsBusy)
                {
                    SetBackup(oldValue, ChangedValue.carValue);
                    CarValueChanged();
                }
                //leaseAmount = Convert.ToDouble(carValue) - DownPay;
                this.NotifyPropertyChanged();
            }
        }
        public double CarValueMin
        {
            get
            {
                return this.carValueMin;
            }

            set
            {
                if (this.carValueMin == value)
                {
                    return;
                }

                this.carValueMin = value;
                this.NotifyPropertyChanged();
            }
        }
        public double CarValueMax
        {
            get
            {
                return this.carValueMax;
            }

            set
            {
                if (this.carValueMax == value)
                {
                    return;
                }

                this.carValueMax = value;
                this.NotifyPropertyChanged();
            }
        }
        public int MonthsSelected
        {
            get
            {
                return this.monthsSelected;
            }

            set
            {
                //if (this.monthsSelected == value)
                //{
                //    return;
                //}

                var oldValue = monthsSelected;
                if (value > MonthsMax || value < 2)
                {
                    this.MonthsSelected = monthsSelected;
                    return;
                }
                this.monthsSelected = value;
                if (!IsBusy)
                {
                    SetBackup(oldValue, ChangedValue.months);
                    MonthsChanged();
                }
                else
                {
                    MonthsSlider.Value = value;
                }
                this.NotifyPropertyChanged();
            }
        }
        public int MonthsMin
        {
            get
            {
                return this.monthsMin;
            }

            set
            {
                if (this.monthsMin == value)
                {
                    return;
                }

                this.monthsMin = value;
                this.NotifyPropertyChanged();
            }
        }
        public int MonthsMax
        {
            get
            {
                return this.monthsMax;
            }

            set
            {
                if (this.monthsMax == value)
                {
                    return;
                }

                this.monthsMax = value;
                this.NotifyPropertyChanged();
            }
        }
        public double Interest
        {
            get
            {
                return this.interest;
            }

            set
            {
                var oldValue = interest;
                var perc = (value * 1200) / (leaseAmount * Convert.ToDouble(MonthsSelected));
                if (perc > InterestMax || perc < InterestMin)
                {
                    Interest = interest;
                    return;
                }
                var val = Math.Round(value, 2);
                if (val != value)
                {
                    this.Interest = val;
                    return;
                }
                this.interest = value;
                if (!IsBusy)
                {
                    SetBackup(oldValue, ChangedValue.interest);
                    InterestChanged();
                }
                this.NotifyPropertyChanged();
            }
        }
        public double InterestPercSelected
        {
            get
            {
                return this.interestPercSelected;
            }

            set
            {
                var oldValue = interestPercSelected;
                if (value > InterestMax || value < InterestMin)
                {
                    this.InterestPercSelected = interestPercSelected;
                    return;
                }
                var val = Math.Round(value, 2);
                if (val != value)
                {
                    this.InterestPercSelected = val;
                    return;
                }
                this.interestPercSelected = value;
                //Interest = ((leaseAmount * interestPercSelected) / 100) * (Convert.ToInt32(monthsSelected) / 12);
                //this.InterestPerc = value.ToString();
                if (!IsBusy)
                {
                    SetBackup(oldValue, ChangedValue.interestPerc);
                    InterestPercChanged();
                }
                else
                {
                    InterestSlider.Value = value;
                }
                this.NotifyPropertyChanged();
            }
        }
        public double InterestMin
        {
            get
            {
                return this.interestMin;
            }

            set
            {
                if (this.interestMin == value)
                {
                    return;
                }

                this.interestMin = value;
                this.NotifyPropertyChanged();
            }
        }
        public double InterestMax
        {
            get
            {
                return this.interestMax;
            }

            set
            {
                if (this.interestMax == value)
                {
                    return;
                }

                this.interestMax = value;
                this.NotifyPropertyChanged();
            }
        }
        public double Insurance
        {
            get
            {
                return this.insurance;
            }

            set
            {
                var oldValue = insurance;
                var perc = (value * 1200) / (Convert.ToDouble(CarValue) * Convert.ToDouble(MonthsSelected));
                if (perc > InsuranceMax || perc < InsuranceMin)
                {
                    Insurance = insurance;
                    return;
                }
                var val = Math.Round(value, 2);
                if (val != value)
                {
                    this.Insurance = val;
                    return;
                }
                this.insurance = value;
                if (!IsBusy)
                {
                    SetBackup(oldValue, ChangedValue.insurance);
                    InsuranceChanged();
                }
                this.NotifyPropertyChanged();
            }
        }
        public double InsurancePercSelected
        {
            get
            {
                return this.insurancePercSelected;
            }

            set
            {
                //if (this.insurancePercSelected == value)
                //{
                //    return;
                //}
                var oldValue = insurancePercSelected;
                if (value > InsuranceMax || value < InsuranceMin)
                {
                    InsurancePercSelected = insurancePercSelected;
                    return;
                }

                var val = Math.Round(value, 2);
                if (val != value)
                {
                    this.InsurancePercSelected = val;
                    return;
                }
                this.insurancePercSelected = value;
                //Insurance = ((Convert.ToDouble(CarValue) * insurancePercSelected) / 100) * (Convert.ToInt32(monthsSelected) / 12);
                //this.InsurancePerc = value.ToString();
                if (!IsBusy)
                {
                    SetBackup(oldValue, ChangedValue.insurancePerc);
                    InsurancePercChanged();
                }
                else
                {
                    InsuranceSlider.Value = value;
                }
                this.NotifyPropertyChanged();
            }
        }
        public double InsuranceMin
        {
            get
            {
                return this.insuranceMin;
            }

            set
            {
                if (this.insuranceMin == value)
                {
                    return;
                }

                this.insuranceMin = value;
                this.NotifyPropertyChanged();
            }
        }
        public double InsuranceMax
        {
            get
            {
                return this.insuranceMax;
            }

            set
            {
                if (this.insuranceMax == value)
                {
                    return;
                }

                this.insuranceMax = value;
                this.NotifyPropertyChanged();
            }
        }
        public string Installment
        {
            get
            {
                return this.installment;
            }

            set
            {
                if (value.ToString().Contains('.'))
                {
                    if (value.ToString().Length - 1 - value.ToString().IndexOf(".") > 2)
                    {
                        var index = value.ToString().IndexOf(".");
                        Installment = value.ToString().Substring(0, index + 3);
                        return;
                    }
                }
                if (value.Length > 1 && value[0] == '0')
                {
                    value = value.Remove(0, 1);
                    Installment = value;
                    return;
                }

                this.installment = value;
                this.NotifyPropertyChanged();
            }
        }
        public double InstallmentSelected
        {
            get
            {
                return this.installmentSelected;
            }

            set
            {
                //if (this.installmentSelected == value)
                //{
                //    return;
                //}
                if (value > installmentMax || value < installmentMin)
                {
                    InstallmentSelected = installmentSelected;
                    return;
                }
                var val = Math.Round(value, 2);
                if (val != value)
                {
                    this.InstallmentSelected = val;
                    return;
                }
                this.installmentSelected = value;
                InstallmentSlider.Value = value;
                //this.Installment = value.ToString();
                this.NotifyPropertyChanged();
            }
        }
        public double InstallmentMin
        {
            get
            {
                return this.installmentMin;
            }

            set
            {
                if (this.installmentMin == value)
                {
                    return;
                }

                this.installmentMin = value;
                this.NotifyPropertyChanged();
            }
        }
        public double InstallmentMax
        {
            get
            {
                return this.installmentMax;
            }

            set
            {
                if (this.installmentMax == value)
                {
                    return;
                }

                this.installmentMax = value;
                this.NotifyPropertyChanged();
            }
        }
        public double DownPay
        {
            get
            {
                return this.downPay;
            }

            set
            {
                var oldValue = downPay;
                var perc = (value * 100) / Convert.ToDouble(CarValue);
                if (perc > DownPayMax || perc < DownPayMin)
                {
                    DownPay = downPay;
                    return;
                }
                var val = Math.Round(value, 2);
                if (val != value)
                {
                    this.DownPay = val;
                    return;
                }
                this.downPay = value;
                if (!IsBusy)
                {
                    SetBackup(oldValue, ChangedValue.downPay);
                    DownPayChanged();
                }
                this.NotifyPropertyChanged();
            }
        }
        public double DownPayPercSelected
        {
            get
            {
                return this.downPayPercSelected;
            }

            set
            {

                //if (this.downPayPercSelected == value)
                //{
                //    return;
                //}
                var oldValue = downPayPercSelected;
                if (value > DownPayMax || value < DownPayMin)
                {
                    DownPayPercSelected = downPayPercSelected;
                    return;
                }
                var val = Math.Round(value, 2);
                if (val != value)
                {
                    this.DownPayPercSelected = val;
                    return;
                }
                this.downPayPercSelected = value;
                //DownPay = (Convert.ToDouble(CarValue) * downPayPercSelected) / 100;
                //this.DownPayPerc = value.ToString();
                if (!IsBusy)
                {
                    SetBackup(oldValue, ChangedValue.downPayPerc);
                    DownPayPercChanged();
                }
                else
                {
                    DownPaySlider.Value = value;
                }
                this.NotifyPropertyChanged();
            }
        }
        public double DownPayMin
        {
            get
            {
                return this.downPayMin;
            }

            set
            {
                if (this.downPayMin == value)
                {
                    return;
                }

                this.downPayMin = value;
                this.NotifyPropertyChanged();
            }
        }
        public double DownPayMax
        {
            get
            {
                return this.downPayMax;
            }

            set
            {
                if (this.downPayMax == value)
                {
                    return;
                }

                this.downPayMax = value;
                this.NotifyPropertyChanged();
            }
        }
        public double LastPay
        {
            get
            {
                return this.lastPay;
            }

            set
            {
                var oldValue = lastPay;
                var perc = (value * 100) / Convert.ToDouble(CarValue);
                if (perc > LastPayMax || perc < LastPayMin)
                {
                    LastPay = lastPay;
                    return;
                }
                var val = Math.Round(value, 2);
                if (val != value)
                {
                    this.LastPay = val;
                    return;
                }
                this.lastPay = value;
                if (!IsBusy)
                {
                    SetBackup(oldValue, ChangedValue.lastPay);
                    LastPayChanged();
                }
                this.NotifyPropertyChanged();
            }
        }
        public double LastPayPercSelected
        {
            get
            {
                return this.lastPayPercSelected;
            }

            set
            {
                //if (this.lastPayPercSelected == value)
                //{
                //    return;
                //}
                var oldValue = lastPayPercSelected;
                if (value > lastPayMax || value < lastPayMin)
                {
                    LastPayPercSelected = lastPayPercSelected;
                    return;
                }
                var val = Math.Round(value, 2);
                if (val != value)
                {
                    this.LastPayPercSelected = val;
                    return;
                }
                this.lastPayPercSelected = value;
                //LastPay = (Convert.ToDouble(CarValue) * lastPayPercSelected) / 100;
                //this.LastPayPerc = value.ToString();
                if (!IsBusy)
                {
                    SetBackup(oldValue, ChangedValue.lastPayPerc);
                    LastPayPercChanged();
                }
                else
                {
                    LastPaySlider.Value = value; 
                }
                this.NotifyPropertyChanged();
            }
        }
        public double LastPayMin
        {
            get
            {
                return this.lastPayMin;
            }

            set
            {
                if (this.lastPayMin == value)
                {
                    return;
                }

                this.lastPayMin = value;
                this.NotifyPropertyChanged();
            }
        }
        public double LastPayMax
        {
            get
            {
                return this.lastPayMax;
            }

            set
            {
                if (this.lastPayMax == value)
                {
                    return;
                }

                this.lastPayMax = value;
                this.NotifyPropertyChanged();
            }
        }
        #endregion

        #region Command
        public Command InitializeCommand => new Command(Initialize);
        #endregion

        #region methods
        private async void Initialize(object obj)
        {
            IsBusy = true;
            await Task.Delay(100);
            try
            {
                var config = await vehiclesService.GetInstallmentsConfigurations();
                if (config != null && config.Data != null && config.Data.Count > 0)
                {
                    foreach (var item in config.Data)
                    {
                        switch (item.Description)
                        {
                            case "Insurance":
                                InsuranceMin = item.MinValue;
                                InsuranceMax = item.MaxValue;
                                InsurancePercSelected = item.DefaultValue;
                                break;
                            case "NoOfMonths":
                                MonthsMin = Convert.ToInt32(item.MinValue);
                                MonthsMax = Convert.ToInt32(item.MaxValue);
                                MonthsSelected = Convert.ToInt32(item.DefaultValue);
                                break;
                            case "LastPayment":
                                LastPayMin = item.MinValue;
                                LastPayMax = item.MaxValue;
                                LastPayPercSelected = item.DefaultValue;
                                break;
                            case "DownPayment":
                                DownPayMin = item.MinValue;
                                DownPayMax = item.MaxValue;
                                DownPayPercSelected = item.DefaultValue;
                                break;
                            case "Interest":
                                InterestMin = item.MinValue;
                                InterestMax = item.MaxValue;
                                InterestPercSelected = item.DefaultValue;
                                break;
                            default:
                                break;
                        }
                    }
                    CarValue = Vehicle.TotalPrice.ToString();
                    InstallmentMin = 0;
                    InstallmentMax = balance;
                    InstallmentSelected = Vehicle.MonthlyInstallments;

                    CalculateDownPayment();
                    CalculateLastPayment();
                    CalculateLeaseAmount();
                    CalculateInstallmentsCount();
                    CalculateInterest();
                    CalculateInsurance();
                    CalculateMonthlyInstallment();
                    CalculateTotalPrice();

                    NotifyValues();

                    //monthSlider.Value = MonthsSelected;
                    //downPaySlider.Value = DownPayPercSelected;
                    //installmentSlider.Value = InstallmentSelected;
                    //insuranceSlider.Value = InsurancePercSelected;
                    //interestSlider.Value = InterestPercSelected;
                    //lastPaySlider.Value = LastPayPercSelected;

                    //MonthsSelected = 0;
                    //MonthsMin = 0;
                    //MonthsMax = 60;
                    //Interest = 0.00;
                    //InterestPercSelected = 0;
                    //InterestMin = 0;
                    //InterestMax = 60;
                    //Insurance = 0.00;
                    //InsurancePercSelected = 0;
                    //InsuranceMin = 0;
                    //InsuranceMax = 60;
                    //DownPay = 0.00;
                    //DownPayPercSelected = 0;
                    //DownPayMin = 0;
                    //DownPayMax = 60;
                    //LastPay = 0.00;
                    //LastPayPercSelected = 0;
                    //LastPayMin = 0;
                    //LastPayMax = 60;
                }
            }
            catch (Exception ex)
            {

            }
            await Task.Delay(2000);
            IsBusy = false;
        }
        private void CarValueChanged()
        {
            IsBusy = true;
            try
            {
                if (!CalculateDownPayment())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateLastPayment())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateLeaseAmount())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateInterest())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateInsurance())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateMonthlyInstallment())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateTotalPrice())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                NotifyValues();
            }
            catch (Exception ex)
            {

            }
            IsBusy = false;
        }
        private void MonthsChanged()
        {
            IsBusy = true;
            try
            {
                if (!CalculateInstallmentsCount())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateInterest())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateInsurance())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateMonthlyInstallment())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateTotalPrice())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                NotifyValues();
            }
            catch (Exception ex)
            {

            }
            IsBusy = false;
        }
        private void InterestPercChanged()
        {
            IsBusy = true;
            try
            {
                if (!CalculateInterest())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateMonthlyInstallment())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateTotalPrice())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                NotifyValues();
            }
            catch (Exception ex)
            {

            }
            IsBusy = false;
        }
        private void InterestChanged()
        {
            IsBusy = true;
            try
            {
                if (!CalculateInterestPerc())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateMonthlyInstallment())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateTotalPrice())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                NotifyValues();
            }
            catch (Exception ex)
            {

            }
            IsBusy = false;
        }
        private void InsurancePercChanged()
        {
            IsBusy = true;
            try
            {
                if (!CalculateInsurance())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateMonthlyInstallment())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateTotalPrice())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                NotifyValues();
            }
            catch (Exception ex)
            {

            }
            IsBusy = false;
        }
        private void InsuranceChanged()
        {
            IsBusy = true;
            try
            {
                if (!CalculateInsurancePerc())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateMonthlyInstallment())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateTotalPrice())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                NotifyValues();
            }
            catch (Exception ex)
            {

            }
            IsBusy = false;
        }
        private void DownPayPercChanged()
        {
            IsBusy = true;
            try
            {
                if (!CalculateDownPayment())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateLeaseAmount())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateInterest())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateMonthlyInstallment())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateTotalPrice())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                NotifyValues();
            }
            catch (Exception ex)
            {

            }
            IsBusy = false;
        }
        private void DownPayChanged()
        {
            IsBusy = true;
            try
            {
                if (!CalculateDownPaymentPerc())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateLeaseAmount())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateInterest())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateMonthlyInstallment())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateTotalPrice())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                NotifyValues();
            }
            catch (Exception ex)
            {

            }
            IsBusy = false;
        }
        private void LastPayPercChanged()
        {
            IsBusy = true;
            try
            {
                if (!CalculateLastPayment())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateMonthlyInstallment())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateTotalPrice())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                NotifyValues();
            }
            catch (Exception ex)
            {

            }
            IsBusy = false;
        }
        private void LastPayChanged()
        {
            IsBusy = true;
            try
            {
                if (!CalculateLastPaymentPerc())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateMonthlyInstallment())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                if (!CalculateTotalPrice())
                {
                    RevertValues();
                    IsBusy = false;
                    return;
                }
                NotifyValues();
            }
            catch (Exception ex)
            {

            }
            IsBusy = false;
        }
        private void SetBackup(object value, ChangedValue type)
        {
            carValue_backup = carValue;
            totalPrice_backup = totalPrice;
            leaseAmount_backup = leaseAmount;
            monthsSelected_backup = monthsSelected;
            interest_backup = interest;
            interestPercSelected_backup = interestPercSelected;
            insurance_backup = insurance;
            insurancePercSelected_backup = insurancePercSelected;
            installmentSelected_backup = installmentSelected;
            downPay_backup = downPay;
            downPayPercSelected_backup = downPayPercSelected;
            lastPay_backup = lastPay;
            lastPayPercSelected_backup = lastPayPercSelected;
            switch (type)
            {
                case ChangedValue.carValue:
                    carValue_backup = value as string;
                    break;
                case ChangedValue.months:
                    monthsSelected_backup = Convert.ToInt32(value);
                    break;
                case ChangedValue.interest:
                    interest_backup = Convert.ToDouble(value);
                    break;
                case ChangedValue.interestPerc:
                    interestPercSelected_backup = Convert.ToDouble(value);
                    break;
                case ChangedValue.insurance:
                    insurance_backup = Convert.ToDouble(value);
                    break;
                case ChangedValue.insurancePerc:
                    insurancePercSelected_backup = Convert.ToDouble(value);
                    break;
                case ChangedValue.downPay:
                    downPay_backup = Convert.ToDouble(value);
                    break;
                case ChangedValue.downPayPerc:
                    downPayPercSelected_backup = Convert.ToDouble(value);
                    break;
                case ChangedValue.lastPay:
                    lastPay_backup = Convert.ToDouble(value);
                    break;
                case ChangedValue.lastPayPerc:
                    lastPayPercSelected_backup = Convert.ToDouble(value);
                    break;
                default:
                    break;
            }

        }
        private void RevertValues()
        {
            CarValue = carValue_backup;
            TotalPrice = totalPrice_backup;
            leaseAmount = leaseAmount_backup;
            MonthsSelected = monthsSelected_backup;
            Interest = interest_backup;
            InterestPercSelected = interestPercSelected_backup;
            Insurance = insurance_backup;
            InsurancePercSelected = insurancePercSelected_backup;
            InstallmentSelected = installmentSelected_backup;
            DownPay = downPay_backup;
            DownPayPercSelected = downPayPercSelected_backup;
            LastPay = lastPay_backup;
            LastPayPercSelected = lastPayPercSelected_backup;
        }
        private void NotifyValues()
        {
            CarValue = carValue;
            TotalPrice = totalPrice;
            leaseAmount = leaseAmount;
            MonthsSelected = monthsSelected;
            Interest = interest;
            InterestPercSelected = interestPercSelected;
            Insurance = insurance;
            InsurancePercSelected = insurancePercSelected;
            InstallmentSelected = installmentSelected;
            DownPay = downPay;
            DownPayPercSelected = downPayPercSelected;
            LastPay = lastPay;
            LastPayPercSelected = lastPayPercSelected;
        }

        #endregion

        #region formulas
        private bool CalculateDownPayment()
        {
            downPay = (Convert.ToDouble(carValue) * downPayPercSelected) / 100;
            return true;
        }
        private bool CalculateDownPaymentPerc()
        {
            downPayPercSelected = (downPay * 100) / Convert.ToDouble(carValue);
            if (downPayPercSelected > downPayMax || downPayPercSelected < downPayMin)
                return false;
            return true;
        }
        private bool CalculateLastPayment()
        {
            lastPay = (Convert.ToDouble(carValue) * lastPayPercSelected) / 100;
            return true;
        }
        private bool CalculateLastPaymentPerc()
        {
            lastPayPercSelected = (lastPay * 100) / Convert.ToDouble(carValue);
            if (lastPayPercSelected > lastPayMax || lastPayPercSelected < lastPayMin)
                return false;
            return true;
        }
        private bool CalculateLeaseAmount()
        {
            leaseAmount = Convert.ToDouble(carValue) - downPay;
            if(leaseAmount > 500000)
            {
                (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.lease_error, TamweelyResources.ok);
                return false;
            }
            return true;
        }
        private bool CalculateInsurance()
        {
            insurance = ((Convert.ToDouble(carValue) * insurancePercSelected) / 100) * (Convert.ToDouble(monthsSelected) / 12);
            return true;
        }
        private bool CalculateInsurancePerc()
        {
            insurancePercSelected = (insurance * 1200) / (Convert.ToDouble(carValue) * Convert.ToDouble(monthsSelected));
            if (insurancePercSelected > insuranceMax || insurancePercSelected < insuranceMin)
                return false;
            return true;
        }
        private bool CalculateInterest()
        {
            interest = ((leaseAmount * interestPercSelected) / 100) * (Convert.ToDouble(monthsSelected) / 12);
            return true;
        }
        private bool CalculateInterestPerc()
        {
            interestPercSelected = (interest * 1200) / (leaseAmount * Convert.ToDouble(monthsSelected));
            if (interestPercSelected > interestMax || interestPercSelected < interestMin)
                return false;
            return true;
        }
        private bool CalculateInstallmentsCount()
        {
            installCount = Convert.ToInt32(monthsSelected) - 1;
            return true;
        }
        private bool CalculateMonthlyInstallment()
        {
            installmentSelected = (leaseAmount + insurance + interest - lastPay) / installCount;
            if (installmentSelected > balance || installmentSelected > installmentMax || installmentSelected < installmentMin)
                return false;
            return true;
        }
        private bool CalculateTotalPrice()
        {
            totalPrice = Math.Round(((installmentSelected * installCount) + downPay + lastPay), 2);
            return true;
        }
        #endregion
        enum ChangedValue
        {
            carValue,
            months,
            interest,
            interestPerc,
            insurance,
            insurancePerc,
            downPay,
            downPayPerc,
            lastPay,
            lastPayPerc,
        }
    }
}
