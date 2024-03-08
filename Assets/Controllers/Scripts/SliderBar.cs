using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SliderBar : MonoBehaviour
{
    public Action TouchAllAtOnce;

    [SerializeField] AudioSource _sound1;
    [SerializeField] AudioSource _sound2;
    [SerializeField] AudioSource _sound3;
    [SerializeField] AudioSource _sound4;
    [SerializeField] AudioSource _sound5;
    [SerializeField] AudioSource _sound6;
    [SerializeField] AudioSource _sound7;
    [SerializeField] AudioSource _sound8;
    [SerializeField] AudioSource _sound9;
    [SerializeField] AudioSource _soundFull;
    [SerializeField] GameObject _videoPlane;

    private Dictionary<ButtonType, bool> _isButtonOnMap;
    private Dictionary<ButtonType, AudioSource> _soundMap;

    private enum ButtonType
    {
        Button1,
        Button2,
        Button3,
        Button4,
        Button5,
        Button6,
        Button7,
        Button8,
        Button9,
    }

    #region Start
    private void Start()
    {
        this._isButtonOnMap = new Dictionary<ButtonType, bool>()
        {
            { ButtonType.Button1, false },
            { ButtonType.Button2, false },
            { ButtonType.Button3, false },
            { ButtonType.Button4, false },
            { ButtonType.Button5, false },
            { ButtonType.Button6, false },
            { ButtonType.Button7, false },
            { ButtonType.Button8, false },
            { ButtonType.Button9, false },
        };
        this._soundMap = new Dictionary<ButtonType, AudioSource>()
        {
            { ButtonType.Button1, this._sound1 },
            { ButtonType.Button2, this._sound2 },
            { ButtonType.Button3, this._sound3 },
            { ButtonType.Button4, this._sound4 },
            { ButtonType.Button5, this._sound5 },
            { ButtonType.Button6, this._sound6 },
            { ButtonType.Button7, this._sound7 },
            { ButtonType.Button8, this._sound8 },
            { ButtonType.Button9, this._sound9 },
        };
    }
    #endregion

    public void ShowInformation(bool isVisible)
    {
        this._videoPlane.SetActive(isVisible);
    }

    //// ボタン

    #region HoverButton1
    public void HoverButton1()
    {
        this.HoverButton(ButtonType.Button1);
    }
    #endregion

    #region HoverButton2
    public void HoverButton2()
    {
        this.HoverButton(ButtonType.Button2);
    }
    #endregion

    #region HoverButton3
    public void HoverButton3()
    {
        this.HoverButton(ButtonType.Button3);
    }
    #endregion

    #region HoverButton4
    public void HoverButton4()
    {
        this.HoverButton(ButtonType.Button4);
    }
    #endregion

    #region HoverButton5
    public void HoverButton5()
    {
        this.HoverButton(ButtonType.Button5);
    }
    #endregion

    #region HoverButton6
    public void HoverButton6()
    {
        this.HoverButton(ButtonType.Button6);
    }
    #endregion

    #region HoverButton7
    public void HoverButton7()
    {
        this.HoverButton(ButtonType.Button7);
    }
    #endregion

    #region HoverButton8
    public void HoverButton8()
    {
        this.HoverButton(ButtonType.Button8);
    }
    #endregion

    #region HoverButton9
    public void HoverButton9()
    {
        this.HoverButton(ButtonType.Button9);
    }
    #endregion

    //// ローカル

    #region HoverButton
    private void HoverButton(ButtonType buttonType)
    {
        this._isButtonOnMap[buttonType] = true;
        StartCoroutine(this.DelayOff(buttonType));
        if (this.CheckAllButtonOn())
        {
            this.InitCheck();
            this._soundFull.Play();
            this.TouchAllAtOnce?.Invoke();
        }
        else
        {
            this._soundMap[buttonType].Play();
        }
    }
    #endregion

    #region CheckAllButtonOn
    private bool CheckAllButtonOn()
    {
        return this._isButtonOnMap.Any(item => item.Value == false) == false;
    }
    #endregion

    #region InitCheck
    private void InitCheck()
    {
        this._isButtonOnMap.Keys.ToList().ForEach(buttonType => this._isButtonOnMap[buttonType] = false);
    }
    #endregion

    #region DelayOff（コルーチン）
    IEnumerator DelayOff(ButtonType buttonType)
    {
        yield return new WaitForSeconds(2);
        this._isButtonOnMap[buttonType] = false;
        yield return null;
    }
    #endregion
}
