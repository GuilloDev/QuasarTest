using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UserData : MonoBehaviour
{
    public static UserData Instance { get; private set; }
    public int iD;
    public string userName, lastName, email, password;
    public TMP_Text userText;

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
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void SetUserData(int _iD, string _username, string _lastName, string _email, string _password)
    {
        iD = _iD;
        userName = _username;
        lastName = _lastName;
        email = _email;
        password = _password;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            userText = GameObject.Find("UserText").GetComponent<TMP_Text>();
            userText.text = userName;
        }
    }
}
