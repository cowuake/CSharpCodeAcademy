using RgbToHexDesktopConverter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RgbToHexDesktopConverter.ViewModels
{
    public class ColorViewModel : INotifyPropertyChanged
    {
        private ColorModel _model;

        public ColorViewModel() : this(255, 255, 255) { }

        public ColorViewModel(byte red, byte green, byte blue)
        {
            _model = new ColorModel(red, green, blue);
        }

        public byte Red
        {
            get => _model.R;

            set
            {
                if (_model.R != value)
                {
                    _model.R = value;
                    _model.FromRgbToHex();
                    Hex = _model.Hex;
                    NotifyPropertyChanged(nameof(Red));
                }
            }
        }

        public byte Green
        {
            get => _model.G;

            set
            {
                if (_model.G != value)
                {
                    _model.G = value;
                    _model.FromRgbToHex();
                    Hex = _model.Hex;
                    NotifyPropertyChanged(nameof(Green));
                }
            }
        }

        public byte Blue
        {
            get => _model.B;

            set
            {
                if (_model.R != value)
                {
                    _model.B = value;
                    _model.FromRgbToHex();
                    Hex = _model.Hex;
                    NotifyPropertyChanged(nameof(Blue));
                }
            }
        }

        public string Hex
        {
            get => _model.Hex;

            set
            {
                _model.Hex = value;
                _model.FromHexToRgb();
                Red = _model.R;
                Green = _model.G;
                Blue = _model.B;
                NotifyPropertyChanged(nameof(Hex));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        //private void OnNotifying(string propName)
        //{
        //    if (PropertyChanged != null)
        //        this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        //}
    }
}