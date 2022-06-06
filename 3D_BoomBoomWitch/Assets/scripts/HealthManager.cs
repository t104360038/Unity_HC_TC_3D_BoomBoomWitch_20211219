using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    [SerializeField, Header("��q"), Range(0, 10000)]
    private float hp = 100;

    [SerializeField, Header("���")]
    private Image imgHp;
    [SerializeField, Header("��q")]
    private Text textHp;
    [SerializeField, Header("�ʵe���")]
    private Animator ani;
    private float hpMax;

    private void Awake()
    {
        instance = this;
        hpMax = hp;
        UpdateUI();
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="damage">�ˮ`</param>
    public void Hurt(float damage)
    {
      //  ani.SetTrigger("Ĳ�o����");
        hp -= damage;
        UpdateUI();

        if (hp <= 0) Dead();
    }

    /// <summary>
    /// ��s����
    /// </summary>
    private void UpdateUI()
    {
        imgHp.fillAmount = hp / hpMax;
        textHp.text = hp.ToString();
    }
    
    /// <summary>
    /// ���`
    /// </summary>
    private void Dead()
    {
        ani.SetBool("�}�����`", true);
    }
    
}
