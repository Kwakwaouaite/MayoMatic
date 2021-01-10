using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MayoMatic
{ 
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private float m_MusicLength = 134;

        [SerializeField]
        private SoundManager m_SoundManager;

        [SerializeField]
        private Countdown m_Countdown;

        [SerializeField]
        private Bowl m_Bowl;

        [SerializeField]
        private GameObject m_PressStartGO;

        [SerializeField]
        private ScoreManager m_ScoreManager;

        [SerializeField]
        private FinalScoreDisplay m_FinalScoreDisplay;

        [SerializeField]
        Ingredients m_IngredientManager;

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
            m_ScoreManager.StartScoring();
        }

        void UpdatePlaying()
        {
            if (m_SoundManager.MusicTime / 1000 > m_MusicLength)
            {
                GoToFinishedState();
            }

        }

        void GoToFinishedState()
        {
            m_Bowl.StopBowl();
            m_ScoreManager.StopScoring();
            m_FinalScoreDisplay.DisplayScore(m_IngredientManager.GetSuceededNotes(), m_IngredientManager.GetNoteCount(), 100 - m_ScoreManager.GetAverageGap() * 100);
        }

        void UpdateFinished()
        {

        }
    }
}