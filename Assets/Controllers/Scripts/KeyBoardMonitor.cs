using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoardMonitor : MonoBehaviour
{
    const int KEY_STEP = 6;

    [SerializeField] TextAsset _textAsset;
    [SerializeField] TextMeshProUGUI _textMeshPro;
    [SerializeField] Scrollbar _scrollBar;

    private int _keyCount = 0;
    private string _code = "12345 67890 \n12345 67890 \n12345 67890 \n12345 67890 \n12345 67890 \n";

    #region Initialize
    public void Initialize()
    {
        this._code = this._textAsset.text;
        this.ResetCode();
    }
    #endregion

    #region InputCode
    public void InputCode()
    {
        int nextKeyCount = 1;
        this._keyCount += nextKeyCount;

        int nextCodeLength = KEY_STEP * nextKeyCount;
        if (this._textMeshPro.text.Length + KEY_STEP >= this._code.Length)
        {
            nextCodeLength = this._code.Length - this._textMeshPro.text.Length;
        }

        string nextCode = this._code.Substring(0, this._keyCount * KEY_STEP + nextCodeLength);
        this._textMeshPro.text = nextCode;
        this._scrollBar.value = 0;
    }
    #endregion

    #region ResetCode
    public void ResetCode()
    {
        this._code = this._code.Substring(this._textMeshPro.text.Length, this._code.Length - this._textMeshPro.text.Length);
        this._keyCount = 0;
        this._textMeshPro.text = string.Empty;
    }
    #endregion
}
