using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterManager
{
    public List<Enemy> m_Enemies;
    public List<Player> m_Players;
    [HideInInspector]
    public Player m_CurrentPlayer;

    public void SetCurrentPlayer(int playerIndex)
    {
        //Return if index is out of range
        if (playerIndex >= m_Players.Count || playerIndex < 0)
            return;

        var player = m_Players[playerIndex];
        if (player == null)
            return;

        //Set current player and move main camera
        m_CurrentPlayer = player;
        m_CurrentPlayer.m_VisualCharacter.GetComponent<CharacterController2D>().enabled = true;

        Camera.main.transform.parent = m_CurrentPlayer.m_VisualCharacter.transform;
        Camera.main.transform.localPosition = new Vector3(0, 0, -1);

        //Controllers
        foreach (var p in m_Players)
        {
            var controller = p.m_VisualCharacter.GetComponent<AIController>();
            if (!controller)
            {
                controller = p.m_VisualCharacter.GetComponent<AIController>();
            }
            if (p != m_CurrentPlayer)
            {
                p.m_VisualCharacter.GetComponent<CharacterController2D>().enabled = false;
                if (controller)
                    p.m_VisualCharacter.GetComponent<AIController>().enabled = true;
            }
            else if(controller)
            {
                p.m_VisualCharacter.GetComponent<AIController>().enabled = false;
            }
        }

        //Reset cooldown
        m_CurrentPlayer.m_CurrentWeaponTimer = m_CurrentPlayer.m_Weapon.m_RateOfAttack;
    }

    public void Update()
    {
        if (m_CurrentPlayer.m_CurrentWeaponTimer < m_CurrentPlayer.m_Weapon.m_RateOfAttack)
            m_CurrentPlayer.m_CurrentWeaponTimer += Time.deltaTime;

        foreach (var p in m_Players)
        {
            if (p.m_CurrentManapoints == p.m_CharacterStats.m_MaxManaPoints)
                continue;

            p.m_CurrentManaTimer += Time.deltaTime;
            if (p.m_CurrentManaTimer > p.m_CharacterStats.m_ManaRefreshRate)
            {
                p.m_CurrentManaTimer = 0;
                p.ApplyManapoints(1);
            }
        } 
    }
}
