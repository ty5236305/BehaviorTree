
namespace BT {

    public enum BTActionStatus
    {
        Ready = 1,
        Running = 2,
    }

    public class BTAction : BTNode {

		private BTActionStatus _status = BTActionStatus.Ready;
		
		protected virtual void Enter () { }

		protected virtual void Exit () { }

		protected virtual BTResult Execute () 
        {
			return BTResult.Running;
		}
		
		public override void Clear () 
        {
			if (_status != BTActionStatus.Ready) 
            {
				Exit();
				_status = BTActionStatus.Ready;
			}
		}
		
		public override BTResult Tick () 
        {
			BTResult result = BTResult.Success;
			if (_status == BTActionStatus.Ready) 
            {
				Enter();
				_status = BTActionStatus.Running;
			}
			if (_status == BTActionStatus.Running) 
            {		
				result = Execute();
				if (result != BTResult.Running) {
					Exit();
					_status = BTActionStatus.Ready;
				}
			}
			return result;
		}
		

	}
}