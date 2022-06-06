using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;                                                   // 事件 p29.07
using UnityEngine.UI;                                       // NEW


public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 回合資料
    /// </summary>
    public Turn turn = Turn.My;

    [Header("敵方回合事件")]                                                 // p29.07
    public UnityEvent onEnemyTurn;
    [Header("怪物陣列")]
    public GameObject[] goEnemys;
    [Header("彈珠")]
    public GameObject goMarble;
    [Header("棋盤群組")]                // p27.02 欄位
    public Transform traCheckboard;
    [Header("生成數量最小最大值")]       // p27.04
    public Vector2Int v2RandomEnemyCount = new Vector2Int(1, 7);

    [HideInInspector]
    public bool allObjectDead;
    [HideInInspector]
    /// <summary>
    /// 層數
    /// </summary>
    public int floorCount;


    [SerializeField]
    private Transform[] traCheckboards;

    [SerializeField]
    /// <summary>
    /// 第二列 : 生成怪物的棋盤
    /// </summary>
    private Transform[] traColumnSecond;
    /// <summary>
    /// 棋盤欄位數量
    /// </summary>
    private int countRow = 8;
    /// <summary>
    /// 第二列的索引值 ， 處理怪物不重複   p28.05
    /// </summary>
    [SerializeField]
    private List<int> indexColumnSecond = new List<int>();
    private ControlSystem controlSystem;                                        //p29.08 攻擊

    private bool canSpawn = true;                                               // NEW
    /// <summary>
    /// 金幣數量
    /// </summary>
 //   private Text textCoin;
    /// <summary>
    /// 取得的金幣數量
    /// </summary>
 //   private int coin;
    /// <summary>
    /// 層數數字
    /// </summary>
    public Text textFloorCount ;

    public static GameManager instance;                                         // NEW

    private void Awake()                // p27.02 抓取子物件
    {
        instance = this;                                                        // NEW

        // 物理.忽略圖層碰撞(A 圖層 ， B 圖層) 忽略 A B 圖層碰撞
        Physics.IgnoreLayerCollision(6, 6);
        Physics.IgnoreLayerCollision(6, 8);
       // Physics.IgnoreLayerCollision(7, 8);

        // 棋盤陣列 = 棋盤群組.取得子物件的元素<變形元件>()
        traCheckboards = traCheckboard.GetComponentsInChildren<Transform>();   // p28.02 

        // 初始第二列數量
        traColumnSecond = new Transform[countRow];
        // 取得第二列的棋盤
        for (int i = 9; i < 9 + countRow; i++)
        {
            traColumnSecond[i - countRow - 1] = traCheckboards[i];
        }

        controlSystem = FindObjectOfType<ControlSystem>();                      // p.29.08

        SpawnEnemy();
    }
    /// <summary>
    /// 生成敵人:隨機數量 v2RandomEnemyCount
    /// </summary>
    private void SpawnEnemy()
    {
        floorCount++;
        textFloorCount.text = floorCount.ToString();

        int countEnemy = Random.Range(v2RandomEnemyCount.x, v2RandomEnemyCount.y);

        indexColumnSecond.Clear();                                                          // 清除上次剩餘的資料        p28.05

        for (int i = 0; i < 8; i++) indexColumnSecond.Add(i);                               // 初始數字 0 ~ 7 使用Random

        for (int i = 0; i < countEnemy; i++)
        {
            int randomEnemy = Random.Range(0, goEnemys.Length);                             // 0 ~ 2 - 隨機 0 或 1

            int randomColumnSecond = Random.Range(0, indexColumnSecond.Count);              // 隨機第二列的索引值：第一次 0 ~ 7 ，第二次抓剩餘的數量隨機值         p28.05

            Instantiate(goEnemys[randomEnemy], traColumnSecond[indexColumnSecond[randomColumnSecond]].position, Quaternion.identity);                           // 改p28.05

            indexColumnSecond.RemoveAt(randomColumnSecond);                                 // 刪除已經放置怪物的第二列棋盤
        }

        int randomMarble = Random.Range(0, indexColumnSecond.Count);                        // 剩餘的棋盤 取得隨機一格
        Instantiate(
            goMarble,
            traColumnSecond[indexColumnSecond[randomMarble]].position + Vector3.up,
            Quaternion.identity);                                                           // 生成彈珠在棋盤上

        if (allObjectDead) allObjectDead =  false;                                           // NEW true?
    }



    /// <summary>
    /// 切換回合            p28.08
    /// </summary>
    /// <param name="isMyTurn">是否玩家回合</param>
    public void SwitchTurn(bool isMyTurn)
    {
        if (isMyTurn) 
        {
            turn = Turn.My;
            controlSystem.canShoot = true;                                      // p.29.08
            RecycleMarble.recycleMarbles = 0;                       // 回收數量歸零   p.29.10
            ControlSystem.instance.ResetUIMarbleCountToMax();                                   // NEW    

            if (canSpawn)                                           // 如果可以生成
            {
                canSpawn = false;                                   
                Invoke("SpawnEnemy", 0.8f );                        // 呼叫生成敵人 延遲 p.29.10
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
    /// 添加金幣並更新介面                                            // NEW
    /// </summary>
  /*  public void AddCoinAndUpdateUI()
    {
        coin++;
        textCoin.text = coin.ToString();
    }
  */
}
/// <summary>
/// 回合 : 我方與敵方
/// </summary>
public enum Turn
{
    My, Enemy
}
