﻿using Angular2CoreBase.Common.Interfaces;
using System;

namespace Angular2CoreBase.Common.CustomAttributes
{
	public class EnumAttributes
	{
		public class ClientSideString : Attribute, IAttribute
		{
			public string Identifier { get; set; }

			public ClientSideString(string value)
			{
				Identifier = value;
			}
		}

		public class FileName : Attribute, IAttribute
		{
			public string Identifier { get; set; }

			public FileName(string value)
			{
				Identifier = value;
			}
		}
	}
}
