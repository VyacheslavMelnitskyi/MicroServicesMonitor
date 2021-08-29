# MicroServicesMonitor

examples:

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
