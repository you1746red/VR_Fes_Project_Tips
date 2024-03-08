using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelManager : MonoBehaviour
{
    [SerializeField] GameObject _controlPanelPrefab;

    [SerializeField] GizmosSphereCreater _gizmosSphereCreater;
    [SerializeField] BigEnterKeyCreater _bigEnterKeyCreater;
    [SerializeField] RealPlaneCreater _realPlaneCreater;
    [SerializeField] FloorOnObjectCreater _floorOnObjectCreater;
    [SerializeField] AngleSphereCreater _angleSphereCreater;
    [SerializeField] KeyBoardCreater _keyBoardCreater;
    [SerializeField] SliderBarCreater _sliderBarCreater;

    private GameObject _controlPanel;

    #region Create
    public void Create()
    {
        this._controlPanel = Instantiate(this._controlPanelPrefab, Vector3.zero, Quaternion.identity, this.transform);

        // イベント登録
        this._controlPanel.GetComponent<ControlPanel>().AllDestroy += this.OnAllDestroy;
        this._controlPanel.GetComponent<ControlPanel>().CreateBigEnterKey += this.OnCreateBigEnterKey;
        this._controlPanel.GetComponent<ControlPanel>().CreateRealPlane += this.OnRealPlaneTimeStart;
        this._controlPanel.GetComponent<ControlPanel>().CreateFloorOnObject += this.OnCreateFloorOnObject;
        this._controlPanel.GetComponent<ControlPanel>().CreateAngleSphere += this.OnCreateAngleSphere;
        this._controlPanel.GetComponent<ControlPanel>().CreateKeyBoard += this.OnCreateKeyBoard;
        this._controlPanel.GetComponent<ControlPanel>().CreateSliderBar += this.OnCreateSliderBar;
        this._controlPanel.GetComponent<ControlPanel>().CreateGizmoSphere += this.OnCreateGizmosSphere;
        this._controlPanel.GetComponent<ControlPanel>().FarGizmoSphere += this.OnDistanceFar;
        this._controlPanel.GetComponent<ControlPanel>().NearGizmoSphere += this.OnDistanceNear;
        this._controlPanel.GetComponent<ControlPanel>().ChangeDistance += this.OnChangeDistance;

        // パネル配置
        this._controlPanel.GetComponent<CircleArrangement>().SetParam(new CircleArrangementData(0.6f, -0.4f, 30f, 0f));
        this._controlPanel.GetComponent<CircleArrangement>().Deploy();
    }
    #endregion

    public void ReDeploy()
    {
        this._gizmosSphereCreater.ReDeploy();
    }

    // イベント

    #region OnAllDestroy
    private void OnAllDestroy()
    {
        this._gizmosSphereCreater.DestroyInChildren();
        this._bigEnterKeyCreater.DestroyInChildren();
        this._realPlaneCreater.TimeStop();
        this._realPlaneCreater.DestroyInChildren();
        this._floorOnObjectCreater.DestroyInChildren();
        this._angleSphereCreater.DestroyInChildren();
        this._keyBoardCreater.DestroyInChildren();
        this._sliderBarCreater.DestroyInChildren();
    }
    #endregion

    #region OnCreateSliderBar
    private void OnCreateSliderBar()
    {
        Debug.Log("@@@ OnCreateSliderBar");
        this._sliderBarCreater.Create();
    }
    #endregion

    #region OnCreateFloorOnObject
    private void OnCreateFloorOnObject()
    {
        Debug.Log("@@@ OnCreateFloorOnObject");
        this._floorOnObjectCreater.Create();
    }
    #endregion

    #region OnCreateAngleSphere
    private void OnCreateAngleSphere()
    {
        Debug.Log("@@@ OnCreateAngleSphere");
        this._angleSphereCreater.Create();
    }
    #endregion

    #region OnCreateBigEnterKey
    private void OnCreateBigEnterKey()
    {
        this._bigEnterKeyCreater.Create();
    }
    #endregion

    #region OnCreateKeyBoard
    private void OnCreateKeyBoard()
    {
        this._keyBoardCreater.Create();
    }
    #endregion

    #region OnRealPlaneTimeStart
    private void OnRealPlaneTimeStart()
    {
        if (this._realPlaneCreater.IsStart == false)
        {
            this._realPlaneCreater.TimeStart(1f);
        }
        else
        {
            this._realPlaneCreater.TimeStop();
            this._realPlaneCreater.DestroyInChildren();
        }
    }
    #endregion

    #region OnCreateGizmosSphere
    private void OnCreateGizmosSphere()
    {
        this._gizmosSphereCreater.Create();
    }
    #endregion

    #region OnDistanceFar
    private void OnDistanceFar()
    {
        this._gizmosSphereCreater.ChangeDistanceDiff(0.1f);
    }
    #endregion

    #region OnDistanceNear
    private void OnDistanceNear()
    {
        this._gizmosSphereCreater.ChangeDistanceDiff(-0.1f);
    }
    #endregion

    #region OnChangeDistance
    private void OnChangeDistance(float value)
    {
        this._gizmosSphereCreater.ChangeDistance(value);
    }
    #endregion
}
