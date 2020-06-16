using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public class UserService : IUserService
	{
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
			});
			return (await RestService.StartRequestTask<AdminLoginResponse>(req))?.Admin;
		}

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
			});
			return (await RestService.StartRequestTask<CompanyLoginResponse>(req))?.CompanyInfo;
		}

		public async Task<bool> Login(User user)
		{
			if (user.UserType.Equals(UserTypeEnum.机构登陆))
			{
				Company re2 = await CompanyLogin(user);
				if (re2 != null)
				{
					user.Name = re2.name;
					user.Token = re2.token;
					user.CompanyDetails = re2;
					AppUser.Instance().LoginUser(user);
					return true;
				}
				return false;
			}
			Admin re = await AdminLogin(user);
			if (re != null)
			{
				user.Name = re.name;
				user.Token = re.token;
				user.AdminDetails = re;
				AppUser.Instance().LoginUser(user);
				return true;
			}
			return false;
		}

		public Task Logout()
		{
			return TaskEx.Run(delegate
			{
				AppUser.Instance().Clear();
			});
		}

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
				if (!user.HasAttribute("Type"))
				{
					return re;
				}
				re.UserType = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), user.Attributes["Type"].Value);
				return re;
			}
			catch (Exception)
			{
				return re;
			}
		}

		public Task RememberMe(User pUser)
		{
			return TaskEx.Run(delegate
			{
				try
				{
					string tempFilePath = Helper.GetTempFilePath();
					XmlDocument xmlDocument = new XmlDocument();
					if (File.Exists(tempFilePath))
					{
						xmlDocument.Load(tempFilePath);
					}
					else
					{
						xmlDocument.LoadXml("<Temp></Temp>");
					}
					XmlElement documentElement = xmlDocument.DocumentElement;
					XmlNode xmlNode = documentElement.SelectSingleNode("Users");
					if (xmlNode == null)
					{
						XmlElement xmlElement = xmlDocument.CreateElement("Users");
						XmlElement xmlElement2 = xmlDocument.CreateElement("User");
						xmlElement2.SetAttribute("Name", pUser.Account);
						xmlElement2.SetAttribute("Password", pUser.Password);
						xmlElement2.SetAttribute("Type", pUser.UserType.ToString());
						xmlElement.AppendChild(xmlElement2);
						documentElement.AppendChild(xmlElement);
					}
					else
					{
						XmlElement obj = (XmlElement)xmlNode.FirstChild;
						obj.SetAttribute("Name", pUser.Account);
						obj.SetAttribute("Password", pUser.Password);
						obj.SetAttribute("Type", pUser.UserType.ToString());
					}
					xmlDocument.Save(tempFilePath);
				}
				catch (Exception pException)
				{
					LogHelper.Error("RememberMe:", pException);
				}
			});
		}
	}
}
