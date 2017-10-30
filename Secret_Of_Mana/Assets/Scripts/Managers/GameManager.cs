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
}
