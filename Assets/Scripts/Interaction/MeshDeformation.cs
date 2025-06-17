using UnityEngine;
using System.Collections.Generic;

namespace Interaction
{
    public class MeshDeformation : MonoBehaviour
    {
        private Mesh originalMesh;
        private Mesh deformedMesh;
        private Vector3[] originalVertices;
        private Vector3[] deformedVertices;
        private int[] originalTriangles;

        // ��� ������������ ��� ����������� ������
        private Dictionary<Vector3, int> vertexIndexMap = new Dictionary<Vector3, int>();

        private void Start()
        {
            MeshFilter filter = GetComponent<MeshFilter>();
            originalMesh = filter.mesh;

            // ������� �������� ����� ����
            deformedMesh = new Mesh();
            deformedMesh.vertices = originalMesh.vertices;
            deformedMesh.triangles = originalMesh.triangles;
            deformedMesh.uv = originalMesh.uv;
            deformedMesh.normals = originalMesh.normals;
            deformedMesh.tangents = originalMesh.tangents;

            filter.mesh = deformedMesh;

            // ��������� ������������ ������
            originalVertices = originalMesh.vertices;
            deformedVertices = deformedMesh.vertices;
            originalTriangles = originalMesh.triangles;

            // �������������� ����� ������
            for (int i = 0; i < originalVertices.Length; i++)
            {
                vertexIndexMap[originalVertices[i]] = i;
            }
        }

        public void AddDeformation(Vector3 hitPoint, float radius, float depth)
        {
            Vector3 localHit = transform.InverseTransformPoint(hitPoint);

            // ������ ��� ����� ������ � �������������
            List<Vector3> newVertices = new List<Vector3>(deformedVertices);
            List<int> newTriangles = new List<int>(deformedMesh.triangles);

            // ������� ��������� �����������
            int closestTriangle = FindClosestTriangle(localHit, newVertices, newTriangles);

            if (closestTriangle != -1)
            {
                // ��������� ����� ������� � ����� �����
                Vector3 newVertexPosition = localHit;
                newVertices.Add(newVertexPosition);
                int newVertexIndex = newVertices.Count - 1;

                // ������������ �����������
                SubdivideTriangle(closestTriangle, newVertexIndex, newVertices, newTriangles);
            }

            // ��������� ������ ����
            deformedVertices = newVertices.ToArray();
            deformedMesh.vertices = deformedVertices;
            deformedMesh.triangles = newTriangles.ToArray();

            // ��������� ����������
            ApplyDeformation(localHit, radius, depth, newVertices);

            // ��������� ���
            deformedMesh.RecalculateNormals();
            deformedMesh.RecalculateBounds();
            deformedMesh.RecalculateTangents();

            // ��������� ���������
            if (TryGetComponent<MeshCollider>(out var collider))
            {
                collider.sharedMesh = deformedMesh;
            }
        }

        private int FindClosestTriangle(Vector3 point, System.Collections.Generic.List<Vector3> vertices, List<int> triangles)
        {
            int closestTriangle = -1;
            float minDistance = float.MaxValue;

            for (int i = 0; i < triangles.Count; i += 3)
            {
                Vector3 v0 = vertices[triangles[i]];
                Vector3 v1 = vertices[triangles[i + 1]];
                Vector3 v2 = vertices[triangles[i + 2]];

                Vector3 closestPoint = ClosestPointOnTriangle(point, v0, v1, v2);
                float distance = Vector3.Distance(point, closestPoint);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTriangle = i;
                }
            }

            return closestTriangle;
        }

        private Vector3 ClosestPointOnTriangle(Vector3 p, Vector3 a, Vector3 b, Vector3 c)
        {
            // ���������, ��������� �� ����� � ������� ������� A
            Vector3 ab = b - a;
            Vector3 ac = c - a;
            Vector3 ap = p - a;

            float d1 = Vector3.Dot(ab, ap);
            float d2 = Vector3.Dot(ac, ap);

            if (d1 <= 0f && d2 <= 0f) return a;

            // ���������, ��������� �� ����� � ������� ������� B
            Vector3 bp = p - b;
            float d3 = Vector3.Dot(ab, bp);
            float d4 = Vector3.Dot(ac, bp);

            if (d3 >= 0f && d4 <= d3) return b;

            // ���������, ��������� �� ����� � ������� ����� AB
            float vc = d1 * d4 - d3 * d2;
            if (vc <= 0f && d1 >= 0f && d3 <= 0f)
            {
                float v = d1 / (d1 - d3);
                return a + v * ab;
            }

            // ���������, ��������� �� ����� � ������� ������� C
            Vector3 cp = p - c;
            float d5 = Vector3.Dot(ab, cp);
            float d6 = Vector3.Dot(ac, cp);

            if (d6 >= 0f && d5 <= d6) return c;

            // ���������, ��������� �� ����� � ������� ����� AC
            float vb = d5 * d2 - d1 * d6;
            if (vb <= 0f && d2 >= 0f && d6 <= 0f)
            {
                float w = d2 / (d2 - d6);
                return a + w * ac;
            }

            // ���������, ��������� �� ����� � ������� ����� BC
            float va = d3 * d6 - d5 * d4;
            if (va <= 0f && (d4 - d3) >= 0f && (d5 - d6) >= 0f)
            {
                float w = (d4 - d3) / ((d4 - d3) + (d5 - d6));
                return b + w * (c - b);
            }

            // ����� ������ ������������
            float denom = 1f / (va + vb + vc);
            float v2 = vb * denom;
            float w2 = vc * denom;
            return a + ab * v2 + ac * w2;
        }

        private void SubdivideTriangle(int triangleIndex, int newVertexIndex, List<Vector3> vertices, List<int> triangles)
        {
            // ������� ������ ������������� ������������
            int i0 = triangles[triangleIndex];
            int i1 = triangles[triangleIndex + 1];
            int i2 = triangles[triangleIndex + 2];

            // ������� ������������ �����������
            triangles.RemoveRange(triangleIndex, 3);

            // ������� ��� ����� ������������
            triangles.Add(i0); triangles.Add(i1); triangles.Add(newVertexIndex);
            triangles.Add(i1); triangles.Add(i2); triangles.Add(newVertexIndex);
            triangles.Add(i2); triangles.Add(i0); triangles.Add(newVertexIndex);
        }

        private void ApplyDeformation(Vector3 hitPoint, float radius, float depth, List<Vector3> vertices)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                float distance = Vector3.Distance(hitPoint, vertices[i]);
                if (distance > radius) continue;

                // ������� ������� (������������ ���������)
                float falloff = 1 - Mathf.Pow(distance / radius, 2);
                Vector3 direction = (vertices[i] - hitPoint).normalized;

                // ������� �������
                vertices[i] += direction * depth * falloff;
            }
        }

        // ������������ ��� �������
        private void OnDrawGizmosSelected()
        {
            if (deformedVertices == null) return;

            Gizmos.color = Color.red;
            foreach (var vertex in deformedVertices)
            {
                Gizmos.DrawSphere(transform.TransformPoint(vertex), 0.01f);
            }
        }
    }
}