using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Transform m_PlayerTransform;

        [SerializeField]
        Transform m_TargetTransform;

        float m_TargetAngle = 0;
        float m_CurrentPlayerAngle = 0;

        float m_CurrentGapPercentage = 0;
        float m_FramePlayerAngleSpeed = 0;

        Vector3 m_PlayerInitialRotation;

        private void Start()
        {
            if (m_PlayerTransform)
            {
                m_PlayerInitialRotation = m_PlayerTransform.localEulerAngles; 
            }
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAngles();
            ComputeGap();

            DisplayPlayer();
            DisplayTarget();
        }

        void UpdateAngles()
        {
            m_TargetAngle += Mathf.Deg2Rad * m_AngleSpeed * Time.deltaTime;

            // To stay in the intervam [-pi; pi[
            if(m_TargetAngle > Mathf.PI)
            {
                m_TargetAngle -= 2 * Mathf.PI;
            }

            float joystickX = Input.GetAxis("Horizontal");
            float joystickY = Input.GetAxis("Vertical");

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
                m_PlayerTransform.localPosition = new Vector3(Input.GetAxis("Horizontal") * 2, Input.GetAxis("Vertical"), 0);
                m_PlayerTransform.eulerAngles = m_PlayerInitialRotation + Vector3.forward * Input.GetAxis("Horizontal") * 10;
            }
        }
        void DisplayTarget()
        {
            if (m_TargetTransform)
            {
                m_TargetTransform.localPosition = new Vector3(0, Mathf.Sign(Mathf.Sin(m_TargetAngle)) *  0.5f + 0.5f * Mathf.Sin(m_TargetAngle), 0);
                m_TargetTransform.localEulerAngles = new Vector3(0, 0, m_TargetAngle * Mathf  .Rad2Deg); 
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