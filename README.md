# cirrus-abilities 
## Action rpg ability framework for Unity (Work in progress)

### Abilities
* Abilities represent the base for a number of object types including items, skills (active/passive)
    * `AbilityBase`, `AbilityInstanceBase`, `IAbility`, `IAbilityInstance`
    * `AbilityImplBase`, `AbilityImplInstanceBase`, `IAbilityImplInstance`

### Abilities.Active
*  Active abilities are used to define effects which are triggered by the player's direct input.
* Example include melee weapons, skills, items, consumables.
    * `ActiveAbility`, `ActiveAbilityInstance`, `IActiveAbility`, `IActiveAbilityInstance`
    * `ActiveAbilityImplBase`, `ActiveAbilityImpl`, `ActiveAbilityImplInstanceBase`, `ActiveAbilityImplInstance`
    * `ChargedActiveAbilityImpl`, `SustainedActiveAbilityImpl`

### Abilities.Passives
* Passive abilities are used to define effects which are happening without user intervention, either at the start of a play session, or after an event has occured. Their lifetime is linked to durations explained below.
    * `PassiveAbilityBase`, `PassiveAbility`, `PassiveAbilityInstanceBase`, `PassiveAbilityInstance`
    * 

### Abilities.Effects
* Effects are used both by active and passive abilities.
    * `EffectBase`, `NonInstancedEffect`, `IEffectInstance`, `EffectInstanceBase`
    * `EffectPool`

### Abilities.Skills
* Skills are a concretization of an ability (Other example include items, weapons, etc). A skill is typically unlocked as part of the game's progression.

### Conditions
* TODO

### Conditions.Durations
* Durations determine the length of a passive ability is determined by it's duration. Some examples of durations are "Time" which is self explanatory, or "Explicit" which requires intervention via an active ability to be removed.

### Implementation details
* MonoBehaviours are used as resources over ScriptableObject as prefab variant enable attribute inheritance. The prefabs are readonly and not instantiated.
