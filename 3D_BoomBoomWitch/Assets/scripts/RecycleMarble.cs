using UnityEngine;

/// <summary>
/// Μ紆痌╰参  ン阀├              P28.07
/// </summary> 
public class RecycleMarble : MonoBehaviour
{
    /// <summary>
    /// Μ紆痌计秖                     p28.08
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
        if (other.name.Contains("紆痌"))   // ノ窱疾ㄆンㄓ矪瞶 p28.06
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;  // 硉耴箂
            other.transform.position = new Vector3(0, 0, 5000);

            // Μ紆痌计秖 糤
            recycleMarbles++;
            //// 狦 Μ计秖 单 ┮Τ紆痌计秖 ち传寄よ  传
            /////if (recycleMarbles == ControlSystem.maxMarbles) gm.SwitchTurn(false);     // p.29.08
            CheckIsRecycleAllMarbles();
            //    Debug.Log("recycleMarbles 计秖: " +recycleMarbles);
        }
    }

    /// <summary>
    /// 浪琩琌Μ┮Τ紆痌
    /// </summary>
    public void CheckIsRecycleAllMarbles()
    {
        // 狦 Μ计秖 单 祇甮程紆痌计秖 ち传 寄よ
        if (recycleMarbles == ControlSystem.shootMarbles)
        {

            gm.SwitchTurn(false);
        }
        if (GameObject.FindGameObjectsWithTag("囱絃ン").Length == 0)    //既
        {
            gm.allObjectDead = true;
            //gm.SwitchTurn(false);

            //mr.FlyToBottomRightnow();
            //Debug.Log("场紆痌莱┕┏场");
        }
    }
}
