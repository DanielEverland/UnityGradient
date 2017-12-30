using UnityEngine;

namespace UnityGradient.Utility
{
    public static class TextureCreator
    {
        public static Sprite GetGradientTexture(Rect rect, GradientBase gradient)
        {
            if(gradient is LinearGradient)
            {
                return GetLinearGradientTexture(rect, (LinearGradient)gradient);
            }

            throw new System.NotImplementedException("Type " + gradient.GetType() + " is not implemented");
        }
        private static Sprite GetLinearGradientTexture(Rect rect, LinearGradient linearGradient)
        {
            Texture2D texture = GetTexture(rect);
            Color[] colors = new Color[texture.width * texture.height];
            
            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    int i = Utility.GetIndex(x, y, texture.width);

                    float xPercentage = (float)x / (float)texture.width;
                    float yPercentage = (float)y / (float)texture.height;
                    
                    colors[i] = Utility.GetColor(new Vector2(xPercentage, yPercentage), linearGradient);
                }
            }

            texture.SetPixels(colors);
            texture.Apply();

            return ToSprite(texture);
        }
        private static Texture2D GetTexture(Rect rect)
        {
            int width = Mathf.RoundToInt(rect.size.x);
            int height = Mathf.RoundToInt(rect.size.y);

            return new Texture2D(width, height, TextureFormat.ARGB32, true, true);
        }
        private static Sprite ToSprite(Texture2D texture)
        {
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2);

            sprite.name = "Rect";

            return sprite;
        }
    }
}
