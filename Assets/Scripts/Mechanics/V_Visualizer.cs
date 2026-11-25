using Assets.Scripts.Cyphers;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Ciphers
{
    public class V_Visualizer : MonoBehaviour
    {
        public enum Mode { Encode, Decode }

        [SerializeField] private TextMeshProUGUI matrixText;

        [SerializeField] private string normalHex = "#FFFFFF";
        [SerializeField] private string yellowHex = "#FFEB00";
        [SerializeField] private string greenHex = "#33D94D";

        private char[] _alphabet;
        private int _n;

        private int _rowIdx = -1;    
        private int _colIdx = -1;    
        private char _resultChar;
        private bool _hasResult;
        private Mode _mode = Mode.Decode;

        public void Init(char firstChar, Localization localization = Localization.RU, Mode mode = Mode.Decode)
        {
            _alphabet = AlphabetManager.GetAlphabet(localization);
            _n = _alphabet.Length;
            _mode = mode;

            SetRowChar(firstChar);
        }

        public void SetMode(Mode mode)
        {
            if (_mode == mode) return;
            _mode = mode;
            _hasResult = false;
            RebuildText();
        }

        public void SetRowChar(char c)
        {
            int idx = GetIndex(c);
            if (idx < 0) return;

            _rowIdx = idx;
            _colIdx = -1;
            _hasResult = false;
            RebuildText();
        }

        public void SetColChar(char c)
        {
            if (_rowIdx < 0) return;
            int idx = GetIndex(c);
            if (idx < 0) return;

            _colIdx = idx;
            _resultChar = ComputeResult(_rowIdx, _colIdx);
            _hasResult = true;
            RebuildText();
        }

        public char GetResult() => _resultChar;
        public bool HasResult => _hasResult;

        private char ComputeResult(int row, int col)
        {
            int v = _mode == Mode.Encode
                ? (row + col) % _n
                : ((row - col) % _n + _n) % _n;
            return _alphabet[v];
        }

        private int GetIndex(char c)
        {
            c = char.ToUpperInvariant(c);
            for (int i = 0; i < _n; i++)
                if (_alphabet[i] == c)
                    return i;
            return -1;
        }

        private void RebuildText()
        {
            var sb = new System.Text.StringBuilder();
            const string space = "\u00A0";

            sb.Append(Colored(space, normalHex)).Append(space);
            for (int col = 0; col < _n; col++)
            {
                string color = (_hasResult && col == _colIdx) ? yellowHex : normalHex;
                sb.Append(Colored(_alphabet[col].ToString(), color)).Append(space);
            }
            sb.Append('\n');

            for (int row = 0; row < _n; row++)
            {
                string rowHeaderColor = (_rowIdx == row) ? yellowHex : normalHex;
                sb.Append(Colored(_alphabet[row].ToString(), rowHeaderColor)).Append(space);

                for (int col = 0; col < _n; col++)
                {
                    char ch = _mode == Mode.Encode
                        ? _alphabet[(row + col) % _n]
                        : _alphabet[((row - col) % _n + _n) % _n];

                    string color = normalHex;
                    if (_rowIdx == row)
                    {
                        color = (_hasResult && col == _colIdx) ? greenHex : yellowHex;
                    }
                    else if (_hasResult && col == _colIdx)
                    {
                        color = yellowHex;
                    }

                    sb.Append(Colored(ch.ToString(), color)).Append(space);
                }
                sb.Append('\n');
            }

            matrixText.text = sb.ToString();
        }

        private static string Colored(string s, string hex) => $"<color={hex}>{s}</color>";
    }
}