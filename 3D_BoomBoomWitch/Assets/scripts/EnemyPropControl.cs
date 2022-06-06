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
    [Header("�򥻦�q")]
    public float hpBase = 100;
    [Header("�C�@�h���ɦ�q")]
    public float hpIncrease = 100;
    [Header("�ˮ`")]
    public float damage = 100;
    [Header("�O�_������")]
    public bool hasUI;
    [Header("�O�_���i�H�Y���u�]")]
    public bool isMarble;
    [Header("���`����")]
    public AudioClip soundDead;

    [HideInInspector]
    public float hpCurrent = 0;
    //[SerializeField, Header("�ʵe���")]
    //private Animator ani;

    private float hpMax;
    private Image imgHp;
    private Text textHp;

    private void Awake()
    {
        if (!isMarble)
        {
            hpMax += hpBase + (GameManager.instance.floorCount - 1) * hpIncrease;
            hpCurrent = hpMax;
        }
        else
        {
            hpCurrent = hpBase;
        }

        if (hasUI)
        {
            imgHp = transform.Find("�e�����").Find("���").GetComponent<Image>();                //��l���󪺤l���� �n���h�A�L�k�@�����      p29.03
            textHp = transform.Find("�e�����").Find("��q").GetComponent<Text>();
            textHp.text = hpCurrent.ToString();                                                   //�]�w��l�� p29.03
        }

        gm = FindObjectOfType<GameManager>();
        gm.onEnemyTurn.AddListener(Move);

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
        if (!isMarble) HealthManager.instance.Hurt(damage);             // NEW

        Destroy(gameObject);
    }

    /// <summary>
    /// ���`
    /// </summary>
    /// <param name="damage">�ˮ`</param>
    private void Hurt(float damage)
    {
        //if (!isMarble) ani.SetTrigger("Ĳ�o����");

        hpCurrent -= damage;

        if (hasUI)
        {
            imgHp.fillAmount = hpCurrent / hpMax;
            textHp.text = hpCurrent.ToString();
        }

        if (hpCurrent <= 0) Dead();
    }

    private void Dead()
    {
        Destroy(gameObject);
        RecycleMarble.instance.CheckIsRecycleAllMarbles();
        SoundManager.instance.PlaySoundRandomVolue(soundDead, 0.3f, 0.8f);

        if (gameObject.name.Contains("�u�]"))         //p29.05
        {
            ControlSystem.maxMarbles++;
        }
        //else DropCoin();
    }
    /*/// <summary>
    /// ��������
    /// </summary>
    private void DropCoin()
    {
        int randomCoin = Random.Range(v2CoinRange.x, v2CoinRange.y);

        for (int i = 0; i < randomCoin; i++)
        {
            GameObject tempCoin = Instantiate(goCoin, transform.position + Vector3.up, Quaternion.Euler(0, Random.Range(0, 360), 0));

            float randomX = Random.Range(100, 300);
            float randomY = Random.Range(500, 800);
            float randomZ = Random.Range(100, 300);

            tempCoin.GetComponent<Rigidbody>().AddForce(randomX, randomY, randomZ);
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        // �p�G�i�Ӫ� ���� �W�� �]�t(�u�])�N
        if (collision.gameObject.name.Contains(nameMarble))
        {
            Hurt(collision.gameObject.GetComponent<Marble>().attack);
        }

    }
}
