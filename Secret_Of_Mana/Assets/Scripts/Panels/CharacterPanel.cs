using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : ManaPanel
{
    public override void Refresh()
    {
        CharacterManager characterManager = GameManager.m_Instance.m_CharacterManager;
        UIManager UIManager = GameManager.m_Instance.m_UIManager;

        //Set content height
        float height = (characterManager.m_Players.Count * UIManager.m_CharacterButtonHeight) +
            ((characterManager.m_Players.Count + 1) * UIManager.m_CharacterButtonDistance);
        UIManager.m_CharacterContentObject.sizeDelta = new Vector2(UIManager.m_CharacterContentObject.sizeDelta.x, height);

        //Set start position
        float yPos = UIManager.m_CharacterButtonHeight / 2f;

        //Create button for every character
        for (int i = 0; i < characterManager.m_Players.Count; i++)
        {
            var player = characterManager.m_Players[i];

            yPos -= UIManager.m_CharacterButtonHeight + UIManager.m_CharacterButtonDistance;

            //Button
            GameObject button = new GameObject(characterManager.m_Players[i].m_CharacterStats.m_CharacterName);
            button.transform.parent = UIManager.m_CharacterContentObject;
            var rectTransform = button.AddComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.offsetMin = new Vector2(UIManager.m_CharacterButtonMargin, yPos - (UIManager.m_CharacterButtonHeight / 2f));
            rectTransform.offsetMax = new Vector2(-UIManager.m_CharacterButtonMargin, yPos + (UIManager.m_CharacterButtonHeight / 2f));

            button.AddComponent<CanvasRenderer>();
            var imageComponent = button.AddComponent<Image>();
            imageComponent.sprite = UIManager.m_CharacterButtonSprite;
            imageComponent.type = Image.Type.Sliced;
            imageComponent.color = UIManager.m_CharacterButtonColor;
            var buttonComponent = button.AddComponent<Button>();
            int charIndex = i;
            buttonComponent.onClick.AddListener(delegate { SwitchCharacter(charIndex); });

            //Text
            var textObject = new GameObject("Text");
            textObject.transform.parent = button.transform;
            rectTransform = textObject.AddComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.offsetMin = new Vector2(0, 0);
            rectTransform.offsetMax = new Vector2(0, 0);
            var textComponent = textObject.AddComponent<Text>();
            textComponent.text = player.m_CharacterStats.m_CharacterName + "\n-\n" + player.m_Weapon.m_ItemName;
            textComponent.font = UIManager.m_Font;
            textComponent.fontSize = UIManager.m_CharacterFontSize;
            textComponent.color = UIManager.m_FontColor;
            textComponent.alignment = TextAnchor.MiddleCenter;
        }
    }

    public void SwitchCharacter(int playerIndex)
    {
        GameManager.m_Instance.m_CharacterManager.SetCurrentPlayer(playerIndex);
        GameManager.m_Instance.m_UIManager.ToggleCharacterPanel();
    }
}
