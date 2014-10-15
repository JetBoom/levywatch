using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

static class ImageManager
{
	public static Bitmap ScreenShot(int x, int y, int w, int h)
	{
		Bitmap bmpScreenshot = new Bitmap(w, h, PixelFormat.Format32bppArgb);

		Graphics gfxScreenshot = Graphics.FromImage(bmpScreenshot);
		gfxScreenshot.CopyFromScreen(x, y, 0, 0, new Size(w, h));

		gfxScreenshot.Dispose();

		return bmpScreenshot;
	}

	public static Bitmap ScreenShot()
	{
		return ScreenShot(Screen.PrimaryScreen.Bounds.Left, Screen.PrimaryScreen.Bounds.Top, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
	}

	public static void Resize(ref Bitmap bmp, float scale)
	{
		int width = (int)(bmp.Width * scale);
		int height = (int)(bmp.Height * scale);

		Bitmap bmpcopy = new Bitmap(width, height, PixelFormat.Format24bppRgb);
		bmpcopy.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);

		Graphics gfx = Graphics.FromImage(bmpcopy);
		gfx.Clear(Color.Black);
		gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

		gfx.DrawImage(bmp, 0, 0, width, height);

		gfx.Dispose();
		bmp.Dispose();

		bmp = bmpcopy;
	}

	// Removes non-grayscale pixels.
	public static void RemoveNoise(Bitmap bmp)
	{
		for (int x = 0; x < bmp.Width; x++)
		{
			for (int y = 0; y < bmp.Height; y++)
			{
				Color col = bmp.GetPixel(x, y);
				if (col.R != col.G || col.G != col.B)
					bmp.SetPixel(x, y, Color.Black);
			}
		}
	}

	public static void SaveImage(Bitmap bmpScreenshot, string outputfile)
	{
		bmpScreenshot.Save(outputfile, ImageFormat.Png);
	}
}
