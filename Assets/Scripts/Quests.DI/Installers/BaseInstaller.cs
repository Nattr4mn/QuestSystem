// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using UnityEngine;

namespace Nattr4mn.Quests.DI
{
    public abstract class BaseInstaller : MonoBehaviour
    {
        public abstract void Install(QuestContextCollection contextCollection);
    }
}