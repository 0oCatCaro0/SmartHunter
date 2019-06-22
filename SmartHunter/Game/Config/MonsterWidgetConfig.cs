﻿using SmartHunter.Core.Config;
using System;
using System.Text.RegularExpressions;

namespace SmartHunter.Game.Config
{
    public class MonsterWidgetConfig : WidgetConfig
    {
        // em[0-9]|ems[0-9]|gm[0-9]
        public string MonsterIdRegex = "em[0-9]";
        public string PartTagsRegex = ".*|Removable|Head|Body|Tail|Wings|Limbs|Arms|Legs|Horns";
        public string StatusEffectGroupIdRegex = "";
        public bool ShowUnchangedMonsters = true;
        public float HideMonstersAfterSeconds = 9999;
        public bool ShowUnchangedParts = false;
        public float HidePartsAfterSeconds = 12f;
        public bool ShowUnchangedStatusEffects = false;
        public float HideStatusEffectsAfterSeconds = 12f;

        public bool ShowCrown = true;
        public bool ShowBars = true;
        public bool ShowNumbers = true;
        public bool ShowPercents = false;

        public MonsterWidgetConfig(float x, float y) : base(x, y)
        {
        }

        public bool MatchMonsterId(string monsterId)
        {
            return new Regex(MonsterIdRegex).IsMatch(monsterId);
        }

        public bool MatchPartTags(string[] tags)
        {
            return new Regex(PartTagsRegex).IsMatch(String.Join(" ", tags));
        }

        public bool MatchStatusEffectGroupId(string groupId)
        {
            return new Regex(StatusEffectGroupIdRegex).IsMatch(groupId);
        }
    }
}
