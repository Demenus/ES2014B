﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class normalButton : MonoBehaviour {
	
	private static float MAX_COLOR_VAL = 0.5f;
	private float secondsToAppear = 1.0f;
	private float delayToAppear = 1.0f;
	private Color color;
	
	// animation
	private float timeBetweenAnimationS = 0.05f;
	private float timeBetweenAnimationUnS = 0.05f;
	private float timeLeftAnimationChange = 0;
	private bool active;
	private bool animationForward;
	private int animationIndex;
	public List<Texture2D> texturesS;
	public List<Texture2D> texturesUnS;
	public easyButton easyButton;
	public hardButton hardButton;
	private List<Texture2D> currentAnimation;
	
	void Awake(){
		Time.timeScale = 1;
		Rect initPixelInset = new Rect(0,0,1,1);
		initPixelInset.height = Screen.height*0.10f;
		initPixelInset.width = initPixelInset.height*3f;
		initPixelInset.x = Screen.width*0.05f;
		initPixelInset.y = -Screen.height*0.475f;
		guiTexture.pixelInset = initPixelInset;
		color = guiTexture.color;
		color.a = 0;
		guiTexture.color = color;
		
		// animation
		timeLeftAnimationChange = timeBetweenAnimationUnS;
		this.active = true;
		animationIndex=0;
		timeLeftAnimationChange = timeBetweenAnimationS;
		currentAnimation = texturesS;
		easyButton.noActive();
		hardButton.noActive();
		PlayerPrefs.SetString("easy", "n");
		PlayerPrefs.SetString("normal", "y");
		PlayerPrefs.SetString("hard", "n");
		animationForward = true;
		animationIndex = 0;
	}
	
	void Update(){
		delayToAppear = Mathf.Max(0,delayToAppear-Mathf.Abs(Time.deltaTime));
		if(delayToAppear <= 0){
			color = guiTexture.color;
			color.a = Mathf.Min(1,color.a+(MAX_COLOR_VAL/secondsToAppear)*Time.deltaTime);
			guiTexture.color = color;
		}
		
		// animation
		timeLeftAnimationChange = Mathf.Max(0,timeLeftAnimationChange-Mathf.Abs(Time.deltaTime));
		if(timeLeftAnimationChange <= 0){
			if(active){
				timeLeftAnimationChange = timeBetweenAnimationS;
			}else{
				timeLeftAnimationChange = timeBetweenAnimationUnS;
			}
			if(animationForward){
				if(animationIndex < currentAnimation.Count-1){
					animationIndex++;
				}else{
					animationIndex=currentAnimation.Count-2;
					animationForward = false;
				}
			}else{
				if(animationIndex > 0){
					animationIndex--;
				}else{
					animationIndex=1;
					animationForward = true;
				}
			}
			guiTexture.texture = currentAnimation[animationIndex];
		}
	}
	
	public void noActive(){
		active = false;
		animationIndex=0;
		timeLeftAnimationChange = timeBetweenAnimationUnS;
		currentAnimation = texturesUnS;
	}
	
	public void OnMouseUpAsButton(){
		this.active = true;
		animationIndex=0;
		timeLeftAnimationChange = timeBetweenAnimationS;
		currentAnimation = texturesS;
		
		easyButton.noActive();
		hardButton.noActive();
		// easy button NO active
		// hard button NO active
		
		
		PlayerPrefs.SetString("easy", "n");
		PlayerPrefs.SetString("normal", "y");
		PlayerPrefs.SetString("hard", "n");
		// save the easy mode NO active
		// save the normal mode active
		// save the hard mode NO active
	}
}