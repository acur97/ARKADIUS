using System.Collections;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class TalkSytem : MonoBehaviour
{
    public static TalkSytem Instance;

    [Header("Resolutions")]
    public int Res1 = 1280;
    private float Res2_2 = 960;
    public int Res3 = 720;
    [Space]
    public int ResEstirableX = 1920;
    public int ResEstirableY = 1280;
    [SerializeField] private float triangleSize = 640;

    [Header("Configs")]
    [SerializeField] private Transform root;
    [SerializeField] private bool estirable = true;
    [SerializeField] private Canvas canva;
    private RectTransform canvaTransform;
    [SerializeField] private TextMeshProUGUI txt;
    private string _texto;
    //private bool readyText = false;

    [Space]
    [SerializeField] private Transform LrenderFollow;
    [SerializeField] private Transform LrenderFollowTrue;
    private Transform LrenderFollowPadre;
    private Vector2 LrenderFollowLocal;
    private Vector3[] Lrender1Pos = new Vector3[3];
    private Vector3[] Lrender2Pos = new Vector3[6];

    [Header("Line")]
    [SerializeField] private LineRenderer Lrender1;
    [SerializeField] private LineRenderer Lrender2;

    [Header("Triangle")]
    [SerializeField] private MeshFilter triangle;
    [SerializeField] private MeshFilter triangle2;
    private Vector3[] triangleVertx = new Vector3[4];

    [Header("Quad")]
    [SerializeField] private MeshFilter plane;
    [SerializeField] private MeshFilter plane2;
    private Vector3[] planeVertx = new Vector3[4];

    private WaitForFixedUpdate wait;

    private void Awake()
    {
        Instance = this;

        LrenderFollowPadre = LrenderFollowTrue.parent;
        if (Application.isPlaying)
        {
            root.gameObject.SetActive(false);
        }
        else
        {
            root.gameObject.SetActive(true);
        }

        canvaTransform = canva.GetComponent<RectTransform>();

        ResetAll();

        wait = new WaitForFixedUpdate();
    }

    private void ResetAll()
    {
        ResetPlaneMesh();

        ResetTriangleMesh();

        ResetLrender1();
        ResetLrender2();
    }

    private void UpdateAll()
    {
        Res2_2 = (Res1 / 4f) * 3;
        SetPlaneMeshPositions();

        SetTriangleMeshPositions();

        SetLine1Positions();
        SetLine2Positions();

        canvaTransform.sizeDelta = new Vector2(Res1, Res3);
    }

    private void ResetLrender1()
    {
        ResetLrender1Pos();

        Lrender1.SetPositions(Lrender1Pos);
    }
    private void ResetLrender1Pos()
    {
        Lrender1Pos[0] = new Vector2(-triangleSize, Res3);
        Lrender1Pos[1] = new Vector2(0, Res1);
        Lrender1Pos[2] = new Vector2(triangleSize, Res3);
    }

    private void ResetLrender2()
    {
        ResetLrender2Pos();

        Lrender1.SetPositions(Lrender2Pos);
    }
    private void ResetLrender2Pos()
    {
        Lrender2Pos[0] = new Vector2(-triangleSize, Res3);
        Lrender2Pos[1] = new Vector2(-Res1, Res3);
        Lrender2Pos[2] = new Vector2(-Res1, -Res3);
        Lrender2Pos[3] = new Vector2(Res1, -Res3);
        Lrender2Pos[4] = new Vector2(Res1, Res3);
        Lrender2Pos[5] = new Vector2(triangleSize, Res3);
    }

    private void ResetPlaneMesh()
    {
        ResetPlaneMeshPos();

        plane.sharedMesh.vertices = planeVertx;
        plane2.sharedMesh.vertices = planeVertx;
    }
    private void ResetPlaneMeshPos()
    {
        planeVertx[0] = new Vector3(-Res1, Res3, 0);
        planeVertx[1] = new Vector3(-Res1, -Res3, 0);
        planeVertx[2] = new Vector3(Res1, Res3, 0);
        planeVertx[3] = new Vector3(Res1, -Res3, 0);

        plane.sharedMesh.vertices = planeVertx;
        plane2.sharedMesh.vertices = planeVertx;
    }

    private void ResetTriangleMesh()
    {
        ResetTriangleMeshPos();

        triangle.sharedMesh.vertices = triangleVertx;
        triangle2.sharedMesh.vertices = triangleVertx;
    }
    private void ResetTriangleMeshPos()
    {
        triangleVertx[0] = new Vector3(triangleSize, 0, 0); //punta
        triangleVertx[1] = new Vector3(0, 0, 0); //mitad abajo
        triangleVertx[2] = new Vector3(0, 0, -triangleSize); //izquierda
        triangleVertx[3] = new Vector3(0, 0, triangleSize); //derecha
    }

    private void Update()
    {
        Res2_2 = (Res1 / 4f) * 3;
        if (estirable)
        {
            SetPlaneMeshPositions();

            SetTriangleMeshPositions();

            SetLine1Positions();
            SetLine2Positions();

            canvaTransform.sizeDelta = new Vector2(Res1, Res3);
        }
    }

    private void SetPlaneMeshPositions()
    {
        ResetPlaneMeshPos();

        planeVertx[0] = new Vector3(triangleSize, 0, 0);
        planeVertx[1] = new Vector3(0, 0, 0);
        planeVertx[2] = new Vector3(0, 0, -triangleSize);
        planeVertx[3] = new Vector3(0, 0, triangleSize);
    }

    private void SetTriangleMeshPositions()
    {
        ResetTriangleMeshPos();
        LrenderFollowLocal = LrenderFollow.localPosition;
        triangleVertx[0] = new Vector3(LrenderFollowLocal.y, 0, LrenderFollowLocal.x); //punta
        triangleVertx[1] = new Vector3(Mathf.Clamp(LrenderFollowLocal.y, -Res3, Res3), 0, Mathf.Clamp(LrenderFollowLocal.x, -Res1, Res1)); //mitad abajo

        if (LrenderFollowLocal.x >= 0 && LrenderFollowLocal.y >= 0 || LrenderFollowLocal.x <= 0 && LrenderFollowLocal.y <= 0)
        {
            triangleVertx[2] = new Vector3(Mathf.Clamp(LrenderFollowLocal.y, -Res3, Res3), 0, Mathf.Clamp(LrenderFollowLocal.x - triangleSize, -Res1, Res2_2)); //izquierda
            triangleVertx[3] = new Vector3(Mathf.Clamp(LrenderFollowLocal.y, -Res3, Res3), 0, Mathf.Clamp(LrenderFollowLocal.x + triangleSize, -Res2_2, Res1)); //derecha

            if (LrenderFollowLocal.x > Res1)
            {
                triangleVertx[3] = new Vector3(Mathf.Clamp(LrenderFollowLocal.y - triangleSize, -Res3, Res3), 0, Mathf.Clamp(LrenderFollowLocal.x + triangleSize, 0, Res1)); //derecha
                if (LrenderFollowLocal.y < Res3 && LrenderFollowLocal.y > -Res3)
                {
                    triangleVertx[2] = new Vector3(Mathf.Clamp(LrenderFollowLocal.y + triangleSize, -Res3, Res3), 0, Mathf.Clamp(LrenderFollowLocal.x, 0, Res1)); //izquierda
                }
            }
            else if (LrenderFollowLocal.x < -Res1)
            {
                triangleVertx[2] = new Vector3(Mathf.Clamp(LrenderFollowLocal.y + triangleSize, -Res3, Res3), 0, Mathf.Clamp(LrenderFollowLocal.x - triangleSize, -Res1, 0)); //izquierda
                if (LrenderFollowLocal.y < Res3 && LrenderFollowLocal.y > -Res3)
                {
                    triangleVertx[3] = new Vector3(Mathf.Clamp(LrenderFollowLocal.y - triangleSize, -Res3, Res3), 0, Mathf.Clamp(LrenderFollowLocal.x, -Res1, 0)); //derecha
                }
            }
        }
        else if (LrenderFollowLocal.x > 0 && LrenderFollowLocal.y < 0 || LrenderFollowLocal.x < 0 && LrenderFollowLocal.y > 0)
        {
            triangleVertx[2] = new Vector3(Mathf.Clamp(LrenderFollowLocal.y, -Res3, Res3), 0, Mathf.Clamp(LrenderFollowLocal.x + triangleSize, -Res2_2, Res1)); //izquierda
            triangleVertx[3] = new Vector3(Mathf.Clamp(LrenderFollowLocal.y, -Res3, Res3), 0, Mathf.Clamp(LrenderFollowLocal.x - triangleSize, -Res1, Res2_2)); //derecha

            if (LrenderFollowLocal.x > Res1)
            {
                if (LrenderFollowLocal.y > 0)
                {
                    triangleVertx[2] = new Vector3(Mathf.Clamp(LrenderFollowLocal.y + triangleSize, -Res3, Res3), 0, Mathf.Clamp(LrenderFollowLocal.x - triangleSize, 0, Res1)); //izquierda
                }
                else
                {
                    triangleVertx[2] = new Vector3(Mathf.Clamp(LrenderFollowLocal.y + triangleSize, -Res3, Res3), 0, Mathf.Clamp(LrenderFollowLocal.x, 0, Res1)); //izquierda
                }
                if (LrenderFollowLocal.y < Res3 && LrenderFollowLocal.y > -Res3)
                {
                    triangleVertx[3] = new Vector3(Mathf.Clamp(LrenderFollowLocal.y - triangleSize, -Res3, Res3), 0, Mathf.Clamp(LrenderFollowLocal.x + triangleSize, 0, Res1)); //derecha
                }
            }
            else if (LrenderFollowLocal.x < -Res1)
            {
                if (LrenderFollowLocal.y > 0)
                {
                    triangleVertx[3] = new Vector3(Mathf.Clamp(LrenderFollowLocal.y - triangleSize, -Res3, Res3), 0, Mathf.Clamp(LrenderFollowLocal.x, -Res1, 0)); //derecha
                }
                else
                {
                    triangleVertx[3] = new Vector3(Mathf.Clamp(LrenderFollowLocal.y - triangleSize, -Res3, Res3), 0, Mathf.Clamp(LrenderFollowLocal.x + triangleSize, -Res1, 0)); //derecha
                }
                if (LrenderFollowLocal.y < Res3 && LrenderFollowLocal.y > -Res3)
                {
                    triangleVertx[2] = new Vector3(Mathf.Clamp(LrenderFollowLocal.y + triangleSize, -Res3, Res3), 0, Mathf.Clamp(LrenderFollowLocal.x - triangleSize, -Res1, 0)); //izquierda
                }
            }
        }

        triangle.sharedMesh.vertices = triangleVertx;
        triangle2.sharedMesh.vertices = triangleVertx;
    }

    private void SetLine1Positions()
    {
        ResetLrender1Pos();
        LrenderFollowLocal = LrenderFollow.localPosition;

        Lrender1Pos[0] = new Vector2(triangleVertx[2].z, triangleVertx[2].x);
        Lrender1Pos[1] = new Vector2(triangleVertx[0].z, triangleVertx[0].x);
        Lrender1Pos[2] = new Vector2(triangleVertx[3].z, triangleVertx[3].x);

        Lrender1.SetPositions(Lrender1Pos);
    }

    private void SetLine2Positions()
    {
        ResetLrender2Pos();
        LrenderFollowLocal = LrenderFollow.localPosition;

        if (LrenderFollowLocal.y >= Res3)
        {
            if (LrenderFollowLocal.x >= Res1)
            {
                Lrender2Pos[4] = new Vector2(triangleVertx[3].z, triangleVertx[3].x);
                Lrender2Pos[0] = new Vector2(triangleVertx[2].z, triangleVertx[2].x);
                Lrender2Pos[5] = new Vector2(triangleVertx[3].z, triangleVertx[3].x);
            }
            else if (LrenderFollowLocal.x <= -Res1)
            {
                Lrender2Pos[1] = new Vector2(triangleVertx[3].z, triangleVertx[3].x);
                Lrender2Pos[0] = new Vector2(triangleVertx[3].z, triangleVertx[3].x);
                Lrender2Pos[5] = new Vector2(triangleVertx[2].z, triangleVertx[2].x);
            }
            else
            {
                if (LrenderFollowLocal.x >= 0)
                {
                    Lrender2Pos[0] = new Vector2(triangleVertx[2].z, triangleVertx[2].x);
                    Lrender2Pos[5] = new Vector2(triangleVertx[3].z, triangleVertx[3].x);
                }
                else
                {
                    Lrender2Pos[0] = new Vector2(triangleVertx[3].z, triangleVertx[3].x);
                    Lrender2Pos[5] = new Vector2(triangleVertx[2].z, triangleVertx[2].x);
                }
            }
        }
        else if (LrenderFollowLocal.y <= -Res3)
        {
            Lrender2Pos[1] = new Vector2(-Res1, -Res3);
            Lrender2Pos[2] = new Vector2(-Res1, Res3);
            Lrender2Pos[3] = new Vector2(Res1, Res3);
            Lrender2Pos[4] = new Vector2(Res1, -Res3);

            if (LrenderFollowLocal.x >= Res1)
            {
                Lrender2Pos[4] = new Vector2(triangleVertx[2].z, triangleVertx[2].x);
                Lrender2Pos[0] = new Vector2(triangleVertx[3].z, triangleVertx[3].x);
                Lrender2Pos[5] = new Vector2(triangleVertx[2].z, triangleVertx[2].x);
            }
            else if (LrenderFollowLocal.x <= -Res1)
            {
                Lrender2Pos[1] = new Vector2(triangleVertx[2].z, triangleVertx[2].x);
                Lrender2Pos[0] = new Vector2(triangleVertx[2].z, triangleVertx[2].x);
                Lrender2Pos[5] = new Vector2(triangleVertx[3].z, triangleVertx[3].x);
            }
            else
            {
                if (LrenderFollowLocal.x >= 0)
                {
                    Lrender2Pos[0] = new Vector2(triangleVertx[3].z, triangleVertx[3].x);
                    Lrender2Pos[5] = new Vector2(triangleVertx[2].z, triangleVertx[2].x);
                }
                else
                {
                    Lrender2Pos[0] = new Vector2(triangleVertx[2].z, triangleVertx[2].x);
                    Lrender2Pos[5] = new Vector2(triangleVertx[3].z, triangleVertx[3].x);
                }
            }
        }
        else
        {
            if (LrenderFollowLocal.x >= Res1)
            {
                Lrender2Pos[1] = new Vector2(Res1, Res3);
                Lrender2Pos[2] = new Vector2(-Res1, Res3);
                Lrender2Pos[3] = new Vector2(-Res1, -Res3);
                Lrender2Pos[4] = new Vector2(Res1, -Res3);

                Lrender2Pos[0] = new Vector2(triangleVertx[2].z, triangleVertx[2].x);
                Lrender2Pos[5] = new Vector2(triangleVertx[3].z, triangleVertx[3].x);
            }
            else if (LrenderFollowLocal.x <= -Res1)
            {
                Lrender2Pos[1] = new Vector2(-Res1, -Res3);
                Lrender2Pos[2] = new Vector2(Res1, -Res3);
                Lrender2Pos[3] = new Vector2(Res1, Res3);
                Lrender2Pos[4] = new Vector2(-Res1, Res3);

                Lrender2Pos[0] = new Vector2(triangleVertx[3].z, triangleVertx[3].x);
                Lrender2Pos[5] = new Vector2(triangleVertx[2].z, triangleVertx[2].x);
            }
        }

        Lrender2.SetPositions(Lrender2Pos);
    }

    public void SetText(string text, Vector3 position, bool estira = false, Transform follow = null)
    {
        root.position = position;
        estirable = estira;
        if (estira)
        {
            LrenderFollowTrue.SetParent(follow);
            LrenderFollowTrue.localPosition = Vector3.zero;
        }
        ResetAll();

        StartCoroutine(PlayText(text));

        root.gameObject.SetActive(true);
    }

    private IEnumerator PlayText(string _text)
    {
        //readyText = false;
        foreach (char c in _text)
        {
            _texto += c;
            txt.SetText(_texto);
            yield return wait;
        }
        //readyText = true;
    }

    public void UnsetText()
    {
        LrenderFollowTrue.SetParent(LrenderFollowPadre);
        LrenderFollowTrue.localPosition = new Vector2(1600, 0);

        StopAllCoroutines();

        root.gameObject.SetActive(false);
    }
}