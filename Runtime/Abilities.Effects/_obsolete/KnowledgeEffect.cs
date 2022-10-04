//using Cirrus.Collections;
//using Cirrus.Unity.Objects;
//using Cirrus.Arpg.AI;
//using System.Collections.Generic;

////using Cirrus.Arpg.Content.Attributes;

//namespace Cirrus.Arpg.Abilities
//{
//	public class KnowledgeEffect : EffectBase
//	{
//		public IEnumerable<KnowledgeEntry> SemanticKnowledgeItems { get; set; } = ArrayUtils.Empty<KnowledgeEntry>();

//		public KnowledgeEntry SemanticKnowledgeItem 
//		{ 
//			set => SemanticKnowledgeItems = value.ToEnumerable(); 
//		}

//		// TODO: We can lower semantic knowledge ..
//		// Not just added and remove. See KnowledgeItem.Update
//		// TODO: The problem here is that a certain (the first) modifier has prevalence
//		// when it is retrieved it affects the others...
//		// TODO: Should not have any pre-existing semantic knowledge other then directed by modifier
//		// NOTE: Since all semantic knowledge (most) will be handled by modifier maybe this is fine?
//		//protected override bool _Apply(Action action, EntityBase target)
//		//{
//		//	return target.With<CharacterEntity, bool>(chara =>
//		//	{
//		//		foreach(var semantic in SemanticKnowledgeItems)
//		//		{
//		//			_Agent(chara).IncreaseSemanticKnowledge(semantic);
//		//		}

//		//		_Agent(chara).IncreaseEpisodicKnowledge();
//		//		return true;
//		//	});
//		//}

//		//protected override bool _Unapply(Action action, EntityBase target = null)
//		//{
//		//	return target.With<CharacterEntity, bool>(chara =>
//		//	{
//		//		foreach(var semantic in SemanticKnowledgeItems)
//		//		{
//		//			if(!chara.AI.Agent.SemanticKnowledge.Remove(semantic))
//		//			{
//		//				return false;
//		//			}
//		//		}

//		//		chara.CharacterObject.AI.Agent.IncreaseEpisodicKnowledge();
//		//		return true;
//		//	});			
//		//}

//		protected override bool _Apply(IEffectSource action, ObjectBase target)
//		{
//			if(target.Character != null)
//			{
//				UtilitySupportComponent agent = target.Ai.UtilitySupport;				
//				if(agent == null) return false;
//				foreach(var semantic in SemanticKnowledgeItems)
//				{
//#if UNITY_EDITOR
//					semantic.__ModifierEffect = this;
//#endif
//					agent.AddSemanticKnowledge(semantic);
//				}

//				// TODO if not initialized (before encounter) then there is no reason to update just yet
//				// TODO: only target entities added in this effect (not all)
//				if(!agent.EpisodicKnowledge.IsEmpty()) agent.UpdateEpisodicKnowledge(null, true);
//				return true;
//			}

//			return false;
//		}

//		protected override bool _Unapply(IEffectSource action, ObjectBase target)
//		{
//			if(target.Character != null)
//			{
//				UtilitySupportComponent agent = target.Ai.UtilitySupport;
//				if(agent == null) return false;
//				foreach(var semantic in SemanticKnowledgeItems)
//				{
//					if(!agent.RemoveSemanticKnowledge(semantic))
//					{
//						return false;
//					}
//				}

//				if(!agent.EpisodicKnowledge.IsEmpty()) agent.UpdateEpisodicKnowledge(null, true);
//				return true;				
//			}

//			return false;
//		}
//	}
//}
