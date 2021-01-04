using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum myTWEEN_VALUE{
	POSTION,
	POSTION_X,
	POSTION_Y,
	POSTION_Z,
	LOCALPOSTION,
	LOCALPOSTION_X,
	LOCALPOSTION_Y,
	LOCALPOSTION_Z,
	LOCALROTATION,
	LOCALROTATION_X,
	LOCALROTATION_Y,
	LOCALROTATION_Z,
	LOCALSCALE,
	LOCALSCALE_X,
	LOCALSCALE_Y,
	LOCALSCALE_Z,
	COLOR_R,
	COLOR_G,
	COLOR_B,
	COLOR_A,
	COLOR_RGB,
	COLOR_RGBA,
}

public enum myTWEEN_OPM{
	NON,
	REPEAT,
	PINGPONG,
	SMOOTHSTEP,
	SMOOTHSTEP_PINGPONG,
	SMOOTHDUMP,
	SMOOTHDUMP_PINGPONG,

	SIN,
	COS,
	TAN,
	RANDOM,
}

public enum myTWEEN_OUT{
	OVERRIDE,
	ADD,
	SUB,
	ADDxWEITGH,
	SUBxWEITGH,
}

public enum myTWEEN_FILTER{
	NON,
	MIN,
	MAX,
	MINMAX,
}

public class MyTween : MonoBehaviour {

	[System.Serializable]

	public class TweenItem{
		public bool enabled = true;
		public myTWEEN_VALUE valueType = myTWEEN_VALUE.LOCALSCALE;
		public myTWEEN_OPM opmMode = myTWEEN_OPM.NON;
		public myTWEEN_OUT outMode = myTWEEN_OUT.ADD;
		public float outWeight = 1.0f;

		public float va = 0.0f;
		public float vb = 1.0f;
		public float speed = 1.0f;
		public float vt = 0.0f;

		public float _smoothDumpVelocity = 0.0f;
		public float _smoothDumpMaxSpeed = 1.0f;

		public myTWEEN_FILTER filterMode = myTWEEN_FILTER.NON;
		public float filterMin = 0.0f;
		public float filterMax = 1.0f;
	}

	public TweenItem[] tweenItemList;

	//================内部パラメーター===========
	Vector3 orgPostion;
	Vector3 orgLocalRotationEA;
	Vector3 orgLocalScale;
	Color orgColoer;
	SpriteRenderer sprite;

	bool cameraVisible = false;
	//=========================================
	void Start () {
		sprite = GetComponent<SpriteRenderer> ();
		orgPostion = this.transform.position;
		orgLocalRotationEA = this.transform.localRotation.eulerAngles;
		orgLocalScale = transform.localScale;
		if (sprite != null) {
			orgColoer = GetComponent<SpriteRenderer> ().color;
		}
	}

	void OnBecameVisible(){
		cameraVisible = true;//カメラに写っている
	}

	void OnBecameInvisible(){
		cameraVisible = false;//カメラに写っていない
	}


	void Update () {
		
		float postion_x = orgPostion.x;
		float postion_y = orgPostion.y;
		float postion_z = orgPostion.z;
		float localRotation_x = orgLocalRotationEA.x;
		float localRotation_y = orgLocalRotationEA.y;
		float localRotation_z = orgLocalRotationEA.z;
		float localScale_x = orgLocalScale.x;
		float localScale_y = orgLocalScale.y;
		float localScale_z = orgLocalScale.z;
		float color_r = orgColoer.r;
		float color_g = orgColoer.g;
		float color_b = orgColoer.b;
		float color_a = orgColoer.a;

		if (!cameraVisible || tweenItemList.Length <= 0) {
			return;
		}

		foreach (TweenItem tw in tweenItemList) {
			if (!tw.enabled) {
				continue;
			}

			//tween
			switch(tw.valueType){
			case myTWEEN_VALUE.POSTION_X:
				postion_x = TweenFloat (tw, postion_x, orgPostion.x);
				break;
			case myTWEEN_VALUE.POSTION_Y:
				postion_y = TweenFloat (tw, postion_y, orgPostion.y);
				break;
			case myTWEEN_VALUE.POSTION_Z:
				postion_z = TweenFloat (tw, postion_z, orgPostion.z);
				break;
			case myTWEEN_VALUE.POSTION:
				postion_x = TweenFloat (tw, postion_x, orgPostion.x);
				postion_y = TweenFloat (tw, postion_y, orgPostion.y);
				postion_z = TweenFloat (tw, postion_z, orgPostion.z);
				break;

			case myTWEEN_VALUE.LOCALROTATION_X:
				localRotation_x = TweenFloat (tw, localRotation_x, localRotation_x);
				break;
			case myTWEEN_VALUE.LOCALROTATION_Y:
				localRotation_y = TweenFloat (tw, localRotation_y, localRotation_y);
				break;
			case myTWEEN_VALUE.LOCALROTATION_Z:
				localRotation_z = TweenFloat (tw, localRotation_z, localRotation_z);
				break;
			case myTWEEN_VALUE.LOCALROTATION:
				localRotation_x = TweenFloat (tw, localRotation_x, localRotation_x);
				localRotation_y = TweenFloat (tw, localRotation_y, localRotation_y);
				localRotation_z = TweenFloat (tw, localRotation_z, localRotation_z);
				break;

			case myTWEEN_VALUE.LOCALSCALE_X:
				localScale_x += TweenFloat (tw, localScale_x, orgLocalScale.x);
				break;
			case myTWEEN_VALUE.LOCALSCALE_Y:
				localScale_y += TweenFloat (tw, localScale_y, orgLocalScale.y);
				break;
			case myTWEEN_VALUE.LOCALSCALE_Z:
				localScale_z += TweenFloat (tw, localScale_z, orgLocalScale.z);
				break;
			case myTWEEN_VALUE.LOCALSCALE:
				localScale_x = TweenFloat (tw, localScale_x, orgLocalScale.x);
				localScale_y = TweenFloat (tw, localScale_y, orgLocalScale.y);
				localScale_z = TweenFloat (tw, localScale_z, orgLocalScale.z);
				break;

			case myTWEEN_VALUE.COLOR_R:
				color_r = TweenFloat (tw, color_r, orgColoer.r);
				break;
			case myTWEEN_VALUE.COLOR_G:
				color_g = TweenFloat (tw, color_g, orgColoer.g);
				break;
			case myTWEEN_VALUE.COLOR_B:
				color_b = TweenFloat (tw, color_b, orgColoer.b);
				break;
			case myTWEEN_VALUE.COLOR_A:
				color_a = TweenFloat (tw, color_a, orgColoer.a);
				break;
			case myTWEEN_VALUE.COLOR_RGB:
				color_r = TweenFloat (tw, color_r, orgColoer.r);
				color_g = TweenFloat (tw, color_g, orgColoer.g);
				color_b = TweenFloat (tw, color_b, orgColoer.b);
				break;
			case myTWEEN_VALUE.COLOR_RGBA:
				color_r = TweenFloat (tw, color_r, orgColoer.r);
				color_g = TweenFloat (tw, color_g, orgColoer.g);
				color_b = TweenFloat (tw, color_b, orgColoer.b);
				color_a = TweenFloat (tw, color_a, orgColoer.a);
				break;
			}

			transform.position = new Vector3 (postion_x, postion_y, postion_z);
			transform.localRotation = Quaternion.Euler (localRotation_x, localRotation_y, localRotation_z);
			transform.localScale = new Vector3 (localScale_x, localScale_y, localScale_z);
			if (sprite != null) {
				sprite.color = new Color (color_r, color_g, color_b, color_a);
			}
		}
	}

