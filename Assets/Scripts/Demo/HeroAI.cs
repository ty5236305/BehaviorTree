using UnityEngine;
using System.Collections;
using BT;

public class HeroAI : BTTree
{

    private static readonly string DATA_KEY_DESTINATION = "Destination";

    private static readonly string OBJECT_NAME_ORC = "Orc";
    private static readonly string OBJECT_NAME_GOBLIN = "Goblin";

    private static readonly string ANIMATION_RUN = "Run";
    private static readonly string ANIMATION_FIGHT = "Fight";
    private static readonly string ANIMATION_IDLE = "Idle";

    public float speed;
    public float sightForOrc;
    public float sightForGoblin;
    public float fightDistance;

    protected override void Initialize()
    {
        //初始化
        base.Initialize();

        //创建选择器作为根节点，每次执行，先有序地遍历子节点，然后执行符合准入条件的第一个子结点。
        //可以看作是根据条件来选择一个子结点的选择器。
        BTSelector selector = new BTSelector();
        _root = selector;

        
        //跑步动作，该动作在逃跑结点和战斗结点都有使用，跑步动作由位移动作和跑步动画动作两个部分组成，所以这里使用并行结点
        BTParallel run = new BTParallel(BTParallel.ParallelOpt.Or);
        run.AddChild(new MoveAction(DATA_KEY_DESTINATION, speed));
        run.AddChild(new PlayAnimationAction(ANIMATION_RUN));


        //逃跑结点，这里使用并行节点，逃跑过程中需要不断查找逃跑目标点
        BTParallel escape = new BTParallel(BTParallel.ParallelOpt.Or);
        FindEscapeAction findEscapeDestination = new FindEscapeAction(OBJECT_NAME_ORC, DATA_KEY_DESTINATION, sightForOrc);
        escape.AddChild(findEscapeDestination);
        escape.AddChild(run);


        //战斗结点，由于要先跑到英雄的攻击范围内，才能发动攻击动作，所以这里使用序列结点
        BTSequence fight = new BTSequence();

        //定义go动作
        BTParallel go = new BTParallel(BTParallel.ParallelOpt.Or);
        FindTargetAction findTargetDestination = new FindTargetAction(OBJECT_NAME_GOBLIN, DATA_KEY_DESTINATION, fightDistance);
        go.AddChild(findTargetDestination);
        go.AddChild(run);

        //定义attack动作
        CheckInSightCondition checkGoblinInFightDistance = new CheckInSightCondition(fightDistance, OBJECT_NAME_GOBLIN);
        //BTConditionDecorator attack = new BTConditionDecorator(new PlayAnimationAction(ANIMATION_FIGHT), checkGoblinInFightDistance);
        PlayAnimationAction attack = new PlayAnimationAction(ANIMATION_FIGHT);

        //将go动作和attack动作添加到fight结点
        fight.AddChild(go);
        fight.AddChild(attack);



        //根结点添加逃跑结点，附带前置条件
        CheckInSightCondition checkOrcInSight = new CheckInSightCondition(sightForOrc, OBJECT_NAME_ORC);
        selector.AddChild(new BTConditionDecorator(escape, checkOrcInSight));

        //根结点添加战斗结点，附带前置条件
        CheckInSightCondition checkGoblinInSight = new CheckInSightCondition(sightForGoblin, OBJECT_NAME_GOBLIN);
        selector.AddChild(new BTConditionDecorator(fight, checkGoblinInSight));

        //根结点添加闲置结点
        selector.AddChild(new PlayAnimationAction(ANIMATION_IDLE));
    }
}
