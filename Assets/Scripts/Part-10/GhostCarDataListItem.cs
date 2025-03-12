using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
// GhostCarDataListItem class
public class GhostCarDataListItem : ISerializationCallbackReceiver
// ISerializationCallbackReceiver interface dùng để kiểm soát thông tin trả về từ trình quản lý tài nguyên
{
	[System.NonSerialized]
	public Vector2 position = Vector2.zero;

	[System.NonSerialized]
	public float rotationZ = 0;

	[System.NonSerialized]
	public float timeSinceLevelLoaded = 0;


	//posion, rotation, timeSinceLevelLoaded
	[SerializeField]
	int x = 0;

	[SerializedField]
	int y = 0;

	[SerializedField]
	int r = 0; //rotation

	[SerializedField]
	int t = 0; //time

	[SerializedField]
	int s = 0; //scale

	/* 
	  Việc lưu trữ các thông số dưới dạng int để giảm kích thước lưu trữ. Tối ưu việc lưu trữ hơn
	Ngoài ra còn tránh các số thập phân không cần thiết (ví dụ: 1.0f, 2.0f, 3.0f, ...)
	System.NonSerialized: không lưu trữ giá trị của biến này khi lưu trữ dữ liệu của đối tượng
	ISerializationCallbackReceiver: interface dùng để kiểm soát thông tin trả về từ trình quản lý tài nguyên
	[SerializeField]: dùng để lưu trữ giá trị của biến này khi lưu trữ dữ liệu của đối tượng
	 */

}
