using UnityEngine;

namespace Assets.Scripts.Save
{
    public class GameSaves
    {
        public static CipherType PresentedCipher
        {
            get => (CipherType)PlayerPrefs.GetInt("PresentedCipher", 0);
            set => PlayerPrefs.SetInt("PresentedCipher", (int)value);
        }

        public Localization Localization = Localization.RU;

        public float Balance {  get; set; }

        private GameSaves() { }
        private static GameSaves _instance;
        public static GameSaves Instance
        {
            get
            {
                if (_instance == null)
                    _instance = GetLatestSave() ?? new GameSaves();

                return _instance;
            }
            set => _instance = value;
        }

        public static GameSaves GetLatestSave()
        {
            SavesSerializer.TryLoad(Paths.SAVES_FILENAME, out GameSaves save);

            return save;
        }

        public void Save() => SavesSerializer.Save(this, Paths.SAVES_FILENAME);
    }
}
