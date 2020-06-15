using System;
using System.Drawing;
using System.Threading.Tasks;
using Patagames.Ocr;
using Patagames.Ocr.Enums;

namespace TTLX.WindowsTool.Common.Utility
{
	// Token: 0x02000017 RID: 23
	public static class OcrHelper
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00003CBC File Offset: 0x00001EBC
		public static async Task<string> GetTextFrom(Bitmap bitmap, Rectangle rect)
		{
			string re = "";
			await TaskEx.Run(delegate()
			{
				try
				{
					using (OcrApi api = OcrApi.Create())
					{
						api.Init(Languages.English, null, OcrEngineMode.OEM_DEFAULT, null, null, null, false);
						re = api.GetTextFromImage(bitmap, rect);
					}
				}
				catch (Exception e)
				{
					LogHelper.Error("OcrHelper->GetTextFrom:", e);
				}
			});
			return re;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003D0C File Offset: 0x00001F0C
		public static async Task<string> GetTextFrom(Bitmap bitmap)
		{
			string re = "";
			await TaskEx.Run(delegate()
			{
				try
				{
					using (OcrApi api = OcrApi.Create())
					{
						api.Init(Languages.English, null, OcrEngineMode.OEM_DEFAULT, null, null, null, false);
						re = api.GetTextFromImage(bitmap);
					}
				}
				catch (Exception e)
				{
					LogHelper.Error("OcrHelper->GetTextFrom:", e);
				}
			});
			return re;
		}
	}
}
