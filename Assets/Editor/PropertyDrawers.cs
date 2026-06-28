using UnityEditor;

[CustomPropertyDrawer(typeof(AttackPattern), true)]
public class AttackPatternDrawer : BaseSerializeReferenceDrawer { }

[CustomPropertyDrawer(typeof(ICondition), true)]
public class IConditionDrawer : BaseSerializeReferenceDrawer { }

[CustomPropertyDrawer(typeof(IEnemyMove), true)]
public class IEnemyMoveDrawer : BaseSerializeReferenceDrawer { }

[CustomPropertyDrawer(typeof(IHitEffect), true)]
public class IHitEffectDrawer : BaseSerializeReferenceDrawer { }