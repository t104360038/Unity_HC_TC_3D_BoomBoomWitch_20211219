using UnityEngine;

/// <summary>
/// 彈珠系統
/// </summary>
public class Marble : MonoBehaviour
{
    /// <summary>
    /// 攻擊力
    /// </summary>
    public float attack;

    [SerializeField, Header("生成後多久飛往底部")]
    private float flyToBottomAfterSpawn = 10;

    private Rigidbody rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 飛往底部倒數
    /// </summary>
    public void FlyToBottomCountDown()
    {
        CancelInvoke();
        Invoke("FlyToBottom", flyToBottomAfterSpawn);
    }

    /// <summary>
    /// 生成後飛往底部
    /// </summary>
    private void FlyToBottom()
    {
        rig.velocity = Vector3.zero;
        rig.AddForce(0, 0, 5000);
        Debug.Log("Text: FlyToBottom ");
    }
}

/*using UnityEngine;

/// <summary>
/// 彈珠系統
/// </summary>
public class Marble : MonoBehaviour
{
    /// <summary>
    /// 攻擊力
    /// </summary>
    public float attack;

    [SerializeField, Header("生成後多久加速")]
    private float raiseSpeedAfterSpawn = 3;
    [SerializeField, Header("生成後多久飛往底部")]
    private float flyToBottomAfterSpawn = 7;

    private Rigidbody rig;

    private void Awake()
    {
        // 物理.忽略圖層碰撞(A 圖層 ， B 圖層) 忽略 A B 圖層碰撞
        //Physics.IgnoreLayerCollision(6, 6);                   // 移到GM

        rig = GetComponent<Rigidbody>();                    // NEW
        SpeedfastCountDown();
        FlyToBottomCountDown();
    }

    /// <summary>
    /// 加速倒數
    /// </summary>
    public void SpeedfastCountDown()
    {
        CancelInvoke();
        Invoke("RaiseSpeed", 3f);
       // Invoke("FlyToBottom", flyToBottomAfterSpawn);
    }
    /// <summary>
    /// 加速
    /// </summary>
    private void RaiseSpeed()
    {
        rig.AddForce(transform.forward * 3000);             // Vector3世界方向， transform現在方向
        Debug.Log("Text: RaiseSpeed 3f");
    }

    /// <summary>
    /// 飛往底部倒數
    /// </summary>
    public void FlyToBottomCountDown()
    {
        CancelInvoke();
        Invoke("FlyToBottom", flyToBottomAfterSpawn);
    }
    /// <summary>
    /// 生成後飛往底部
    /// </summary>
    private void FlyToBottom()
    {
        rig.velocity = Vector3.zero;                        //瞬間停止
        rig.AddForce(0, 0, 5000);                           //，並向下
        //rig.velocity = Vector3.forward * 1000;               //消失
        Debug.Log("Text: FlyToBottom 6f");
    }
}*/