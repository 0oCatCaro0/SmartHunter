﻿using SmartHunter.Core.Data;
using SmartHunter.Game.Helpers;

namespace SmartHunter.Game.Data
{
    public class MonsterStatusEffect : TimedVisibility
    {
        Monster m_Owner;

        int m_Index;
        public int Index
        {
            get { return m_Index; }
            set
            {
                if (SetProperty(ref m_Index, value))
                {
                    NotifyPropertyChanged(nameof(Tags));
                    NotifyPropertyChanged(nameof(Name));
                    NotifyPropertyChanged(nameof(IsVisible));
                }
            }
        }

        public Progress Buildup { get; private set; }
        public Progress Duration { get; private set; }

        int m_TimesActivatedCount;
        public int TimesActivatedCount
        {
            get { return m_TimesActivatedCount; }
            set { SetProperty(ref m_TimesActivatedCount, value); }
        }

        public string Name
        {
            get
            {
                return LocalizationHelper.GetMonsterStatusEffectName(Index);
            }
        }

        public string[] Tags
        {
            get
            {
                return GetTagsFromIndex(Index);
            }
        }

        public bool IsVisible
        {
            get
            {
                return IsIncluded(Tags) && IsTimeVisible(ConfigHelper.Main.Values.Overlay.MonsterWidget.ShowUnchangedStatusEffects, ConfigHelper.Main.Values.Overlay.MonsterWidget.HideStatusEffectsAfterSeconds);
            }
        }

        public MonsterStatusEffect(Monster owner, int index, float maxBuildup, float currentBuildup, float maxDuration, float currentDuration, int timesActivatedCount)
        {
            m_Owner = owner;
            m_Index = index;
            Buildup = new Progress(maxBuildup, currentBuildup, true);
            Duration = new Progress(maxDuration, currentDuration, true);
            m_TimesActivatedCount = timesActivatedCount;

            PropertyChanged += MonsterStatusEffect_PropertyChanged;
            Buildup.PropertyChanged += Bar_PropertyChanged;
            Duration.PropertyChanged += Bar_PropertyChanged;
        }

        private void MonsterStatusEffect_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TimesActivatedCount))
            {
                UpdateLastChangedTime();
            }
        }

        private void Bar_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateLastChangedTime();
        }

        public static string[] GetTagsFromIndex(int index)
        {
            return ConfigHelper.MonsterData.Values.StatusEffects[index].Tags;
        }

        public static bool IsIncluded(string[] tags)
        {
            return ConfigHelper.Main.Values.Overlay.MonsterWidget.MatchStatusEffectTags(tags);
        }
    }
}
