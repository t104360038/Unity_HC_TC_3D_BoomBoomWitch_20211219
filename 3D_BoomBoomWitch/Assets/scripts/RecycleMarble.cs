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

    public GameManager gm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("�u�]"))   // �Q�θI���ƥ�ӳB�z p28.06
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;  // �[�t���k�s
            other.transform.position = new Vector3(0, 0, 100);

            // �^���u�]�ƶq �W�[
            recycleMarbles++;
            // �p�G �^���ƶq ���� �Ҧ��u�]�ƶq �������Ĥ�^�X
            if (recycleMarbles == ControlSystem.maxMarbles) gm.SwitchTurn(false);     // p.29.08
        }
    }
}
