using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mayomatic
{
    public class Menu : MonoBehaviour
    {
        public enum Menutype
        {
            Title,
            Pause,
            EndScreen
        }
        public Menutype m_Type = Menutype.Title;

        bool m_IsEnabled;

        //MyInputAction m_InputAction = new MyInputAction();


        // Start is called before the first frame update
        void Start()
        {
            m_IsEnabled = false;
            gameObject.SetActive(false);

            //m_InputAction.Mayomatic.OilButton.performed += ctx => OnAPressed();
            //m_InputAction.Mayomatic.VinegarButton.performed += ctx => OnBPressed();
            //m_InputAction.Mayomatic.MustardButton.performed += ctx => OnYPressed();
            //m_InputAction.Mayomatic.SaltButton.performed += ctx => OnXPressed();
        }

        void StartMenu()
        {
            gameObject.SetActive(true);
            m_IsEnabled = true;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void OnAPressed()
        {
            //if (m_Type)
        }

        void OnBPressed()
        {

        }

        void OnYPressed()
        {

        }

        void OnXPressed()
        {

        }
    }
}
