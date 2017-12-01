using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PictureSplitter.Annotations;
using Point = System.Windows.Point;

namespace PictureSplitter
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private static readonly Random Random = new Random();
        private readonly MainView _View;
        private string _filePath = @"C:\Users\Jan Bader\Desktop\1.bild.jp";
        private HashSet<Point> _loadedParts = new HashSet<Point>();
        private int _numParts = 5;

        public MainViewModel(MainView xView)
        {
            _View = xView;
            PropertyChanged += OnFilePathChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public BitmapImage BaseImage { get; set; }

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

        public WriteableBitmap Image { get; private set; }

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
            if (_loadedParts.Count == NumParts * NumParts)
                return;

            int partX;
            int partY;
            do
            {
                partX = Random.Next(0, NumParts);
                partY = Random.Next(0, NumParts);
            } while (_loadedParts.Contains(new Point(partX, partY)));

            _loadedParts.Add(new Point(partX, partY));

            var width = BaseImage.PixelWidth / NumParts;
            var height = BaseImage.PixelHeight / NumParts;

            var writeable = new WriteableBitmap(BaseImage);
            writeable = writeable.Crop(width * partX, height * partY, width, height);

            Image.Blit(new Rect(width * partX, height * partY, width, height), writeable, new Rect(0, 0, width, height));

            SetImage(Image);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnFilePathChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName != nameof(FilePath) && propertyChangedEventArgs.PropertyName != nameof(NumParts))
                return;

            _loadedParts = new HashSet<Point>();
            BaseImage = new BitmapImage(new Uri(FilePath));
            var writeable = new WriteableBitmap(BaseImage);
            writeable.Clear(Colors.White);
            SetImage(writeable);
            NextPart();
        }

        private void SetImage(WriteableBitmap writeableBitmap)
        {
            Image = writeableBitmap;
            _View.ImageControl.Source = Image;
        }
    }
}