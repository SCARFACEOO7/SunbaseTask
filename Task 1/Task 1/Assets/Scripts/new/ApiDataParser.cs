using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DataModel;

public class ApiDataParser : MonoBehaviour
{
    private const string url = "https://qa2.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";


    void Start()
    {
        StartCoroutine(FetchData());
    }

    IEnumerator FetchData()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.LogError("API call failed: " + webRequest.error);  
            string jsonData = webRequest.downloadHandler.text;
            Debug.Log(jsonData);

            Welcome clientData = JsonUtility.FromJson<Welcome>(jsonData);

            foreach(Client client in clientData.Clients)
            {
                Debug.Log(client.Id);
                Debug.Log(client.Label);
                Debug.Log(client.IsManager);
            }
            
        }
    }
}
