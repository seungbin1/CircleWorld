using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class RoundPlatformer_Component : MonoBehaviour {
	private float positiony;

	[Range(1, 100)]
	[Tooltip("Position of the platform from the center")]
	public float Position;
	private int a = 0;

	[Range(-360, 360)]
	[Tooltip("Rotation of the platform")]
	public float Rotation;


	[Range(1, 365)]
	[Tooltip("Width of the platform")]
	public float Width;


	[Tooltip("Control the Z position of the visual render of the platform. Tweak this value if you want to have your platform in front or behind another element.")]
	[Range(-10, 10)]
	public float Z_Position;


	[Space] [Space]
	[Range(1,100)]
	[Tooltip("")]
	public int Segments;
	[Tooltip("If true, the script will automaticly calculate the number of segments so the texture will tile correctly")]
	public bool AutoSegments = true;



	private LineRenderer Linerender;
	private EdgeCollider2D EdgeCol2D;

	public void Teleport()
	{
		Rotation += 180;
		CreatePoints(Width, Rotation);
		EdgeCollider_Generate(Width, Rotation);
	}
	private void OnEnable() 
	{
		Initialize();
	}

	void Update()
	{
		CreatePoints(Width, Rotation);
		EdgeCollider_Generate(Width, Rotation);

		if (Time.timeScale == 1)
		{
			if (GameManager.Instance.playerY > 6.25f)
			{
				if (Input.touchCount > 0)
				{
					positiony = GameManager.Instance.playerY;
					Touch touch = Input.GetTouch(0);
					if (touch.position.x > Screen.width / 2 && GameManager.Instance.right)
					{
						Rotation -= (45 - positiony) * Time.deltaTime;
					}
					if (touch.position.x < Screen.width / 2&& GameManager.Instance.left)
					{
						Rotation += (45 - positiony) * Time.deltaTime;
					}
				}
			}
		}
	}

	private void Initialize(){
		Linerender = GetComponent<LineRenderer>();
		EdgeCol2D = GetComponent<EdgeCollider2D>();

		Linerender.positionCount = Segments + 1;

		CreatePoints(Width, Rotation);
	}


	public void OnValidate() {

		if(Linerender == null){Linerender = GetComponent<LineRenderer>();}
		if(EdgeCol2D == null){EdgeCol2D = GetComponent<EdgeCollider2D>();}
		if(Width > 365){ Width = 365;}

		if(Linerender != null){
			if(AutoSegments == true){
				RespectTextureRatio();
			}
			CreatePoints(Width, Rotation);
			Linerender.positionCount = Segments + 1;
			EdgeCollider_Generate(Width, Rotation);
		}
	}


	private const float AngleToSegments = 2.91F; 
	private const float RadiusToSegments = 6F; 

	public void RespectTextureRatio(){
		Segments = Mathf.RoundToInt(Width / AngleToSegments);
		Segments = Mathf.RoundToInt(((Width / RadiusToSegments)/10) * Position);
	}



	 public void CreatePoints(float InputAngle, float Rotation){
		float X;
		float Y;
		float angle = (-(InputAngle / 2)) + Rotation;

		for (int i = 0; i < (Segments + 1); i++){
			X = Mathf.Sin (Mathf.Deg2Rad * angle) * Position;
			Y = Mathf.Cos (Mathf.Deg2Rad * angle) * Position;

			Linerender.SetPosition(i, new Vector3(X, Y, Z_Position)); 

			angle += (InputAngle / Segments);
		}
	}


	private Vector2[] LineCoordPos;

	public void EdgeCollider_Generate(float InputAngle, float rotation){
		LineCoordPos = new Vector2[(Linerender.positionCount)];

		float X;
		float Y;
		float angle = (-(InputAngle / 2 )) + rotation;

		for (int i = 0; i < (Linerender.positionCount); i++){ 
			X = Mathf.Sin(Mathf.Deg2Rad * angle) * Position;
			Y = Mathf.Cos(Mathf.Deg2Rad * angle) * Position;

			LineCoordPos[i] = new Vector2(X, Y);
			angle += (InputAngle/Segments);
		}

		if(EdgeCol2D != null){
			EdgeCol2D.points = LineCoordPos;
		}
	}
}
