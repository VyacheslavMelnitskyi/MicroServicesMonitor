using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MicroServicesMonitorCore.Core;
using MicroServicesMonitorCore.Enums;
using MicroServicesMonitorCore.View;

namespace MicroServicesMonitor.Classes
{
    public class ControlsBuilder
    {
        private readonly ServiceManager _serviceManager;
        private const char Separator = '_';
        public ControlsBuilder(ServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public List<Control> Build(ViewElements elements, Control mainForm)
        {
            mainForm.Height = elements.TotalHeight;
            mainForm.Width = elements.TotalWidth;

            var result = new List<Control>();

            foreach (var element in elements.Elements)
            {
                var control = CreateControl(element);
                result.Add(control);
                mainForm.Controls.Add(control);
            }
            
            return result;
        }
        
        public void UpdateControl(List<Control> controls, Element element)
        {
            var controlName = GetControlName(element);
            var control = controls.First(x => x.Name == controlName);

            UpdateStatusInvoke(element, control);
        }

        private Control CreateControl(Element element)
        {
            var result = element.Type == ElementType.Label ? (Control)new Label() : new Button();
            UpdateStatus(element, result);

            result.Width = element.Width;
            result.Height = element.Height;
            result.Location = new Point(element.X,element.Y);
            
            result.Name = GetControlName(element);

            if (element.Type == ElementType.Button)
            {
                result.Click += Button_Click;
            }
            if (element.Type == ElementType.Label)
            {
                CreateToolTipForLabel(result,_serviceManager.GetUrl(result.Name.Split(Separator).Last()));
                result.Click += Label_Click;
            }

            return result;

        }

        private static string GetControlName(Element element)
        {
            return $"{element.Type}{Separator}{element.Id}";
        }

        private static void UpdateStatus(Element element, Control result)
        {
            if (element.Color.HasValue)
            {
                result.BackColor = element.Color.Value;
                
            }

            result.Text = element.Text;
        }

        private static void UpdateStatusInvoke(Element element, Control result)
        {
            if (element.Color.HasValue)
            {
                result.Invoke((MethodInvoker) (() => { result.BackColor = element.Color.Value; }));
            }

            result.Invoke((MethodInvoker) (() => { result.Text = element.Text; }));
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


        public void Label_Click(object sender, EventArgs e)
        {
            var id = ((Control)sender).Name.Split(Separator).Last();
            try
            {
                var url = _serviceManager.GetUrl(id);
                Clipboard.SetText(url);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                //todo write to log
            }
        }

        public void CreateToolTipForLabel(Control label, string text)
        {
            var labelToolTip = new ToolTip();
            //The below are optional, of course,
            
            labelToolTip.SetToolTip(label, text);
        }


    }
}
