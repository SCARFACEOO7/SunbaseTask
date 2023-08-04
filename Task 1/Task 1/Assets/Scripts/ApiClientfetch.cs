using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ApiClientfetch : MonoBehaviour
{
    #region Variable and classes
    private const string url = "https://qa2.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";

    public List<ClientData> clients;
    public Dictionary<string, ClientInfo> dataDictionary;

    public delegate void DataFetchedEventHandler();
    public event DataFetchedEventHandler OnDataFetched;

    [System.Serializable]
    private class DataWrapper
    {
        public List<ClientData> clients;
        public Dictionary<string, ClientInfo> data;
    }
    

    #endregion

    #region private Functions

    private void Start() 
    {
        StartCoroutine(FetchClientData());
    }
    IEnumerator FetchClientData()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("API call failed: " + webRequest.error);  
            yield break;

        }

            string jsonData = webRequest.downloadHandler.text;  
            
            DataWrapper dataWrapper = JsonUtility.FromJson<DataWrapper>(jsonData);
            Dictionary<string,  object> jsonDataDictionary = 
            JsonUtility.FromJson<Dictionary<string,  object>>(jsonData);

            Dictionary<string, object> dataDict = 
            jsonDataDictionary["data"] as Dictionary<string, object>;
            
            Dictionary<string, ClientInfo> clientInfoDictionary = new Dictionary<string, ClientInfo>();

            foreach(var info in dataWrapper.data)
            {
                ClientInfo clientInfo = 
                JsonUtility.FromJson<ClientInfo>(info.Value.ToString());


                clientInfoDictionary.Add(info.Key, clientInfo);
            }

            dataWrapper.data = clientInfoDictionary;

            clients = dataWrapper.clients;
            dataDictionary = dataWrapper.data;

            OnDataFetched   ?.Invoke();
    }

    

    #endregion 

    #region public methods
    public ClientInfo GetClientInfoById(int id)
    {
        if(dataDictionary != null && 
        dataDictionary.TryGetValue(id.ToString(), out ClientInfo clientInfo))
        {
            return clientInfo;
        }
        else
        {
            Debug.Log("No Information found");
            return null;
        }
    }
    #endregion
}
