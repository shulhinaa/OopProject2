using System.Collections.Generic;
using lab2.ViewModels;
using lab2.XmlProcessingStrategy; // Ensure this namespace matches your project structure

namespace lab2
{
    public partial class ResultsPage : ContentPage
    {
        public ResultsPage(List<string> results)
        {
            InitializeComponent();
            BindingContext = new ResultsViewModel(results);
        }
    }
}
