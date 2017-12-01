using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PictureSplitter.Annotations;

namespace PictureSplitter
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private readonly MainView _View;
        private string _filePath;
        private int _numParts;

        public MainViewModel(MainView xView)
        {
            _View = xView;
            PropertyChanged += OnFilePathChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string FilePath
        {
            get => _filePath;
            set
            {
                if (value == _filePath) return;
                _filePath = value;
                OnPropertyChanged();
            }
        }

        public int NumParts
        {
            get => _numParts;
            set
            {
                if (value == _numParts) return;
                _numParts = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnFilePathChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName != nameof(FilePath))
                return;

            var image = new BitmapImage(new Uri(FilePath));
            _View.ImageControl.Source = image;
        }
    }
}