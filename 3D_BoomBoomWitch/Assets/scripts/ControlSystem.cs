    using UnityEngine;
    using System.Collections;                                   // p27.08   �ޥ� �t��.���X
    using System.Collections.Generic;                           // p27.06   �ޥ� �t��.���X.�@�� (�]�t List)
    using UnityEngine.UI;                                       // NEW �W�[text���O

/// <summary>
/// ����t��
/// ���V�ƹ���m
/// �o�g�u�]
/// �^�X����
/// </summary>
public class ControlSystem : MonoBehaviour
{
    #region ���
    [Header("�b�Y")]
    public GameObject goArrow;
    [Header("�ͦ��u�]��m")]
    public Transform traSpawnPoint;
    [Header("�u�]�w�s��")]
    public GameObject goMarbles;
    [Header("�o�g�t��"), Range(0, 5000)]
    public float speedShoot = 750;
    [Header("�g�u�n�I��������")]
    public LayerMask layerToHit;
    [Header("���շƹ���m")]
    public Transform traTestMousePosition;
    [Header("�Ҧ��u�]")]
    public List<GameObject> listMarbles = new List<GameObject>();
    [Header("�o�g���j"), Range(0, 5)]
    public float fireInterval = 0.5f;

    /// <summary>
    /// �Ҧ��u�]�ƶq              p28.08
    /// </summary>
    public static int allMarbles;
    /// <summary>
    /// �i�H�o�g���̤j�u�]�ƶq
    /// </summary>
    public static int maxMarbles = 2 ;
    /// <summary>
    /// �C���o�g�X�h���u�]�ƶq              p.29.08
    /// </summary>
    public static int shootMarbles;                                         // p29.08


    public static ControlSystem instance;                                   // NEW

    /// <summary>
    /// ��_�o�g
    /// </summary>
    public bool canShoot = true ;

    /// <summary>
    /// �u�]�ƶq
    /// </summary>
    private Text textMarbleCount;

    #endregion

    #region �ƥ�
    private void Start()
    {
        for (int i = 0; i < maxMarbles; i++) SpawnMarble();
    }

    private void Update()
    {
        MouseControl();
    }
    #endregion

    #region ��k
    /// <summary>
    /// �ͦ��u�]�s���M�椤
    /// </summary>
    private void SpawnMarble()
    {
        // �u�]�`�ƼW�[                           p28.08
        allMarbles++;
        // �Ҧ��u�]�M��.�K�[(�ͦ��u�])
        listMarbles.Add(Instantiate(goMarbles, new Vector3(0, 0, 100), Quaternion.identity));    // p27.06 ���\����
    } 

    /// <summary>
    /// �ƹ�����
    /// </summary>
    private void MouseControl()
    {
        if (!canShoot) return;                  // p29.06 �p�G�{�b�O �Ĥ�^�X �N���X

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            goArrow.SetActive(true);                        // p27.05 ���}���Y
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 v3Mouse = Input.mousePosition;

            //print("�ƹ��y��" + v3Mouse);

            // �g�u = �D�n��v��.�ù��y����g�u(�ƹ��y��)
            Ray rayMouse = Camera.main.ScreenPointToRay(v3Mouse);
            // �g�u�I����T
            RaycastHit hit;

            // �p�G �g�u���쪫��N�B�z
            // ���z �g�u�I��(�g�u�A�Z��)                    p26.10
            // ���z �g�u�I��(�g�u�A�g�u�I����T�A�Z���A�ϼh)  p26.11
            if (Physics.Raycast(rayMouse, out hit, 100, layerToHit))
            {
                //print("�ƹ��g�u���쪫��~" + hit.collider.name);     // hit �O�S����T�� �����θI����(collider)�h���W��(name)   p26.11

                Vector3 hitPosition = hit.point;                    // ���o�I����T���y��    p26.12
                hitPosition.y = 0.5f;                               // �վ㰪�׶b�V
                traTestMousePosition.position = hitPosition;        // ��s���ժ���y��

                // ���� �� Z �b = ���ժ��󪺮y�� - ���⪺�y�� (�V�q)
                transform.forward = traTestMousePosition.position - transform.position;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StartCoroutine(FireMarble());
            canShoot = false;                           // p29.06 �o�g��N����o�g
        }

    }

    /// <summary>
    /// �o�g�u�]
    /// </summary>
    /// <returns></returns>
    private IEnumerator FireMarble()
    {
        shootMarbles = 0;                               // p.29.08

        for (int i = 0; i < maxMarbles; i++)            // p.29.05
        {
            shootMarbles++;                             // p.29.08
            GameObject temp = listMarbles[i];           //Instantiate(goMarbles, traSpawnPoint.position, traSpawnPoint.rotation);
            temp.transform.position = traSpawnPoint.position;
            temp.transform.rotation = traSpawnPoint.rotation;
            temp.GetComponent<Rigidbody>().velocity = Vector3.zero;
            temp.GetComponent<Rigidbody>().AddForce(traSpawnPoint.forward * speedShoot);    // �o�g �u�]
            //SoundManager.instance.PlaySoundRandomVolue(soundShoot, 0.8f, 1.2f);
            temp.GetComponent<Marble>().FlyToBottomCountDown();                             // NEW   �קK���d�Ӥ[�i�J�˼ƭ��^�����^����
            UpdateUIMarbleCount();                                                          // NEW

            yield return new WaitForSeconds(fireInterval);
        }
        goArrow.SetActive(false);                       // p27.05 �������Y
    }

    /// <summary>
    /// ��s�����G�u�]�ƶq
    /// </summary>
    public void UpdateUIMarbleCount()
    {
        int marblesLess = maxMarbles - shootMarbles;
        string content = marblesLess != 0 ? "x " + marblesLess : "";
        textMarbleCount.text = content;
    }

    /// <summary>
    /// ���]�����G�u�]�ƶq��_���̤j��
    /// </summary>
    public void ResetUIMarbleCountToMax()
    {
        textMarbleCount.text = "x " + maxMarbles;
    }
    #endregion
}
