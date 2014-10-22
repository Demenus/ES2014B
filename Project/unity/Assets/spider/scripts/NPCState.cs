using UnityEngine;
using System.Collections;

public class NPCState : MonoBehaviour {
	
	public float moveSpeed = 1.5f;
	public int maxHealth = 100;
	public int health = 100;
	public Vector3 destination;
	CharacterController characterController;
	
	void Awake(){
		characterController = GetComponent<CharacterController>();
		setDestination(transform.position.x,transform.position.y,transform.position.z);
	}
	
	void Update(){
		move();
	}
	
	private void move(){
		Vector3 moveDirection = destination-transform.position;
		moveDirection.Normalize();
		moveDirection *= moveSpeed;
		characterController.Move (moveDirection * Time.deltaTime);
		lookAt();
	}
	
	// LOOK
	public void lookAt(){
		if((destination - transform.position).magnitude < 0.1){
			return;
		}
		Quaternion newRotation = Quaternion.LookRotation(destination - transform.position);
		newRotation.x = 0f;
		newRotation.z = 0f;
		transform.rotation = Quaternion.Slerp(transform.rotation,newRotation,(moveSpeed/1)*Time.deltaTime);
	}

	// ATTACK
	public void attack(){
		// TODO
	}
	
	// MOVEMENT
	public Vector3 getDestination(){
		return destination;
	}
	public void setDestination(float x,float y,float z){
		destination = new Vector3(x,y,z);
	}
	
	// HP
	public void setHealth(int newHealth){
		health = newHealth;
	}
	public int getHealth(){
		return health;
	}
	public void setMaxHealth(int newMaxHealth){
		maxHealth = newMaxHealth;
	}
	public int getMaxHealth(){
		return maxHealth;
	}
	public void addHealth(int healthToAdd){
		setHealth(health+healthToAdd);
	}
	public void substractHealth(int healthToSubstract){
		setHealth(health-healthToSubstract);
	}
}
