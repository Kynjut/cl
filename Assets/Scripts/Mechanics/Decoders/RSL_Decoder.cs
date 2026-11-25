using UnityEngine;

namespace Assets.Scripts.Mechanics.Decoders
{
    public class RSL_Decoder : Decoder
    {
        [SerializeField] private GameObject _decodeScreen;
        [SerializeField] private InputKeysReaderWithChecker _reader;

        public override void Decode()
        {
            _decodeScreen.SetActive(true);
            //_decodeScreen.GetComponent<RSL_DecodeScreen>().SetCipher();
        }
    }
}
