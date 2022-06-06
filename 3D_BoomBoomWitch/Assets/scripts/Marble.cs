using UnityEngine;

/// <summary>
/// �u�]�t��
/// </summary>
public class Marble : MonoBehaviour
{
    /// <summary>
    /// �����O
    /// </summary>
    public float attack;

    [SerializeField, Header("�ͦ���h�[��������")]
    private float flyToBottomAfterSpawn = 10;

    private Rigidbody rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// ���������˼�
    /// </summary>
    public void FlyToBottomCountDown()
    {
        CancelInvoke();
        Invoke("FlyToBottom", flyToBottomAfterSpawn);
    }

    /// <summary>
    /// �ͦ��᭸������
    /// </summary>
    private void FlyToBottom()
    {
        rig.velocity = Vector3.zero;
        rig.AddForce(0, 0, 5000);
        Debug.Log("Text: FlyToBottom ");
    }
}

/*using UnityEngine;

/// <summary>
/// �u�]�t��
/// </summary>
public class Marble : MonoBehaviour
{
    /// <summary>
    /// �����O
    /// </summary>
    public float attack;

    [SerializeField, Header("�ͦ���h�[�[�t")]
    private float raiseSpeedAfterSpawn = 3;
    [SerializeField, Header("�ͦ���h�[��������")]
    private float flyToBottomAfterSpawn = 7;

    private Rigidbody rig;

    private void Awake()
    {
        // ���z.�����ϼh�I��(A �ϼh �A B �ϼh) ���� A B �ϼh�I��
        //Physics.IgnoreLayerCollision(6, 6);                   // ����GM

        rig = GetComponent<Rigidbody>();                    // NEW
        SpeedfastCountDown();
        FlyToBottomCountDown();
    }

    /// <summary>
    /// �[�t�˼�
    /// </summary>
    public void SpeedfastCountDown()
    {
        CancelInvoke();
        Invoke("RaiseSpeed", 3f);
       // Invoke("FlyToBottom", flyToBottomAfterSpawn);
    }
    /// <summary>
    /// �[�t
    /// </summary>
    private void RaiseSpeed()
    {
        rig.AddForce(transform.forward * 3000);             // Vector3�@�ɤ�V�A transform�{�b��V
        Debug.Log("Text: RaiseSpeed 3f");
    }

    /// <summary>
    /// ���������˼�
    /// </summary>
    public void FlyToBottomCountDown()
    {
        CancelInvoke();
        Invoke("FlyToBottom", flyToBottomAfterSpawn);
    }
    /// <summary>
    /// �ͦ��᭸������
    /// </summary>
    private void FlyToBottom()
    {
        rig.velocity = Vector3.zero;                        //��������
        rig.AddForce(0, 0, 5000);                           //�A�æV�U
        //rig.velocity = Vector3.forward * 1000;               //����
        Debug.Log("Text: FlyToBottom 6f");
    }
}*/