using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BigEnterKeyCreater : MonoBehaviour
{
    [SerializeField] GameObject _bigEnterKeyPrefab;

    private List<GameObject> _bigEnterKeys = new List<GameObject>();

    [HideInInspector] public bool IsCreated { get; private set; }

    #region Create
    public void Create()
    {
        if (this.IsCreated) return;
        this.IsCreated = true;

        for (int i = 0; i < 1; i++)
        {
            GameObject obj = Instantiate(this._bigEnterKeyPrefab, Vector3.zero, Quaternion.identity, this.transform);
            var param = new CircleArrangementData(0.6f, -0.4f, -30f, 90 * i);
            obj.GetComponent<CircleArrangement>().SetParam(param);
            obj.GetComponent<CircleArrangement>().Deploy();
            this._bigEnterKeys.Add(obj);
        }
    }
    #endregion

    #region DestroyInChildren
    public void DestroyInChildren()
    {
        this._bigEnterKeys.ForEach(obj => Destroy(obj.gameObject));
        this._bigEnterKeys.Clear();
        this.IsCreated = false;
    }
    #endregion

    #region ReDeploy
    public void ReDeploy()
    {
        this._bigEnterKeys.ForEach(obj => obj.GetComponent<CircleArrangement>().Deploy());
    }
    #endregion
}
