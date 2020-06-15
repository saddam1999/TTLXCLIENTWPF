using System;
using System.ComponentModel.DataAnnotations;
using TTLX.WindowsTool.Common.Core;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x0200003A RID: 58
	public class User : ValidateModelBase
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00004901 File Offset: 0x00002B01
		// (set) Token: 0x060001ED RID: 493 RVA: 0x000048F8 File Offset: 0x00002AF8
		[Required(ErrorMessage = "手机号不能为空")]
		[RegularExpression("^(1)\\d{10}$", ErrorMessage = "请输入有效的手机号")]
		public string Account { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00004912 File Offset: 0x00002B12
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00004909 File Offset: 0x00002B09
		[Required(ErrorMessage = "密码不能为空")]
		public string Password { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00004923 File Offset: 0x00002B23
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000491A File Offset: 0x00002B1A
		public UserTypeEnum UserType { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000492B File Offset: 0x00002B2B
		public bool IsAdmin
		{
			get
			{
				return this.AdminDetails != null;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000493F File Offset: 0x00002B3F
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00004936 File Offset: 0x00002B36
		public string Name { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00004950 File Offset: 0x00002B50
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x00004947 File Offset: 0x00002B47
		public string Token { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00004961 File Offset: 0x00002B61
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00004958 File Offset: 0x00002B58
		public Company CompanyDetails { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00004972 File Offset: 0x00002B72
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00004969 File Offset: 0x00002B69
		public Admin AdminDetails { get; set; }

		// Token: 0x060001FC RID: 508 RVA: 0x0000497A File Offset: 0x00002B7A
		public string ToInfoString()
		{
			return "Account:" + this.Account + " | Name:" + this.Name;
		}
	}
}
