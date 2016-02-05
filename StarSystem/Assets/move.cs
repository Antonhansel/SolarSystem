using UnityEngine;
using System.Collections;
using SocketIO;


public class move : MonoBehaviour {
	public float speed = 100.0F;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 nextDirection = Vector3.zero;
	private bool socketInput = false;
	private SocketIOComponent socket;
	private CharacterController controller; 

	public void Start() {
		controller = GetComponent<CharacterController>();
		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();

		// Defining the socket callback
		socket.On("move", (SocketIOEvent e) => {
			Debug.Log(e.data);
			nextDirection = new Vector3(e.data["x"].f, 0, e.data["y"].f);
			socketInput = true;
		});
	}

	void Update() {
		if (socketInput) {
			Debug.Log (nextDirection);
			moveDirection = nextDirection;
			socketInput = false;
		} else {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		}
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		moveDirection.y -= Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}
}
