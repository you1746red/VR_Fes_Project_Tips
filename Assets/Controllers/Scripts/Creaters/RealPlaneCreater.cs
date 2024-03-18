using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RealPlaneCreater : MonoBehaviour
{
    [SerializeField] GameObject _realPlanePrefab;

    private float _time = 0f;
    private float _createIntervalSec;
    private int _settingIndex = 0;
    private List<int> _indexies = new List<int>();
    private List<SphericalArrangementData> _settings = new List<SphericalArrangementData>();
    private List<GameObject> _realPlanes = new List<GameObject>();

    [HideInInspector] public bool IsStart { get; private set; } = false;

    void Start()
    {
        // 設定データ作成
        var xAngles = Enumerable.Range(0, 9).ToList().Select(index => -60f + 15f * index);
        var yAngles = Enumerable.Range(0, 360/15).ToList().Select(index => -180f + 15f * index);
        //var xAngles = new List<float>() { -60f, -45f, -30f, -15f, 0f, 15f, 30f, 45f, 60f };
        //var yAngles = new List<float>() { -120f, -90f, -60f, -30f, 0f, 30f, 60f, 90f, 120f };
        foreach (float xAngle in xAngles)
        {
            foreach (float yAngle in yAngles)
            {
                this._settings.Add(new SphericalArrangementData(0.8f, xAngle, yAngle));
            }
        }

        // シャッフル
        this.ShuffleIndex();
    }

    void Update()
    {
        if (this.IsStart == false) return;

        this._time += Time.deltaTime;
        if (this._time >= this._createIntervalSec)
        {
            this._time = 0f;

            // ウィンドウ生成（サイズをランダム調整）
            GameObject obj = Instantiate(this._realPlanePrefab, Vector3.zero, Quaternion.identity, this.transform);
            float xScale = Random.Range(0.2f, 0.5f);
            float yScale = Random.Range(0.2f, 0.5f);
            obj.transform.localScale = new Vector3(xScale, yScale, 1f);
            SphericalArrangementData data = this._settings[this._indexies[this._settingIndex]];
            obj.GetComponent<SphericalArrangement>().SetParam(data);
            obj.GetComponent<SphericalArrangement>().Deploy(true);
            this._realPlanes.Add(obj);

            this._settingIndex++;
            if (this._settingIndex == this._indexies.Count)
            {
                this.IsStart = false;
                this._settingIndex = 0;
                this.ShuffleIndex();
            }
        }
    }

    // 破棄

    #region DestroyInChildren
    public void DestroyInChildren()
    {
        this._realPlanes.ForEach(obj => Destroy(obj.gameObject));
        this._realPlanes.Clear();
    }
    #endregion

    #region Touch
    public void Touch()
    {
        //タッチ時の処理を記述
    }
    #endregion

    //// タイマー

    #region TimeStart
    public void TimeStart(float createIntervalSec)
    {
        this._createIntervalSec = createIntervalSec;
        this.IsStart = true;
    }
    #endregion

    #region TimeStop
    public void TimeStop()
    {
        this.IsStart = false;
    }
    #endregion

    //// ローカル

    #region ShuffleIndex
    private void ShuffleIndex()
    {
        this._indexies = Enumerable.Range(0, this._settings.Count).ToList();
        foreach (int index in Enumerable.Range(0, this._indexies.Count))
        {
            int temp = this._indexies[index];
            int randomIndex = Random.Range(0, this._indexies.Count);
            this._indexies[index] = this._indexies[randomIndex];
            this._indexies[randomIndex] = temp;
        }
    }
    #endregion
}
