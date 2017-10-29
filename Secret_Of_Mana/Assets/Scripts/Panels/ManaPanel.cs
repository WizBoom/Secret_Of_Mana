using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManaPanel : MonoBehaviour
{
    public void Initialize()
    {
        this.gameObject.SetActive(false);

        //TODO: Create panels if they don't exist
    }
}
