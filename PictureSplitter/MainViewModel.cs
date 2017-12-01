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
        private static readonly Random Random = new Random();
        private readonly MainView _View;
        private string _filePath = @"C:\Users\Jan Bader\Desktop\1.bild.jp";
        private int _numParts = 5;

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

        public BitmapImage Image { get; set; }

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

        public void NextPart()
        {
            Image = new BitmapImage(new Uri(FilePath));
            var writeable = new WriteableBitmap(Image);

            var part = Random.Next(0, NumParts);

            var width = Image.PixelWidth / NumParts;
            var height = Image.PixelHeight / NumParts;
            writeable = writeable.Crop(width * part, height * part, width, height);

            _View.ImageControl.Source = writeable.Clone();
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

            NextPart();
        }
    }
}