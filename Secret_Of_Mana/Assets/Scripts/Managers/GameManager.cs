using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public CharacterManager m_CharacterManager;
    public UIManager m_UIManager;
    public Inventory m_Inventory;

    public static GameManager m_Instance = null;

    private void Awake()
    {
        if (!m_Instance)
            m_Instance = this;

        m_UIManager.Init();

        CreateCharacterPrefabs();
        CreateCharacterPanel();
        CreateInventoryPanel();
    }

    private void Update()
    {
        if (Input.GetButtonDown("InventoryPanel"))
        {
            m_UIManager.ToggleInventoryPanel();
        }
        else if (Input.GetButtonDown("CharacterPanel"))
        {
            m_UIManager.ToggleCharacterPanel();
        }
    }

    private void CreateCharacterPrefabs()
    {
        //Make Enemies
        foreach (var enemy in m_CharacterManager.m_Enemies)
        {
            if (enemy.m_VisualCharacter)
            {
                VisualCharacter character = Instantiate(enemy.m_VisualCharacter, enemy.m_LastSavedPosition,
                    Quaternion.identity);
                enemy.m_VisualCharacter = character;
                character.m_Character = enemy;
                enemy.InitCharacter();
            }
        }

        foreach (var player in m_CharacterManager.m_Players)
        {
            if (player.m_VisualCharacter)
            {
                VisualCharacter character = Instantiate(player.m_VisualCharacter, player.m_LastSavedPosition,
                    Quaternion.identity);
                //Add character controller, but disable it
                var controller = character.gameObject.GetComponent<CharacterController2D>();
                if (!controller)
                {
                    controller = character.gameObject.AddComponent<CharacterController2D>();
                    controller.m_Speed = player.m_CharacterStats.m_Speed;
                }
                controller.enabled = false;
                player.m_VisualCharacter = character;
                character.m_Character = player;
                player.InitCharacter();
            }
        }

        if (m_CharacterManager.m_Players.Count > 0)
        {
            m_CharacterManager.m_CurrentPlayer = m_CharacterManager.m_Players[0];
            m_CharacterManager.m_CurrentPlayer.m_VisualCharacter.GetComponent<CharacterController2D>().enabled = true;
            Camera.main.transform.parent = m_CharacterManager.m_CurrentPlayer.m_VisualCharacter.transform;
        }
    }

    private void CreateCharacterPanel()
    {
        //Set content height
        float height = (m_CharacterManager.m_Players.Count*m_UIManager.m_CharacterButtonHeight) + 
            ((m_CharacterManager.m_Players.Count + 1)*m_UIManager.m_CharacterButtonDistance);
        m_UIManager.m_CharacterContentObject.sizeDelta = new Vector2(m_UIManager.m_CharacterContentObject.sizeDelta.x,height);

        //Set start position
        float yPos = m_UIManager.m_CharacterButtonHeight / 2f;

        //Create button for every character
        for (int i = 0; i < m_CharacterManager.m_Players.Count; i++)
        {
            var player = m_CharacterManager.m_Players[i];

            yPos -= m_UIManager.m_CharacterButtonHeight + m_UIManager.m_CharacterButtonDistance;

            //Button
            GameObject button = new GameObject(m_CharacterManager.m_Players[i].m_CharacterStats.m_CharacterName);
            button.transform.parent = m_UIManager.m_CharacterContentObject;
            var rectTransform = button.AddComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.offsetMin = new Vector2(m_UIManager.m_CharacterButtonMargin, yPos - (m_UIManager.m_CharacterButtonHeight / 2f));
            rectTransform.offsetMax = new Vector2(-m_UIManager.m_CharacterButtonMargin, yPos + (m_UIManager.m_CharacterButtonHeight / 2f));

            button.AddComponent<CanvasRenderer>();
            var imageComponent = button.AddComponent<Image>();
            imageComponent.sprite = m_UIManager.m_CharacterButtonSprite;
            imageComponent.type = Image.Type.Sliced;
            imageComponent.color = m_UIManager.m_CharacterButtonColor;
            var buttonComponent = button.AddComponent<Button>();
            int charIndex = i;
            buttonComponent.onClick.AddListener(delegate { TempFunction(charIndex); });

            //Text
            var textObject = new GameObject("Text");
            textObject.transform.parent = button.transform;
            rectTransform = textObject.AddComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.offsetMin = new Vector2(0, 0);
            rectTransform.offsetMax = new Vector2(0, 0);
            var textComponent = textObject.AddComponent<Text>();
            textComponent.text = player.m_CharacterStats.m_CharacterName;
            textComponent.font = m_UIManager.m_Font;
            textComponent.fontSize = m_UIManager.m_CharacterFontSize;
            textComponent.color = m_UIManager.m_FontColor;
            textComponent.alignment = TextAnchor.MiddleCenter;
        }
    }

    private void CreateInventoryPanel()
    {
        //Set content height & panel width
        int rows = Mathf.CeilToInt(m_Inventory.m_Items.Count/(float) m_UIManager.m_InventoryTileColumns);
        float height = (rows * m_UIManager.m_InventoryTileHeight) + ((rows + 1) * m_UIManager.m_InventoryTileDistance);
        float width = (m_UIManager.m_InventoryTileColumns * m_UIManager.m_InventoryTileWidth) +
              ((m_UIManager.m_InventoryTileColumns + 1 )* m_UIManager.m_InventoryTileDistance) + m_UIManager.m_ScrollbarWidth;
        var panelRect = m_UIManager.m_InventoryPanel.GetComponent<RectTransform>();
        panelRect.offsetMin = new Vector2(- width / 2f, panelRect.offsetMin.y);
        panelRect.offsetMax = new Vector2(width / 2f,panelRect.offsetMax.y);
        m_UIManager.m_InventoryContentObject.sizeDelta = new Vector2(m_UIManager.m_CharacterContentObject.sizeDelta.x, height);

        //Set start position
        Vector2 pos = new Vector2(-m_UIManager.m_InventoryTileWidth /2f , -m_UIManager.m_InventoryTileHeight / 2f - m_UIManager.m_InventoryTileDistance);
        int column = 0;

        for (int i = 0; i < m_Inventory.m_Items.Count; i++)
        {
            pos.x += m_UIManager.m_InventoryTileWidth + m_UIManager.m_InventoryTileDistance;

            //Generate tile
            GameObject tile = new GameObject(m_Inventory.m_Items[i].m_ItemName);
            tile.transform.parent = m_UIManager.m_InventoryContentObject;
            var rectTransform = tile.AddComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.offsetMin = new Vector2(pos.x - m_UIManager.m_InventoryTileWidth / 2f, pos.y - m_UIManager.m_InventoryTileHeight / 2f);
            rectTransform.offsetMax = new Vector2(pos.x + m_UIManager.m_InventoryTileWidth / 2f, pos.y + m_UIManager.m_InventoryTileHeight / 2f);

            tile.AddComponent<CanvasRenderer>();
            var imageComponent = tile.AddComponent<Image>();
            imageComponent.color = Item.GetTypeColor(m_Inventory.m_Items[i].m_Type);

            //Text
            var textObject = new GameObject("Text");
            textObject.transform.parent = tile.transform;
            rectTransform = textObject.AddComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.offsetMin = new Vector2(0, 0);
            rectTransform.offsetMax = new Vector2(0, 0);
            var textComponent = textObject.AddComponent<Text>();
            textComponent.text = string.Format("{0} \n-\n {1}x", m_Inventory.m_Items[i].m_ItemName, m_Inventory.m_Items[i].m_Amount);
            textComponent.font = m_UIManager.m_Font;
            textComponent.fontSize = m_UIManager.m_InventoryFontSize;
            textComponent.color = m_UIManager.m_FontColor;
            textComponent.alignment = TextAnchor.MiddleCenter;

            //Check if new row is starting
            column++;
            if (column >= m_UIManager.m_InventoryTileColumns)
            {
                column = 0;
                pos.x = -m_UIManager.m_InventoryTileWidth / 2f;
                pos.y -= m_UIManager.m_InventoryTileHeight + m_UIManager.m_InventoryTileDistance;
            }
        }
    }

    //TODO: CharacterSwitch
    public void TempFunction(int a)
    {
        Debug.Log(a);
    }
}
