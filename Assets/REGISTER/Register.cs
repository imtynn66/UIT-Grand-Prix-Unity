using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class RegisterManager : MonoBehaviour
{
    public InputField usernameField;            // Legacy InputField
    public InputField passwordField;
    public InputField confirmPasswordField;
    public Button registerButton;
    public TMP_Text messageText;                // Chỉ Text thông báo dùng TMP
    public Button loginButton;                // Nút chuyển đến trang đăng nhập
    void Start()
    {
        registerButton.onClick.AddListener(OnRegisterClick);
        loginButton.onClick.AddListener(OnLoginClick); // Thêm sự kiện cho nút đăng nhập
    }

    void OnLoginClick()
    {
        SceneManager.LoadScene("Login"); // Chuyển đến scene đăng nhập
    }
    void OnRegisterClick()
    {
        string username = usernameField.text.Trim();
        string password = passwordField.text;
        string confirmPassword = confirmPasswordField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            messageText.text = "Please fill in all fields.";
            return;
        }

        if (password != confirmPassword)
        {
            messageText.text = "Passwords do not match!";
            return;
        }

        StartCoroutine(RegisterUser(username, password));
    }

    IEnumerator RegisterUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/uit-grand-prix/register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error: " + www.error);
                messageText.text = "Server connection error.";
            }
            else
            {
                string response = www.downloadHandler.text.Trim();
                Debug.Log("Server response: " + response);

                switch (response)
                {
                    case "success":
                        messageText.text = "Registration successful!";
                        SceneManager.LoadScene("Login");
                        break;
                    case "username_taken":
                        messageText.text = "Username already exists!";
                        break;
                    case "empty_fields":
                        messageText.text = "Please fill in all fields.";
                        break;
                    default:
                        messageText.text = "Registration failed!";
                        break;
                }
            }
        }
    }
}
