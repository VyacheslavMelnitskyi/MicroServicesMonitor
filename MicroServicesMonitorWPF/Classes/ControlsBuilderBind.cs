using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MicroServicesMonitorCore.Core;
using MicroServicesMonitorCore.Enums;
using MicroServicesMonitorCore.View;

namespace MicroServicesMonitorWPF.Classes
{
    public class ControlsBuilderBind
    {
        private const char Separator = '_';
        private readonly ServiceManager _serviceManager;
        public ControlsBuilderBind(ServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public List<ContentControl> Build(ViewElements elements, Control mainForm, Grid grid)
        {
            mainForm.Height = elements.TotalHeight;
            mainForm.Width = elements.TotalWidth;

            var result = new List<ContentControl>();
            grid.DataContext = elements;
            foreach (var element in elements.Elements)
            {
                var control = CreateControl(element);
                result.Add(control);
                grid.Children.Add(control);
            }

            
            return result;
        }
        
        private ContentControl CreateControl(Element element)
        {
            var result = element.Type == ElementType.Label ? (ContentControl)new Label() : new Button();
            UpdateStatus(element, result);

            result.Width = element.Width;
            result.Height = element.Height;
            result.Margin = new Thickness(element.X, element.Y,0,0);

            result.Name = GetControlName(element);
            result.HorizontalAlignment = HorizontalAlignment.Left;
            result.VerticalAlignment = VerticalAlignment.Top;
            if (element.Type == ElementType.Button)
            {
                ((Button)result).Click += Button_Click;
            }

            return result;

        }

        private static string GetControlName(Element element)
        {
            return $"{element.Type}{Separator}{element.Id}";
        }

        private void UpdateStatus(Element element, ContentControl result)
        {
            if (element.Color.HasValue)
            {
                Binding bindingColor = new Binding("Color") { Source = element,Converter = new ColorConvertor() };
                result.SetBinding(Control.BackgroundProperty, bindingColor);
            }

            Binding binding = new Binding("Text") {Source = element};
            result.SetBinding( ContentControl.ContentProperty, binding);

        }
        

        public void Button_Click(object sender, EventArgs e)
        {
            var id = ((Control)sender).Name.Split(Separator).Last();
            try
            {
                _serviceManager.DoAction(id);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                //todo write to log
            }
        }

    }
}