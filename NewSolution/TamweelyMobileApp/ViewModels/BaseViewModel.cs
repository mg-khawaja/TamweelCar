using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TamweelyMobileApp.Models;
using Xamarin.Forms;

namespace TamweelyMobileApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        //private LocalResources _resourse;
        //public LocalResources Resources
        //{
        //    get { return _resourse; }
        //    private set
        //    {
        //        _resourse = value;
        //        NotifyPropertyChanged();
        //    }
        //}
        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }

            set
            {
                if (this.isBusy == value)
                {
                    return;
                }

                this.isBusy = value;
                this.NotifyPropertyChanged();
            }
        }

        #region Event handler
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        public BaseViewModel()
        {
            //Resources = new LocalResources(typeof(TurnUp.Local.Local), App.CurrentLanguage);
        }
        #region Methods

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
