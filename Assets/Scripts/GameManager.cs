using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

namespace MayoMatic
{
    public class GameManager : MonoBehaviour
    {
        InputAction m_InputController;

        bool m_StartIsPressed;
        bool m_AIsPressed;
        bool m_YIsPressed;
        bool m_BIsPressed;

        [SerializeField]
        private float m_MusicLength = 134;

        [SerializeField]
        private SoundManager m_SoundManager;

        [SerializeField]
        private Countdown m_Countdown;

        [SerializeField]
        private Bowl m_Bowl;

        [SerializeField]
        private GameObject m_TitleMenu;

        [SerializeField]
        private GameObject m_PauseMenu;

        [SerializeField]
        private GameObject[] m_TutorialMenus;

        private int m_CurrentTutorial = 0;

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
            Finished,
            Paused,
            Tutorial
        }

        private GameState m_State;

        [SerializeField]
        private float m_CountDownLength = 3.0f;

        private float m_CountdownStartTime;

        private void Awake()
        {
            BindInput();
        }


        void BindInput()
        {
            // Create input bindings since the auto generatd code have some errors
            var action = new InputAction(
                    type: InputActionType.PassThrough,
                    binding: "<Gamepad>/start");

            action.started +=
                ctx =>
                {
                    var button = (ButtonControl)ctx.control;
                    m_StartIsPressed = button.wasPressedThisFrame;
                };

            action.Enable();

            var actionA = new InputAction(
                    type: InputActionType.Button,
                    binding: "<Gamepad>/buttonSouth");

            actionA.started +=
                ctx =>
                {
                    var button = (ButtonControl)ctx.control;
                    m_AIsPressed = button.wasPressedThisFrame;
                };

            actionA.Enable();

            var actionY = new InputAction(
                    type: InputActionType.Button,
                    binding: "<Gamepad>/buttonNorth");

            actionY.started +=
                ctx =>
                {
                    var button = (ButtonControl)ctx.control;
                    m_YIsPressed = button.wasPressedThisFrame;
                };

            actionY.Enable();

            var actionB = new InputAction(
                        type: InputActionType.Button,
                        binding: "<Gamepad>/buttonEast");

            actionB.started +=
                ctx =>
                {
                    var button = (ButtonControl)ctx.control;
                    m_BIsPressed = button.wasPressedThisFrame;
                };

            actionB.Enable();
        }

        // Start is called before the first frame update
        void Start()
        {
            GoToBeginingState();
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
                case GameState.Paused:
                    UpdatePaused();
                    break;
                case GameState.Tutorial:
                    UpdateTutorial();
                    break;
            }

            m_AIsPressed = false;
            m_YIsPressed = false;
            m_BIsPressed = false;
        }

        void GoToBeginingState()
        {
            m_State = GameState.Beginning;

            ResetAllVisible();

            m_TitleMenu?.SetActive(true);
            m_Bowl?.SetHelpEnabled(false);
        }

        void UpdateBeginning()
        {
            if (m_StartIsPressed || m_AIsPressed)
            {
                GoToCountdownState();
            }

            if (m_BIsPressed)
            {
                ReturnToMainMenu();
            }

            if(m_YIsPressed)
            {
                GoToTutorialState();
                Debug.LogWarning("Tutorial -> Not implemented");
            }
        }

        void GoToCountdownState()
        {
            ResetAllVisible();

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
            ResetAllVisible();

            m_State = GameState.Playing;

            m_ScoreManager.StartScoring();
            m_Bowl?.SetHelpEnabled(true);
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
            ResetAllVisible();

            m_FinalScoreDisplay.gameObject.SetActive(true);

            m_State = GameState.Finished;
            m_Bowl.StopBowl();
            m_ScoreManager.StopScoring();
            m_FinalScoreDisplay.DisplayScore(m_IngredientManager.GetSuceededNotes(), m_IngredientManager.GetNoteCount(), 100 - m_ScoreManager.GetAverageGap() * 100);
        }

        void UpdateFinished()
        {
            if (m_AIsPressed)
            {
                ReturnToMainMenu();
            }

            if (m_YIsPressed)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        void GoToPausedState()
        {
            m_State = GameState.Paused;
            ResetAllVisible();

            m_PauseMenu?.SetActive(true);
        }

        void UpdatePaused()
        {

        }

        void GoToTutorialState()
        {
            m_State = GameState.Tutorial;
            ResetAllVisible();

            m_CurrentTutorial = 0;
            m_TutorialMenus[0]?.SetActive(true);
        }

        void UpdateTutorial()
        {
            if (m_AIsPressed)
            {
                m_CurrentTutorial += 1;

                if (m_TutorialMenus.Length <= m_CurrentTutorial)
                {
                    GoToBeginingState();
                }
                else
                {
                    ChangePageTutorial(m_CurrentTutorial); 
                }

            }
        }

        void ChangePageTutorial(int index)
        {
            int newIndex = Mathf.Clamp(index, 0, m_TutorialMenus.Length);

            ResetAllVisible();

            m_TutorialMenus[index]?.SetActive(true);
        }

        void ResetAllVisible()
        {
            m_PauseMenu?.SetActive(false);
            m_TitleMenu?.SetActive(false);
            m_FinalScoreDisplay.gameObject.SetActive(false);
            m_Bowl?.SetHelpEnabled(false);

            foreach (GameObject menu in m_TutorialMenus)
            {
                menu?.SetActive(false);
            }

            //TODO: add the joystick help disable;
        }

        void ReturnToMainMenu()
        {
            //INTEGRATION
            Debug.Log("On retourne au bus!");
        }
    }
}