﻿using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
	
	
	
		private float amplada;
		private float altura;
	
		//atributos de la vida
		public Texture texVida, vidaCover;
		private float vida, alturaVida, vidapercent;

	

		//atributos del mana
		public Texture texMana, manaCover;
		private float mana, alturaMana, manapercent;
		public Texture[] magicTextures = new Texture[3];
		public Texture[] magicTexturesSpelled = new Texture[3];
		public Texture texEscut;
		//textura que saldra cuando el personaje muera
		public Texture gameOverTexture, hud_bg, winTexture;
		private float xPos;
		private float yPos;
		private MainPjMovement pj;
		private Player player;
		private float buttonSizeWidth, buttonSizeHeight;
		public GUISkin myskin;
		public Texture continueTextureSelected, continueTextureNormal, backMainMenuTextureNormal, backMainMenuTextureSelected, 
				audioOFFSelected, audioOFFNormal, audioONSelected, audioONNormal, titolPausa, restartTextureSelected, restartTextureNormal,
				fonsMenuPausa, fonsMenuGameover;
		public Texture bloodTexture;
		private Texture audioTexture, restartTexture;
		private bool sona = true, incrementar = true;
		private AmbientalMusic AmbientAudio;
		private int  magiaEscollida = -1;
		private float timeLeft = 2f;
		private float lifetime, pintadatime;
		private float intervalPintada = 0f, alphaValue, tempsPintada = 0.4f, minAlpha = 0.4f, maxAlpha = 0.75f;
		public float bloodTantPerCentVida = 0.4f;
		private Texture magiaSelect, magiaNormal, continueTexture, backMainMenuTexture;

		public bool win_message_enabled = false;
		public bool endMusicEnabled = false;

		
		void Start ()
		{
				lifetime = intervalPintada;
				pintadatime = tempsPintada;
				alphaValue = minAlpha;
				GameObject go = GameObject.FindGameObjectWithTag ("Player");
				pj = go.GetComponent ("MainPjMovement") as MainPjMovement;
				player = go.GetComponent ("Player") as Player;
		
				audioTexture = audioONNormal;
				AmbientAudio = GameObject.FindObjectOfType (typeof(AmbientalMusic)) as AmbientalMusic;

				string playerSelected = PlayerPrefs.GetString ("player");
				if (playerSelected.Equals ("player1")) {
						magiaNormal = magicTextures [0];
						magiaSelect = magicTexturesSpelled [0];
				} else if (playerSelected.Equals ("player2")) {
						magiaNormal = magicTextures [1];
						magiaSelect = magicTexturesSpelled [1];
				} else {
						magiaNormal = magicTextures [2];
						magiaSelect = magicTexturesSpelled [2];
				}
		
		
		}
	
		void OnGUI ()
		{
				GameObject go = GameObject.FindGameObjectWithTag ("Player");

				if (go != null) {
						pj = go.GetComponent ("MainPjMovement") as MainPjMovement;
						player = go.GetComponent ("Player") as Player;
				}

				if (pj == null) {
						return;
				}

				Time.timeScale = 1;
				amplada = Screen.width / 10;
				altura = Screen.height / 8;
				xPos = Screen.width / 2.7f;
				yPos = Screen.height / 3.2f;
				
				GUI.skin = myskin;

				buttonSizeHeight = Screen.height / 15;
				buttonSizeWidth = Screen.width / 5;
				float maxVida, maxMana;
				
				vida = pj.getHP ();
				mana = pj.getMP ();
				magiaEscollida = pj.getSelectedSpell () + 1;
				maxVida = pj.getMAXHP ();
				maxMana = pj.getMAXMP ();


				
		        
				
				vidapercent = vida / maxVida;
				

				if (vidapercent < 0)
						vidapercent = 0;
				if (vidapercent > 100)
						vidapercent = 100;
		
				alturaVida = vidapercent * altura;
				
				manapercent = mana / maxMana;

				if (manapercent < 0)
						manapercent = 0;
				if (manapercent > 100)
						manapercent = 100;

				alturaMana = manapercent * altura;
				
				float xVida = Screen.width * 0.31f;
				float xBG = Screen.width * 0.25f;
				float yVida = Screen.height - alturaVida;
				float yMana = Screen.height - alturaMana;
				float ampladaBG = Screen.width / 2;
				//float xMana = Screen.width - Screen.width * 2 / 10;
				float xMana = xBG + ampladaBG - amplada - Screen.width * 0.06f;
				float xActual = xVida + amplada;
				float alturaMagia = Screen.height / 15;
				float yMagies = Screen.height - alturaMagia - Screen.height * 0.01f;


				int numTextures = 3;
				

				if (vida < maxVida * bloodTantPerCentVida && pj.isAlive ()) {
						
						//Vector4 alpha = new Vector4 (1, 0, 0, 1 - vida / maxVida);
						Vector4 alpha = new Vector4 (1, 0, 0, alphaValue);
						Color original = GUI.color;
						GUI.color = alpha;
						GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), bloodTexture);
						GUI.color = original;
						pintadatime -= Time.deltaTime;
						
						float timeScale = Time.timeScale;
						Time.timeScale = 1;
						print ("incrementar " + incrementar + ", " + Time.deltaTime);
						if (incrementar) {
								alphaValue += Time.deltaTime * 0.4f;
								if (alphaValue >= maxAlpha) {
										incrementar = false;
								}
						} else {
								alphaValue -= Time.deltaTime * 0.4f;
								if (alphaValue <= minAlpha) {
										incrementar = true;
								}
						}
						Time.timeScale = timeScale;
						
						
						
						
						
						/*if (pintadatime < 0f && incrementar) {
								pintadatime = tempsPintada;
									
								alphaValue += 0.1f;
								if (alphaValue >= maxAlpha) {
										incrementar = false;
								}
						} else if (pintadatime < 0f && !incrementar) {
								alphaValue -= 0.1f;
								if (alphaValue < minAlpha) {
										incrementar = true;
					
								}
						}*/

						

						

				}
				GUI.DrawTexture (new Rect (xBG, Screen.height - Screen.height / 5, ampladaBG, Screen.height / 5), this.hud_bg);
				for (int i = 0; i < numTextures; i++) {
						

						if (i == 0) {//vida

								GUI.BeginGroup (new Rect (xVida, yVida, amplada, Screen.height - yVida));
								GUI.DrawTexture (new Rect (0, alturaVida - altura, altura, altura), this.texVida);
								//GUI.DrawTexture (new Rect (0, alturaVida - altura, amplada, altura), this.vidaCover);
								GUI.EndGroup ();
								GUI.BeginGroup (new Rect (xVida, Screen.height - altura, amplada, Screen.height - altura));
								GUI.DrawTexture (new Rect (0, 0, altura, altura), this.vidaCover);
								GUI.EndGroup ();
						} else if (i == numTextures - 1) {//mana
								//GUI.BeginGroup (new Rect (xMana, yMana, amplada, Screen.height - yMana));

								GUI.BeginGroup (new Rect (xMana, yMana, amplada, Screen.height - yMana));
								GUI.DrawTexture (new Rect (Mathf.Abs (altura - amplada), alturaMana - altura, altura, altura), this.texMana);
								//GUI.DrawTexture (new Rect (0, alturaMana - altura, amplada, altura), this.manaCover);
								GUI.EndGroup ();
								GUI.BeginGroup (new Rect (xMana, Screen.height - altura, amplada, Screen.height - altura));
								GUI.DrawTexture (new Rect (Mathf.Abs (altura - amplada), 0, altura, altura), this.manaCover);
								GUI.EndGroup ();
						} else {//altres
								Texture texturaMagia = magiaEscollida == i ? magiaNormal : magiaSelect;
								GUI.DrawTexture (new Rect (xActual, yMagies, alturaMagia, alturaMagia), texturaMagia);


						}
						
						
						xActual += alturaMagia;
				}
				if (pj.getShield ())//descomentar a devel
						GUI.DrawTexture (new Rect (xMana - alturaMagia - Screen.width * 0.01f, yMagies, alturaMagia, alturaMagia), texEscut);


				
				if (player.canShowMenuPause () && pj.isAlive ()) { 
						Time.timeScale = 0;
						GUI.DrawTexture (new Rect (xPos - Screen.width * 0.12f, yPos - Screen.height * 0.3f, Screen.width * 0.45f, Screen.height * 0.25f), this.titolPausa);
						GUI.DrawTexture (new Rect (xPos - Screen.width * 0.05f, Screen.height * 0.32f, Screen.width * 0.3f, Screen.height * 0.25f), this.fonsMenuPausa);
						Rect pauseRect = new Rect (xPos, yPos, buttonSizeWidth, buttonSizeHeight);

						continueTexture = pauseRect.Contains (Event.current.mousePosition) ? this.continueTextureSelected : this.continueTextureNormal;

						if (GUI.Button (pauseRect, continueTexture)) {
								player.hideMenuPause ();	

				
						}
						Rect audioRect = new Rect (xPos, buttonSizeHeight + yPos, buttonSizeWidth, buttonSizeHeight);
						if (sona) {//pausar audio
								audioTexture = audioRect.Contains (Event.current.mousePosition) ? this.audioOFFSelected : this.audioOFFNormal;
						} else {//reproduir audio
								audioTexture = audioRect.Contains (Event.current.mousePosition) ? this.audioONSelected : this.audioONNormal;
						}
						if (GUI.Button (audioRect, audioTexture)) {
								if (sona) {//pausar audio
										AmbientAudio.PauseAudio ();
								} else {//reproduir audio
										AmbientAudio.UnPauseAudio ();
								}
								sona = !sona;
						}
						Rect restart = new Rect (xPos, 2 * buttonSizeHeight + yPos, buttonSizeWidth, buttonSizeHeight);
						restartTexture = restart.Contains (Event.current.mousePosition) ? this.restartTextureSelected : this.restartTextureNormal;
						if (GUI.Button (restart, restartTexture)) {
								Object.Destroy (GameObject.FindGameObjectWithTag ("Player"));
								Application.LoadLevel (Application.loadedLevel);
				
						}
						Rect returnPause = new Rect (xPos, 3 * buttonSizeHeight + yPos, buttonSizeWidth, buttonSizeHeight);
						backMainMenuTexture = returnPause.Contains (Event.current.mousePosition) ? this.backMainMenuTextureSelected : this.backMainMenuTextureNormal;
						if (GUI.Button (returnPause, backMainMenuTexture)) {
								Object.Destroy (GameObject.FindGameObjectWithTag ("Player"));
								Application.LoadLevel (0);
							
						}
				} 

				if (!pj.isAlive ()) {
						timeLeft -= Time.deltaTime;
						if (timeLeft < 0) {
								AmbientAudio.PlayGameOver ();
								GUI.DrawTexture (new Rect (Screen.width * 0.5f - Screen.width * 0.2f, 0, Screen.width * 0.4f, Screen.height * 0.4f), gameOverTexture);
								GUI.DrawTexture (new Rect (xPos + Screen.width * 0.01f, Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.27f), this.fonsMenuGameover);
								Time.timeScale = 0;
								Rect restart = new Rect (xPos + Screen.width * 0.01f, Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.1f);
								restartTexture = restart.Contains (Event.current.mousePosition) ? this.restartTextureSelected : this.restartTextureNormal;
								if (GUI.Button (restart, restartTexture)) {
										Object.Destroy (GameObject.FindGameObjectWithTag ("Player"));
										Application.LoadLevel (Application.loadedLevel);
					
								}
								Rect returnOver = new Rect (xPos + Screen.width * 0.01f, Screen.height * 0.5f, Screen.width * 0.2f, Screen.height * 0.2f);
								backMainMenuTexture = returnOver.Contains (Event.current.mousePosition) ? this.backMainMenuTextureSelected : this.backMainMenuTextureNormal;


								if (GUI.Button (returnOver, backMainMenuTexture)) {
										Object.Destroy (GameObject.FindGameObjectWithTag ("Player"));
										Application.LoadLevel (0);
				
								}
						}
				}
		
				if (win_message_enabled) {
					if ( !endMusicEnabled ) {
						AmbientAudio.PlayCredits();
						endMusicEnabled = true;
					}

					GUI.DrawTexture (new Rect (Screen.width * 0.5f - Screen.width * 0.2f, 0, Screen.width * 0.4f, Screen.height * 0.4f), winTexture);
					GUI.DrawTexture (new Rect (xPos + Screen.width * 0.01f, Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.27f), this.fonsMenuGameover);
					Time.timeScale = 0;
					Rect restart = new Rect (xPos + Screen.width * 0.01f, Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.1f);
					restartTexture = restart.Contains (Event.current.mousePosition) ? this.restartTextureSelected : this.restartTextureNormal;
					if (GUI.Button (restart, restartTexture)) {
						Object.Destroy (GameObject.FindGameObjectWithTag ("Player"));
						Application.LoadLevel (Application.loadedLevel);
						
					}
					Rect returnOver = new Rect (xPos + Screen.width * 0.01f, Screen.height * 0.5f, Screen.width * 0.2f, Screen.height * 0.2f);
					backMainMenuTexture = returnOver.Contains (Event.current.mousePosition) ? this.backMainMenuTextureSelected : this.backMainMenuTextureNormal;
					
					
					if (GUI.Button (returnOver, backMainMenuTexture)) {
						Object.Destroy (GameObject.FindGameObjectWithTag ("Player"));
						Application.LoadLevel (0);
						
					}
				}
	
		
		}

	public void show_win_message() {
		win_message_enabled = true;
	}
		
}
