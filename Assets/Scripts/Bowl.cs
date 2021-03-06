﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace MayoMatic
{
    public class Bowl : MonoBehaviour
    {
        /// <summary>
        /// Speed of the bowl, deg/s
        /// </summary>
        [SerializeField]
        float m_AngleSpeed = 180.0f;

        [SerializeField]
        GameObject m_TurnJoystickhelp;

        [SerializeField]
        float m_TimeBeforeShowingHelp = 2.0f;

        float m_NoInputJoystickTime = 0;

        bool m_HelpIsDisplayed = false;
        public bool m_HelpIsEnabled = false; 

        [SerializeField]
        Transform m_PlayerTransform;

        [SerializeField]
        Transform m_TargetTransform;

        [SerializeField]
        LineRenderer m_TargetLineRenderer;

        float m_TargetAngle = 0;
        float m_CurrentPlayerAngle = 0;

        float m_CurrentGapPercentage = 0;
        float m_FramePlayerAngleSpeed = 0;

        bool m_HasStarted = false;

        Vector3 m_PlayerInitialRotation;

        Vector2 m_PlayerJoystick;

        readonly Color m_GoodAimColor =  new Color(1, 0.9f, 0.5f);
        readonly Color m_BadAimColor = new Color(1, 0.2f, 0);

        private void Awake()
        {
            var action = new InputAction(
                type: InputActionType.Value,
                binding: "<Gamepad>/leftStick");

            action.started +=
                ctx =>
                {
                    ShowHelp(false);
                    m_PlayerJoystick = ctx.ReadValue<Vector2>();
                    m_NoInputJoystickTime = float.MinValue;
                };

            action.performed +=
                ctx =>
                {
                    m_PlayerJoystick = ctx.ReadValue<Vector2>();
                };
            action.canceled +=
                ctx =>
                {
                    m_PlayerJoystick = Vector2.zero;
                    m_NoInputJoystickTime = 0;
                };

            action.Enable();
        }

        private void Start()
        {
            if (m_PlayerTransform)
            {
                m_PlayerInitialRotation = m_PlayerTransform.localEulerAngles; 
            }

            m_HasStarted = false;
            ShowHelp(false);
        }

        public void StartBowl()
        {
            m_HasStarted = true;
            m_TargetLineRenderer.gameObject.SetActive(true);
        }

        public void StopBowl()
        {
            m_HasStarted = false;
            m_TargetLineRenderer.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (m_HasStarted)
            {
                UpdateAngles();
                ComputeGap();

                DisplayTarget();
                DisplayHelp();
            }

            DisplayPlayer();
        }

        private void DisplayHelp()
        {
            if(!m_HelpIsEnabled)
            {
                return;
            }

            if (!m_HelpIsDisplayed && m_NoInputJoystickTime > m_TimeBeforeShowingHelp)
            {
                ShowHelp(true);
            }

            m_NoInputJoystickTime += Time.deltaTime;
        }


        public void SetHelpEnabled(bool enable)
        {
            if (enable == m_HelpIsEnabled)
            {
                return;
            }

            m_HelpIsEnabled = enable;

            if(!m_HelpIsEnabled)
            {
                ShowHelp(false);
            }
        }

        private void ShowHelp( bool state)
        {
            m_HelpIsDisplayed = state;

            if (m_TurnJoystickhelp)
            {
                m_TurnJoystickhelp.SetActive(state);
            }
        }

        void UpdateAngles()
        {
            m_TargetAngle += Mathf.Deg2Rad * m_AngleSpeed * Time.deltaTime;

            // To stay in the intervam [-pi; pi[
            if(m_TargetAngle > Mathf.PI)
            {
                m_TargetAngle -= 2 * Mathf.PI;
            }

            //float joystickX = Input.GetAxis("Horizontal");
            //float joystickY = Input.GetAxis("Vertical");
            
            float joystickX = m_PlayerJoystick.x;
            float joystickY = m_PlayerJoystick.y;

            float newPlayerAngle = Mathf.Atan2(joystickY, joystickX);

            float angleThisFrame = newPlayerAngle - m_CurrentPlayerAngle;

            if (angleThisFrame > Mathf.PI)
            {
                angleThisFrame -= Mathf.PI;
            }

            m_FramePlayerAngleSpeed = angleThisFrame / Time.deltaTime;
            //Debug.Log("Speed: " + m_FramePlayerAngleSpeed);

            m_CurrentPlayerAngle = newPlayerAngle;
        }

        void ComputeGap()
        {
            if (m_PlayerJoystick.magnitude < 0.1)
            {
                m_CurrentGapPercentage = 1;
                return;
            }

            m_CurrentGapPercentage = Mathf.Abs(m_CurrentPlayerAngle - m_TargetAngle) / (2*Mathf.PI);

            // Get the acute angle
            if (m_CurrentGapPercentage > 0.5f)
            {
                m_CurrentGapPercentage = 1 - m_CurrentGapPercentage;
            }

            // Go from [0,0.5] to [0, 1]
            m_CurrentGapPercentage *= 2; 
        }

        void DisplayPlayer()
        {
            if (m_PlayerTransform)
            {


                m_PlayerTransform.localPosition = new Vector3(m_PlayerJoystick.x * 2, m_PlayerJoystick.y, 0);
                //m_PlayerTransform.localPosition = new Vector3(Input.GetAxis("Horizontal") * 2, Input.GetAxis("Vertical"), 0);
                m_PlayerTransform.eulerAngles = m_PlayerInitialRotation + Vector3.forward * m_PlayerJoystick.y * 10;
                //m_PlayerTransform.eulerAngles = m_PlayerInitialRotation + Vector3.forward * Input.GetAxis("Horizontal") * 10;
            }
        }
        void DisplayTarget()
        {
            if (m_TargetTransform)
            {
                //m_TargetTransform.localPosition = new Vector3(0, Mathf.Sign(Mathf.Sin(m_TargetAngle)) * 0.5f + 0.5f * Mathf.Sin(m_TargetAngle), 0);
                //m_TargetTransform.localEulerAngles = new Vector3(0, 0, m_TargetAngle * Mathf.Rad2Deg);
            }

            if (m_TargetLineRenderer && m_TargetTransform)
            {

                // m_TargetTransform
                m_TargetLineRenderer.SetPositions( new Vector3[] { m_TargetTransform.position, m_TargetTransform.position + new Vector3(Mathf.Cos(m_TargetAngle) * 3.5f, Mathf.Sin(m_TargetAngle) * 1.5f, 0) } );
                //m_TargetLineRenderer.SetColors(Color.black, Color.red);

                Color currentColor = m_CurrentGapPercentage * m_BadAimColor + (1 - m_CurrentGapPercentage) * m_GoodAimColor;
                m_TargetLineRenderer.startColor = currentColor;
                m_TargetLineRenderer.endColor  = currentColor;
            }
        }

        public void OnDrawGizmosSelected()
        {
            // Center of the sphere
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 0.1f);

            // Target vector
            Gizmos.color = Color.green * (1 - m_CurrentGapPercentage*2) + Color.red * m_CurrentGapPercentage*2 ;
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(Mathf.Cos(m_TargetAngle), Mathf.Sin(m_TargetAngle), 0));

            // Player vector
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(Mathf.Cos(m_CurrentPlayerAngle), Mathf.Sin(m_CurrentPlayerAngle), 0));
        }

        public float GetCurrentGap()
        {
            return m_CurrentGapPercentage;
        }

        public float GetFramePlayerAngleSpeed()
        {
            return m_CurrentGapPercentage;
        }
    }

}