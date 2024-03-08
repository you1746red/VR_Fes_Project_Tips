using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] ControlPanelManager _controlPanelManager;

    #region Awake
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    void Start()
    {
        StartCoroutine(this.DelayDeploy());
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // �e�X�g�@�\
            WorldCenterSphere.Instance.Initialize();
            this._controlPanelManager.ReDeploy();
            //StartCoroutine(this.DelayDeploy());
        }
    }

    // ���[�J��

    #region DelayDeploy�i�R���[�`���j
    IEnumerator DelayDeploy()
    {
        yield return new WaitForSeconds(0.5f);

        WorldCenterSphere.Instance.Initialize();
        this._controlPanelManager.Create();
        //StartCoroutine(this.DeploySphericalArrangement());
        //StartCoroutine(this.DeployCircleArrangement());
        yield return null;
    }
    #endregion

    #region DeploySphericalArrangement�i�R���[�`���j
    IEnumerator DeploySphericalArrangement()
    {
        // �I�u�W�F�N�g�Ĕz�u�F��
        var scripts1 = FindObjectsByType<SphericalArrangement>(FindObjectsSortMode.None);
        scripts1.ToList().ForEach(script => script.Deploy());
        yield return null;
    }
    #endregion

    #region DeployCircleArrangement�i�R���[�`���j
    IEnumerator DeployCircleArrangement()
    {
        // �I�u�W�F�N�g�Ĕz�u�F�~
        var scripts2 = FindObjectsByType<CircleArrangement>(FindObjectsSortMode.None);
        scripts2.ToList().ForEach(script => script.Deploy());
        yield return null;
    }
    #endregion

    #region ChangeDistanceDiffSphericalArrangement
    private void ChangeDistanceDiffSphericalArrangement(float diff)
    {
        var scripts1 = FindObjectsByType<SphericalArrangement>(FindObjectsSortMode.None);
        scripts1.ToList().ForEach(script => script.ChangeDistanceDiff(diff));

        //var scripts2 = FindObjectsByType<CircleArrangement>(FindObjectsSortMode.None);
        //scripts2.ToList().ForEach(script => script.ChangeDistance(diff));
    }
    #endregion

    #region ChangeDistanceSphericalArrangement
    private void ChangeDistanceSphericalArrangement(float value)
    {
        var scripts1 = FindObjectsByType<SphericalArrangement>(FindObjectsSortMode.None);
        scripts1.ToList().ForEach(script => script.ChangeDistance(value));

        //var scripts2 = FindObjectsByType<CircleArrangement>(FindObjectsSortMode.None);
        //scripts2.ToList().ForEach(script => script.ChangeDistance(value));
    }
    #endregion
}
