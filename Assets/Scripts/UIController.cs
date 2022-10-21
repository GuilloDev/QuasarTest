using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject loginPanel;
    public GameObject registerPanel;

    [Header("Login InputFields")]
    public TMP_InputField emailL;
    public TMP_InputField passwordL;

    [Header("Register InputFields")]
    public TMP_InputField userNameR;
    public TMP_InputField lastNameR;
    public TMP_InputField emailR;
    public TMP_InputField passwordR;

    //Toggle panels visibility
    public void ToggleActivePanel()
    {
        registerPanel.SetActive(!registerPanel.activeSelf);
        loginPanel.SetActive(!loginPanel.activeSelf);
    }
    public void ClearInputFieldsText()
    {
        emailL.text = "";
        passwordL.text = "";

        userNameR.text = "";
        lastNameR.text = "";
        emailR.text = "";
        passwordR.text = "";
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}

