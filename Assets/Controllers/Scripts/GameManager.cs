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
        }
    }

    // ���[�J��

    #region DelayDeploy�i�R���[�`���j
    IEnumerator DelayDeploy()
    {
        yield return new WaitForSeconds(0.5f);

        WorldCenterSphere.Instance.Initialize();
        this._controlPanelManager.Create();
        yield return null;
    }
    #endregion
}
