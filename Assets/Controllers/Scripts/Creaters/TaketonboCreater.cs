using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaketonboCreater : MonoBehaviour
{
    [SerializeField] GameObject _taketonboPrefab;
    [SerializeField] SphereCollider _leftCollider;
    [SerializeField] SphereCollider _rightCollider;

    private List<GameObject> _taketonbos = new List<GameObject>();

    [HideInInspector] public bool IsCreated { get; private set; }

    #region Create
    public void Create()
    {
        if (this.IsCreated) return;
        this.IsCreated = true;

        GameObject obj = Instantiate(this._taketonboPrefab, Vector3.zero, Quaternion.identity, this.transform);
        this._taketonbos.Add(obj);
    }
    #endregion

    #region DestroyInChildren
    public void DestroyInChildren()
    {
        this._taketonbos.ForEach(obj => Destroy(obj.gameObject));
        this._taketonbos.Clear();
        this.IsCreated = false;
    }
    #endregion

    #region ReDeploy
    public void ReDeploy()
    {
        this._taketonbos.ForEach(obj => obj.GetComponent<CircleArrangement>().Deploy());
    }
    #endregion
}
