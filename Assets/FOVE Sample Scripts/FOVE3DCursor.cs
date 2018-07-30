using UnityEngine;
using System.Collections;

public class FOVE3DCursor : MonoBehaviour
{
	public enum LeftOrRight
	{
		Left,
		Right
	}

	[SerializeField]
	public LeftOrRight whichEye;
	public FoveInterfaceBase foveInterface;
    private int frame = 0;
	// Use this for initialization
	void Start () {
	}

	// Latepdate ensures that the object doesn't lag behind the user's head motion
	void Update() {
		FoveInterfaceBase.EyeRays rays = foveInterface.GetGazeRays();

		Ray r = whichEye == LeftOrRight.Left ? rays.left : rays.right;

		RaycastHit hit;
		Physics.Raycast(r, out hit, Mathf.Infinity);
        if (hit.point != Vector3.zero) // Vector3 is non-nullable; comparing to null is always false
        {
            transform.position = hit.point;
            if (frame % 2 == 0)
            {
                if(hit.collider.tag != "cubes")
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.localScale = new Vector3(0.2f, 0.2f, 0.01f);
                    cube.transform.position = hit.point;
                    cube.GetComponent<Renderer>().material.color = Color.white;
                    cube.GetComponent<Collider>().tag = "cubes";
                }
                else
                {
                    Color c = hit.collider.GetComponent<Renderer>().material.color;
                    hit.collider.GetComponent<Renderer>().material.color = new Color(c.r - 0.08f,c.g - 0.08f,c.b -0.08f);
                }
            }
		}
		else
		{
			transform.position = r.GetPoint(3.0f);
		}
        frame++;
	}
}
