using AutoMapper;
using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloombergTrader.Client.GUI
{


    public class SettingsViewModel : ReactiveObject, ISettingsViewModel
    {
        private static readonly string _bloombergTraderLocalFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\BloombergTrader";

        public SettingsViewModel()
        {
            if (!Directory.Exists(_bloombergTraderLocalFolderPath))
            {
                Directory.CreateDirectory(_bloombergTraderLocalFolderPath);
            }
        }

        private string _serverHost;
        public string ServerHost
        {
            get { return _serverHost; }
            set { this.RaiseAndSetIfChanged(ref _serverHost, value); }
        }


        private int _serverPort;
        public int ServerPort
        {
            get { return _serverPort; }
            set { this.RaiseAndSetIfChanged(ref _serverPort, value); }
        }


        public static void Save(SettingsViewModel settings)
        {
            string settingFilePath = _bloombergTraderLocalFolderPath + "\\UserSettings.json";
            
            var result = JsonConvert.SerializeObject(Mapper.Map<Settings>(settings));
            File.WriteAllText(settingFilePath, result);
        }

        public static SettingsViewModel Load()
        {
            string settingFilePath = _bloombergTraderLocalFolderPath + "\\UserSettings.json";

            SettingsViewModel settingsViewModel = null;
            if (File.Exists(settingFilePath))
            {
                var settings= JsonConvert.DeserializeObject<Settings>(File.ReadAllText(settingFilePath));
                settingsViewModel = new SettingsViewModel
                {
                    ServerHost = settings.ServerHost,
                    ServerPort = settings.ServerPort
                };
            }
            else
            {
                settingsViewModel = new SettingsViewModel
                {
                    ServerHost = "127.0.0.1",
                    ServerPort = 8194
                };
            }
            return settingsViewModel;

        }
    }
}
