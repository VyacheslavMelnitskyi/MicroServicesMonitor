using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MicroServicesMonitorCore.Core;
using MicroServicesMonitorCore.Enums;
using MicroServicesMonitorCore.View;

namespace MicroServicesMonitorWPF.Classes
{
    [Obsolete("Use ControlsBuilderBind (ControlsBuilder worked as Windows Forms not WPF)")]
    public class ControlsBuilder
    {
        private const char Separator = '_';

        private readonly ServiceManager _serviceManager;
        public ControlsBuilder(ServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public List<ContentControl> Build(ViewElements elements, Control mainForm, Grid grid)
        {
            mainForm.Height = elements.TotalHeight;
            mainForm.Width = elements.TotalWidth;

            var result = new List<ContentControl>();

            foreach (var element in elements.Elements)
            {
                var control = CreateControl(element);
                result.Add(control);
                grid.Children.Add(control);
            }

            return result;
        }

        public void UpdateControl(List<ContentControl> controls, Element element)
        {
            var controlName = GetControlName(element);
            var control = controls.First(x => x.Name == controlName);

            UpdateStatusInvoke(element, control);
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

        private static void UpdateStatus(Element element, ContentControl result)
        {
            if (element.Color.HasValue)
            {
                result.Background = new SolidColorBrush(ToMediaColor(element.Color.Value));
            }

            result.Content = element.Text;
        }

        public static Color ToMediaColor(System.Drawing.Color color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        private static void UpdateStatusInvoke(Element element, ContentControl result)
        {
            if (element.Color.HasValue)
            {
                result.Dispatcher.Invoke(() => { result.Background = new SolidColorBrush(ToMediaColor(element.Color.Value)); });
            }

            result.Dispatcher.Invoke(() => { result.Content = element.Text; });
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