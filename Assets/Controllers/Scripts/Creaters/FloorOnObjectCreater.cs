using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloorOnObjectCreater : MonoBehaviour
{
    [SerializeField] GameObject _floorOnObjectPrefab;

    private List<GameObject> _floorOnObjects = new List<GameObject>();

    [HideInInspector] public bool IsCreated { get; private set; }

    #region Create
    public void Create()
    {
        if (this.IsCreated) return;
        this.IsCreated = true;

        for (int i = 0; i < 12; i++)
        {
            GameObject obj = Instantiate(this._floorOnObjectPrefab, Vector3.zero, Quaternion.identity, this.transform);
            var param = new CircleArrangementData(1.5f, 0f, 0f, -180f + 30f * i);
            obj.GetComponent<CircleArrangement>().SetParam(param);
            obj.GetComponent<CircleArrangement>().Deploy();
            param.SetHeightDiff(WorldCenterSphere.Instance.FloorHeight + 0.01f);
            obj.GetComponent<CircleArrangement>().SetParam(param);
            obj.GetComponent<CircleArrangement>().Deploy();
            this._floorOnObjects.Add(obj);
        }
    }
    #endregion

    #region DestroyInChildren
    public void DestroyInChildren()
    {
        this._floorOnObjects.ForEach(obj => Destroy(obj.gameObject));
        this._floorOnObjects.Clear();
        this.IsCreated = false;
    }
    #endregion

    #region ReDeploy
    public void ReDeploy()
    {
        this._floorOnObjects.ForEach(obj => obj.GetComponent<CircleArrangement>().Deploy());
    }
    #endregion
}
