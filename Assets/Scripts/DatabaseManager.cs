using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance { get; private set; }

    [Header("Connection variables")]
    public string host = "http://localhost/quasartest/Test/controller/";
    [HideInInspector]
    public string answerDB;
    bool registerCompleted = false;

    [Header("Scripts Reference")]
    public UIController uiControl;
    public UserData userLog;

    public class User
    {
        public int iD;
        public string name, lastName, email, password;

        public User(int iD, string name, string lastName, string email, string password)
        {
            this.iD = iD;
            this.name = name;
            this.lastName = lastName;
            this.email = email;
            this.password = password;
        }
    }
    [HideInInspector]
    public User userClass, userAux;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }
    public void Register()
    {
        string url = host + "controller.php?page=register";
        WWWForm form = new WWWForm();
        form.AddField("_name", uiControl.userNameR.text);
        form.AddField("_lastName", uiControl.lastNameR.text);
        form.AddField("_email", uiControl.emailR.text);
        form.AddField("_password", uiControl.passwordR.text);
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        StartCoroutine(WaitForRequestInsert(request));
    }
    public void Login()
    {
        string url = host + "controller.php?page=login";
        WWWForm form = new WWWForm();
        form.AddField("_email", uiControl.emailL.text);
        form.AddField("_password", uiControl.passwordL.text);
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        StartCoroutine(WaitForRequestSelect(request));
    }

    #region Request functions
    //Database call to insert user data
    private IEnumerator WaitForRequestInsert(UnityWebRequest _request)
    {
        _request.timeout = 0;
        yield return _request.SendWebRequest();

        //check for errors
        if (_request.result == UnityWebRequest.Result.ConnectionError || _request.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log("Request Error: " + _request.error);
        else
        {
            answerDB = _request.downloadHandler.text;
            registerCompleted = true;
        }
        if (registerCompleted)
        {
            uiControl.ToggleActivePanel();
            uiControl.ClearInputFieldsText();
        }
        _request.Dispose();
    }

    //Database call to select user data
    private IEnumerator WaitForRequestSelect(UnityWebRequest _request)
    {
        _request.timeout = 0;
        yield return _request.SendWebRequest();
        string[] finalString = new string[0];

        //check for errors
        if (_request.result == UnityWebRequest.Result.ConnectionError || _request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Request Error: " + _request.error);
            yield break;
        }

        string jsonStringHP = _request.downloadHandler.text;

        if (jsonStringHP.Contains("},{"))
        {
            string[] text0 = jsonStringHP.Split(new[] { "},{" }, StringSplitOptions.None);
            finalString = new string[text0.Length];

            for (int i = 0; i < text0.Length; i++)
            {
                if (i == 0)
                    finalString[i] = text0[i] + "}]";
                else
                {
                    if (i == text0.Length - 1)
                        finalString[i] = "[{" + text0[i];
                    else
                        finalString[i] = "[{" + text0[i] + "}]";
                }
            }
            string temp = text0[0] + "}]";
            jsonStringHP = temp;
        }
        else
            finalString = new string[1] { jsonStringHP };

        for (int i = 0; i < finalString.Length; i++)
        {
            finalString[i] = finalString[i].Replace("[", "");
            finalString[i] = finalString[i].Replace("]", "");
            userAux = JsonUtility.FromJson<User>(finalString[i]);

            if (userAux.email == uiControl.emailL.text)
            {
                userClass = userAux;
                userLog.SetUserData(userAux.iD, userAux.name, userAux.lastName, userAux.email, userAux.password);
                Debug.Log(userAux.name + " logged succesfully.");
                uiControl.LoadMainScene();
            }
        }
        _request.Dispose();
    }
    #endregion
}
