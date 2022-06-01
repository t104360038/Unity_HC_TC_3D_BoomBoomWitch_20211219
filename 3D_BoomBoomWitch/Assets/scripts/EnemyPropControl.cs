using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敵人跟道具控制器 P29.01
/// </summary>
public class EnemyPropControl : MonoBehaviour
{
    private GameManager gm;

    [Header("每次移動的距離")]
    public float moveDistance = 2;
    [Header("移動的座標底線")]
    public float moveUnderLine = -2;
    [Header("彈珠的名稱")]
    public string nameMarble;
    [Header("血量")]
    public float hp = 100;
    [Header("是否有介面")]
    public bool hasUI;

    private float hpMax;
    private Image imgHp;
    private Text textHp;

    private void Awake()
    {
        hpMax = hp;

        if (hasUI)
        {
            //抓子物件的子物件 要抓兩層，無法一次到位      p29.03
            imgHp = transform.Find("畫布血條").Find("血條").GetComponent<Image>();
            textHp = transform.Find("畫布血條").Find("血量").GetComponent<Text>();
            textHp.text = hp.ToString();                //設定初始值 p29.03
        }

        gm = FindObjectOfType<GameManager>();       //p29.07
        gm.onEnemyTurn.AddListener(Move);           //p29.07
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        transform.position += Vector3.forward * moveDistance;

        gm.SwitchTurn(true);                        //p29.08 移動後恢復我方回合

        if (transform.position.z >= moveUnderLine) DestroyObject();     //p29.07
    }

    /// <summary>
    /// 刪除物件
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

        if (gameObject.name.Contains("彈珠"))         //p29.05
        {
            ControlSystem.maxMarbles++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 如果進來的 物件 名稱 包含(彈珠)就
        if (collision.gameObject.name.Contains(nameMarble))
        {
            Hurt(collision.gameObject.GetComponent<Marble>().attack);
        }

    }
}
