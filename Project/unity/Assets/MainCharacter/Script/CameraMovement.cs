using UnityEngine;
using System.Collections; 

using System.Collections.Generic;

using System.Collections.Generic;

public class CameraMovement : MonoBehaviour
{
	
		public Vector3 posCamera = new Vector3 (9, 25, 9);
		public float distanciaMin = 30f;
		public float distanciaMax = 40f;
		public int marge = 20;
		public GameObject playerGo;
		public Transform player;
		public Vector3 relCameraPos;
		public float relCameraPosMag;
		public Vector3 newPos;
		public float zoomSpeed = 25f;
		public float distancia, distanciaAnt = 0;
		public float smooth = 1.5f;         // suavitat de moviment de la camera
		private List<GameObject> amagats;
		private Vector3 firstMovement;
		private Vector3 anterior;
		private bool posSetejada = false;
		private float timeout = 3f;
	private float maxZ = 150f;
	private float minZ = 60f;
	private float zInit = -1f;
		
		void Awake ()
		{
				updatePlayerGo ();
				
		}

		void updatePlayerGo ()
		{
				if (player == null) {
						playerGo = GameObject.FindGameObjectWithTag ("Player");
						if (playerGo != null) {
								player = playerGo.transform;
								amagats = new List<GameObject> ();
								anterior = player.position;	
								firstMovement = player.transform.position;
								transform.position = posCamera;
								transform.LookAt (player.position);
				
				
								relCameraPos = transform.position - player.position;
								relCameraPosMag = relCameraPos.magnitude; //- 0.5f;
						}
				}
		}
	
		void Update ()
		{
				updatePlayerGo ();
				if (playerGo == null) {
						return;
				}
				if (Gameflow.getPhase () == Gameflow.TROLL_FIGHT && timeout > 0) {
						
						timeout -= Time.deltaTime;
						if (!posSetejada) {
								posSetejada = true;
								Transform teranyina = GameObject.FindGameObjectWithTag ("Door").transform;
								posCamera [0] = teranyina.position.x+25;
								posCamera [1] = teranyina.position.y-10;
								posCamera [2] = teranyina.position.z - maxZ;
								zInit = teranyina.position.z;
								transform.LookAt (teranyina.position);
								transform.position = posCamera;
						}
						
						zInit += 1f;
						float z = Mathf.Min (zInit-maxZ,zInit);
						posCamera [2] = z;
						transform.position = posCamera;

			

				} else {
						if (Mathf.Abs (player.transform.position.x - firstMovement [0]) > 2 && Mathf.Abs (player.transform.position.z - firstMovement [2]) > 3) {
								Vector3 standardPos = player.position + relCameraPos;
						
								Vector3 abovePos = player.position + Vector3.up * relCameraPosMag;
		
		
						
								Vector3[] checkPoints = new Vector3[5];
		
		
								checkPoints [0] = standardPos;
		
		
								checkPoints [1] = Vector3.Lerp (standardPos, abovePos, 0.25f);
								checkPoints [2] = Vector3.Lerp (standardPos, abovePos, 0.5f);
								checkPoints [3] = Vector3.Lerp (standardPos, abovePos, 0.75f);
		
		
								checkPoints [4] = abovePos;
		
								if (player.position != anterior) {
										anterior = player.position;
										for (int i = 0; i < checkPoints.Length; i++) {
												ViewingPosCheck (checkPoints [i]);
						
										}
										ViewingPosCheck (transform.position);
										bool enmig;
										for (int j = 0; j < amagats.Count; j++) {
						
												enmig = changeAlpha (amagats [j]);
												if (enmig)
														amagats.Remove (amagats [j]);
								
						
										}
								}
				
		
								distancia = Mathf.Clamp (distancia + -1 * Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed, distanciaMin, distanciaMax);


								posCamera [0] = player.position.x - distancia;
								posCamera [1] = player.position.y + distancia;
								posCamera [2] = player.position.z - distancia;			

								transform.position = posCamera;

								transform.LookAt (player.position);
						}
				}
		}

		void ViewingPosCheck (Vector3 checkPos)
		{
				RaycastHit hit;
				
				Transform pare = null;
				if (Physics.Raycast (checkPos, player.position - checkPos, out hit, relCameraPosMag)) {
						
						
						if (hit.transform != player) {
								GameObject go = null;
								
								List<GameObject> jerarquia = new List<GameObject> ();
								
								if (hit.transform.gameObject.name.Contains ("ID")) {
										pare = hit.transform.parent;
										while (pare.parent != null && !pare.name.Contains("chaflan")) {
												pare = pare.parent;
												
										}
										go = pare.gameObject;
										
								} else {
										go = hit.transform.gameObject;
										pare = go.transform;
										
								}
								
								transparentar (pare.gameObject);
								recorrerArbre (pare);
					
										
								
					
						}
				
				}
			
		}

		void recorrerArbre (Transform pare)
		{
				if (pare.childCount == 0)
						return;
			
				for (int j = 0; j < pare.childCount; j++) {
						Transform fill = pare.GetChild (j);
						transparentar (fill.gameObject);
						recorrerArbre (fill);
				}
		}

		void transparentar (GameObject obj)
		{
				if (obj.renderer != null) {
						Vector4 color = obj.renderer.material.color;
		
						obj.renderer.material.shader = Shader.Find ("Transparent/Diffuse");
		
						color [3] = 0.5f;
		
						obj.renderer.material.color = color;
						
						if (!amagats.Contains (obj))
								amagats.Add (obj);
				}
		}

		bool changeAlpha (GameObject obj)
		{
				if (obj != null) {
						Vector3 posObj = obj.transform.position;

						 
						if ((transform.position.x - posObj [0]) > marge || (posObj [0] - player.transform.position.x) > marge && (transform.position.z - posObj [2]) > marge || (posObj [2] - player.transform.position.z) > marge) {
								//no transparent
								obj.renderer.material.shader = Shader.Find ("Diffuse");
						
								return true;

						} else {//transparent
						
								return false;
						}
				} else {
						return true;
				}
		}

}

