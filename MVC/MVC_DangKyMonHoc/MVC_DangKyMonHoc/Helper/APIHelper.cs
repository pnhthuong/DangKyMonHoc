using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVC_DangKyMonHoc.Helper
{
	public class APIHelper
	{
		public HttpClient Intial()
		{
			var Client = new HttpClient();
			Client.BaseAddress = new Uri("https://localhost:44319");
			return Client;
		}
	}
}
