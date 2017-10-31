using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIManager
{
    public InventoryPanel m_InventoryPanel;
    public CharacterPanel m_CharacterPanel;
    public RectTransform m_InventoryContentObject;
    public RectTransform m_CharacterContentObject;

    //General UI
    public Font m_Font;
    public Color m_FontColor = Color.black;
    public float m_ScrollbarWidth = 20f;

    //Character UI
    public float m_CharacterButtonHeight = 50f;
    public float m_CharacterButtonMargin = 10f;
    public float m_CharacterButtonDistance = 10f;
    public Sprite m_CharacterButtonSprite;
    public Color m_CharacterButtonColor;
    public int m_CharacterFontSize = 14;

    //Inventory UI
    public float m_InventoryTileHeight = 10f;
    public float m_InventoryTileWidth = 10f;
    public float m_InventoryTileDistance = 10f;
    public int m_InventoryTileColumns = 3;
    public int m_InventoryFontSize = 8;

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
            GameManager.m_Instance.m_UIManager.m_InventoryPanel.Refresh();
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
            GameManager.m_Instance.m_UIManager.m_CharacterPanel.Refresh();
            Time.timeScale = 0f;
        }
    }
}
