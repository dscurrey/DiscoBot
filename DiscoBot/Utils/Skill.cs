using System;
using System.Drawing;

namespace DiscoBot.Utils
{
    public enum Skill
    {
        Logic,
        Encyclopedia,
        Rhetoric,
        Drama,
        Conceptualization,
        VisualCalculus,
        Volition,
        Empathy,
        Authority,
        EspritDeCorps,
        Suggestion,
        Endurance,
        PainThreshold,
        PhysicalInstrument,
        ElectroChemistry,
        Shivers,
        HalfLight,
        HandEyeCoordination,
        Perception,
        ReactionSpeed,
        SavoirFaire,
        Interfacing,
        Composure
    }

    public class SkillsUtil
    {
        public static string GetSkillName(Skill skill)
        {
            switch (skill)
            {
                case Skill.Logic:
                case Skill.Encyclopedia:
                case Skill.Rhetoric:
                case Skill.Drama:
                case Skill.Conceptualization:
                case Skill.Perception:
                case Skill.Volition:
                case Skill.Empathy:
                case Skill.Authority:
                case Skill.Suggestion:
                case Skill.Endurance:
                case Skill.Shivers:
                case Skill.Interfacing:
                case Skill.Composure:
                    return skill.ToString();
                case Skill.EspritDeCorps:
                    return "Esprit de Corps";
                case Skill.PainThreshold:
                    return "Pain Threshold";
                case Skill.PhysicalInstrument:
                    return "Physical Instrument";
                case Skill.ElectroChemistry:
                    return "Electro-Chemistry";
                case Skill.HalfLight:
                    return "Half Light";
                case Skill.HandEyeCoordination:
                    return "Hand-Eye Coordination";
                case Skill.ReactionSpeed:
                    return "Reaction Speed";
                case Skill.SavoirFaire:
                    return "Savoir Faire";
                case Skill.VisualCalculus:
                    return "Visual Calculus";
                default:
                    throw new ArgumentOutOfRangeException(nameof(skill), skill, null);
            }
        }

        public static Color GetSkillColour(Skill skill)
        {
            switch (skill)
            {
                case Skill.Logic:
                case Skill.Encyclopedia:
                case Skill.Rhetoric:
                case Skill.Drama:
                case Skill.Conceptualization:
                case Skill.VisualCalculus:
                    return Color.RoyalBlue;
                case Skill.Volition:
                case Skill.Empathy:
                case Skill.Authority:
                case Skill.EspritDeCorps:
                case Skill.Suggestion:
                    return Color.MediumSlateBlue;
                case Skill.Endurance:
                case Skill.PainThreshold:
                case Skill.PhysicalInstrument:
                case Skill.ElectroChemistry:
                case Skill.Shivers:
                case Skill.HalfLight:
                    return Color.DarkRed;
                case Skill.HandEyeCoordination:
                case Skill.Perception:
                case Skill.ReactionSpeed:
                case Skill.SavoirFaire:
                case Skill.Interfacing:
                case Skill.Composure:
                    return Color.Yellow;
                default:
                    throw new ArgumentOutOfRangeException(nameof(skill), skill, null);
            }
        }
    }
}
