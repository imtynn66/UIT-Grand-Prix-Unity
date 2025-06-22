using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
public class LoginAuth : MonoBehaviour
{
    // UI References
    public InputField emailInput;
    public InputField passwordInput;
    public Button loginButton;
    public Button goToRegisterButton;
    public TextMeshProUGUI statusText;
    private FirebaseAuth auth;

    void Start()
    {
        // Khởi tạo Firebase
        auth = FirebaseAuth.DefaultInstance;

        // Gắn sự kiện cho các nút
        if (loginButton != null) loginButton.onClick.AddListener(Login);
        if (goToRegisterButton != null) goToRegisterButton.onClick.AddListener(() => SceneManager.LoadScene("Register"));

        // Kiểm tra trạng thái người dùng
    }

    void Login()
    {
        if (emailInput == null || passwordInput == null || statusText == null)
        {
            if (statusText != null) statusText.text = "UI không được cấu hình đúng!";
            return;
        }

        string email = emailInput.text;
        string password = passwordInput.text;

        // Kiểm tra email hợp lệ
        if (!IsValidEmail(email))
        {
            statusText.text = "Email không hợp lệ!";
            return;
        }

        // Đăng nhập
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                statusText.text = "Đăng nhập bị hủy.";
                return;
            }
            if (task.IsFaulted)
            {
                statusText.text = "Lỗi đăng nhập: " + task.Exception.InnerException.Message;
                return;
            }

            // Đăng nhập thành công
            FirebaseUser user = task.Result.User;
            statusText.text = $"Đăng nhập thành công! Chào {user.Email}";
            ClearInputs();
            SceneManager.LoadScene("Menu"); // Chuyển sang MainMenu
        });
    }


    bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    void ClearInputs()
    {
        if (emailInput != null) emailInput.text = "";
        if (passwordInput != null) passwordInput.text = "";
    }
}