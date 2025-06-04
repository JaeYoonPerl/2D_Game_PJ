using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guage : MonoBehaviour
{
    [SerializeField]
    Image m_img;

    [SerializeField]
    Image m_imgShadow;

    [SerializeField]
    TMPro.TextMeshProUGUI m_txtLable;

    [SerializeField]
    TMPro.TextMeshProUGUI m_txtLableMax;

    public void SetGuage(float v)
    {
        m_img.fillAmount = v;

    }

    public void SetColor(Color v)
    {
        m_img.color = v;

    }

    public void SetLable(string v)
    {
        m_txtLable.text = v;
    }

    public void SetLableMax(string v)
    {
        m_txtLableMax.text = v;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (m_imgShadow != null)
        {
            float v = Mathf.Lerp(m_imgShadow.fillAmount, m_img.fillAmount, Time.deltaTime);
            m_imgShadow.fillAmount = v;
        }
    }
}
