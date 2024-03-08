using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GizmosSphereCreater : MonoBehaviour
{
    [SerializeField] GameObject _gizmosSpherePrefab;
    [SerializeField] Material _material;

    private List<GameObject> _spheres = new List<GameObject>();

    [HideInInspector] public bool IsCreated { get; private set; }

    #region Create
    public void Create()
    {
        if (this.IsCreated) return;
        this.IsCreated = true;

        var angles = new List<float>() { 60f, 30f, 0f, -30f, -60f };
        foreach (float angle in angles)
        {
            for (int i = 0; i < 12; i++)
            {
                GameObject obj = Instantiate(this._gizmosSpherePrefab, Vector3.zero, Quaternion.identity, this.transform);
                var param = new SphericalArrangementData(1f, angle, -180 + 30 * i);
                obj.GetComponent<SphericalArrangement>().SetParam(param);
                obj.GetComponent<SphericalArrangement>().Deploy(true);
                this._spheres.Add(obj);
            }
        }
    }
    #endregion

    #region DestroyInChildren
    public void DestroyInChildren()
    {
        this._spheres.ForEach(obj => Destroy(obj.gameObject));
        this._spheres.Clear();
        this.IsCreated = false;
    }
    #endregion

    #region ReDeploy
    public void ReDeploy()
    {
        this._spheres.ForEach(obj => obj.GetComponent<SphericalArrangement>().Deploy());
    }
    #endregion

    #region ChangeDistanceDiff
    public void ChangeDistanceDiff(float diff)
    {
        SphericalArrangement[] components = this.gameObject.GetComponentsInChildren<SphericalArrangement>();
        components.ToList().ForEach(component => component.ChangeDistanceDiff(diff));
    }
    #endregion

    #region ChangeDistance
    public void ChangeDistance(float value)
    {
        SphericalArrangement[] components = this.gameObject.GetComponentsInChildren<SphericalArrangement>();
        components.ToList().ForEach(component => component.ChangeDistance(value));
    }
    #endregion
}
