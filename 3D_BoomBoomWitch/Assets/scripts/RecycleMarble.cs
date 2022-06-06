using UnityEngine;

/// <summary>
/// �^���u�]�t��  �����������              P28.07
/// </summary> 
public class RecycleMarble : MonoBehaviour
{
    /// <summary>
    /// �^�����u�]�ƶq                     p28.08
    /// </summary>
    public static int recycleMarbles;
    public static RecycleMarble instance;           // NEW

    public GameManager gm;
    public Marble mr;

    private void Awake()                            // NEW
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("�u�]"))   // �Q�θI���ƥ�ӳB�z p28.06
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;  // �[�t���k�s
            other.transform.position = new Vector3(0, 0, 5000);

            // �^���u�]�ƶq �W�[
            recycleMarbles++;
            //// �p�G �^���ƶq ���� �Ҧ��u�]�ƶq �������Ĥ�^�X  ����U��
            /////if (recycleMarbles == ControlSystem.maxMarbles) gm.SwitchTurn(false);     // p.29.08
            CheckIsRecycleAllMarbles();
            //    Debug.Log("recycleMarbles �ƶq: " +recycleMarbles);
        }
    }

    /// <summary>
    /// �ˬd�O�_�^���Ҧ��u�]
    /// </summary>
    public void CheckIsRecycleAllMarbles()
    {
        // �p�G �^���ƶq ���� �i�o�g�̤j�u�]�ƶq ������ �Ĥ�^�X
        if (recycleMarbles == ControlSystem.shootMarbles)
        {

            gm.SwitchTurn(false);
        }
        if (GameObject.FindGameObjectsWithTag("�ѽL�W������").Length == 0)    //��
        {
            gm.allObjectDead = true;
            //gm.SwitchTurn(false);

            //mr.FlyToBottomRightnow();
            //Debug.Log("�����u�]����������");
        }
    }
}
