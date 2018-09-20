using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OpenCVFaceDetection : MonoBehaviour {

	public Camera camera;
	public static List<Vector2> NormalizedFacePositions {get; private set;}
	public static Vector2 CameraResolution;
	private const int DetectionDownScale = 1;
	private bool _ready;
	private int _maxFaceDetectCount = 5;
	private OpenCVWrapper.CvCircle[] _faces;
	private Quaternion baseRotation;
	private WebCamTexture webCamTexture;

	// Use this for initialization
	void Start () {
		int camWidth = 0, camHeight = 0;
		webCamTexture = new WebCamTexture();
		Renderer renderer = GetComponent<Renderer>();
		renderer.material.mainTexture = webCamTexture;
		baseRotation = transform.rotation;
		webCamTexture.Play();
		camWidth = webCamTexture.width;
		camHeight = webCamTexture.height;

		int result = OpenCVWrapper.Init(ref camWidth, ref camHeight);
		if(result < 0)
		{
			if(result == -1)
			{
				Debug.LogWarningFormat("[{0}] Failed to find cascasdes definition.", GetType());
			}
			else if(result == -2)
			{
				Debug.LogWarningFormat("[{0}] Failed to open camera stream.", GetType());
			}
			return;
		}

		CameraResolution = new Vector2(camWidth, camHeight);
		_faces = new OpenCVWrapper.CvCircle[_maxFaceDetectCount];
		NormalizedFacePositions = new List<Vector2>();
		OpenCVWrapper.SetScale(DetectionDownScale);
		_ready = true;
	}

	private void OnApplicationQuit() {
		if(_ready)
		{
			OpenCVWrapper.Close();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!_ready)
		{
			return;
		}

		transform.rotation = baseRotation * Quaternion.AngleAxis(webCamTexture.videoRotationAngle, Vector3.up);

		int detectedFaceCount = 0;
		unsafe{
			fixed (OpenCVWrapper.CvCircle* outFaces = _faces)
			{
				OpenCVWrapper.Detect(outFaces, _maxFaceDetectCount, ref detectedFaceCount);
			}
		}

		NormalizedFacePositions.Clear();
		for(int i = 0; i < detectedFaceCount; i++)
		{
			NormalizedFacePositions.Add(new Vector2((_faces[i].X * DetectionDownScale)/CameraResolution.x, 1f - ((_faces[i].Y * DetectionDownScale)/CameraResolution.y)));
		}
	}
}
