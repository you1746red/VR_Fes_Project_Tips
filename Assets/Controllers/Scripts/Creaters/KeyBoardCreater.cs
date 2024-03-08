using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardCreater : MonoBehaviour
{
    [SerializeField] GameObject _keyBoardPrefab;
    [SerializeField] GameObject _keyBoardMonitorPrefab;

    private List<GameObject> _keyBoards = new List<GameObject>();
    private List<GameObject> _keyBoardMonitors = new List<GameObject>();

    [HideInInspector] public bool IsCreated { get; private set; }

    #region Create
    public void Create()
    {
        if (this.IsCreated) return;
        this.IsCreated = true;

        for (int i = 0; i < 2; i++)
        {
            // モニター
            GameObject obj1 = Instantiate(this._keyBoardMonitorPrefab, Vector3.zero, Quaternion.identity, this.transform);
            var param1 = new CircleArrangementData(0.5f, 0f, 0f, -45f + 90f * i);
            obj1.GetComponent<CircleArrangement>().SetParam(param1);
            obj1.GetComponent<CircleArrangement>().Deploy();
            obj1.GetComponent<KeyBoardMonitor>().Initialize();
            this._keyBoardMonitors.Add(obj1);

            // キーボード
            GameObject obj2 = Instantiate(this._keyBoardPrefab, Vector3.zero, Quaternion.identity, this.transform);
            var param2 = new CircleArrangementData(0.6f, -0.4f, -15f, -45f + 90f * i);
            obj2.GetComponent<CircleArrangement>().SetParam(param2);
            obj2.GetComponent<CircleArrangement>().Deploy();
            obj2.GetComponent<KeyBoard>().PushKey += obj1.GetComponent<KeyBoardMonitor>().InputCode;
            this._keyBoards.Add(obj2);
        }
    }
    #endregion

    #region DestroyInChildren
    public void DestroyInChildren()
    {
        this._keyBoards.ForEach(obj => Destroy(obj.gameObject));
        this._keyBoardMonitors.ForEach(obj => Destroy(obj.gameObject));
        this._keyBoards.Clear();
        this._keyBoardMonitors.Clear();
        this.IsCreated = false;
    }
    #endregion

    #region ReDeploy
    public void ReDeploy()
    {
        this._keyBoards.ForEach(obj => obj.GetComponent<CircleArrangement>().Deploy());
        this._keyBoardMonitors.ForEach(obj => obj.GetComponent<CircleArrangement>().Deploy());
    }
    #endregion

    // ローカル

    #region OnPushKey
    private void OnPushKey()
    {

    }
    #endregion
}
