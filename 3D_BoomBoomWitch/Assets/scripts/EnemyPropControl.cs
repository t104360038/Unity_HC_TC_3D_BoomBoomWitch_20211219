using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ĤH��D�㱱� P29.01
/// </summary>
public class EnemyPropControl : MonoBehaviour
{
    private GameManager gm;

    [Header("�C�����ʪ��Z��")]
    public float moveDistance = 2;
    [Header("���ʪ��y�Щ��u")]
    public float moveUnderLine = -2;
    [Header("�u�]���W��")]
    public string nameMarble;
    [Header("��q")]
    public float hp = 100;
    [Header("�O�_������")]
    public bool hasUI;

    private float hpMax;
    private Image imgHp;
    private Text textHp;

    private void Awake()
    {
        hpMax = hp;

        if (hasUI)
        {
            //��l���󪺤l���� �n���h�A�L�k�@�����      p29.03
            imgHp = transform.Find("�e�����").Find("���").GetComponent<Image>();
            textHp = transform.Find("�e�����").Find("��q").GetComponent<Text>();
            textHp.text = hp.ToString();                //�]�w��l�� p29.03
        }

        gm = FindObjectOfType<GameManager>();       //p29.07
        gm.onEnemyTurn.AddListener(Move);           //p29.07
    }

    /// <summary>
    /// ����
    /// </summary>
    private void Move()
    {
        transform.position += Vector3.forward * moveDistance;

        gm.SwitchTurn(true);                        //p29.08 ���ʫ��_�ڤ�^�X

        if (transform.position.z >= moveUnderLine) DestroyObject();     //p29.07
    }

    /// <summary>
    /// �R������
    /// </summary>
    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void Hurt(float damage)
    {
        hp -= damage;

        if (hasUI)
        {
            imgHp.fillAmount = hp / hpMax; //p29.03
            textHp.text = hp.ToString();

        }
        if (hp <= 0) Dead();
        
    }

    private void Dead()
    {
        Destroy(gameObject);

        if (gameObject.name.Contains("�u�]"))         //p29.05
        {
            ControlSystem.maxMarbles++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �p�G�i�Ӫ� ���� �W�� �]�t(�u�])�N
        if (collision.gameObject.name.Contains(nameMarble))
        {
            Hurt(collision.gameObject.GetComponent<Marble>().attack);
        }

    }
}
