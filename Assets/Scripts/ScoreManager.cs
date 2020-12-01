using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MayoMatic
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField]
        float m_MaxScorePerSecond = 100;

        [SerializeField]
        Bowl m_MainBowl;

        [SerializeField]
        Text m_ScoreText;

        float m_Score = 0;
        float m_AverageGap = 0;
        float m_TimeSinceStart = 0;

        // Update is called once per frame
        void LateUpdate()
        {
            if (m_MainBowl)
            {
                m_Score += m_MaxScorePerSecond * ( 1 - m_MainBowl.GetCurrentGap()) * Time.deltaTime;

                m_AverageGap = (m_AverageGap * m_TimeSinceStart + m_MainBowl.GetCurrentGap() * Time.deltaTime) / (m_TimeSinceStart + Time.deltaTime);

                m_TimeSinceStart += Time.deltaTime;
                
                if (m_ScoreText)
                {
                    m_ScoreText.text = "Score: " + m_Score;
                    m_ScoreText.text += "\nCurrentGap: " + (int) (m_MainBowl.GetCurrentGap() * 100) + "%";
                    m_ScoreText.text += "\nAverageGap: " + (m_AverageGap * 100).ToString("#.0") + "%";
                    m_ScoreText.text += "\nPress 'R' to reset";
                }
            }

            if (Input.GetKeyDown("r"))
            {
                m_Score = 0;
                m_AverageGap = 0;
                m_TimeSinceStart = 0;
            }
        }
    }
}
