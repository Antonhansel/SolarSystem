using UnityEngine;
using System.Collections;
using SocketIO;


public class move : MonoBehaviour {
	public float speed = 500.0F;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 nextDirection = Vector3.zero;
	private bool socketInput = false;
	private SocketIOComponent socket;
	private CharacterController controller; 
	private float nextRotation = 0F;

	public void Start() {
		controller = GetComponent<CharacterController>();
		GameObject go = GameObject.Find("SocketIO");

		socket = go.GetComponent<SocketIOComponent>();
		// Defining the socket callback
		socket.On ("test", (SocketIOEvent e) => {
			Debug.Log("TEST EVENT RECIEVED");
		});
		socket.On("move", (SocketIOEvent e) => {
			Debug.Log(e.data);
			nextDirection = new Vector3(0, e.data["z"].f, e.data["y"].f);
			nextRotation = e.data["x"].f * 50;
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
		transform.Rotate(0, nextRotation * Time.deltaTime, 0);
	}
}
