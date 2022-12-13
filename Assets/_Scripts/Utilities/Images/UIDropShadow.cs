using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Glitic.UI.Feature
{
    [AddComponentMenu("UI/Effects/DropShadow", 14)]
    public class UIDropShadow : BaseMeshEffect
    {
        [SerializeField]
        public Color shadowColor = new Color(0f, 0f, 0f, 0.5f);

        [SerializeField]
        private Vector2 shadowDistance = new Vector2(1f, -1f);

        [SerializeField]
        private Vector2 shadowPosition = new Vector2(0f, -0f);

        [SerializeField]
        private float shadowScale = 1;

        [SerializeField]
        private bool m_UseGraphicAlpha = true;
        public int iterations = 5;
        public Vector2 shadowSpread = Vector2.one;

        protected UIDropShadow()
        { }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            EffectDistance = shadowDistance;
            base.OnValidate();
        }

#endif

        public Color effectColor
        {
            get { return shadowColor; }
            set
            {
                shadowColor = value;
                if (graphic != null)
                    graphic.SetVerticesDirty();
            }
        }

        public Vector2 ShadowSpread
        {
            get { return shadowSpread; }
            set
            {
                shadowSpread = value;
                if (graphic != null)
                    graphic.SetVerticesDirty();
            }
        }

        public int Iterations
        {
            get { return iterations; }
            set
            {
                iterations = value;
                if (graphic != null)
                    graphic.SetVerticesDirty();
            }
        }

        public Vector2 EffectDistance
        {
            get { return shadowDistance; }
            set
            {
                shadowDistance = value;

                if (graphic != null)
                    graphic.SetVerticesDirty();
            }
        }

        public bool useGraphicAlpha
        {
            get { return m_UseGraphicAlpha; }
            set
            {
                m_UseGraphicAlpha = value;
                if (graphic != null)
                    graphic.SetVerticesDirty();
            }
        }

        private UIVertex vt;
        private int count;
        private List<UIVertex> vertsCopy;
        private Vector3 position;
        private float fac;
        private Color32 color;
        void DropShadowEffect(List<UIVertex> verts)
        {
            count = verts.Count;

            vertsCopy = new List<UIVertex>(verts);
            verts.Clear();

            for (int i = 0; i < iterations; i++)
            {
                for (int v = 0; v < count; v++)
                {
                    vt = vertsCopy[v];
                    position = vt.position;
                    fac = (float)i / (float)iterations;
                    position.x *= (1 + shadowSpread.x * fac * 0.01f);
                    position.y *= (1 + shadowSpread.y * fac * 0.01f);
                    position.x += shadowDistance.x * fac;
                    position.y += shadowDistance.y * fac;
                    vt.position = new Vector3(position.x + shadowPosition.x, position.y + shadowPosition.y, position.z) * shadowScale;
                    color = shadowColor;
                    color.a = (byte)((float)color.a / (float)iterations);
                    vt.color = color;
                    verts.Add(vt);
                }
            }

            for (int i = 0; i < vertsCopy.Count; i++)
            {
                verts.Add(vertsCopy[i]);
            }
        }

        private List<UIVertex> output;
        public override void ModifyMesh(VertexHelper vh)
        {
            if (!IsActive())
                return;

            output = new List<UIVertex>();
            vh.GetUIVertexStream(output);

            DropShadowEffect(output);

            vh.Clear();
            vh.AddUIVertexTriangleStream(output);
        }
    }
}