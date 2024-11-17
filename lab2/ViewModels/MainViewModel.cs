using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using lab2.XmlProcessingStrategy;

namespace lab2.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly FileHandler fileHandlerService;
        private readonly HtmlTransformer htmlTransformerService;
        private IXmlProcessing strategy;

        public string WorkshopName { get; set; }
        public string Faculty { get; set; }
        public string Department { get; set; }
        public string Leader { get; set; }
        public string Course { get; set; }
        public string Day { get; set; }

        
        public bool IsSAX { get; set; }
        public bool IsDOM { get; set; }
        public bool IsLINQ { get; set; }

        
        public string FileLoadStatus { get; set; }

        
        public ICommand SelectFileCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand TransformToHtmlCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand ExitCommand { get; }

        public MainViewModel()
        {
            fileHandlerService = new FileHandler();
            htmlTransformerService = new HtmlTransformer();

            SelectFileCommand = new Command(SelectFile);
            SearchCommand = new Command(Search);
            TransformToHtmlCommand = new Command(TransformToHtml);
            ClearCommand = new Command(Clear);
            ExitCommand = new Command(Exit);
        }

        private async void SelectFile()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync();
                if (result != null)
                {
                    fileHandlerService.LoadFile(result.FullPath);
                    FileLoadStatus = $"File loaded successfully: {result.FileName}";
                }
                else
                {
                    FileLoadStatus = "File selection canceled.";
                }
            }
            catch (Exception ex)
            {
                FileLoadStatus = $"Error loading file: {ex.Message}";
            }

            OnPropertyChanged(nameof(FileLoadStatus));
        }

        private void Search()
        {
            try
            {
                if (fileHandlerService.XmlContent == null)
                {
                    FileLoadStatus = "No XML file is loaded. Please load a file first.";
                    OnPropertyChanged(nameof(FileLoadStatus));
                    return;
                }

                
                var criteria = new Dictionary<string, string>
        {
            { "workshopName", WorkshopName },
            { "faculty", Faculty },
            { "department", Department },
            { "leader", Leader },
            { "course", Course },
            { "day", Day }
        }.Where(c => !string.IsNullOrWhiteSpace(c.Value)) 
                 .ToDictionary(c => c.Key, c => c.Value);

                if (!criteria.Any())
                {
                    FileLoadStatus = "Please enter at least one search criterion.";
                    OnPropertyChanged(nameof(FileLoadStatus));
                    return;
                }

                
                strategy = GetSelectedStrategy();

                if (strategy == null)
                {
                    FileLoadStatus = "Please select a parsing strategy.";
                    OnPropertyChanged(nameof(FileLoadStatus));
                    return;
                }

                
                var searchService = new SearchCommandService(strategy);
                var results = searchService.ExecuteSearch(criteria, fileHandlerService.XmlContent);

                if (results.Matches.Any())
                {
                    var resultsPage = new ResultsPage(results.GetResultsAsStrings());
                    Application.Current.MainPage.Navigation.PushAsync(resultsPage);
                }
                else
                {
                    FileLoadStatus = "No matching results found.";
                }
            }
            catch (Exception ex)
            {
                FileLoadStatus = $"Error during search: {ex.Message}";
            }

            OnPropertyChanged(nameof(FileLoadStatus));
        }

        private void TransformToHtml()
        {
            try
            {
                var outputFilePath = htmlTransformerService.TransformToHtml(fileHandlerService.XmlContent);
                FileLoadStatus = $"HTML file generated: {outputFilePath}";

                
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = outputFilePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                FileLoadStatus = $"Error transforming to HTML: {ex.Message}";
            }

            OnPropertyChanged(nameof(FileLoadStatus));
        }

        private void Clear()
        {
            WorkshopName = string.Empty;
            Faculty = string.Empty;
            Department = string.Empty;
            Leader = string.Empty;
            Course = string.Empty;
            Day = string.Empty;

            
            OnPropertyChanged(nameof(WorkshopName));
            OnPropertyChanged(nameof(Faculty));
            OnPropertyChanged(nameof(Department));
            OnPropertyChanged(nameof(Leader));
            OnPropertyChanged(nameof(Course));
            OnPropertyChanged(nameof(Day));

            
            FileLoadStatus = string.Empty;
            OnPropertyChanged(nameof(FileLoadStatus));
            FileLoadStatus = "Fields cleared.";
            OnPropertyChanged(nameof(FileLoadStatus));
        }

        private async void Exit()
        {
            var result = await Application.Current.MainPage.DisplayAlert(
                "Exit",
                "Do you really want to exit the application?",
                "Yes",
                "No");

            if (result)
            {
                Application.Current.Quit();
            }
        }
   
        private IXmlProcessing GetSelectedStrategy()
        {
            if (IsSAX) return new XmlSAXProcessing();
            if (IsDOM) return new XmlDOMProcessing();
            if (IsLINQ) return new XmlLINQProcessing();
            return null; 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
