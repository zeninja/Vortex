using UnityEngine;
using System.Collections;

/// <summary>
/// TEMPORARRRYYYYYYYYYYYYYYYYYYYYYYYYYY
/// </summary>
public class GraphicsManager : MonoBehaviour {

	PlayerManager playerManager;

	[System.NonSerialized]
	public Vector3[] corners = new Vector3[3];
	
	Mesh mesh;

	// Use this for initialization
	void Start () {
		playerManager = transform.root.GetComponent<PlayerManager>();
		mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;
		SetupMesh();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMesh();
	}
	
//	void Trigger() {
//		
//	}

	void SetupMesh() {
		for (int i = 0; i < 3; i++) {
			corners[i] = playerManager.characters[i].transform.position;
		}
		
		// Setting vertices based on the characters' positions
		Vector3[] vertices = new Vector3[3];
		
		for(int i = 0; i < 3; i++) {
			vertices[i] = corners[i];				
		}
		
		mesh.vertices = vertices;
		
		// Creating the triangle
		int[] tri = new int[3];
		
		tri[0] = 0;
		tri[1] = 2;
		tri[2] = 1;
		
		mesh.triangles = tri;
		
		Vector3[] normals = new Vector3[3];
		
		normals[0] = -Vector3.forward;
		normals[1] = -Vector3.forward;
		normals[2] = -Vector3.forward;
		
		mesh.normals = normals;
		
		Vector2[] uv = new Vector2[3];
		
		uv[0] = new Vector2(0, 0);
		uv[1] = new Vector2(1, 0);
		uv[2] = new Vector2(0, 1);
		
		mesh.uv = uv;
	}

	void UpdateMesh() {
		// We only need to update the vertices because they are the only values that change
		for (int i = 0; i < 3; i++) {
			corners[i] = playerManager.characters[i].transform.position;
		}
		
		Vector3[] vertices = new Vector3[3];
		
		for(int i = 0; i < 3; i++) {
			vertices[i] = corners[i];				
		}
		
		mesh.vertices = vertices;
	}
	
	void Trigger() {
		GetComponent<SpriteRenderer>().enabled = true;
		iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "time", .5f, "onupdate", "UpdateAlpha"));
	}
	
	void UpdateAlpha(float newAlpha) {
		GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, newAlpha);
	}
}
