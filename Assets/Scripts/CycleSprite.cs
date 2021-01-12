using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleSprite : MonoBehaviour
{
    [SerializeField]
    private Sprite[] m_Sprites;

    [SerializeField]
    private float m_SpanTime = 0.5f;

    float m_CurrentTime;

    int m_CurrentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Sprites.Length == 0)
        {
            return;
        }

        m_CurrentTime += Time.deltaTime;

        if (m_CurrentTime > m_SpanTime)
        {
            m_CurrentTime = 0;

            GoToNextSprite();
        }
    }

    void GoToNextSprite()
    {
        m_CurrentIndex = (m_CurrentIndex + 1) % m_Sprites.Length;

        SpriteRenderer sprtRend = GetComponent<SpriteRenderer>();

        if (sprtRend)
        {
            sprtRend.sprite = m_Sprites[m_CurrentIndex];
        }
    }
}
