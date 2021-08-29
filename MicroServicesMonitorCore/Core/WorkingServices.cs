using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using MicroServicesMonitorCore.Core.Config;
using MicroServicesMonitorCore.Enums;

namespace MicroServicesMonitorCore.Core
{
    public class WorkingServiceDataItem
    {
        public WorkingServiceDataItem()
        {
            Id = Guid.NewGuid().ToString().Replace('-','H');
        }

        public WorkingServiceDataItem(ConfigDataItem config):this()
        {
            Title = config.Title;
            Name = config.Name;
            Url = config.Url;
            StartProcess = config.StartProcess;
            StopProcess = config.StopProcess;
            StartCommand = config.StartCommand;
            StopCommand = config.StopCommand;
            StopType = config.StopType;
            CheckType = config.CheckType;
        }

        private bool _isOnline;
        private string _title;

        public string Title
        {
            get => string.IsNullOrWhiteSpace(_title) ? Name : _title;
            set => _title = value;
        }

        public string Name { get; set; }
        public string Url { get; set; }
        public string StartProcess { get; set; }
        public string StartCommand { get; set; }
        public string StopProcess { get; set; }
        public string StopCommand { get; set; }

        public StopType StopType { get; set; }
        public CheckType CheckType { get; set; }


        public bool IsOnline
        {
            get => _isOnline;
            set
            {
                var oldValue = _isOnline;
                _isOnline = value;

                if (oldValue != _isOnline)
                {
                    StatusChanged?.Invoke(this, new StatusChangedEventArg(Id, _isOnline));
                }
            }
        }

        public string Id { get; }
        
        public int? Pid { get; set; }
        
        public Process Proc { get; set; }
        
        public HttpClient HttpClient { get; set; }
       
        public event StatusChangedDelegate StatusChanged;

    }
    
    public class WorkingServices
    {
       public WorkingServices(SettingsConfig config)
        {
            Items = config.MicroServiceConfigs.Select(mc => new WorkingServiceDataItem(mc)).ToList(); ;
            SubscribeEvents();
        }

        public event StatusChangedDelegate AnyItemStatusChanged;
        public List<WorkingServiceDataItem> Items { get; }

        public void SubscribeEvents()
        {
            foreach (var workingServiceDataItem in Items)
            {
                workingServiceDataItem.StatusChanged += RaiseEventAnyItemStatusChanged;
            }
        }

        private void RaiseEventAnyItemStatusChanged(WorkingServiceDataItem senderServiceDataItem,StatusChangedEventArg eventArg)
        {
            AnyItemStatusChanged?.Invoke(senderServiceDataItem,eventArg);
        }

    }

    public delegate void StatusChangedDelegate(WorkingServiceDataItem senderService,StatusChangedEventArg arg);
    
    public class StatusChangedEventArg
    {
        public StatusChangedEventArg(string id, bool newStatus)
        {
            Id = id;
            NewStatus = newStatus;
        }

        public string Id { get; }
        public bool NewStatus { get; set; }
    }

}
