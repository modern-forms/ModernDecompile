﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JustDecompile.External.JustAssembly
{
	public interface IOffsetSpan
	{
		int StartOffset { get; }
		int EndOffset { get; }
	}
}
