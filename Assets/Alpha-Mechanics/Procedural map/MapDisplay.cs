using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour {

	public Renderer textureRender;
	public MeshFilter meshFilter;
	public MeshRenderer meshRenderer;

	public void DrawTexture(Texture2D texture) {
		textureRender.sharedMaterial.mainTexture = texture;
		textureRender.transform.localScale = new Vector3 (texture.width, 1, texture.height);
	}

	public void DrawMesh(MeshData meshData, Texture2D texture){
		meshFilter.sharedMesh = meshData.CreateMesh();
		meshRenderer.sharedMaterial.mainTexture = texture;
		}
	
	public void ConvertToSphere(MeshData meshData, Texture2D texture){
		float thetaDelta = ((2 * Mathf.PI) / (texture.width -1));
		float phiDelta = ((Mathf.PI) / (texture.height - .5f));
		float radiusBase = ((texture.width) / Mathf.PI / 2f);
		for (int i = 0; i < meshData.vertices.Length; i++){
			float theta = (meshData.vertices[i].z * thetaDelta);
			float phi = (meshData.vertices[i].x * phiDelta);
			if (theta > Mathf.PI){theta = theta - Mathf.PI;}
			if (theta < 0.0){theta = theta + Mathf.PI;}
			if (phi > 2*Mathf.PI){phi = phi - (2*Mathf.PI);}
			if (phi < 0.0){phi = phi + (2*Mathf.PI);}
			Vector3 coords2 = new Vector3();
			coords2.x = (float)(((radiusBase) * Mathf.Sin(theta) * Mathf.Cos(phi)) + texture.width / 2f);
			coords2.y = (float)((radiusBase) * Mathf.Sin(theta) * Mathf.Sin(phi));
			coords2.z = (float)(((radiusBase) * Mathf.Cos(theta)) + texture.height / 2f);
			meshData.vertices[i] = coords2;
			DrawMesh(meshData, texture);
		}
	}
}