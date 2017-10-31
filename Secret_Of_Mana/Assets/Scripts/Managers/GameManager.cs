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

    public static int m_PlayerLayer = 8;
    public static GameManager m_Instance = null;

    private void Awake()
    {
        if (!m_Instance)
            m_Instance = this;

        m_UIManager.Init();
        m_CharacterManager.InitEnemy();

        CreateCharacterPrefabs();
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
        
        m_CharacterManager.Update();
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

        //HUD chunk
        float chunkWidth = (m_UIManager.m_Canvas.offsetMax.x - m_UIManager.m_Canvas.offsetMin.x) / (m_CharacterManager.m_Players.Count + 2);

        //Create players / link their HUD
        for (int i = 0; i < m_CharacterManager.m_Players.Count; i++)
        {
            var player = m_CharacterManager.m_Players[i];
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
                player.m_VisualCharacter = character;
                character.m_Character = player;

                var HUDObject = Instantiate(m_UIManager.m_HUDPrefab, m_UIManager.m_Canvas.transform);

                //Calculate position
                float index = (i - ((m_CharacterManager.m_Players.Count - 1) / 2f))*(m_CharacterManager.m_Players.Count/2f);
                Vector2 pos = new Vector2(index*chunkWidth, m_UIManager.m_HUDYPos);
                var HUDRect = HUDObject.GetComponent<RectTransform>();
                float width = HUDRect.offsetMax.x - HUDRect.offsetMin.x;
                float height = HUDRect.offsetMax.y - HUDRect.offsetMin.y;
                HUDRect.offsetMin = new Vector2(pos.x - width / 2f, pos.y - height / 2f);
                HUDRect.offsetMax = new Vector2(pos.x + width / 2f, pos.y + height / 2f);

                player.m_HUD = HUDObject.GetComponent<HUD>();
                player.m_HUD.m_Character = player;
                player.InitCharacter();
            }
        }

        m_UIManager.Refresh();
        m_CharacterManager.SetCurrentPlayer(0);
    }
}
