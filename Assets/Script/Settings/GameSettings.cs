using System;
using System.IO;
using Util;

namespace Settings
{
    public static class GameSettings
    {
        private const string Filetype = "ckn";
        private const string Filename = "settings";

        private static readonly string
            FolderPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/chickens3d/";

        private static GameSettingsFile _file;

        public static GameSettingsFile Get
        {
            get
            {
                if(_file == null)
                {
                    Load();
                }

                return _file;
            }
        }

        private static string GetSettingsFullPath()
        {
            return $"{FolderPath}/{Filename}.{Filetype}";
        }

        public static void Save()
        {
            JsonHelper.Save(GetSettingsFullPath(), _file);
        }

        public static void Load()
        {
            //  checks if directory exists, if not - creates new directory
            Directory.CreateDirectory(FolderPath);

            string fullPath = GetSettingsFullPath();

            //  if no settings file could be found - create new & save
            if(!File.Exists(fullPath))
            {
                CreateNew();
            }
            else
            {
                if((_file = JsonHelper.Load<GameSettingsFile>(fullPath)) == null)
                    CreateNew();
            }
        }

        private static void CreateNew()
        {
            _file = new GameSettingsFile();
            Save();
        }
    }


}