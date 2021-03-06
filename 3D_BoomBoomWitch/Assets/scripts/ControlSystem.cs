    using UnityEngine;
    using System.Collections;                                   // p27.08   まノ ╰参.栋
    using System.Collections.Generic;                           // p27.06   まノ ╰参.栋. ( List)
    using UnityEngine.UI;                                       // NEW 糤text

/// <summary>
/// 北╰参
/// 菲公竚
/// 祇甮紆痌
/// 北
/// </summary>
public class ControlSystem : MonoBehaviour
{
    #region 逆
    [Header("絙繷")]
    public GameObject goArrow;
    [Header("ネΘ紆痌竚")]
    public Transform traSpawnPoint;
    [Header("紆痌箇籹")]
    public GameObject goMarbles;
    [Header("祇甮硉"), Range(0, 5000)]
    public float speedShoot = 5000;
    [Header("甮絬璶窱疾瞅鲤")]
    public LayerMask layerToHit;
    [Header("代刚菲公竚")]
    public Transform traTestMousePosition;
    [Header("┮Τ紆痌")]
    public List<GameObject> listMarbles = new List<GameObject>();
    [Header("祇甮丁筳"), Range(0, 5)]
    public float fireInterval = 0.5f;
    [Header("祇甮")]
    public AudioClip soundShoot;

    /// <summary>
    /// ┮Τ紆痌计秖              p28.08
    /// </summary>
    public static int allMarbles;
    /// <summary>
    /// 祇甮程紆痌计秖
    /// </summary>
    public static int maxMarbles = 5 ;
    /// <summary>
    /// –Ω祇甮紆痌计秖              p.29.08
    /// </summary>
    public static int shootMarbles;                                         // p29.08


    public static ControlSystem instance;                                   // NEW

    /// <summary>
    /// 祇甮
    /// </summary>
    public bool canShoot = true ;

    [SerializeField, Header("笆礶北竟")]
    private Animator ani;

    /// <summary>
    /// 紆痌计秖
    /// </summary>
    private Text textMarbleCount;

    #endregion

    #region ㄆン
    private void Awake()
    {
        instance = this;
        textMarbleCount = GameObject.Find("紆痌计秖").GetComponent<Text>();
    }

    private void Start()
    {
        for (int i = 0; i < 50; i++) SpawnMarble();

        UpdateUIMarbleCount();
    }

    private void Update()
    {
        MouseControl();
    }
    #endregion

    #region よ猭
    /// <summary>
    /// ネΘ紆痌睲虫い
    /// </summary>
    private void SpawnMarble()
    {
        // 紆痌羆计糤                           p28.08
        allMarbles++;
        // ┮Τ紆痌睲虫.睰(ネΘ紆痌)
        listMarbles.Add(Instantiate(goMarbles, new Vector3(0, 0, 100), Quaternion.identity));    // p27.06 耚娩
    } 

    /// <summary>
    /// 菲公北
    /// </summary>
    private void MouseControl()
    {
        if (!canShoot) return;                  // p29.06 狦瞷琌 寄よ 碞铬

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            goArrow.SetActive(true);                        // p27.05 ゴ秨龄繷
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 v3Mouse = Input.mousePosition;

            //print("菲公畒夹" + v3Mouse);

            // 甮絬 = 璶尼紇诀.棵辊畒夹锣甮絬(菲公畒夹)
            Ray rayMouse = Camera.main.ScreenPointToRay(v3Mouse);
            // 甮絬窱疾戈癟
            RaycastHit hit;

            // 狦 甮絬ゴン碞矪瞶
            // 瞶 甮絬窱疾(甮絬禯瞒)                    p26.10
            // 瞶 甮絬窱疾(甮絬甮絬窱疾戈癟禯瞒瓜糷)  p26.11
            if (Physics.Raycast(rayMouse, out hit, 100, layerToHit))
            {
                //print("菲公甮絬ゴン~" + hit.collider.name);     // hit 琌⊿Τ戈癟 ゲ斗ノ窱疾竟(collider)т嘿(name)   p26.11

                Vector3 hitPosition = hit.point;                    // 眔窱疾戈癟畒夹    p26.12
                hitPosition.y = 0.5f;                               // 秸俱蔼禸
                traTestMousePosition.position = hitPosition;        // 穝代刚ン畒夹

                // à︹  Z 禸 = 代刚ン畒夹 - à︹畒夹 (秖)
                transform.forward = traTestMousePosition.position - transform.position;

                /*Vector3 angle = transform.eulerAngles;              // NEW
                angle.x = 0;
                angle.z = 0;
                transform.eulerAngles = angle;*/
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StartCoroutine(FireMarble());
            canShoot = false;                           // p29.06 祇甮碞ぃ祇甮
        }

    }
    /// <summary>
    /// 祇甮紆痌
    /// </summary>
    /// <returns></returns>
    private IEnumerator FireMarble()
    {
        shootMarbles = 0;                               // p.29.08

        for (int i = 0; i < maxMarbles; i++)            // p.29.05
        {
            ani.SetTrigger("牟祇ю阑");
            shootMarbles++;                             // p.29.08
            GameObject temp = listMarbles[i];           //Instantiate(goMarbles, traSpawnPoint.position, traSpawnPoint.rotation);
            temp.transform.position = traSpawnPoint.position;
            temp.transform.rotation = traSpawnPoint.rotation;
            temp.GetComponent<Rigidbody>().velocity = Vector3.zero;
            temp.GetComponent<Rigidbody>().AddForce(traSpawnPoint.forward * speedShoot);    // 祇甮 紆痌
            SoundManager.instance.PlaySoundRandomVolue(soundShoot, 0.3f, 0.8f);
            //temp.GetComponent<Marble>().SpeedfastCountDown();                               // NEW   磷氨痙び钡硉
            temp.GetComponent<Marble>().FlyToBottomCountDown();                             // NEW   磷氨痙び秈计┏场Μ跋
            UpdateUIMarbleCount();                                                          // NEW

            yield return new WaitForSeconds(fireInterval);
        }
        goArrow.SetActive(false);                       // p27.05 闽超龄繷
    }

    /// <summary>
    /// 穝ざ紆痌计秖
    /// </summary>
    public void UpdateUIMarbleCount()
    {
        int marblesLess = maxMarbles - shootMarbles;
        string content = marblesLess != 0 ? "x " + marblesLess : "";
        textMarbleCount.text = content;
    }

    /// <summary>
    /// 砞ざ紆痌计秖確程
    /// </summary>
    public void ResetUIMarbleCountToMax()
    {
        textMarbleCount.text = "x " + maxMarbles;
    }
    #endregion
}
