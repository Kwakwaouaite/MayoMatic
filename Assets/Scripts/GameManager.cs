using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace MayoMatic
{ 
    public class GameManager : MonoBehaviour
    {    
        InputAction m_InputController;

        bool m_StartIsPressed;
        bool m_AIsPressed;
        bool m_YIsPressed;

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

        private void Awake()
        {
            // Create input bindings since the auto generatd code have some errors
            var action = new InputAction(
                    type: InputActionType.PassThrough,
                    binding: "<Gamepad>/start");

            action.performed +=
                ctx =>
                {
                    var button = (ButtonControl)ctx.control;
                    m_StartIsPressed = button.wasPressedThisFrame;
                };

            action.Enable();

            var actionA = new InputAction(
                    type: InputActionType.PassThrough,
                    binding: "<Gamepad>/buttonSouth");

            actionA.performed +=
                ctx =>
                {
                    var button = (ButtonControl)ctx.control;
                    m_AIsPressed = button.wasPressedThisFrame;
                };

            actionA.Enable();

            var actionY = new InputAction(
                    type: InputActionType.PassThrough,
                    binding: "<Gamepad>/buttonNorth");

            actionY.performed +=
                ctx =>
                {
                    var button = (ButtonControl)ctx.control;
                    m_YIsPressed = button.wasPressedThisFrame;
                };

            actionY.Enable();
        }

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
            if (m_StartIsPressed)
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
            m_State = GameState.Finished;
            m_Bowl.StopBowl();
            m_ScoreManager.StopScoring();
            m_FinalScoreDisplay.DisplayScore(m_IngredientManager.GetSuceededNotes(), m_IngredientManager.GetNoteCount(), 100 - m_ScoreManager.GetAverageGap() * 100);
        }

        void UpdateFinished()
        {
            if (m_AIsPressed)
            {
                Debug.Log("Go back to menu");
            }

            if (m_YIsPressed)
            {
                Debug.Log("Restart");
            }
        }
    }
}