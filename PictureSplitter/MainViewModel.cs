using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using PictureSplitter.Annotations;

namespace PictureSplitter
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            PropertyChanged += OnFilePathChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string FilePath { get; set; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnFilePathChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var image = Image.FromFile(FilePath);
        }
    }
}