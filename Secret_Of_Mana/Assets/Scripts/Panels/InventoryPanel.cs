using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : ManaPanel
{
    public override void Refresh()
    {
        Inventory inventory = GameManager.m_Instance.m_Inventory;
        UIManager UIManager = GameManager.m_Instance.m_UIManager;

        //Set content height & panel width
        int rows = Mathf.CeilToInt(inventory.m_Items.Count / (float)UIManager.m_InventoryTileColumns);
        float height = (rows * UIManager.m_InventoryTileHeight) + ((rows + 1) * UIManager.m_InventoryTileDistance);
        float width = (UIManager.m_InventoryTileColumns * UIManager.m_InventoryTileWidth) +
              ((UIManager.m_InventoryTileColumns + 1) * UIManager.m_InventoryTileDistance) + UIManager.m_ScrollbarWidth;
        var panelRect = UIManager.m_InventoryPanel.GetComponent<RectTransform>();
        panelRect.offsetMin = new Vector2(-width / 2f, panelRect.offsetMin.y);
        panelRect.offsetMax = new Vector2(width / 2f, panelRect.offsetMax.y);
        UIManager.m_InventoryContentObject.sizeDelta = new Vector2(UIManager.m_CharacterContentObject.sizeDelta.x, height);

        //Set start position
        Vector2 pos = new Vector2(-UIManager.m_InventoryTileWidth / 2f, -UIManager.m_InventoryTileHeight / 2f - UIManager.m_InventoryTileDistance);
        int column = 0;

        for (int i = 0; i < inventory.m_Items.Count; i++)
        {
            pos.x += UIManager.m_InventoryTileWidth + UIManager.m_InventoryTileDistance;

            //Generate tile
            GameObject tile = new GameObject(inventory.m_Items[i].m_ItemName);
            tile.transform.parent = UIManager.m_InventoryContentObject;
            var rectTransform = tile.AddComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.offsetMin = new Vector2(pos.x - UIManager.m_InventoryTileWidth / 2f, pos.y - UIManager.m_InventoryTileHeight / 2f);
            rectTransform.offsetMax = new Vector2(pos.x + UIManager.m_InventoryTileWidth / 2f, pos.y + UIManager.m_InventoryTileHeight / 2f);

            tile.AddComponent<CanvasRenderer>();
            var imageComponent = tile.AddComponent<Image>();
            imageComponent.color = Item.GetTypeColor(inventory.m_Items[i].m_Type);

            //Text
            var textObject = new GameObject("Text");
            textObject.transform.parent = tile.transform;
            rectTransform = textObject.AddComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.offsetMin = new Vector2(0, 0);
            rectTransform.offsetMax = new Vector2(0, 0);
            var textComponent = textObject.AddComponent<Text>();
            textComponent.text = string.Format("{0} \n-\n {1}x", inventory.m_Items[i].m_ItemName, inventory.m_Items[i].m_Amount);
            textComponent.font = UIManager.m_Font;
            textComponent.fontSize = UIManager.m_InventoryFontSize;
            textComponent.color = UIManager.m_FontColor;
            textComponent.alignment = TextAnchor.MiddleCenter;

            //Check if new row is starting
            column++;
            if (column >= UIManager.m_InventoryTileColumns)
            {
                column = 0;
                pos.x = -UIManager.m_InventoryTileWidth / 2f;
                pos.y -= UIManager.m_InventoryTileHeight + UIManager.m_InventoryTileDistance;
            }
        }
    }
}
