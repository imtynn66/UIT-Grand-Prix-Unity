using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
public class RegisterAuth : MonoBehaviour
{
    // UI References
    public InputField emailInput;
    public InputField passwordInput;
    public InputField confirmPasswordInput;
    public Button registerButton;
    public Button goToLoginButton;
    public TextMeshProUGUI statusText;

    private FirebaseAuth auth;

    void Start()
    {
        // Khởi tạo Firebase
        auth = FirebaseAuth.DefaultInstance;

        // Gắn sự kiện cho các nút
        if (registerButton != null) registerButton.onClick.AddListener(Register);
        if (goToLoginButton != null) goToLoginButton.onClick.AddListener(() => SceneManager.LoadScene("Login"));

        // Kiểm tra trạng thái người dùng
    }

    void Register()
    {
        if (emailInput == null || passwordInput == null || confirmPasswordInput == null || statusText == null)
        {
            if (statusText != null) statusText.text = "UI không được cấu hình đúng!";
            return;
        }

        string email = emailInput.text;
        string password = passwordInput.text;
        string confirmPassword = confirmPasswordInput.text;

        // Kiểm tra xác nhận mật khẩu
        if (password != confirmPassword)
        {
            statusText.text = "Mật khẩu và xác nhận mật khẩu không khớp!";
            return;
        }

        // Kiểm tra email hợp lệ
        if (!IsValidEmail(email))
        {
            statusText.text = "Email không hợp lệ!";
            return;
        }

        // Đăng ký người dùng mới
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                statusText.text = "Đăng ký bị hủy.";
                return;
            }
            if (task.IsFaulted)
            {
                statusText.text = "Lỗi đăng ký: " + task.Exception.InnerException.Message;
                return;
            }

            // Đăng ký thành công
            FirebaseUser newUser = task.Result.User;
            statusText.text = $"Đăng ký thành công! Chào {newUser.Email}";
            ClearInputs();
            SceneManager.LoadScene("Login"); // Chuyển sang LoginScene
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
        if (confirmPasswordInput != null) confirmPasswordInput.text = "";
    }
}