// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using System.Collections.Generic;
using UnityEngine;

namespace Nattr4mn.Quests.DI
{
    public class QuestContext : MonoBehaviour
    {
        [SerializeField] private bool _autoMode = true;
        [SerializeField] private List<BaseInstaller> _installers;

        public static QuestContext Instance { get; private set; }
        public static QuestContextCollection Collection => Instance.ContextCollection;

        public void Initialize()
        {
            if (Instance != null)
            {
                return;
            }
            Instance = this;
            Instance.InstallBindings();
            DontDestroyOnLoad(Instance);
        }

        private void Awake()
        {
            if (!_autoMode)
            {
                return;
            }
            Initialize();
        }

        public QuestContextCollection ContextCollection { get; private set; } 

        private void InstallBindings()
        {
            ContextCollection = new QuestContextCollection();
            foreach (var installer in _installers)
            {
                installer.Install(ContextCollection);
            }
        }
    }
}
