using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;                                                   // �ƥ� p29.07
using UnityEngine.UI;                                       // NEW


public class GameManager : MonoBehaviour
{
    /// <summary>
    /// �^�X���
    /// </summary>
    public Turn turn = Turn.My;

    [Header("�Ĥ�^�X�ƥ�")]                                                 // p29.07
    public UnityEvent onEnemyTurn;
    [Header("�Ǫ��}�C")]
    public GameObject[] goEnemys;
    [Header("�u�]")]
    public GameObject goMarble;
    [Header("�ѽL�s��")]                // p27.02 ���
    public Transform traCheckboard;
    [Header("�ͦ��ƶq�̤p�̤j��")]       // p27.04
    public Vector2Int v2RandomEnemyCount = new Vector2Int(1, 7);

    [HideInInspector]
    public bool allObjectDead;
    [HideInInspector]
    /// <summary>
    /// �h��
    /// </summary>
    public int floorCount;


    [SerializeField]
    private Transform[] traCheckboards;

    [SerializeField]
    /// <summary>
    /// �ĤG�C : �ͦ��Ǫ����ѽL
    /// </summary>
    private Transform[] traColumnSecond;
    /// <summary>
    /// �ѽL���ƶq
    /// </summary>
    private int countRow = 8;
    /// <summary>
    /// �ĤG�C�����ޭ� �A �B�z�Ǫ�������   p28.05
    /// </summary>
    [SerializeField]
    private List<int> indexColumnSecond = new List<int>();
    private ControlSystem controlSystem;                                        //p29.08 ����

    private bool canSpawn = true;                                               // NEW
    /// <summary>
    /// �����ƶq
    /// </summary>
 //   private Text textCoin;
    /// <summary>
    /// ���o�������ƶq
    /// </summary>
 //   private int coin;
    /// <summary>
    /// �h�ƼƦr
    /// </summary>
    public Text textFloorCount ;

    public static GameManager instance;                                         // NEW

    private void Awake()                // p27.02 ����l����
    {
        instance = this;                                                        // NEW

        // ���z.�����ϼh�I��(A �ϼh �A B �ϼh) ���� A B �ϼh�I��
        Physics.IgnoreLayerCollision(6, 6);
        Physics.IgnoreLayerCollision(6, 8);
       // Physics.IgnoreLayerCollision(7, 8);

        // �ѽL�}�C = �ѽL�s��.���o�l���󪺤���<�ܧΤ���>()
        traCheckboards = traCheckboard.GetComponentsInChildren<Transform>();   // p28.02 

        // ��l�ĤG�C�ƶq
        traColumnSecond = new Transform[countRow];
        // ���o�ĤG�C���ѽL
        for (int i = 9; i < 9 + countRow; i++)
        {
            traColumnSecond[i - countRow - 1] = traCheckboards[i];
        }

        controlSystem = FindObjectOfType<ControlSystem>();                      // p.29.08

        SpawnEnemy();
    }
    /// <summary>
    /// �ͦ��ĤH:�H���ƶq v2RandomEnemyCount
    /// </summary>
    private void SpawnEnemy()
    {
        floorCount++;
        textFloorCount.text = floorCount.ToString();

        int countEnemy = Random.Range(v2RandomEnemyCount.x, v2RandomEnemyCount.y);

        indexColumnSecond.Clear();                                                          // �M���W���Ѿl�����        p28.05

        for (int i = 0; i < 8; i++) indexColumnSecond.Add(i);                               // ��l�Ʀr 0 ~ 7 �ϥ�Random

        for (int i = 0; i < countEnemy; i++)
        {
            int randomEnemy = Random.Range(0, goEnemys.Length);                             // 0 ~ 2 - �H�� 0 �� 1

            int randomColumnSecond = Random.Range(0, indexColumnSecond.Count);              // �H���ĤG�C�����ޭȡG�Ĥ@�� 0 ~ 7 �A�ĤG����Ѿl���ƶq�H����         p28.05

            Instantiate(goEnemys[randomEnemy], traColumnSecond[indexColumnSecond[randomColumnSecond]].position, Quaternion.identity);                           // ��p28.05

            indexColumnSecond.RemoveAt(randomColumnSecond);                                 // �R���w�g��m�Ǫ����ĤG�C�ѽL
        }

        int randomMarble = Random.Range(0, indexColumnSecond.Count);                        // �Ѿl���ѽL ���o�H���@��
        Instantiate(
            goMarble,
            traColumnSecond[indexColumnSecond[randomMarble]].position + Vector3.up,
            Quaternion.identity);                                                           // �ͦ��u�]�b�ѽL�W

        if (allObjectDead) allObjectDead =  false;                                           // NEW true?
    }



    /// <summary>
    /// �����^�X            p28.08
    /// </summary>
    /// <param name="isMyTurn">�O�_���a�^�X</param>
    public void SwitchTurn(bool isMyTurn)
    {
        if (isMyTurn) 
        {
            turn = Turn.My;
            controlSystem.canShoot = true;                                      // p.29.08
            RecycleMarble.recycleMarbles = 0;                       // �^���ƶq�k�s   p.29.10
            ControlSystem.instance.ResetUIMarbleCountToMax();                                   // NEW    

            if (canSpawn)                                           // �p�G�i�H�ͦ�
            {
                canSpawn = false;                                   
                Invoke("SpawnEnemy", 0.8f );                        // �I�s�ͦ��ĤH ���� p.29.10
            }
        }
        else 
        {
            canSpawn = true;                                        // p.29.10
            turn = Turn.Enemy;

            if (!allObjectDead)  onEnemyTurn.Invoke();                                               // p.29.07
            else if (allObjectDead) SwitchTurn(true);               // NEW
        }
        
    }

    /// <summary>
    /// �K�[�����ç�s����                                            // NEW
    /// </summary>
  /*  public void AddCoinAndUpdateUI()
    {
        coin++;
        textCoin.text = coin.ToString();
    }
  */
}
/// <summary>
/// �^�X : �ڤ�P�Ĥ�
/// </summary>
public enum Turn
{
    My, Enemy
}
