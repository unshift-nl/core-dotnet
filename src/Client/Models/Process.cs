﻿using System;

namespace Runly.Models
{
	public class Job
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string ConfigType { get; set; }
	}
}
