using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public GameObject signPanel;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            signPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        signPanel.SetActive(false);
    }
}
