using Cirrus.Collections;

namespace Cirrus.Arpg.Abilities
{
	//
	//protected Collections.BasicPool<DamageEffect> _damagePool =
	//new Collections.BasicPool<DamageEffect>(max: DamagePoolSize);

	//	public abstract class GenericAbilityAsset<A, B, C>
	//: AbilityAsset
	//where A : class, IConcreteAbilityResource
	//where B : class, IConcreteAbility
	//where C : Inventory

	public class EffectPool<TEffect, TInstance>
		: BasicPool<TInstance>
		where TEffect : EffectBase
		where TInstance : EffectInstanceBase, new()
	{
		public EffectPool(int max) : base(max) { }

		public TInstance Get(TEffect res)
		{
			var e = base.Get();
			EffectUtils.Populate(e, res);
			return e;
		}
	}
}