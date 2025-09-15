using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text;
using System.Net;

public class UnityToNode : MonoBehaviour
{

    public Button btnGetExample;
    public Button btnPostExample;
    public Button btnResDataExample;
    public string host;             //IP�ּ� 
    public int port;     //��Ʈ �ּ�
    public string route;

    public string postUrl;
    public string resUrl;
    public int id;
    public string data;



    public void Start()
    {
        btnGetExample.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/{2}", host, port, route);

            Debug.Log(url);
            StartCoroutine(GetData(url, (raw) =>
            {
                var res = JsonConvert.DeserializeObject<Protocols.Packets.common>(raw);
                Debug.LogFormat("{0},{1}", res.cmd, res.message);
            }));

        });

        btnPostExample.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/{2}", host, port, postUrl);
            Debug.Log(url);
            var req = new Protocols.Packets.req_data();
            req.cmd = 1000;
            req.id = id;
            req.data = data;
            var json = JsonConvert.SerializeObject(req);

            StartCoroutine(PostData(url, json, (raw) =>
            {
                Protocols.Packets.common res = JsonConvert.DeserializeObject<Protocols.Packets.common>(raw);
                Debug.LogFormat("{0},{1}", res.cmd, res.message);
            }));

        });

        btnResDataExample.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/{2}", host, port, resUrl);

            Debug.Log(url);
            StartCoroutine(GetData(url, (raw) =>
            {
                var res = JsonConvert.DeserializeObject <Protocols.Packets.res_data>(raw);
                foreach (var user in res.result)
                {
                    Debug.LogFormat("{0} , {1}", user.id, user.data);
                }

            }));
        });


    }
    
    private IEnumerator GetData(string url, System.Action<string> callback) //get��û�ϴ� �ڷ�ƾ�Լ�
    {
        var webRequest = UnityWebRequest.Get(url);                            //�� ��û get
        yield return webRequest.SendWebRequest();                              //��û�� ���ƿö����� ���

        Debug.Log("Get :" + webRequest.downloadHandler.text);
        if (webRequest.result == UnityWebRequest.Result.ConnectionError
            || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("��Ʈ��ũ ȯ���� ���� �ʾ� ��� �Ұ���");
        }
        else
        {
            callback(webRequest.downloadHandler.text);
        }
    }

    private IEnumerator PostData(string url, string json, System.Action<string> callback) //get��û�ϴ� �ڷ�ƾ�Լ�
    {
        var webRequest = new UnityWebRequest(url, "POST");                            //�� ��û get
        var bodyRaw = Encoding.UTF8.GetBytes(json);                           //��û�� ���ƿö����� ���


        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();


        if (webRequest.result == UnityWebRequest.Result.ConnectionError
            || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("��Ʈ��ũ ȯ���� ���� �ʾ� ��� �Ұ���");
        }
        else
        {
            callback(webRequest.downloadHandler.text);
        }


        webRequest.Dispose();
    }


}
