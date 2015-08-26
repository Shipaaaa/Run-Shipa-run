using UnityEngine;
using System.Collections;

public class JControlledCar : JCar {
	
	// automatic, if true car shifts automatically up/down
	public bool automatic = true;

	public float shiftDownRPM = 1500.0f; // rpm script will shift gear down
	public float shiftUpRPM = 2700.0f; // rpm script will shift gear up
	
	public GUITexture xButton;
	public GUITexture yButton;
	public GUITexture lButton;
	public GUITexture rButton;
	public GUITexture gButton;
	void Update() {
		if (Input.GetKey(KeyCode.Escape)){
		Application.Quit();
		}
		/*if (Input.GetKeyDown("page up")) {
			ShiftUp();
		}
		if (Input.GetKeyDown("page down")) {
			ShiftDown();
		}
		if (Input.GetKeyDown("g")) {
			automatic = !automatic;
		}
		if (Input.GetKeyDown("t")) {
			switch (wheelDrive) {
				case JWheelType.Front : wheelDrive = JWheelType.All; break;
				case JWheelType.Back : wheelDrive = JWheelType.Front; break;
				case JWheelType.All : wheelDrive = JWheelType.Back; break;
			}
			foreach (WheelData w in wheels) {
				WheelCollider col = w.col;
				col.motorTorque = 0f;
				col.brakeTorque = 0f;
			}
		}*/
		//Gear Button Part	
		if (Input.touchCount == 1) {
	    var f8 = Input.GetTouch(0);
	    if (f8.phase == TouchPhase.Began) {
	        if (gButton.HitTest(f8.position)) {	
	        switch (wheelDrive) {
				case JWheelType.Front : wheelDrive = JWheelType.All; break;
				case JWheelType.Back : wheelDrive = JWheelType.Front; break;
				case JWheelType.All : wheelDrive = JWheelType.Back; break;
			}
			foreach (WheelData w in wheels) {
				WheelCollider col = w.col;
				col.motorTorque = 0f;
				col.brakeTorque = 0f;
			}
	        }
	    }
	    }
	    if (Input.touchCount == 2) {
	    var f9 = Input.GetTouch(1);
	    if (f9.phase == TouchPhase.Began) {
	        if (rButton.HitTest(f9.position)) {	
	        switch (wheelDrive) {
				case JWheelType.Front : wheelDrive = JWheelType.All; break;
				case JWheelType.Back : wheelDrive = JWheelType.Front; break;
				case JWheelType.All : wheelDrive = JWheelType.Back; break;
			}
			foreach (WheelData w in wheels) {
				WheelCollider col = w.col;
				col.motorTorque = 0f;
				col.brakeTorque = 0f;
			}
	        }
	    }
	    }		
     
		
	}

	
	// handle the physics of the engine
	void FixedUpdate () {
		
		float steer = 0; // steering -1.0 .. 1.0
		float accel = 0; // accelerating -1.0 .. 1.0
		bool brake = false; // braking (true is brake)
		
		if ((checkForActive == null) || checkForActive.active) {
			// we only look at input when the object we monitor is
			// active (or we aren't monitoring an object).
			//steer = Input.GetAxis("Horizontal");
			//accel = Input.GetAxis("Vertical");

		//X Button Part
	    if (Input.touchCount == 1) {
	    var f4 = Input.GetTouch(0);
	    if (f4.phase == TouchPhase.Stationary) {
	        if (xButton.HitTest(f4.position)) {	
	        accel--;
	        }
	    }
	    }
	    if (Input.touchCount == 2) {
	    var f5 = Input.GetTouch(1);
	    if (f5.phase == TouchPhase.Stationary) {
	        if (xButton.HitTest(f5.position)) {	
	        accel--;
	        }
	    }
	    }
			
		//Y Button Part	
		if (Input.touchCount == 1) {
	    var f = Input.GetTouch(0);
	    if (f.phase == TouchPhase.Stationary) {
	        if (yButton.HitTest(f.position)) {	
	        accel++;
	        }
	    }
	    }
	    if (Input.touchCount == 2) {
	    var f2 = Input.GetTouch(1);
	    if (f2.phase == TouchPhase.Stationary) {
	        if (yButton.HitTest(f2.position)) {	
	        accel++;
	        }
	    }
	    }
			
		//Steer left Button Part	
		if (Input.touchCount == 1) {
	    var f1 = Input.GetTouch(0);
	    if (f1.phase == TouchPhase.Stationary) {
	        if (lButton.HitTest(f1.position)) {	
	        steer--;
	        }
	    }
	    }
	    if (Input.touchCount == 2) {
	    var f3 = Input.GetTouch(1);
	    if (f3.phase == TouchPhase.Stationary) {
	        if (lButton.HitTest(f3.position)) {	
	        steer--;
	        }
	    }
	    }	
			
		//Steer right Button Part	
		if (Input.touchCount == 1) {
	    var f6 = Input.GetTouch(0);
	    if (f6.phase == TouchPhase.Stationary) {
	        if (rButton.HitTest(f6.position)) {	
	        steer++;
	        }
	    }
	    }
	    if (Input.touchCount == 2) {
	    var f7 = Input.GetTouch(1);
	    if (f7.phase == TouchPhase.Stationary) {
	        if (rButton.HitTest(f7.position)) {	
	        steer++;
	        }
	    }
	    }		
		//brake = Input.GetButton("Jump");
		}
		
		// handle automatic shifting
		if (automatic && (CurrentGear == 1) && (accel < 0.0f)) {
			ShiftDown(); // reverse
		}
		else if (automatic && (CurrentGear == 0) && (accel > 0.0f)) {
			ShiftUp(); // go from reverse to first gear
		}
		else if (automatic && (MotorRPM > shiftUpRPM) && (accel > 0.0f)) {
			ShiftUp(); // shift up
		}
		else if (automatic && (MotorRPM < shiftDownRPM) && (CurrentGear > 1)) {
			ShiftDown(); // shift down
		}
		if (automatic && (CurrentGear == 0)) {
			accel = - accel; // in automatic mode we need to hold arrow down for reverse
		}
		if (brake) {
			accel = -1f;
		}
		
		HandleMotor(steer, accel);
	}
}