	float TweenFloat(TweenItem tw,float nowN,float orgN){
		float n = tw.va;
		float v = tw.vb - tw.va;
		float t = Time.time * tw.speed + tw.vt;

		//Tween
		switch(tw.opmMode){
		case myTWEEN_OPM.REPEAT:
			n = tw.va + Mathf.Repeat (t, v);
			break;
		case myTWEEN_OPM.PINGPONG:
			n = tw.va + Mathf.PingPong (t, v);
			break;
		case myTWEEN_OPM.SMOOTHSTEP:
			n = tw.va + Mathf.SmoothStep (tw.va, tw.vb, Mathf.Repeat (t, 1.0f));
			break;
		case myTWEEN_OPM.SMOOTHSTEP_PINGPONG:
			n = tw.va + Mathf.SmoothStep (tw.va, tw.vb, Mathf.PingPong (t, 1.0f));
			break;
		case myTWEEN_OPM.SMOOTHDUMP:
			n = tw.va + Mathf.SmoothDamp (tw.va, tw.vb, ref tw._smoothDumpVelocity, tw._smoothDumpMaxSpeed, Mathf.Repeat (t, 1.0f));
			break;
		case myTWEEN_OPM.SMOOTHDUMP_PINGPONG:
			n = tw.va + Mathf.SmoothDamp (tw.va, tw.vb, ref tw._smoothDumpVelocity, tw._smoothDumpMaxSpeed, Mathf.PingPong (t, 1.0f));
			break;
		case myTWEEN_OPM.SIN:
			n = tw.va + (v / 2.0f) * Mathf.Sin (t) + (v / 2.0f);
			break;
		case myTWEEN_OPM.COS:
			n = tw.va + (v / 2.0f) * Mathf.Cos (t) + (v / 2.0f);
			break;
		case myTWEEN_OPM.TAN:
			n = tw.va + (v / 2.0f) * Mathf.Tan (t) + (v / 2.0f);
			break;
		case myTWEEN_OPM.RANDOM:
			n = Random.Range (tw.va, tw.vb);
			break;
		}

		//Out
		switch(tw.outMode){
		case myTWEEN_OUT.OVERRIDE:
			nowN = orgN;
			break;
		/*case myTWEEN_OUT.ADD:
			n = n;
			break;*/
		case myTWEEN_OUT.SUB:
			n = -n;
			break;
		case myTWEEN_OUT.ADDxWEITGH:
			n = n * tw.outWeight;
			break;
		case myTWEEN_OUT.SUBxWEITGH:
			n = -n * tw.outWeight;
			break;
		}

		//Filter
		switch(tw.filterMode){
		case myTWEEN_FILTER.MIN:
			n = Mathf.Min (n, tw.filterMin);
			break;
		case myTWEEN_FILTER.MAX:
			n = Mathf.Max (n, tw.filterMax);
			break;
		case myTWEEN_FILTER.MINMAX:
			n = Mathf.Clamp (n, tw.filterMin, tw.filterMax);
			break;
		}
		return nowN + n;
	}
}