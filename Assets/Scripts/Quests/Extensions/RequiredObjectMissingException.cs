// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using System;

namespace Nattr4mn.QuestSystem.Exceptions
{
	public class RequiredObjectMissingException : Exception
	{
		public RequiredObjectMissingException(string objectName) :
			base($"The required object is missing: {objectName}") {}
	}
}
