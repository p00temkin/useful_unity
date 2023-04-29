using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class JsonRestClient : MonoBehaviour
{

    private T fromJSON<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }

    private string toJSON<T>(T model)
    {
        return JsonUtility.ToJson(model);
    }

    public void Get<J>(string url, string successCallback, string errorCallback)
    {
        this.request<Object, J>(url, "GET", null, successCallback, errorCallback);
    }

    public void Post<T, J>(string url, T payload, string successCallback, string errorCallback)
    {
        this.request<T, J>(url, "POST", payload, successCallback, errorCallback);
    }

    public void Post<J>(string url, string successCallback, string errorCallback)
    {
        this.request<Object, J>(url, "POST", null, successCallback, errorCallback);
    }

    public void Put<T, J>(string url, T payload, string successCallback, string errorCallback)
    {
        this.request<T, J>(url, "PUT", payload, successCallback, errorCallback);
    }

    public void Put<J>(string url, string successCallback, string errorCallback)
    {
        this.request<Object, J>(url, "PUT", null, successCallback, errorCallback);
    }

    public void Delete<T, J>(string url, T payload, string successCallback, string errorCallback)
    {
        this.request<T, J>(url, "DELETE", payload, successCallback, errorCallback);
    }

    public void Delete<J>(string url, string successCallback, string errorCallback)
    {
        this.request<Object, J>(url, "DELETE", null, successCallback, errorCallback);
    }

    private void request<T, J>(string url, string method, T payload, string successCallback, string errorCallback)
    {
        this.StartCoroutine(this.handleAsyncRequest<T, J>(payload, method, url, successCallback, errorCallback));
    }

    private IEnumerator handleAsyncRequest<T, J>(T payload, string method, string url, string successCallback, string errorCallback)
    {
        UnityWebRequest webRequest = new UnityWebRequest(url, method);
        byte[] payloadBytes;
        if (payload != null)
        {
            payloadBytes = System.Text.Encoding.UTF8.GetBytes(this.toJSON<T>(payload));
        }
        else
        {
            payloadBytes = System.Text.Encoding.UTF8.GetBytes("{}");
        }

        UploadHandler upload = new UploadHandlerRaw(payloadBytes);
        webRequest.uploadHandler = upload;
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            gameObject.BroadcastMessage(errorCallback, webRequest.error);
        }
        else
        {
            Debug.Log("webRequest.responseCode: " + webRequest.responseCode);
            Debug.Log("webRequest.downloadHandler.text: " + webRequest.downloadHandler.text);
            if (webRequest.responseCode < 200 || webRequest.responseCode >= 400)
            {
                gameObject.BroadcastMessage(errorCallback, webRequest.downloadHandler.text);
            }
            else
            {
                var response = this.fromJSON<J>(webRequest.downloadHandler.text);
                gameObject.BroadcastMessage(successCallback, response);
            }
        }
    }
}