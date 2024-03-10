using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WorldCenterSphere : MonoBehaviour
{
    public static WorldCenterSphere Instance;

    [SerializeField] Transform _cameraTrans;
    [SerializeField] PenetratePassthroughSphere _penetratePassthroughSphere;
    [SerializeField] string _ovrPlaneName;

    private float _floorHeight;

    public float FloorHeight { get => this._floorHeight; }

    #region Awake
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region Initialize
    /// <summary>
    /// �I�u�W�F�N�g�̏����������s��<br />
    /// �@�J�����ʒu�ɍ��W�E�p�x�C��<br />
    /// �A�p�X�X���[�̕ǂ�����<br />
    /// �B���܂ł̋����Z�o<br />
    /// </summary>
    public void Initialize()
    {
        // �����ʒu�E�p�x
        this.transform.position = this._cameraTrans.position;
        this.transform.rotation = Quaternion.Euler(0f, this._cameraTrans.rotation.eulerAngles.y, 0f);

        // �p�X�X���[�Ǐ����i���ʁ}45�x�A�����{0.5m�Ŏ��s�j
        this._penetratePassthroughSphere.PenetrateRange(this.transform, 45, 0.5f, this._ovrPlaneName);

        // ���݈ʒu�Ə��v���[���ʒu����Y���␳�l���v�Z
        GameObject floorPlane = this.GetFloorPlaneRayTest();
        if (floorPlane == null) return;
        this._floorHeight = floorPlane.transform.position.y - this.transform.position.y;

        // ���Ƀ^�O�ݒ�
        floorPlane.tag = "Ground";
    }
    #endregion

    #region GetPositionOnFloor
    /// <summary>
    /// ���ݍ��W���珰����W���擾����
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Vector3 GetPositionOnFloor(Vector3 position)
    {
        // Y���W�X�V�i���E���S�{���܂ł̍����{�␳0.01�j
        var newPosition = new Vector3(
            position.x,
            this.transform.position.y + this._floorHeight + 0.01f,
            position.z);
        return newPosition;
    }
    #endregion

    // ���[�J��

    #region GetFloorPlaneRayTest
    /// <summary>
    /// ���I�u�W�F�N�g���擾����
    /// </summary>
    /// <returns></returns>
    private GameObject GetFloorPlaneRayTest()
    {
        Vector3 forward = (-1) * this.transform.up;
        Ray ray = new Ray(this.transform.position, forward);
        RaycastHit hit;

        int distance = 10;
        Debug.DrawLine(ray.origin, ray.direction * distance, Color.red);

        // Ray���w�薼�̃I�u�W�F�N�g���Փˎ��͔�A�N�e�B�u��
        if (Physics.Raycast(ray, out hit, distance))
        {
            Debug.Log("@@@ {hit.collider.name}");
            if (hit.collider.name.Equals(this._ovrPlaneName))
            {
                return hit.collider.gameObject;
            }
        }

        return null;
    }
    #endregion
}
