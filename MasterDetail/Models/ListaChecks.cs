using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MasterDetail.Models
{
    public class ListaChecks: INotifyPropertyChanged
    {
        
            private string _title;
            private bool _isChecked;

            public String Title
            {
                get { return _title; }
                set
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
            public Boolean IsChecked
            {
                get { return _isChecked; }
                set
                {
                    _isChecked = value;
                    OnPropertyChanged();
                }
            }

            public ListaChecks()
            {
                Title = String.Empty;
                IsChecked = false;
            }

            public ListaChecks(string title)
                : this()
            {
                Title = title;
            }

            public ListaChecks(string title, bool isChecked)
                : this(title)
            {
                IsChecked = isChecked;
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged([CallerMemberName]string propertyName="")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
}
