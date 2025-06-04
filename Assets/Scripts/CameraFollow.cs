using UnityEngine;
using Photon.Pun;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform Target;

    private bool targetFound = false;

    void Start()
    {
        // Tìm target khi game bắt đầu
        FindLocalPlayerTarget();
    }

    void Update()
    {
        // Nếu chưa tìm thấy target, tiếp tục tìm
        if (!targetFound)
        {
            FindLocalPlayerTarget();
            return;
        }

        // Follow target nếu đã tìm thấy
        if (Target != null)
        {
            Vector3 newPos = new Vector3(Target.position.x, Target.position.y, -10f);
            transform.position = Vector3.Lerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        }
    }

    void FindLocalPlayerTarget()
    {
        // Tìm tất cả PhotonView trong scene
        PhotonView[] photonViews = FindObjectsOfType<PhotonView>();

        foreach (PhotonView pv in photonViews)
        {
            // Kiểm tra nếu PhotonView này thuộc về local player
            if (pv.IsMine)
            {
                // Kiểm tra nếu object này có tag "Player" hoặc tên chứa "Car"
                if (pv.gameObject.CompareTag("Player") || pv.gameObject.name.Contains("Car"))
                {
                    Target = pv.transform;
                    targetFound = true;
                    Debug.Log("Camera target found: " + Target.name);
                    break;
                }
            }
        }
    }
}