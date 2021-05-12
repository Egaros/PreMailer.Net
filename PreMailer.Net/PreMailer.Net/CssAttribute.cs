﻿using System;

namespace PreMailer.Net
{
	public class CssAttribute
	{
		public string Style { get; set; }
		public string Value { get; set; }
		public bool Important { get; set; }

		private CssAttribute() { }

		public static CssAttribute FromRule(string rule)
		{
			var parts = rule.Split(new[] { ':' }, 2);

			if (parts.Length == 1)
			{
				return null;
			}

			var value = parts[1].Trim();
			var important = false;

			if (value.IndexOf("!important", StringComparison.CurrentCultureIgnoreCase) != -1)
			{
				important = true;
				value = value.Replace("!important", "").Trim();
			}

			return new CssAttribute
			{
				Style = parts[0].Trim(),
				Value = value,
				Important = important
			};
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return ToString(removeImportant: false);
		}

		/// <summary>
		/// Generates css styles with or without !important
		/// </summary>
		/// <param name="removeImportant"> When set to <c>true</c> generates css styles without !important </param>
		/// <returns> css styles with or without !important </returns>
		public string ToString(bool removeImportant)
		{
			return removeImportant
				? $"{Style}: {Value}"
				: $"{Style}: {Value}{(Important ? " !important" : string.Empty)}";
		}
	}
}
