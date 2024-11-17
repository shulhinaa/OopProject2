using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace lab2.ViewModels
{
    public class ResultsViewModel : INotifyPropertyChanged
    {
       
        public ObservableCollection<string> Columns { get; set; }
        public ObservableCollection<Dictionary<string, string>> Rows { get; set; }

        public ICommand BackCommand { get; }

        public ResultsViewModel(List<string> results)
        {
            Columns = new ObservableCollection<string>
            {
                "Назва", "Факультет", "Кафедра", "Керівник", "Курс", "День", "Час"
            };

            Rows = new ObservableCollection<Dictionary<string, string>>();

            
            for (int i = 1; i < results.Count; i += Columns.Count + 1)
            {
                var row = new Dictionary<string, string>();

                for (int j = 0; j < Columns.Count; j++)
                {
                    string columnName = Columns[j];
                    string value = (i + j) < results.Count ? results[i + j] : string.Empty;
                    row[columnName] = value;
                }

                Rows.Add(row);
            }

           
            BackCommand = new Command(async () => await Application.Current.MainPage.Navigation.PopAsync());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

