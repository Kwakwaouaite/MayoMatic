using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MayoMatic
{ 
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private SoundManager m_SoundManager;

        [SerializeField]
        private Countdown m_Countdown;

        [SerializeField]
        private Bowl m_Bowl;

        [SerializeField]
        private GameObject m_PressStartGO;

        enum GameState
        {
            Beginning,
            Countdown,
            Playing,
            Finished
        }

        private GameState m_State;

        [SerializeField]
        private float m_CountDownLength = 3.0f;

        private float m_CountdownStartTime;

        // Start is called before the first frame update
        void Start()
        {
            GoToBeginingState();
            m_PressStartGO.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            switch (m_State)
            {
                case GameState.Beginning:
                    UpdateBeginning();
                    break;
                case GameState.Countdown:
                    UpdateCountdown();
                    break;
                case GameState.Playing:
                    UpdatePlaying();
                    break;
                case GameState.Finished:
                    UpdateFinished();
                    break;
            }
        }

        void GoToBeginingState()
        {
            m_State = GameState.Beginning;
        }

        void UpdateBeginning()
        {
            if (Input.GetButtonDown("ANote"))
            {
                GoToCountdownState();
                m_PressStartGO.SetActive(false);
            }

        }

        void GoToCountdownState()
        {
            m_State = GameState.Countdown;
            m_SoundManager.StartMusic();
            m_Countdown.StartCountdown(3);
            m_Bowl.StartBowl();
        }

        void UpdateCountdown()
        {
            if (m_SoundManager && m_Countdown && m_Countdown.HasExpired())
            {
                GoToPlayingState();
            }
        }

        void GoToPlayingState()
        {
            m_State = GameState.Playing;
        }

        void UpdatePlaying()
        {

        }

        void GoToFinishedState()
        {

        }

        void UpdateFinished()
        {

        }
    }
}