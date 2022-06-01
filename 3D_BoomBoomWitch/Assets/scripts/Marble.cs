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
    private float flyToBottomAfterSpawn = 7;

    private Rigidbody rig;

    private void Awake()
    {
        // 物理.忽略圖層碰撞(A 圖層 ， B 圖層) 忽略 A B 圖層碰撞
        //Physics.IgnoreLayerCollision(6, 6);

        rig = GetComponent<Rigidbody>();                    // NEW
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
        rig.AddForce(0, 0, 1000);
    }
}
