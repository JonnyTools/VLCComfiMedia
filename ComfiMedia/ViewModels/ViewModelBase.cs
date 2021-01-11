using ComfiMedia.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ComfiMedia.ViewModels 
{
    //BindableBase, IInitialize, INavigationAware, IDestructible
    public class ViewModelBase : INavigationAware, INotifyPropertyChanged, IInitialize
    {

        //Title Logic for every View
        string title = string.Empty;
        public string Title
        {
            get => title;
            set
            {
                if (title == value)
                return;
                title = value;
                OnPropertyChanged();        
            }
        }

        // IsBusy Logic for every ViewModel 
        bool isBusy = false;
        // Not good -> nameof(!IsBusy)
        public bool IsNotBusy => !IsBusy;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (isBusy == value)
                    return;
                isBusy = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }

        // INavigationAware :https://prismlibrary.com/docs/xamarin-forms/navigation/passing-parameters.html
        public virtual void Initialize(INavigationParameters parameters)
        {

        }
        //Kommend von einer View/ViewModel
        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }
        // Wenn eine View instanziert wird aber nicht in den NavigationService registriert wurde
        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }
        // Wird aufgerufen nachdem View und ViewModel erstellt wurden und View in den NavigationService registriert wurde
        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        #region INotifyPropertyChanged
        /**
         * Interface to notify view about changes in Model
         * -> Refresh view
         */
        protected bool SetProperty<T>(ref T backingStore, T value,
        [CallerMemberName] string propertyName = "",
         Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
