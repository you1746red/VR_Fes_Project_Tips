using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    public Action AllDestroy;

    public Action CreateBigEnterKey;
    public Action CreateKeyBoard;
    public Action CreateRealPlane;

    public Action CreateAngleSphere;
    public Action CreateSliderBar;
    public Action CreateFloorOnObject;

    public Action CreateTaketonbo;

    public Action CreateGizmoSphere;
    public Action FarGizmoSphere;
    public Action NearGizmoSphere;
    public Action<float> ChangeDistance;

    [SerializeField] Slider _slider;

    // ボタンクリック

    #region PushLeftArrow
    public void PushLeftArrow()
    {
        var param = this.gameObject.GetComponent<CircleArrangement>().GetParam();
        var newParam = new CircleArrangementData(
            param.Distance,
            param.HeightDiff,
            param.XAngle,
            param.YAngle - 45f);
        this.gameObject.GetComponent<CircleArrangement>().SetParam(newParam);
        this.gameObject.GetComponent<CircleArrangement>().Deploy();
    }
    #endregion

    #region PushRightArrow
    public void PushRightArrow()
    {
        var param = this.gameObject.GetComponent<CircleArrangement>().GetParam();
        var newParam = new CircleArrangementData(
            param.Distance,
            param.HeightDiff,
            param.XAngle,
            param.YAngle + 45f);
        this.gameObject.GetComponent<CircleArrangement>().SetParam(newParam);
        this.gameObject.GetComponent<CircleArrangement>().Deploy();
    }
    #endregion

    #region PushExit
    public void PushExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //ゲームプレイ終了
#else
        Application.Quit();//ゲームプレイ終了
#endif
    }
    #endregion

    #region PushDestroy
    public void PushDestroy()
    {
        this.AllDestroy?.Invoke();
    }
    #endregion

    #region PushBigEnterKey
    public void PushBigEnterKey()
    {
        this.CreateBigEnterKey?.Invoke();
    }
    #endregion

    #region PushKeyBoard
    public void PushKeyBoard()
    {
        this.CreateKeyBoard?.Invoke();
    }
    #endregion

    #region PushRealPanel
    public void PushRealPanel()
    {
        this.CreateRealPlane?.Invoke();
    }
    #endregion

    #region PushAngleSphere
    public void PushAngleSphere()
    {
        this.CreateAngleSphere?.Invoke();
    }
    #endregion

    #region PushSliderBar
    public void PushSliderBar()
    {
        this.CreateSliderBar?.Invoke();
    }
    #endregion

    #region PushFloorOnObject
    public void PushFloorOnObject()
    {
        this.CreateFloorOnObject?.Invoke();
    }
    #endregion

    #region PushTaketonbo
    public void PushTaketonbo()
    {
        this.CreateTaketonbo?.Invoke();
    }
    #endregion

    #region PushGizmoSphere
    public void PushGizmoSphere()
    {
        this.CreateGizmoSphere?.Invoke();
    }
    #endregion

    #region PushFar
    public void PushFar()
    {
        this.FarGizmoSphere?.Invoke();
    }
    #endregion

    #region PushNear
    public void PushNear()
    {
        this.NearGizmoSphere?.Invoke();
    }
    #endregion

    #region SliderValueChanged
    public void SliderValueChanged()
    {
        this.ChangeDistance?.Invoke(this._slider.value * 2);
    }
    #endregion
}
