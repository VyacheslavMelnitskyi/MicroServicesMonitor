# MicroServicesMonitor

MicroServicesMonitor is a tool that helps works with a lot of micro-services in project but for some reason you cannot use Docker for them. (For example, processor, or motherboard or bios does not support virtualization; or slow processor, or not enough memory for docker). 
This tool can start/stop any micro-service and show it status and check is it working now?
<br/>
examples:
```json
config.json
{
    "Transparent": "false",
    "TopMost": "true",
    "TimerStart": "false",
    "MicroServiceConfigs":
    [{
            "Name": "Notepad",
            "Title": "Note",
            "Url": "testurl",
            "StartProcess": "notepad",
            "StartCommand": "c:\\Projects\\Windows\\MicroServicesMonitor\\MicroServicesMonitorForm\\bin\\Debug\\netcoreapp3.1\\config1.json",
            "StopProcess": "taskkill",
            "StopCommand": "/im notepad.exe",
            "StopType": "ProcessName",
            "CheckType": "ProcessName"

        }, {
            "Name": "Notepad",
            "Url": "",
            "StartProcess": "notepad",
            "StartCommand": "c:\\Projects\\Windows\\MicroServicesMonitor\\MicroServicesMonitorForm\\bin\\Debug\\netcoreapp3.1\\config1.json",
            "StopProcess": "taskkill",
            "StopCommand": "/im notepad.exe",
            "StopType": "WindowTitle",
            "CheckType": "WindowTitle"

        },
        {
            "Name": "Notepad",
            "Url": "test3url",
            "StartProcess": "notepad",
            "StartCommand": "c:\\Projects\\Windows\\MicroServicesMonitor\\MicroServicesMonitorForm\\bin\\Debug\\netcoreapp3.1\\config1.json",
            "StopProcess": "taskkill",
            "StopCommand": "/im notepad.exe",
            "StopType": "Command",
            "CheckType": "ProcessName"

        },
        {
            "Name": "Notepad",
            "Url": "http://google.com",
            "StartProcess": "cmd",
            "StartCommand": "/C copy /b c:\\Projects\\Windows\\MicroServicesMonitor\\MicroServicesMonitorForm\\bin\\Debug\\netcoreapp3.1\\config1.json c:\\Projects\\Windows\\MicroServicesMonitor\\MicroServicesMonitorForm\\bin\\Debug\\netcoreapp3.1\\config2.json",
            "StopProcess": "taskkill",
            "StopCommand": "/im notepad",
            "StopType": "WindowTitle",
            "CheckType": "Http"
        }

    ]
}
```