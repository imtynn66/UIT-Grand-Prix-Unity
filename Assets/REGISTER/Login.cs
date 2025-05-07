using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;


public class LoginManager : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;
    public Button loginButton;
    public TMP_Text messageText; // Thông báo trạng thái (nếu muốn)
    public Button registerButton; // Nút chuyển đến trang đăng ký
    void Start()
    {
        loginButton.onClick.AddListener(OnLoginClick);
        registerButton.onClick.AddListener(OnRegisterClick); // Thêm sự kiện cho nút đăng ký
    }

    void OnLoginClick()
    {
        string username = usernameField.text;
        string password = passwordField.text;
        StartCoroutine(LoginUser(username, password));
    }

    void OnRegisterClick()
    {
        SceneManager.LoadScene("Register"); // Chuyển đến scene đăng ký
    }

    IEnumerator LoginUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/uit-grand-prix/login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error: " + www.error);
                if (messageText) messageText.text = "Server error";
            }
            else
            {
                string response = www.downloadHandler.text.Trim();
                if (response == "success")
                {
                    PlayerPrefs.SetString("username", username);
                    Debug.Log("Login successful");
                    if (messageText) messageText.text = "Login successful!";
                    SceneManager.LoadScene("Menu"); // thay bằng tên scene của bạn
                }
                else
                {
                    Debug.Log("Login failed");
                    if (messageText) messageText.text = "Invalid username or password!";
                }
            }
        }
    }
}
