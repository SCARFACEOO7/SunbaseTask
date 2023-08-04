using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIcontroller : MonoBehaviour
{
    public GameObject listPanel;
    public GameObject startPanel;
    public void OnClickFetchData()
    {
        startPanel.SetActive(false);
        listPanel.SetActive(true);
    }
}
