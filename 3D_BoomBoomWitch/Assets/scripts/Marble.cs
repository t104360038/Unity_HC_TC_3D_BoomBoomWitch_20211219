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
    private float flyToBottomAfterSpawn = 7;

    private Rigidbody rig;

    private void Awake()
    {
        // ���z.�����ϼh�I��(A �ϼh �A B �ϼh) ���� A B �ϼh�I��
        //Physics.IgnoreLayerCollision(6, 6);

        rig = GetComponent<Rigidbody>();                    // NEW
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
        rig.AddForce(0, 0, 1000);
    }
}
