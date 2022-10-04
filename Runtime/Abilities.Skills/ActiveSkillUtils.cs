using Cirrus.Arpg.Entities;
using Cirrus.Unity.Numerics;
using System;

namespace Cirrus.Arpg.Abilities
{
	public static class ActiveSkillUtils
	{
		public static bool Test(this ActiveSkillInstance skill, ActiveSkillFlags flags)
		{
			return skill.ActiveSkillFlags.Intersects(flags);
		}

		public static bool Test(this ActiveSkill skill, ActiveSkillFlags flags)
		{
			return skill.ActiveSkillFlags.Intersects(flags);
		}

		//public static ActiveSkill DefaultSkill = new ActiveSkill()
		//{
		//	//Descriptor = new Descriptor
		//	//{
		//	//	Name = nameof(DefaultSkill)
		//	//}
		//};
	}
}
