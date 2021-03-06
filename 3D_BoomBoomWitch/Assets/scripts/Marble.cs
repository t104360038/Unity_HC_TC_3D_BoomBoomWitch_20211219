using UnityEngine;

/// <summary>
/// 紆痌╰参
/// </summary>
public class Marble : MonoBehaviour
{
    /// <summary>
    /// ю阑
    /// </summary>
    public float attack;

    [SerializeField, Header("ネΘ┕┏场")]
    private float flyToBottomAfterSpawn = 10;

    private Rigidbody rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// ┕┏场计
    /// </summary>
    public void FlyToBottomCountDown()
    {
        CancelInvoke();
        Invoke("FlyToBottom", flyToBottomAfterSpawn);
    }

    /// <summary>
    /// ネΘ┕┏场
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
/// 紆痌╰参
/// </summary>
public class Marble : MonoBehaviour
{
    /// <summary>
    /// ю阑
    /// </summary>
    public float attack;

    [SerializeField, Header("ネΘ硉")]
    private float raiseSpeedAfterSpawn = 3;
    [SerializeField, Header("ネΘ┕┏场")]
    private float flyToBottomAfterSpawn = 7;

    private Rigidbody rig;

    private void Awake()
    {
        // 瞶.┛菠瓜糷窱疾(A 瓜糷  B 瓜糷) ┛菠 A B 瓜糷窱疾
        //Physics.IgnoreLayerCollision(6, 6);                   // 簿GM

        rig = GetComponent<Rigidbody>();                    // NEW
        SpeedfastCountDown();
        FlyToBottomCountDown();
    }

    /// <summary>
    /// 硉计
    /// </summary>
    public void SpeedfastCountDown()
    {
        CancelInvoke();
        Invoke("RaiseSpeed", 3f);
       // Invoke("FlyToBottom", flyToBottomAfterSpawn);
    }
    /// <summary>
    /// 硉
    /// </summary>
    private void RaiseSpeed()
    {
        rig.AddForce(transform.forward * 3000);             // Vector3よ transform瞷よ
        Debug.Log("Text: RaiseSpeed 3f");
    }

    /// <summary>
    /// ┕┏场计
    /// </summary>
    public void FlyToBottomCountDown()
    {
        CancelInvoke();
        Invoke("FlyToBottom", flyToBottomAfterSpawn);
    }
    /// <summary>
    /// ネΘ┕┏场
    /// </summary>
    private void FlyToBottom()
    {
        rig.velocity = Vector3.zero;                        //俐丁氨ゎ
        rig.AddForce(0, 0, 5000);                           //
        //rig.velocity = Vector3.forward * 1000;               //ア
        Debug.Log("Text: FlyToBottom 6f");
    }
}*/