using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using RestSharp;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000037 RID: 55
	public class UserService : IUserService
	{
		// Token: 0x0600019E RID: 414 RVA: 0x000063F0 File Offset: 0x000045F0
		private async Task<Admin> AdminLogin(User user)
		{
			RestRequest req = new RestRequest("login", Method.POST);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"mobile",
					user.Account
				},
				{
					"password",
					user.Password
				}
			}, false);
			AdminLoginResponse adminLoginResponse = await RestService.StartRequestTask<AdminLoginResponse>(req, null, null);
			return (adminLoginResponse != null) ? adminLoginResponse.Admin : null;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006438 File Offset: 0x00004638
		private async Task<Company> CompanyLogin(User user)
		{
			RestRequest req = new RestRequest("login", Method.POST);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"mobile",
					user.Account
				},
				{
					"password",
					user.Password
				}
			}, false);
			CompanyLoginResponse companyLoginResponse = await RestService.StartRequestTask<CompanyLoginResponse>(req, null, null);
			return (companyLoginResponse != null) ? companyLoginResponse.CompanyInfo : null;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006480 File Offset: 0x00004680
		public async Task<bool> Login(User user)
		{
			bool result;
			if (user.UserType.Equals(UserTypeEnum.机构登陆))
			{
				Company re = await this.CompanyLogin(user);
				if (re != null)
				{
					user.Name = re.name;
					user.Token = re.token;
					user.CompanyDetails = re;
					AppUser.Instance().LoginUser(user);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			else
			{
				Admin re2 = await this.AdminLogin(user);
				if (re2 != null)
				{
					user.Name = re2.name;
					user.Token = re2.token;
					user.AdminDetails = re2;
					AppUser.Instance().LoginUser(user);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000064CD File Offset: 0x000046CD
		public Task Logout()
		{
			return TaskEx.Run(delegate()
			{
				AppUser.Instance().Clear();
			});
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000064F4 File Offset: 0x000046F4
		public User LoadLastRec()
		{
			User re = new User();
			try
			{
				string path = Helper.GetTempFilePath();
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(path);
				XmlElement user = (XmlElement)xmlDocument.DocumentElement.SelectSingleNode("Users").FirstChild;
				if (user.HasAttribute("Name"))
				{
					re.Account = user.Attributes["Name"].Value;
				}
				if (user.HasAttribute("Password"))
				{
					re.Password = user.Attributes["Password"].Value;
				}
				if (user.HasAttribute("Type"))
				{
					re.UserType = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), user.Attributes["Type"].Value);
				}
			}
			catch (Exception)
			{
			}
			return re;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000065D8 File Offset: 0x000047D8
		public Task RememberMe(User pUser)
		{
			return TaskEx.Run(delegate()
			{
				try
				{
					string path = Helper.GetTempFilePath();
					XmlDocument doc = new XmlDocument();
					if (File.Exists(path))
					{
						doc.Load(path);
					}
					else
					{
						doc.LoadXml("<Temp></Temp>");
					}
					XmlElement root = doc.DocumentElement;
					XmlNode userNodes = root.SelectSingleNode("Users");
					if (userNodes == null)
					{
						XmlElement xe = doc.CreateElement("Users");
						XmlElement user = doc.CreateElement("User");
						user.SetAttribute("Name", pUser.Account);
						user.SetAttribute("Password", pUser.Password);
						user.SetAttribute("Type", pUser.UserType.ToString());
						xe.AppendChild(user);
						root.AppendChild(xe);
					}
					else
					{
						XmlElement xmlElement = (XmlElement)userNodes.FirstChild;
						xmlElement.SetAttribute("Name", pUser.Account);
						xmlElement.SetAttribute("Password", pUser.Password);
						xmlElement.SetAttribute("Type", pUser.UserType.ToString());
					}
					doc.Save(path);
				}
				catch (Exception e)
				{
					LogHelper.Error("RememberMe:", e);
				}
			});
		}
	}
}
