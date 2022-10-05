namespace Settings
{
    public class GameSettingsFile
    {
        public GameSetting<float> MasterVolume = new("mastervol", 0.5f);
        public GameSetting<float> SfxVolume = new("sfxvol", 1f);
        public GameSetting<float> MusicVolume = new("musicvol", 0.5f);
    }
}