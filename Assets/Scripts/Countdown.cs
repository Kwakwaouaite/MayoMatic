using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MayoMatic
{
    public class Countdown : MonoBehaviour
    {
        [SerializeField]
        Text m_Text;

        private float m_CurrentTimeCountdown;

        private bool m_HasStarted;

        private void Start()
        {
            m_HasStarted = false;

            if (m_Text)
            {
                m_Text.text = "";
            }
        }

        // Start is called before the first frame update
        public void StartCountdown(int length)
        {
            gameObject.SetActive(true);
            m_HasStarted = true;
            m_CurrentTimeCountdown = length;
        }

        // Update is called once per frame
        void Update()
        {
            m_CurrentTimeCountdown -= Time.deltaTime;
            DisplayTime();
        }

        void DisplayTime()
        {
            if (m_Text)
            {
                if (m_HasStarted)
                {
                    m_Text.text = Mathf.CeilToInt(m_CurrentTimeCountdown).ToString();
                }

                if (HasExpired())
                {
                    m_Text.text = "";
                }
            }
        }

        public bool HasExpired()
        {
            return m_CurrentTimeCountdown < 0;
        }
    }
}