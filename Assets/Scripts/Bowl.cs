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
        LineRenderer m_PlayerLineRenderer;

        [SerializeField]
        Transform m_TargetTransform;

        float m_TargetAngle = 0;
        float m_CurrentPlayerAngle = 0;
        float m_CurrentGapPercentage = 0;


        // Start is called before the first frame update
        void Start()
        {
        
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

            if(m_TargetAngle > 2 * Mathf.PI)
            {
                m_TargetAngle -= 2 * Mathf.PI;
            }

            float joystickX = Input.GetAxis("Horizontal");
            float joystickY = Input.GetAxis("Vertical");

            m_CurrentPlayerAngle = Mathf.Atan2(joystickY, joystickX);
        }

        void ComputeGap()
        {
            m_CurrentGapPercentage = Mathf.Abs(m_CurrentPlayerAngle - m_TargetAngle) / (2*Mathf.PI);

            if (m_CurrentGapPercentage > 0.5f)
            {
                m_CurrentGapPercentage = 1 - m_CurrentGapPercentage;
            }

        }

        void DisplayPlayer()
        {
            if (m_PlayerLineRenderer)
            {
                Vector3[] positions = { Vector3.zero, (new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0)).normalized * 2 };
                m_PlayerLineRenderer.SetPositions(positions);

                Color playerColor = Color.green * (1 - m_CurrentGapPercentage * 2) + Color.red * m_CurrentGapPercentage * 2;
                m_PlayerLineRenderer.startColor = playerColor;
                m_PlayerLineRenderer.endColor = playerColor;
            }
        }
        void DisplayTarget()
        {
            if (m_TargetTransform)
            {
                m_TargetTransform.eulerAngles = new Vector3(0, 0, m_TargetAngle * Mathf.Rad2Deg); 
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
    }

}