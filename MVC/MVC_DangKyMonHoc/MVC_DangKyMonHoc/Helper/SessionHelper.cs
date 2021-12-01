using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;	
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MVC_DangKyMonHoc.Helper
{
	public static class SessionHelper
	{
		public static string getString(ISession session, string key)
        {
			if (session.GetString(key) == null) return "";
			return session.GetString(key);
        }

		public static void setString(ISession session, string key, string value)
        {
			session.SetString(key, value);
        }

		public static void setObject(ISession session, string key, object value)
        {
			session.SetString(key, JsonConvert.SerializeObject(value));
        }

		public static T getObject<T>(ISession session, string key)
        {
			if (session.GetString(key) == null) return default(T);
			return JsonConvert.DeserializeObject<T>(session.GetString(key));
        }
	}
}