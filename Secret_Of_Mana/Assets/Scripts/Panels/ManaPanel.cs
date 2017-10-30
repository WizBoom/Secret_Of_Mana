using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManaPanel : MonoBehaviour
{
    public void Initialize()
    {
        this.gameObject.SetActive(false);
        Refresh();
    }

    public abstract void Refresh();
}
