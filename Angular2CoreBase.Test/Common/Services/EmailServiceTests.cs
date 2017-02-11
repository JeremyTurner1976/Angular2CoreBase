using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Angular2CoreBase.Common;

namespace Angular2CoreBase.Test.Common.Services
{
	[TestClass]
	public class EmailServiceTests
	{
		[TestMethod]
		public void GetSampleTest()
		{
			Assert.AreEqual("Pong", new Class1().Ping(), "Values do not match");
		}
	}
}
