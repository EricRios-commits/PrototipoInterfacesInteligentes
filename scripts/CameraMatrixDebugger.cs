using UnityEngine;

namespace SeminarioMundos
{
    public class CameraMatrixDebugger : MonoBehaviour
    {
        [SerializeField] private Transform redCube;
        [SerializeField] private Transform greenCube;
        [SerializeField] private Transform blueCube;
        [SerializeField] private float scale = 1.0f;

        [SerializeField] private int redVertexIndex = 1;
        [SerializeField] private int greenVertexIndex = 1;
        [SerializeField] private int blueVertexIndex = 1;

        private int _viewportWidth = 1920;
        private int _viewportHeight = 1080;
        private Camera _cam;

        private void Awake()
        {
            _viewportWidth = Screen.width;
            _viewportHeight = Screen.height;
            _cam = GetComponent<Camera>();
        }
        
        private void OnGUI()
        {
            if (_cam == null) { GUI.Label(new Rect(10,10,600,20), "Attach this script to a Camera."); return; }

            Matrix4x4 prevMatrix = GUI.matrix;
            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one * scale) * prevMatrix;

            Matrix4x4 view = _cam.worldToCameraMatrix;
            Matrix4x4 proj = _cam.projectionMatrix;
            float x = 10f;
            float y = 10f;

            GUI.Label(new Rect(x, y, 1000, 20), $"Camera.worldToCameraMatrix (View):");
            GUI.Label(new Rect(x, y + 20, 1000, 80), MatrixToString(view));
            GUI.Label(new Rect(x, y + 100, 1000, 20), $"Camera.projectionMatrix (Projection):");
            GUI.Label(new Rect(x, y + 120, 1000, 80), MatrixToString(proj));

            y += 200f;

            float cubeBlockHeight = 202f; // matches DrawCubeInfo layout height
            DrawCubeInfo("Red", redCube, x, y, view, proj, redVertexIndex);
            y += cubeBlockHeight;
            DrawCubeInfo("Green", greenCube, x, y, view, proj, greenVertexIndex);
            y += cubeBlockHeight;
            DrawCubeInfo("Blue", blueCube, x, y, view, proj,  blueVertexIndex);

            GUI.matrix = prevMatrix;

            GUI.Label(new Rect(10, Screen.height - 32, 70, 20), "GUI scale:");
            scale = GUI.HorizontalSlider(new Rect(90, Screen.height - 28, 200, 20), scale, 0.25f, 2.0f);
        }

        private void DrawCubeInfo(string label, Transform t, float x, float y, Matrix4x4 view, Matrix4x4 proj, int vertexIndex)
        {
            if (t == null) { GUI.Label(new Rect(x, y, 400, 20), $"{label} not assigned."); return; }

            Matrix4x4 model = t.localToWorldMatrix;
            GUI.Label(new Rect(x, y, 1000, 20), $"{label} Model (localToWorld):");
            GUI.Label(new Rect(x, y+18, 1000, 60), MatrixToString(model));

            Vector3? meshV = GetMeshVertexLocal(t, vertexIndex);
            if (!meshV.HasValue) { return; }
            Vector3 chosenLocal3 = meshV.Value;
            Vector4 vLocal = new Vector4(chosenLocal3.x, chosenLocal3.y, chosenLocal3.z, 1.0f);
            Vector4 vWorld = model * vLocal;
            Vector4 vCamera = view * vWorld;
            Vector4 vClip = proj * vCamera;

            Vector4 vNDC = new Vector4(float.NaN, float.NaN, float.NaN, float.NaN);
            Vector2 vViewport = new Vector2(float.NaN, float.NaN);
            string behindMsg = string.Empty;
            if (Mathf.Approximately(vClip.w, 0f))
            {
                behindMsg = "(w ≈ 0, cannot divide)";
            }
            else
            {
                vNDC = vClip / vClip.w;
                if (vClip.w < 0f) behindMsg = "(point is behind the camera)";
                vViewport = new Vector2((vNDC.x * 0.5f + 0.5f) * _viewportWidth, (vNDC.y * 0.5f + 0.5f) * _viewportHeight);
            }

            string vertexInfoLabel = "Vertex at index " + vertexIndex;
            GUI.Label(new Rect(x, y+82, 800, 120),
                $"using: {vertexInfoLabel}\n" +
                $"v_local = {vLocal}\n" +
                $"v_world = {vWorld}\n" +
                $"v_camera(view space) = {vCamera}\n" +
                $"v_clip = {vClip} {behindMsg}\n" +
                $"v_ndc = {vNDC}\n" +
                $"v_viewport(px) = {vViewport}\n"
            );
        }

        private Vector3? GetMeshVertexLocal(Transform t, int index)
        {
            MeshFilter mf = t.GetComponent<MeshFilter>();
            if (mf == null) return null;
            Mesh mesh = mf.sharedMesh;
            if (mesh == null) return null;
            Vector3[] verts = mesh.vertices;
            if (verts == null || verts.Length == 0) return null;

            if (index >= 0 && index < verts.Length)
            {
                return verts[index];
            }

            return null;
        }

        private string MatrixToString(Matrix4x4 m)
        {
            return string.Format(
                "{0,6:0.000} {1,6:0.000} {2,6:0.000} {3,6:0.000}\n{4,6:0.000} {5,6:0.000} {6,6:0.000} {7,6:0.000}\n{8,6:0.000} {9,6:0.000} {10,6:0.000} {11,6:0.000}\n{12,6:0.000} {13,6:0.000} {14,6:0.000} {15,6:0.000}",
                m.m00,m.m01,m.m02,m.m03,
                m.m10,m.m11,m.m12,m.m13,
                m.m20,m.m21,m.m22,m.m23,
                m.m30,m.m31,m.m32,m.m33
            );
        }

        void OnDrawGizmos()
        {
            if (redCube)
            {
                Mesh mesh = redCube.GetComponent<MeshFilter>().sharedMesh;
                if (mesh && redVertexIndex >= 0 && redVertexIndex < mesh.vertexCount)
                {
                    Vector3 vLocal = mesh.vertices[redVertexIndex];
                    Vector3 vWorld = redCube.localToWorldMatrix.MultiplyPoint3x4(vLocal);
                    Gizmos.color = Color.mediumAquamarine;
                    Gizmos.DrawSphere(vWorld, 0.1f);
                }
            }
        }
    }
}