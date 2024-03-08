using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleSphereCreater : MonoBehaviour
{
    [SerializeField] GameObject _angleSpherePrefab;
    [SerializeField] Transform _leftHandTransform;
    [SerializeField] Transform _rightHandTransform;

    private List<GameObject> _angleSpheres = new List<GameObject>();

    [HideInInspector] public bool IsCreated { get; private set; }

    #region Create
    public void Create()
    {
        if (this.IsCreated) return;
        this.IsCreated = true;

        GameObject obj = Instantiate(this._angleSpherePrefab, Vector3.zero, Quaternion.identity, this.transform);
        var param = new CircleArrangementData(0.5f, -0.2f, 0f, -45f);
        obj.GetComponent<CircleArrangement>().SetParam(param);
        obj.GetComponent<CircleArrangement>().Deploy();
        obj.GetComponent<AngleSphere>().Initialize(true, this._leftHandTransform, this._rightHandTransform);
        this._angleSpheres.Add(obj);

        GameObject obj2 = Instantiate(this._angleSpherePrefab, Vector3.zero, Quaternion.identity, this.transform);
        var param2 = new CircleArrangementData(0.5f, -0.2f, 0f, 45f);
        obj2.GetComponent<CircleArrangement>().SetParam(param2);
        obj2.GetComponent<CircleArrangement>().Deploy();
        obj2.GetComponent<AngleSphere>().Initialize(false, this._leftHandTransform, this._rightHandTransform);
        this._angleSpheres.Add(obj2);
    }
    #endregion

    #region DestroyInChildren
    public void DestroyInChildren()
    {
        this._angleSpheres.ForEach(obj => Destroy(obj.gameObject));
        this._angleSpheres.Clear();
        this.IsCreated = false;
    }
    #endregion

    #region ReDeploy
    public void ReDeploy()
    {
        this._angleSpheres.ForEach(obj => obj.GetComponent<CircleArrangement>().Deploy());
    }
    #endregion
}
