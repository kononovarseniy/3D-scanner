using System;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Scan3D
{
    class GLTexture : IDisposable
    {
        private static GLTexture bindedTexture = null;

        private bool disposed = false;

        public bool Binded => this == bindedTexture;
        public uint Texture { get; private set; }

        private GLTexture(uint texture)
        {
            Texture = texture;
        }

        private static unsafe uint MakeGLTexture(System.Drawing.Imaging.BitmapData imageData)
        {
            int pixels = imageData.Width * imageData.Height;
            byte* scan0 = (byte*)imageData.Scan0;
            for (int i = 0; i < pixels; i++)
            {
                byte tmp = scan0[i * 3 + 0];
                scan0[i * 3 + 0] = scan0[i * 3 + 2];
                scan0[i * 3 + 2] = tmp;
            }
            uint textureId;
            GL.GenTextures(1, out textureId);

            // устанавливаем режим упаковки пикселей 
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);

            GL.BindTexture(TextureTarget.Texture2D, textureId);

            // устанавливаем режим фильтрации и повторения текстуры 
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (Int32)All.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (Int32)All.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (Int32)All.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (Int32)All.Nearest);
            GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (Int32)All.Replace);

            GL.TexImage2D(
                target: TextureTarget.Texture2D,
                level: 0,
                internalformat: PixelInternalFormat.Rgb,
                width: imageData.Width,
                height: imageData.Height,
                border: 0,
                format: PixelFormat.Rgb,
                type: PixelType.UnsignedByte,
                pixels: imageData.Scan0);

            return textureId;
        }

        public static void Bind(GLTexture texture)
        {
            GL.BindTexture(TextureTarget.Texture2D, texture.Texture);
            bindedTexture = texture;
        }

        public static void Unbind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
            bindedTexture = null;
        }

        public static GLTexture FromBitmap(Bitmap bitmap)
        {
            var imageData = bitmap.LockBits(
                new Rectangle(Point.Empty, bitmap.Size),
                System.Drawing.Imaging.ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            uint id = MakeGLTexture(imageData);
            bitmap.UnlockBits(imageData);
            return new GLTexture(id);
        }

        public static GLTexture FromFile(string filename)
        {
            using (Bitmap image = (Bitmap)Image.FromFile(filename))
            {
                return FromBitmap(image);
            }
        }
        
        public static implicit operator uint(GLTexture texture)
        {
            return texture.Texture;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
            }

            if (Binded) Unbind();
            GL.DeleteTexture(Texture);

            disposed = true;
        }
    }
}
