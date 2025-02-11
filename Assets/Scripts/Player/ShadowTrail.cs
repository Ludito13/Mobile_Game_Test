using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Efecto de "Sombra" que hace el player al hacer el Dash
public class ShadowTrail : MonoBehaviour
{
    public float activeTime;
    public float meshRefreshRate;
    public float meshDestroy;
    public Material mat;
    private bool isTrailActive;
    private SkinnedMeshRenderer[] skinnedRenderer;

    private void Start()
    {
        EventManager.Subscribe("Shadow Trail", TrailDash);
    }

    public void TrailDash()
    {
        if (!isTrailActive)
        {
            isTrailActive = true;
            StartCoroutine(ActiveTrail(activeTime));
        }
    }

    IEnumerator ActiveTrail(float timeActive)
    {
        while (timeActive > 0)
        {
            timeActive -= meshRefreshRate;

            if (skinnedRenderer == null)
            {
                skinnedRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
            }

            for (int i = 0; i < skinnedRenderer.Length; i++)
            {
                GameObject objt = new GameObject();
                objt.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
                MeshRenderer mr = objt.AddComponent<MeshRenderer>();
                MeshFilter mf = objt.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();
                skinnedRenderer[i].BakeMesh(mesh);
                mf.mesh = mesh;
                mr.material = mat;
                Destroy(objt, meshDestroy);
            }

            yield return new WaitForSeconds(meshRefreshRate);
        }

        isTrailActive = false;
    }

    private void OnDestroy()
    {
        EventManager.Unsuscribe("Shadow Trail", TrailDash);

    }
}
