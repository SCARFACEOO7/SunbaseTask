using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class MakeListUI : MonoBehaviour
{
    #region Variables
    public ApiClientfetch apiClientfetch;
    public GameObject listPrefab;
    public Transform contentTransform;

    public List<ClientData> clients;
    
    #endregion  

    #region  unity Methods
    private void OnEnable()
    {
        if(apiClientfetch != null)
        {
            apiClientfetch.OnDataFetched += FillScrollView;
        }
    }

    private void OnDisable()
    {
        if(apiClientfetch != null)
        {
            apiClientfetch.OnDataFetched -= FillScrollView;
        }
    }


    #endregion

    #region private methods

    private void FillScrollView()
    {
        List<ClientData> clients = apiClientfetch.clients;

        if(apiClientfetch.dataDictionary == null)
        {
            return;
        }
        foreach(ClientData client in clients)
        {
            int clientID = int.Parse(client.id);
            ClientInfo clientInfo = apiClientfetch.GetClientInfoById(clientID);
            
            if(clientInfo != null)
            {
                client.address = clientInfo.address;
                client.name = clientInfo.name;
                client.points = clientInfo.points;
            }

            GameObject listEntry = Instantiate(listPrefab, contentTransform);

            TMP_Text name = listEntry.transform.Find("Name").GetComponent<TMP_Text>();
            TMP_Text points = listEntry.transform.Find("Points").GetComponent<TMP_Text>();

            name.text = clientInfo.name;
            points.text = clientInfo.points.ToString();
        }
    }

    #endregion
}
