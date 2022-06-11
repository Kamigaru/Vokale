using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Canvas F3Menu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            TriggerF3();
        }
    }

    private void TriggerF3()
    {
        if (F3Menu.isActiveAndEnabled)
        {
            F3Menu.enabled = false;
        }
        else
        {
            F3Menu.enabled = true;
        }
    }
}
