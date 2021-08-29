using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using MicroServicesMonitorCore.Annotations;
using MicroServicesMonitorCore.Enums;

namespace MicroServicesMonitorCore.View
{
    public class Element: INotifyPropertyChanged
    {
        private string _text;
        private Color? _color;
        public int Width { get; set; }
        public int Height { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public ElementType Type { get; set; }

        public System.Drawing.Color? Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }
        
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public string Id { get; set; }

        public Action<Guid> OnAction { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    

}
