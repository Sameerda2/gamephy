bool InWater { get; set; }
void ClearState () { … InWater = false; } 

//For Ignoring Triger Colliders

void OnTriggerEnter (Collider other) {
		if ((waterMask & (1 << other.gameObject.layer)) != 0) {
			InWater = true;
		}
	}

	void OnTriggerStay (Collider other) {
		if ((waterMask & (1 << other.gameObject.layer)) != 0) {
			InWater = true;
		}
	}

///Changing the Submergence Range

	void OnTriggerEnter (Collider other) {
		if ((waterMask & (1 << other.gameObject.layer)) != 0) {
			EvaluateSubmergence();
		}
	}

	void OnTriggerStay (Collider other) {
		if ((waterMask & (1 << other.gameObject.layer)) != 0) {
			EvaluateSubmergence();
		}
	}

	void EvaluateSubmergence () {
		submergence = 1f;

void EvaluateSubmergence () {
		if (Physics.Raycast(
			body.position + upAxis * submergenceOffset,
			-upAxis, out RaycastHit hit, submergenceRange + 1f,
			waterMask, QueryTriggerInteraction.Collide
		)) {
			submergence = 1f - hit.distance / submergenceRange;
		}
		else {
			submergence = 1f;
		}
	}

//Configure Water Drag

	[SerializeField, Range(0f, 10f)]
	float waterDrag = 1f;

void FixedUpdate () {
		Vector3 gravity = CustomGravity.GetGravity(body.position, out upAxis);
		UpdateState();

		if (InWater) {
			velocity *= 1f - waterDrag * Time.deltaTime;
		}

		AdjustVelocity();

		…
	}
//Configure Bouyancy

	if (Climbing) {
			velocity -=
				contactNormal * (maxClimbAcceleration * 0.9f * Time.deltaTime);
		}
		else if (InWater) {
			velocity +=
				gravity * ((1f - buoyancy * submergence) * Time.deltaTime);
		}
		else if (OnGround && velocity.sqrMagnitude < 0.01f) { … }
	}
