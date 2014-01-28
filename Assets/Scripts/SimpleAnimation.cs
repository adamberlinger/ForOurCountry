using UnityEngine;
using System.Collections;
using System;

public class SimpleAnimation<T> {
	protected T startPosition;
	protected T endPosition;
	protected float interpolate;
	protected int frames;

	protected Func<T,T,float,T> interpolator;

	public SimpleAnimation(T start,T end,int frames,
	                 Func<T,T,float,T> interpolator){
		startPosition = start;
		endPosition = end;
		interpolate = 0f;
		this.frames = frames;
		this.interpolator = interpolator;
	}

	public void refresh(T end,int frames = 0){
		startPosition = interpolator(startPosition,endPosition,Mathf.Min(1.0f,interpolate));
		endPosition = end;
		interpolate = 0f;
		if(frames > 0) this.frames = frames;
	}

	public void refresh(T start,T end,int frames = 0){
		startPosition = start;
		endPosition = end;
		interpolate = 0f;
		if(frames > 0) this.frames = frames;
	}

	public bool isActive(){
		return interpolate < 1.0;
	}

	public T getEndPosition(){
		return endPosition;
	}

	public T step(){
		interpolate += 1.0f / frames;
		if(interpolate >= 1.0f){
			return interpolator(startPosition,endPosition,1.0f);
		}

		return interpolator(startPosition,endPosition,interpolate);
	}
}
