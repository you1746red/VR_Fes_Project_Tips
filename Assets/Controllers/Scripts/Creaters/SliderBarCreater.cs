using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBarCreater : MonoBehaviour
{
    [SerializeField] GameObject _sliderBarHorizontalPrefab;
    [SerializeField] GameObject _sliderBarVerticalPrefab;

    private GameObject _sliderBar;
    private bool _isVertical;

    [HideInInspector] public bool IsCreated { get; private set; }

    #region Create
    public void Create()
    {
        if (this.IsCreated) return;
        this.IsCreated = true;

        this.CreateSliderBar();
    }
    #endregion

    #region DestroyInChildren
    public void DestroyInChildren()
    {
        if (this._sliderBar != null) Destroy(this._sliderBar);
        this.IsCreated = false;
    }
    #endregion

    #region ReDeploy
    public void ReDeploy()
    {
        this._sliderBar.GetComponent<CircleArrangement>().Deploy();
    }
    #endregion

    // イベント

    #region OnTouchAllAtOnce
    private void OnTouchAllAtOnce()
    {
        StartCoroutine(this.DelayDestroy());
    }
    #endregion

    // ローカル

    #region CreateSliderBar
    private void CreateSliderBar()
    {
        GameObject prefab = this._sliderBarHorizontalPrefab;
        if (this._isVertical)
        {
            prefab = this._sliderBarVerticalPrefab;
        }
        this._isVertical = !this._isVertical;

        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, this.transform);
        var param = new CircleArrangementData(0.6f, 0f, 0f, 0);
        obj.GetComponent<CircleArrangement>().SetParam(param);
        obj.GetComponent<CircleArrangement>().Deploy();
        obj.GetComponent<SliderBar>().TouchAllAtOnce += this.OnTouchAllAtOnce;
        this._sliderBar = obj;
    }
    #endregion

    #region DelayDestroy
    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(2f);

        Destroy(this._sliderBar);
        this.CreateSliderBar();
        yield return null;
    }
    #endregion
}
