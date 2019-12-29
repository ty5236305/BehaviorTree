using UnityEngine;
using System.Collections;
using BT;

public class HeroAI : BTTree
{

    private static readonly string DESTINATION_DATA_KEY = "Destination";

    private static readonly string ORC_OBJECT_NAME = "Orc";
    private static readonly string GOBLIN_OBJECT_NAME = "Goblin";

    private static readonly string RUN_ANIMATION = "Run";
    private static readonly string FIGHT_ANIMATION = "Fight";
    private static readonly string IDLE_ANIMATION = "Idle";

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

        //创建条件
        CheckInSightCondition checkOrcInSight = new CheckInSightCondition(sightForOrc, ORC_OBJECT_NAME);
        CheckInSightCondition checkGoblinInFightDistance = new CheckInSightCondition(fightDistance, GOBLIN_OBJECT_NAME);
        CheckInSightCondition checkGoblinInSight = new CheckInSightCondition(sightForGoblin, GOBLIN_OBJECT_NAME);

        //跑步动作，跑步动作由位移动作和跑步动画动作两个部分组成，所以这里使用并行节点
        BTParallel run = new BTParallel(BTParallel.ParallelOpt.Or);
        run.AddChild(new MoveAction(DESTINATION_DATA_KEY, speed));
        run.AddChild(new PlayAnimationAction(RUN_ANIMATION));


        FindEscapeAction findEscapeDestination = new FindEscapeAction(ORC_OBJECT_NAME, DESTINATION_DATA_KEY, sightForOrc);
        FindTargetAction findTargetDestination = new FindTargetAction(GOBLIN_OBJECT_NAME, DESTINATION_DATA_KEY, fightDistance * 0.9f);

        //逃跑节点，这里使用并行节点，逃跑过程中需要不断查找逃跑目标点
        //所以这里定义
        BTParallel escape = new BTParallel(BTParallel.ParallelOpt.Or);
        escape.AddChild(findEscapeDestination);
        escape.AddChild(run);
        selector.AddChild(new BTConditionDecorator(escape, checkOrcInSight));

        // 3.2 Fight node
        BTSequence fight = new BTSequence();
        BTParallel go = new BTParallel(BTParallel.ParallelOpt.Or);
        go.AddChild(findTargetDestination);
        go.AddChild(run);     
        fight.AddChild(go);
        fight.AddChild(new BTConditionDecorator(new PlayAnimationAction(FIGHT_ANIMATION), checkGoblinInFightDistance));
        selector.AddChild(new BTConditionDecorator(fight, checkGoblinInSight));


        // 3.3 Idle node
        selector.AddChild(new PlayAnimationAction(IDLE_ANIMATION));
    }
}
