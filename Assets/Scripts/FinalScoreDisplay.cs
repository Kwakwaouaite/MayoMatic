using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MayoMatic
{
    public class FinalScoreDisplay : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_ContainerFinalScore;

        [SerializeField]
        private GameObject[] m_FullStars;

        [SerializeField]
        private string[] m_PossibleAdjectives;

        [SerializeField]
        Text m_AdjectiveText;

        [SerializeField]
        Text m_IngredientScoreText;

        [SerializeField]
        Text m_MixScoreText;

        [SerializeField]
        int m_PercentageStar = 45;

        // Start is called before the first frame update
        void Start()
        {
            m_ContainerFinalScore.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            //DisplayScore(2, 10, 92.1f);
        }

        public void DisplayScore(int currentIngredient, int maxIngredient, float mixPercentage)
        {
            //ActivateStars(3);

            m_IngredientScoreText.text = "Ingrédients : " + currentIngredient.ToString() + " / " + maxIngredient.ToString();
            m_MixScoreText.text = "Mélange : " + mixPercentage.ToString("#") + "%";

            int nbrStar = ComputeNumberStar(currentIngredient, maxIngredient, mixPercentage);

            ActivateStars(nbrStar + 1);

            m_AdjectiveText.text = m_PossibleAdjectives.Length > 0 ? m_PossibleAdjectives[Mathf.Min(nbrStar, m_PossibleAdjectives.Length)] : "Notext";
            

           m_ContainerFinalScore.SetActive(true);
        }

        void ActivateStars(int numberToActivate)
        {
            for (int i = 0; i < m_FullStars.Length; i++)
            {
                m_FullStars[i].SetActive(i < numberToActivate);
            }
        }

        int ComputeNumberStar(int currentIngredient, int maxIngredient, float mixPercentage)
        {
            float nbrStar = 0;

            nbrStar += ((float)currentIngredient / (float)maxIngredient) *100 / m_PercentageStar;

            Debug.Log("FinalScore - nbrStar ingrédient : " + nbrStar);

            nbrStar += mixPercentage / m_PercentageStar;

            Debug.Log("FinalScore - nbrStar mixPercentage : " + nbrStar);

            return Mathf.FloorToInt(nbrStar);
        }
    }
}
