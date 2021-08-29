using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MicroServicesMonitorCore.Core;
using MicroServicesMonitorCore.Enums;

namespace MicroServicesMonitorCore.View
{
    public class CalcControls
    {
        private const int InitialTopPadding = 80+40;

        private const int DefaultHeight = 30;

        private const int DefaultTopPadding = 10;

        private const int DefaultWidth = 100;

        private const int DefaultWidthButton = 100;

        private const int DefaultLeftPadding = 10;


        public ViewElements CalcElements(WorkingServices services)
        {
            var result = new ViewElements()
            {
                TotalHeight = services.Items.Count*(DefaultHeight+DefaultTopPadding)+ DefaultTopPadding+40+InitialTopPadding,
                TotalWidth = 2*(DefaultWidth+DefaultLeftPadding)+ DefaultLeftPadding + 20,
                Elements = new List<Element>()
            };

            var index = 0;
            foreach (var serviceDataItem in services.Items)
            {
                result.Elements.Add(CreateLabelName(serviceDataItem,index));
                result.Elements.Add(CreateStatus(serviceDataItem, index));
                index++;
            }
            
            return result;
        }
        
        public Element UpdateElement(WorkingServiceDataItem service)
        {
            var result = CreateStatusElementBase(service);

            return result;
        }


        private Element CreateLabelName(WorkingServiceDataItem serviceDataItem, int index)
        {
            return new Element()
            {
                Text = serviceDataItem.Title,
                Height = DefaultHeight,
                Width = DefaultWidth,
                X = DefaultLeftPadding,
                Y = (DefaultHeight + DefaultTopPadding) * index + DefaultTopPadding + InitialTopPadding,
                Id = serviceDataItem.Id,
                Type = ElementType.Label
            };
        }


        private Element CreateStatus(WorkingServiceDataItem serviceDataItem, int index)
        {
            var result = CreateStatusElementBase(serviceDataItem);
            
            result.Height = DefaultHeight;
            result.Width = DefaultWidthButton;
            result.X = 2*DefaultLeftPadding+DefaultWidth;
            result.Y = (DefaultHeight + DefaultTopPadding) * index + DefaultTopPadding + InitialTopPadding;
            
            return result;
        }

        private static Element CreateStatusElementBase(WorkingServiceDataItem serviceDataItem)
        {
            var result = new Element();
            UpdateElement(serviceDataItem, result);
            return result;
        }

        private static void UpdateElement(WorkingServiceDataItem serviceDataItem, Element result)
        {
            result.Text = serviceDataItem.IsOnline ? "Stop" : "Start";
            result.Id = serviceDataItem.Id;
            result.Type = ElementType.Button;
            result.Color = serviceDataItem.IsOnline ? Color.LightGreen : Color.Red;
        }

        public Element UpdateElement(WorkingServiceDataItem senderService, ViewElements elements, ElementType type = ElementType.Button)
        {
            
            var result = elements.Elements.First(x=>x.Id.Contains(senderService.Id) && x.Type== type);

            UpdateElement(senderService,result);

            return result;
        }
    }
}
