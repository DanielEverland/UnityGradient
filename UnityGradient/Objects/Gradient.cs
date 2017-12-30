using UnityGradient.Utility;

namespace UnityEngine.UI
{
    /// <summary>
    /// Gradient is a linear interpolation of colors in the UI hierarchy.
    /// </summary>

    [ExecuteInEditMode]
    [AddComponentMenu("UI/Gradient", 11)]
    public class Gradient : Image
    {
        [SerializeField]
        private UnityEngine.Gradient _gradient;

        public int DataHash
        {
            get
            {
                unchecked
                {
                    int hash = 13;

                    hash += sprite.GetHashCode() * 17;
                    hash += _gradient.alphaKeys.GetHashCode() * 17;
                    hash += _gradient.colorKeys.GetHashCode() * 17;
                    hash += _gradient.mode.GetHashCode() * 17;

                    return hash;
                }                
            }
        }
        public int LayoutHash
        {
            get
            {
                return rectTransform.rect.size.GetHashCode();                
            }
        }
        public bool IsDirty { get { return LayoutHash != _oldLayoutHash || _oldDataHash != DataHash; } }
        public Sprite Texture
        {
            get
            {
                if (sprite == null)
                    CreateTexture();

                return sprite;
            }
        }
        
        private int _oldDataHash;
        private int _oldLayoutHash;

        private void Update()
        {
            if(IsDirty)
            {
                Rebuild();
            }

            _oldDataHash = DataHash;
            _oldLayoutHash = LayoutHash;
        }
        private void Rebuild()
        {
            CreateTexture();
        }
        private void CreateTexture()
        {
            if (!IsDirty)
                return;
            
            sprite = TextureCreator.GetGradientTexture(rectTransform.rect, new LinearGradient(_gradient, Vector2.zero, Vector2.one));
        }
    }
}