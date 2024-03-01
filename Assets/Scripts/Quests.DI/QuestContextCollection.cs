// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using System;
using System.Collections.Generic;

namespace Nattr4mn.Quests.DI
{
    public sealed class QuestContextCollection
	{
		private readonly Dictionary<Type, object> _cache = new Dictionary<Type, object>();

		public void RegisterSingle(object implementation, Type type)
			=> _cache[type] = implementation;
		public void RegisterSingle<TService>(TService implementation)
			=> RegisterSingle(implementation, typeof(TService));

		public TService GetSingle<TService>()
        {
			if(_cache.TryGetValue(typeof(TService), out var service))
			{
				return (TService)service;
            }
            else
            {
				throw new Exception($"Can't find service of type {typeof(TService).FullName}");
            }
        }
	}
}