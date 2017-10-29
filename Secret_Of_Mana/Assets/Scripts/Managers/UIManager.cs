using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIManager
{
    public InventoryPanel m_InventoryPanel;
    public CharacterPanel m_CharacterPanel;
    public RectTransform m_ContentObject;
    public float m_ButtonHeight = 50f;
    public float m_ButtonMargin = 10f;
    public float m_ButtonDistance = 10f;
    public Sprite m_ButtonSprite;
    public Color m_ButtonColor;
    public int m_ButtonTextSize = 14;
    public Font m_ButtonTextFont;
    public Color m_ButtonTextColor;

    public void Init()
    {
        m_InventoryPanel.Initialize();
        m_CharacterPanel.Initialize();    
    }

    public void ToggleInventoryPanel()
    {
        if (m_InventoryPanel.gameObject.activeSelf)
        {
            m_InventoryPanel.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            m_CharacterPanel.gameObject.SetActive(false);
            m_InventoryPanel.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ToggleCharacterPanel()
    {
        if (m_CharacterPanel.gameObject.activeSelf)
        {
            m_CharacterPanel.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            m_InventoryPanel.gameObject.SetActive(false);
            m_CharacterPanel.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
