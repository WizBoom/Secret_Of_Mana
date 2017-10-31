using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Character m_Character;
    public Text m_CharacterNameText;
    public Image m_HPBarImage;
    public Image m_MPBarImage;
    public Text m_HPText;
    public Text m_MPText;

    public void Refresh()
    {
        m_CharacterNameText.text = m_Character.m_CharacterStats.m_CharacterName;

        //Calculate health percentage
        float healthPercentage = m_Character.m_CurrentHealthpoints/(float)m_Character.m_CharacterStats.m_MaxHealthPoints;
        m_HPBarImage.fillAmount = healthPercentage;
        m_HPText.text = string.Format("{0} / {1}", m_Character.m_CurrentHealthpoints,
            m_Character.m_CharacterStats.m_MaxHealthPoints);

        //Calculate mana percentage
        float manaPercentage = m_Character.m_CurrentManapoints/(float) m_Character.m_CharacterStats.m_MaxManaPoints;
        m_MPBarImage.fillAmount = manaPercentage;
        m_MPText.text = string.Format("{0} / {1}", m_Character.m_CurrentManapoints,
            m_Character.m_CharacterStats.m_MaxManaPoints);
    }
}
