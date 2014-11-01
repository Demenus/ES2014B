﻿using UnityEngine;
using System.Collections;

public abstract class AbstractEntity : MonoBehaviour {
	protected int STR; 
	protected int DEX; 
	protected int CON;
	protected int INT;
	protected int HP;
	protected int MP;
	protected int FOR;
	protected int REF;
	protected int ARM;
	protected int DMG;
	
	public bool isAlive(){
		return (this.HP >0);
	}
	
	public void setSTR(int valor){
		this.STR = valor;
	}
	
	public void setDEX(int valor){
		this.DEX = valor;
	}
	
	public void setCON(int valor){
		this.CON = valor;
	}
	
	public void setINT(int valor){
		this.INT = valor;
	}
	
	public void setHP(int valor){
		this.HP = valor;
	}
	
	public void setMP(int valor){
		this.MP = valor;
	}
	
	public void setFOR(int valor){
		this.FOR = valor;
	}
	
	public void setREF(int valor){
		this.REF = valor;
	}
	
	public void setARM(int valor){
		this.ARM = valor;
	}
	public void setDMG(int valor){
		this.DMG = valor;
	}
	
	public int getSTR(){
		return this.STR;
	}
	
	public int getDEX(){
		return this.DEX;
	}
	
	public int getCON(){
		return this.CON;
	}
	
	public int getINT(){
		return this.INT;
	}
	
	public int getHP(){
		return this.HP;
	}
	
	public int getMP(){
		return this.MP;
	}
	public int getFOR(){
		return this.FOR;
	}
	
	public int getREF(){
		return this.REF;
	}
	
	public int getARM(){
		return this.ARM;
	}
	
	public int getDMG(){
		return this.DMG;
	}

	public abstract void onAttackReceived (int baseDMG);

}
