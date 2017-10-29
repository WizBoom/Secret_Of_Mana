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

    public static GameManager m_Instance = null;

    private void Awake()
    {
        if (!m_Instance)
            m_Instance = this;

        m_UIManager.Init();

        CreateCharacterPrefabs();
        CreateCharacterPanel();
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
        float height = (m_CharacterManager.m_Players.Count*m_UIManager.m_ButtonHeight) + ((m_CharacterManager.m_Players.Count + 1)*m_UIManager.m_ButtonDistance);
        m_UIManager.m_ContentObject.sizeDelta = new Vector2(m_UIManager.m_ContentObject.sizeDelta.x,height);

        //Set start position
        float yPos = m_UIManager.m_ButtonHeight / 2f;

        //Create button for every character
        for (int i = 0; i < m_CharacterManager.m_Players.Count; i++)
        {
            var player = m_CharacterManager.m_Players[i];

            yPos -= m_UIManager.m_ButtonHeight + m_UIManager.m_ButtonDistance;

            //Button
            GameObject button = new GameObject(m_CharacterManager.m_Players[i].m_CharacterStats.m_CharacterName);
            button.transform.parent = m_UIManager.m_ContentObject;
            var rectTransform = button.AddComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.offsetMin = new Vector2(m_UIManager.m_ButtonMargin, yPos - (m_UIManager.m_ButtonHeight / 2f));
            rectTransform.offsetMax = new Vector2(-m_UIManager.m_ButtonMargin, yPos + (m_UIManager.m_ButtonHeight / 2f));

            button.AddComponent<CanvasRenderer>();
            var imageComponent = button.AddComponent<Image>();
            imageComponent.sprite = m_UIManager.m_ButtonSprite;
            imageComponent.type = Image.Type.Sliced;
            imageComponent.color = m_UIManager.m_ButtonColor;
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
            textComponent.font = m_UIManager.m_ButtonTextFont;
            textComponent.fontSize = m_UIManager.m_ButtonTextSize;
            textComponent.color = m_UIManager.m_ButtonTextColor;
            textComponent.alignment = TextAnchor.MiddleCenter;
        }
    }

    //TODO: CharacterSwitch
    public void TempFunction(int a)
    {
        Debug.Log(a);
    }
}
